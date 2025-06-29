//using Microsoft.Data.SqlClient; I think this is for SQL Server
using Microsoft.EntityFrameworkCore; //to use DbContext, DbSet<t>

namespace Northwind.EntityModels;
public class NorthwindDb : DbContext 
{
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        #region to Use SQLite
        string database = "Northwind.db";
        string dir = Environment.CurrentDirectory;
        string path = string.Empty;

        //The database file will stay in the project folder
        //We will automatically adjust the relative path to 
        // account for runningi n VS2022 or from terminal 

        if (dir.EndsWith("net8.0"))
        {
            //running in the <project\bin\<debug\release>\net8.0 directory 
            path = Path.Combine("..", "..", "..", database);
        }
        else 
        {
            //Running in the <project> directory
            path = database;
        }

        path = Path.GetFullPath(path); //convert to absolute path 
        WriteLine($"SQLite database path: {path}");

        if (!File.Exists(path)) 
        { 
            throw new FileNotFoundException(
                message: $"{path} not found.", fileName: path );
        }

        //to use SQLite
        optionsBuilder.UseSqlite($"Data Source={path}");

        #endregion


    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (Database.ProviderName is not null && Database.ProviderName.Contains("Sqlite")) 
        { 
        //SQLITE data provider does not directly support the 
        //Decimal type so we can convert to double instead
        modelBuilder.Entity<Product>()
            .Property(product => product.UnitPrice)
            .HasConversion<double>();
        }
    }


}