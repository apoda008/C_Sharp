using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics; // to use RelationalEventID

namespace Northwind.EntityModels;

//this manages interactions with the northwind database
public class  NorthwindDb : DbContext
{
    //These two props map to tables in the database
    public DbSet<Category>? Categories { get; set; } = null!;
    public DbSet<Product>? Products { get; set; } = null!;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string databaseFile = "Northwind.db";
        string path = Path.Combine(Environment.CurrentDirectory, databaseFile);
        string connectionString = $"Data Source ={path};";
        WriteLine($"Connection: {connectionString}");
        optionsBuilder.UseSqlite(connectionString);
        optionsBuilder.LogTo(WriteLine, new[] { RelationalEventId.CommandExecuting }) 
#if DEBUG
            .EnableSensitiveDataLogging() //Include SQL parameters
            .EnableDetailedErrors()
#endif
            ;

        optionsBuilder.UseLazyLoadingProxies();
        /*Logto requires an Action<string> delegate. EF Core will call this delegate, 
         * passing a string value for each log message. Passing the COnsole class 
         * Writeline method, therefore, tells the logger to write each method to the 
         * console
         */
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Example of using Fluent API instead of attributes
        //to limit the Length of a catagory name to 15
        modelBuilder.Entity<Category>()
            .Property(category => category.CategoryName)
            .IsRequired() //not null!
            .HasMaxLength(15);

        //Some SQLite-specific configs 
        if (Database.ProviderName?.Contains("Sqlite") ?? false) 
        {
            //to 'fix' the lack of decimal support in SQLITE
            modelBuilder.Entity<Product>()
                .Property(product => product.Cost)
                .HasConversion<double>();
        }

        //a global filter to remove discontinured products 
        modelBuilder.Entity<Product>()
            .HasQueryFilter(p => !p.Discontinued);
    }
}