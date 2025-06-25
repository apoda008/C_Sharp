using Microsoft.EntityFrameworkCore;

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
    }
}