using Northwind.EntityModels; //to use northwind

using NorthwindDb db = new();
WriteLine($"Provider: {db.Database.ProviderName}");
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

