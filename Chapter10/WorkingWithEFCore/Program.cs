﻿using Northwind.EntityModels; //to use northwind

//using NorthwindDb db = new();
//WriteLine($"Provider: {db.Database.ProviderName}");
//disposes the database context

/*
 * Entity model: represents the structure of the table and an instance of the class represents a row in that table.
 * 
 * Annotation attributes: used to define the properties of the entity model.
 * 
 * Fluent API: used to define the properties of the entity model in a more fluent way.
 *  Ex: modelBuilder.Entity<Customer>().Property(product => product.ProductName).HasMaxLength(5).IsRequired();
 *  
 *  SETTING UP DOTNET-EF TOOL
 *  BUILDING EF CORE MODELS FOR NORTHWIND TABLES
 *  EF CORE FLUENT API
 *  ADDING TABLES//COLUMNS
 *  
 *  GOOD PRACTICE: dont be afraid to overrule a tool when you know better. 
 *  
 *  scaffolding is the process of useing a tool to create classes that represent the model of an existing database 
 *  using reverse engineering
 *  
 *  
 */

ConfigureConsole();
//QueryingCategories();

//Filtering included entities 
//FilteredIncludes();

//Filtering and sorting products 
//QueryingProducts();

//Getting the generated SQL 

//Logging EF CORE 
/*
 * Good Practice: by default, EF core logging will be exclude any data that is sensitive. You can include this data 
 * by calling the EnableSensitiveDataLogging method, especially during development. You should disable it before 
 * deploying to production. You can all call EnableDetailedErrors 
 */

//(FOR LOGGING VIEW NORTHWINDDB.CS)
//FILTERING LOGS BY PROVIDER SPECIFIC VALUES
//LOGGING WITH QUERY TAGS
/*
 IQueryable<Product>? products = db.Products?
                    .TagWith("Products filtered by price")
                    .Where(product => product.Cost > price)
                    .OrderByDescending(product => product.Cost);
 */
//GETTING A SINGLE ENTITY
//GettingOneProduct();

//PATTERN MATCHING WITH LIKE
//QueryingWithLike();

//GENERATING RANDOM NUMBER IN QUERIES 
//GetRandomProduct();

//DEFINING GLOBAL FILTERS 
//Loading and TRACKING PATTERNS WITH EF CORE
/*
 *Eagar loading: Load data early 
 *Lazy loading: load data automatically just before it is needed 
 *Explicit loading: Load data manually
 */

//ENABLING LAZY LOADING
//EXPLICIT LOADING ENTITIES USING LOAD METHOD
//==========LAST COMMIT=======================

//CONTROL THE TRACKING OF ENTITIES
//---> Identity Resolution: EF Core resolves each entity instance by reading its unique primary key value. This ensures
//no ambiguities about the identities or relationships between them 

/*
 * EF Core can only track entities with keys because it uses the key to uniquely identify the entity in the database. 
 * Keyless entities, like those returned by views, are never tracked in any scenario
 */

//LAZYLOADINGWITHNOTRACKING
//LazyLoadingWithNoTracking();
/*
 C: for create
 R: for Retrieve (or Read)
 U: for Update
 D: for Delete
 
 */

//INSERTING ENTITIES  ==LAST COMMIT===
//var resultAdd = AddProduct(categoryId: 6, productName: "Bob's Burgers", price: 500M, stock: 72);

//if (resultAdd.affected == 1)
//{
//    WriteLine($"Add prodcut successful with ID: {resultAdd.productId}");

//}

//ListProducts(productIdsToHighligh: new[] { resultAdd.productId });

/*
 When the new product is first created in memory and tracked by the EF core change tracker, it has a state of ADded and 
its ID is 0. After the call to SaveChanges, it has a state of Unchanged and its ID is 78, the value assigned by the 
database
 */

//UPDATING ENTITIES
//var resultUpdate = IncreaseProductPrice(
//    productNameStartsWith: "Bob", amount: 20M);

//if (resultUpdate.affected == 1) 
//{ 
//    WriteLine($"Increased price success for ID: {resultUpdate.productId}");
//}

//ListProducts(productIdsToHighligh: new[] { resultUpdate.productId });

//DELETING ENTITIES
//RemoveRange is more effecient when you want to delete multiple entities
//WriteLine("About to delete all products whose name starts with Bob");
//Write("Press enter to continue or any other key to exit");

//if (ReadKey(intercept: true).Key == ConsoleKey.Enter)
//{
//    int deleted = DeleteProducts(productNameStartsWith: "Bob");
//    WriteLine($"{deleted} products were deleted");
//}
//else 
//{
//    WriteLine("Delete was canceled");
//}


//MORE EFFECIENT DELETES AND UPDATES
/*
 * ExecuteUpdate and ExecuteDelete can only act on a single table, so although you can write quite unique LINQ queries, 
 * they can only update or delete from a single table 
 * 
 * you can chain multiple calls to SetProperty in the same query to update multiple properties in one command. 
 */

//var resultUpdateBetter = IncreaseProductPricesBetter(
//    productNameStartsWith: "Bob", amount: 20M);

//if (resultUpdateBetter.affected > 0) 
//{
//    WriteLine("Increase product prises successful");
//}

//ListProducts(productIdsToHighligh: resultUpdateBetter.productIds);

WriteLine("About to delete all products whose name starts with Bob");
Write("Press enter to continue or any other key to exit");

if (ReadKey(intercept: true).Key == ConsoleKey.Enter)
{
    int deleted = DeleteProductsBetter(productNameStartsWith: "Bob");
    WriteLine($"{deleted} products were deleted");
}
else 
{
    WriteLine("Delete was canceled");
}
/*
 WARNING: if you mix traditional tracked changes with ExecuteUpdate and ExecuteDelete methods, then note that they are 
not kept synchronized. The change tracker will not know what you have updated and deleted using those methods
 */