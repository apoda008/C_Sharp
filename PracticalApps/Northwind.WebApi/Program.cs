using Microsoft.AspNetCore.Mvc.Formatters; //to use IOutputFormatter
using Northwind.EntityModels; //to use AddNorthwindContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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
 * 
 * **/