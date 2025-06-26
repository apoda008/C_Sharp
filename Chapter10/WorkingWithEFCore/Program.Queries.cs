using System.ComponentModel;
using Microsoft.EntityFrameworkCore; //to use Include Method
using Northwind.EntityModels; //to use northwind Category Product

partial class Program 
{
    private static void QueryingCategories() 
    {
        using NorthwindDb db = new();

        SectionTitle("Categories and how many products they have");

        //a query to get all categories and their related products
        IQueryable<Category>? categories = db.Categories?.Include(c => c.Products);

        if(categories is null || !categories.Any())
        {
            Fail("No categories found");
            return;
        }

        //execute query and enumerate results 
        foreach (Category c in categories) 
        { 
            WriteLine($"{c.CategoryName} has {c.Products.Count} products");
        }
    }
    /*
     Note the order of the clauses in the if statement is important. we must check the 
    categories is null first. if this is true then the code will never execute the second 
    clause and therefore wont throw an exception
     */

    private static void FilteredIncludes() 
    {
        using NorthwindDb db = new();

        SectionTitle("Products with a min # of units in stock");

        string? input;
        int stock;

        do
        {
            Write("Enter a min for units in stock");
            input = ReadLine();
        }
        while (!int.TryParse(input, out stock));

        IQueryable<Category>? categories = db.Categories?
            .Include(c => c.Products.Where(p => p.Stock >= stock));

        if ( categories is null || !categories.Any())
        {
            Fail("No categories found");
            return;
        }

        Info($"ToQueryString: {categories.ToQueryString()}");
        foreach (Category c in categories) 
        {
            WriteLine(
                "{0} has {1} products with a min {2} units in stock.",
                c.CategoryName, c.Products.Count, stock);

            foreach (Product p in c.Products) 
            {
                WriteLine($"   {p.ProductName} has {p.Stock} units in stock");
            
            }
        }
    }

    private static void QueryingProducts() 
    { 
        using NorthwindDb db = new();

        SectionTitle("Products that cost more than a price, highest at top");

        string? input;
        decimal price;

        do
        {
            Write("Enter a product price: ");
            input = ReadLine();

        } while (!decimal.TryParse(input, out price));

        IQueryable<Product>? products = db.Products?
            .Where(product => product.Cost > price)
            .OrderByDescending(product => product.Cost);

        if ( products is null || !products.Any())  
        {
            Fail("No products found");
            return;
        }

        Info($"ToQueryString: {products.ToQueryString()}");
        foreach (Product p in products)
        {
            WriteLine(
                "{0}: {1} costs {2:$#,##0.00} and has {3} in stock.",
                p.ProductId, p.ProductName, p.Cost, p.Stock);
        }
    }
}