using Northwind.EntityModels; //to use northiwinddb, category, product
using Microsoft.EntityFrameworkCore;
using System.Security.Permissions;
using System.Runtime.Intrinsics.Arm; // to use dbset<t>

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

    private static void AggregateProducts()
    {
        SectionTitle("Aggregate Products");

        using NorthwindDb db = new();

        //Try to get an efficient count from EF Core DbSet<T>
        if (db.Products.TryGetNonEnumeratedCount(out int countDbSet))
        {
            WriteLine($"{"Product count from Dbset:",-25} {countDbSet,10}");
        }
        else
        {
            WriteLine("Products DbSet does not have a count property");
        }

        //Try to get an efficient count from a list<t>
        List<Product> products = db.Products.ToList();

        if (products.TryGetNonEnumeratedCount(out int countList))
        {
            WriteLine($"{"Product count from list:",-25} {countList,10}");

        }
        else
        {
            WriteLine("Products list does not have a Count Property");

        }

        WriteLine($"{"Product count:",-25} {db.Products.Count(),10}");

        WriteLine($"{"Discontinued product count:",27} {db.Products.Count(product => product.Discontinued),8}");

        WriteLine($"{"Highest product price:", -25} {db.Products.Max(p => p.UnitPrice), 10:$#,##0.00}");

        WriteLine($"{"Sum of units in stock:",-25} {db.Products.Sum(p => p.UnitsInStock),10:N0}");

        WriteLine($"{"Sum of units on order:",-25} {db.Products.Sum(p => p.UnitsOnOrder),10:N0}");

        WriteLine($"{"Average unit price:",-25} {db.Products.Average(p => p.UnitPrice),10:$#,##0.00}");

        WriteLine($"{"Value of units in stock",-25} {db.Products.Sum(p => p.UnitPrice * p.UnitsInStock),10:$#,##0.00}");

    }

    /*
     GOOD PRACTICE: Getting a count can seem like a simple operation but counting can be costly. A dbSet<> like Products 
    does not have a count property so TryGetNonEnmueratedCount returns False. A List<T> like products does have a count 
    property because it implements ICollection so TryGetNonEnmueratedCount returns true. (In this case we had to 
    instantiate a list, which is itself a costly operations but if you already have a list and need to know the num of 
    items, then this would be effecient) You can always call count() on DbSet<> but i can be slow because it might have to 
    enumerate the sequence depending on the data provider implementation. For any araay, use the Length property to get a 
    count. You can pass a lambda expression to Count() to filter which items in the sequence should be counted which you 
    cannot do with either the Count or Length Properties
     */

    private static void OutputTableOfProducts(Product[] products, int currentPage, int totalPages) 
    {
        string line = new('-', count: 73);
        string lineHalf = new('-', count: 30);

        WriteLine(line);
        WriteLine("{0,4} {1,-40} {2,12:C} {3,-15}",
                "ID", "Product Name", "Unit Price", "Discontinued");
        WriteLine(line);

        foreach (Product p in products) 
        {
            WriteLine("{0,4} {1,-40} {2,12:C} {3,-15}",
                p.ProductId, p.ProductName, p.UnitPrice, p.Discontinued);

        }

        WriteLine("{0} Page {1} of {2} {3}",
            lineHalf, currentPage + 1, totalPages + 1, lineHalf);

        /*
         As usual in computing, our code will start counting from 0, so we need to add 1 to both the currentpage and totalpages 
        \count before showing there values
         */
    }

    private static void OutputPageOfProducts(IQueryable<Product> products, int pageSize, int currentPage, int totalPages)
    {
        //we must order data before skipping and taking to ensure
        //the data is not randomly sorted in each page 
        var pagingQuery = products.OrderBy(p => p.ProductId)
            .Skip(currentPage * pageSize).Take(pageSize);

        Clear(); // clear the console screen

        SectionTitle(pagingQuery.ToQueryString());

        OutputTableOfProducts(pagingQuery.ToArray(), currentPage, totalPages);  
    
    }

    private static void PagingProducts() 
    {
        SectionTitle("Paging products");

        using NorthwindDb db = new();

        int pagesize = 10;
        int currentpage = 0; 
        int productcount = db.Products.Count();
        int totalpages = productcount / pagesize;

        while (true) //  use break to escape this infinite loop
        { 
            OutputPageOfProducts(db.Products, pagesize, currentpage, totalpages);

            Write("Press <- to page back, press -> to page forward, any key to exit)");
            ConsoleKey key = ReadKey().Key;

            if (key == ConsoleKey.LeftArrow)
                currentpage = currentpage == 0 ? totalpages : currentpage - 1;
            else if (key == ConsoleKey.RightArrow)
                currentpage = currentpage == totalpages ? 0 : currentpage + 1;
            else
                break;

            WriteLine();
        }
    }

}