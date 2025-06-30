using Northwind.EntityModels; //to use northiwinddb, category, product
using Microsoft.EntityFrameworkCore;
using System.Security.Permissions; // to use dbset<t>

partial class Program 
{
    private static void FilterAndSort() 
    {
        SectionTitle("Filter and sort");

        using NorthwindDb db = new();

        DbSet<Product> allProducts = db.Products;

        IQueryable<Product> filteredProducts = allProducts.Where(product => product.UnitPrice < 10M);

        IOrderedQueryable<Product> sortedAndFilteredProducts = filteredProducts.OrderByDescending(product => product.UnitPrice);

        var projectedProducts = sortedAndFilteredProducts
            .Select(product => new //Anonymous type
            {
                product.ProductId,
                product.ProductName,
                product.UnitPrice
            });

        WriteLine("Products that cost less than $10");
        WriteLine(projectedProducts.ToString());

        foreach (var p in projectedProducts) 
        { 
            WriteLine("{0}: {1} costs {2:$#,##0.00}",
                p.ProductId, p.ProductName, p.UnitPrice);
        }
        WriteLine();
    }

    private static void JoinCatagoriesAndProducts() 
    {
        SectionTitle("Join categories and products");

        using NorthwindDb db = new();

        //Join every product to its category to return 77 matches 
        var queryJoin = db.Categories.Join(
            inner: db.Products,
            outerKeySelector: category => category.CategoryId,
            innerKeySelector: product => product.CategoryId,
            resultSelector: (c,p) => new {c.CategoryName, p.ProductName, p.ProductId})
            .OrderBy(cp => cp.CategoryName);

        foreach (var p in queryJoin) 
        { 
            WriteLine($"{p.ProductId}: {p.ProductName} in {p.CategoryName}");
        }

        /*
         In a join, there are two sequences, outer and inner. In the preceding example, categories is the outer sequences and products
        is the inner sequence
         */
    }

    private static void GroupJoinCategoriesAndProducts() 
    {
        SectionTitle("Group Join categories and products");

        using NorthwindDb db = new();

        //Group all products by their category to return 8 mathces
        var queryGroup = db.Categories.AsEnumerable().GroupJoin(
            inner: db.Products,
            outerKeySelector: category => category.CategoryId,
            innerKeySelector: product => product.CategoryId,
            resultSelector: (c, matchingProducts) => new
            {
                c.CategoryName,
                Products = matchingProducts.OrderBy(p => p.ProductName)
            });

        foreach (var c in queryGroup) 
        { 
            WriteLine($"{c.CategoryName} has {c.Products.Count()} products");

            foreach (var product in c.Products) 
            {
                WriteLine($"    {product.ProductName}");
            }
        }
    }

    private static void ProductsLookUp() 
    {
        SectionTitle("Products lookup");

        using NorthwindDb db = new();

        //join all products to their category to return 77 matches 
        var productQuery = db.Categories.Join(
            inner: db.Products,
            outerKeySelector: category => category.CategoryId,
            innerKeySelector: product => product.CategoryId,
            resultSelector: (c,p) => new { c.CategoryName, Product = p });

        ILookup<string, Product> productLookup = productQuery.ToLookup(
            keySelector: cp => cp.CategoryName,
            elementSelector: cp => cp.Product);

        foreach (IGrouping<string, Product> group in productLookup) 
        {
            //key is beverages, condiments, and so on 
            WriteLine($"{group.Key} has {group.Count()} products");

            foreach (Product product in group) 
            {
                WriteLine($"    {product.ProductName}");
            }
        }

        //We can look up the products by a category name 
        Write("Enter a category name:  ");
        string categoryName = ReadLine()!;
        WriteLine();
        WriteLine($"Products in {categoryName}:");

        IEnumerable<Product> productsInCategory = productLookup[categoryName];
        foreach (Product product in productsInCategory) 
        {
            WriteLine($"    {product.ProductName}");
        }

    }
    /*
     Selector para are lambda expressions that select sub-elements for different purposes. 
    For example, ToLookUp has a keySelector to select the part of each item that will be 
    the value
     
     */

}