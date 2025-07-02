using Microsoft.EntityFrameworkCore; //to use Sqlite 
using Microsoft.Extensions.DependencyInjection;
using Northwind.EntityModels.Sqlite; // to use IserviceCollection

namespace Northwind.EntityModels;

public static class NorthwindContextExtensions 
{
     // <Summary>
     // Adds NorthwindContext to the specified IserviceCollection. Uses the sqlite database provider
     // </Summary>
     // <param name="services">The service collection. </param>
     // <param name="relativePath">Default is ".." </param>
     // <param name="databaseName">Default is "Northwind.db"</param>
     // <returns>An IServiceCollection that can be used to add more services</returns>

    public static IServiceCollection AddNorthwindContext(
        this IServiceCollection services, //The type to extend
        string relativePath = "..",
        string databaseName = "Northwind.db")
    {
        string path = Path.Combine(relativePath, databaseName);
        path = Path.GetFullPath(path);
        NorthwindContextLogger.WriteLine($"Database path: {path}");

        if (!File.Exists(path))
        {
            throw new FileNotFoundException(
                message: $"{path} not found.", fileName: path);
        }

        services.AddDbContext<NorthwindContext>(options =>
        {
            //data source is the modern equivalent of filename 
            options.UseSqlite($"Data Source={path}");

            options.LogTo(NorthwindContextLogger.WriteLine, new[] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting });
        },
        //register with a transient lifetime to avoid concurrency 
        //issues in Blazor server-side projects 
        contextLifetime: ServiceLifetime.Transient,
        optionsLifetime: ServiceLifetime.Transient);

        return services;
        }
    }