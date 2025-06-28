﻿using Microsoft.EntityFrameworkCore; //To use ExecuteUpdate, Execute Delete
using Microsoft.EntityFrameworkCore.ChangeTracking; // to use EntityEntry<T>
using Northwind.EntityModels;

partial class Program 
{
    private static void ListProducts(
        int[]? productIdsToHighligh = null)
    { 
        using NorthwindDb db = new();

        if (db.Products is null || !db.Products.Any()) 
        {
            Fail("There are no products");
            return;

        }

        WriteLine("|  {0,-3}  |  |  {1,-35}  |  |  {2,8}  |  |  {3,5}  |  |  {4}  |",
            "Id", "Product Name", "Cost", "Stock", "Disc.");

        foreach (Product p in db.Products) 
        { 
            ConsoleColor previousColor = ForegroundColor;

            if (productIdsToHighligh is not null &&
                productIdsToHighligh.Contains(p.ProductId)) 
            { 
                ForegroundColor = ConsoleColor.Green;
            }

            WriteLine("|  {0:000}  |  |  {1,-35}  |  |  {2,8:$#,##0.00}  |  |  {3,5}  |  |  {4}  |",
                p.ProductId, p.ProductName, p.Cost, p.Stock, p.Discontinued);

            ForegroundColor = previousColor;
        }
        /*
         Remeber that {1, -35} means left align 1 within a 35-character wide column, and {3,5} means 
        right align argument 3 within a 5 character wide column
         */
    }

    private static (int affected, int productId) AddProduct(
        int categoryId, string productName, decimal? price, short? stock)
    { 
        using NorthwindDb db = new();

        if (db.Products is null) return (0, 0);

        Product p = new()
        { 
            CategoryId = categoryId,
            ProductName = productName,
            Cost = price,
            Stock = stock
        };

        //Set product as added in change tracking 
        EntityEntry<Product> entity = db.Products.Add(p);
        WriteLine($"State: {entity.State}, ProductId: {p.ProductId}");

        //save tracked change to database 
        int affected = db.SaveChanges();
        WriteLine($"State: {entity.State}, ProductId: {p.ProductId}");

        return(affected, p.ProductId);
    }

    private static (int affected, int productId) IncreaseProductPrice(
        string productNameStartsWith, decimal amount)
    {
        using NorthwindDb db = new();

        if (db.Products is null) return (0, 0);

        //get the 1st product whos name starts with the para value
        Product updateProduct = db.Products.First(p => p.ProductName.StartsWith(productNameStartsWith));

        updateProduct.Cost += amount;

        int affected = db.SaveChanges();
        return(affected, updateProduct.ProductId);
    }

    private static int DeleteProducts(string productNameStartsWith) 
    {
        using NorthwindDb db = new();

        IQueryable<Product>? products = db.Products?.Where(p => p.ProductName.StartsWith(productNameStartsWith));

        if (products is null || !products.Any())
        {
            WriteLine("No products found to delete");
            return 0;
        }

        else 
        {
            if (db.Products is null) return 0;
            db.Products.RemoveRange(products);
        }

        int affected = db.SaveChanges();
        return affected;
    }

    private static (int affected, int[]? productIds) IncreaseProductPricesBetter(
        string productNameStartsWith, decimal amount)
    {
        using NorthwindDb db = new();

        if (db.Products is null) return (0, null);

        //get products whoe name starts with the parameter value
        IQueryable<Product>? products = db.Products.Where(
            p => p.ProductName.StartsWith(productNameStartsWith));

        int affected = products.ExecuteUpdate(s => s.SetProperty(
            p => p.Cost, //Property selector Lambda expression
            p => p.Cost + amount)); //Value to update to lambda expression 

        int[] productIds = products.Select(p => p.ProductId).ToArray();

        return (affected, productIds);
    }

    private static int DeleteProductsBetter(string productNameStartsWith) 
    {
        using NorthwindDb db = new();

        int affected = 0; 

        IQueryable<Product>? products = db.Products?.Where(
            p => p.ProductName.StartsWith(productNameStartsWith));

        if (products is null || !products.Any())
        {
            WriteLine("No products found to delete");
            return 0;
        }

        else 
        {
            affected = products.ExecuteDelete();
        }

        return affected;
    }
}