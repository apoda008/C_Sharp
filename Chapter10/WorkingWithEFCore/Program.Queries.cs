﻿using System.ComponentModel;
using Microsoft.EntityFrameworkCore; //to use Include Method
using Northwind.EntityModels; //to use northwind Category Product
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Cryptography; // to use Collection entry 

partial class Program 
{
    private static void QueryingCategories() 
    {
        using NorthwindDb db = new();

        SectionTitle("Categories and how many products they have");

        //a query to get all categories and their related products
        IQueryable<Category>? categories; //= db.Categories; //.Include(c => c.Products);

        //-------------EXPLICIT-----------------

        db.ChangeTracker.LazyLoadingEnabled = false;

        Write("Enable eager loading? (y/n): ");
        bool eagarLoading = (ReadKey().Key == ConsoleKey.Y);
        bool explicitLoading = false;
        WriteLine();

        if (eagarLoading)
        {
            categories = db.Categories?.Include(c => c.Products);
        }
        else 
        {
            categories = db.Categories;
            Write("Enable Explicit loading? (Y/N): ");
            explicitLoading = (ReadKey().Key == ConsoleKey.Y);
            WriteLine();
        }

        
        
        //--------------------------------------
        
        
        if(categories is null || !categories.Any())
        {
            Fail("No categories found");
            return;
        }

        //execute query and enumerate results 
        foreach (Category c in categories) 
        {
            if (explicitLoading)
            {
                Write($"Explicitlly load products for {c.CategoryName}? (Y/N): ");
                ConsoleKeyInfo key = ReadKey();
                WriteLine();

                if (key.Key == ConsoleKey.Y)
                {
                    CollectionEntry<Category, Product> products = db.Entry(c).Collection(c2 => c2.Products);
                    if (!products.IsLoaded) products.Load();
                }
                /*
                 Carefully consider which loading patter is best for your code. Lazy loading could literally make you a lazy dev!
                 */
        }
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

    private static void GettingOneProduct() 
    { 
        using NorthwindDb db = new();

        SectionTitle("Getting a single product by ID");

        string? input;
        int id;

        do 
        { 
            Write("Enter a product ID: ");
            input = ReadLine();
        } while (!int.TryParse(input, out id));

        Product? product = db.Products?
            .First(product => product.ProductId == id);

        Info($"First: {product?.ProductName}");

        if(product is null) Fail("No product found using First");

        product = db.Products?
            .Single(product => product.ProductId == id);

        Info($"Single: {product?.ProductName}");

        if(product is null) Fail("No product found using Single");
        //If you do not need to make sure that only one entity matches, use
        //First instead of Single to avoid retrieving two records

    }

    private static void QueryingWithLike() 
    { 
        using NorthwindDb db = new();

        SectionTitle("Pattern matching with LIKE");

        Write("Enter part of a product name: ");

        string? input = ReadLine();

        if(string.IsNullOrWhiteSpace(input))
        {
            Fail("No input provided");
            return;
        }

        IQueryable<Product>? products = db.Products?
            .Where(p => EF.Functions.Like(p.ProductName, $"%{input}%"));

        if (products is null || !products.Any())
        {
            Fail("No products found");
            return;
        }

        foreach(Product p in products)
        {
            WriteLine("{0} has {1} units in stock. Discontinued: {2}", 
                p.ProductName, p.Stock, p.Discontinued);
        }
    }

    private static void GetRandomProduct() 
    { 
        using NorthwindDb db = new();

        SectionTitle("Getting a random product");

        int? rowCount = db.Products?.Count();

        if (rowCount is null) 
        { 
            Fail("Products table is empty");    
            return;
        }

        Product? p = db.Products?.FirstOrDefault(
            p => p.ProductId == (int)(EF.Functions.Random() * rowCount));

        if (p is null) 
        { 
            Fail("Product not found");
            return;
        }

        WriteLine($"Random product: {p.ProductId} - {p.ProductName}");
    }

        
}