using Microsoft.EntityFrameworkCore; //To use ExecuteUpdate, Execute Delete
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
}