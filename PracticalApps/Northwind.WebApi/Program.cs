using Microsoft.AspNetCore.Mvc.Formatters; //to use IOutputFormatter
using Northwind.EntityModels; //to use AddNorthwindContext
using Microsoft.Extensions.Caching.Memory; // to use IMemoryCache and so on
using Northwind.WebApi.Repositories;
using Swashbuckle.AspNetCore.SwaggerUI; // tp use IcustomerREpository 
using Microsoft.AspNetCore.HttpLogging; // to use HttpLoggingFields

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpLogging(options =>
{ 
    options.LoggingFields = HttpLoggingFields.All;
    options.RequestBodyLogLimit = 4096; //default is 32k
    options.ResponseBodyLogLimit = 4096; //default is 32k
});

builder.Services.AddSingleton<IMemoryCache>(new
    MemoryCache(new MemoryCacheOptions()));

builder.Services.AddNorthwindContext();

builder.Services.AddControllers(options =>
{
    WriteLine("Default output formatters:");
    foreach (IOutputFormatter formatter in options.OutputFormatters)
    {
        OutputFormatter? mediaFormatter = formatter as OutputFormatter;
        if (mediaFormatter is null)
        {
            WriteLine($"   {formatter.GetType().Name}");
        }
        else  //outputformatter class has supportedmediatypes 
        {
            WriteLine("    {0}, media types: {1}",
                arg0: mediaFormatter.GetType().Name,
                arg1: string.Join(", ",
                mediaFormatter.SupportedMediaTypes));
        }
    }
})
.AddXmlDataContractSerializerFormatters()
.AddXmlSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

var app = builder.Build();

app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json",
            "Northwind Service API Version 1");

        c.SupportedSubmitMethods(new[] {
        SubmitMethod.Get, SubmitMethod.Post,
        SubmitMethod.Put, SubmitMethod.Delete});
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

/*======NOTES======
 * Transient: These services are created each time theyre requested. Transient services should be lightwieght and stateless
 * 
 * scoped: These services are created once per client request and are disposed of then the response returns to the client 
 * 
 * singleton: these ervices are usually created the first time they are requested and then shared, although you can provide 
 * an instance at the time of registration too
 * 
 * In real web service, you should use a distributed chase like Redis, an open source data structore store that can be used 
 * as a high performance high availability database, cache or message broker. 
 * 
 * Short-Cicuit: When routing matches a request to an endpoint it lets the rest of the middleware pipeline run before invoking 
 * the endpoint logic. You can invoke the endpoint immediately and return the response
 * 
 * Ex: 
 *  app.MapGet("/". () => "Hello World").ShortCircuit();
 * 
 * GOOD PRACTICE: Decorate action methods with the [ProducesResponseType] attribute to indicate all the known types and HTTP 
 * status codes that the client should expect in a response. This information can then be publicly exposed to document how a 
 * client should interact with your web service. Think of it as part of your formal documentation. 
 * 
 * GOOD PRACTICE: Our repository uses a database context that is registered as a scoped dependency. You can only use scoped 
 * dependencies inside other scoped dependencies, so we cannot register the repository as a singleton. 
 * 
 * Although the Default log level might be set to information, more specific configs take priority. For example; any logging 
 * systems in the Microsoft.AspNetCore namespace will use the Warning level. 
 * 
 * GIT: 
 * Creating data repositories with caching for entities
 * Routing WebServices 
 * Route Constraints
 * short-circuit routs in ASP.NET Core 8
 * Improved route tooling in ASP.NET Core 8
 * Understanding action method return types
 * Configuring the customer repository and Web API controller
 * ====LAST GIT COMMIT======
 * Documenting and testing web services
 * specifying problem details
 * controlling XML serialization
 * Testing GET requests using browser
 * Making Get requests using HTTP/REST tools
 * Making other requests using HTTP/REST tools
 * Passing environment variables
 * Understanding swagger
 * Enabling HTTP Logging
 * ====LAST GIT======
 * 
 * 
 * **/