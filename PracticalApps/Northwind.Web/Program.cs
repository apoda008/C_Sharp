using Northwind.EntityModels; // to use AddNorthwindContext method

#region Configure the web server host and services
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddNorthwindContext();


var app = builder.Build();

#endregion


#region Configure the HTTP pipeline and routes]


//ENABLING STRONGER SECURITY
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

//Implementing an anonymous inline delegate as middleware
//to intercept HTTP requests and responses
app.Use(async (HttpContext context, Func<Task> next) => 
{
    RouteEndpoint? rep = context.GetEndpoint() as RouteEndpoint;

    if (rep is not null) 
    {
        WriteLine($"Endpoint name: {rep.DisplayName}");
        WriteLine($"Endpoint route pattern: {rep.RoutePattern.RawText}");
    }

    if (context.Request.Path == "/bonjour") 
    {
        //In the case of a match on URL path, this becomes a terminating
        //delegate that returns so does not call the next delegate
        await context.Response.WriteAsync("Bonjour Model!");
        return;
    }

    //we could modify the request before calling the next delegate
    await next();

    //we could modify the response after calling the next delegate
});


app.UseHttpsRedirection();

app.UseDefaultFiles(); //index.html, default.html and so on
app.UseStaticFiles();
//WARNING: The call to UseDefaultFiles must come before the call to UseStaticFiles or it will not work.




app.MapRazorPages();

//app.MapGet("/", () => "Hello World!");
app.MapGet("/hello", () => $"Environment is {app.Environment.EnvironmentName}");

#endregion

//Start the web server, host the website, and wait for requests
app.Run(); //This is a thread blocking call 
WriteLine("This executes after the web server has stopped");














//=======================NOTES==============================================

/*
 Good Practice: An optional but recommended security enhancement is HTTP 
Strict Transport Security (HSTS) which you should always enable. If a 
website specifies it and a browser supports it, then it forces all 
communication over HTTPS and prevents the visitor form using untrusted or 
invalid Certificates
 */

/*
 ViewData this dictionary lasts for the lifetime of a signle http request. A component of the website 
like a controller or a specific page, can store some data in it that can then be read by another 
component of the website, like a view or a shared layout. that executes later in that same request 
process. 
 
TempData this dictionary exists during the lifetime of an HTTP request and the next http request from the 
same browser. This allows a part of the website like a controller to store some data in it, respond to the 
browser with a redirect and then another part of the website can read the data on the second request. Only 
the browser that made the original request can access this data

GOOD PRACTIE: When using tools that automatically 'fix' problems without telling you, review your project 
file for unexpected elements when you unexpected results happen
 
Endpoint routing gets its name because it represents the route table as a compiled tree of endpoints that 
can be walked efficiently by the routing system.  
 */



//SSL Cert: pg 662

//FOR GIT 
//BASIC HTML AND HTTPS CONTROL
//RAZOR PAGES
//add code to razor pages 
//using shared layout w/ razor pages
//Configuring Files included in an ASP.NET Core Project --LAST COMMIT--
//using entity framework core with ASP.NET core
//Enabling a model to insert entities 
//Defining a form to insert a new supplier
//injecting a dependecy service into a razor page
//Understanding/configuring endpoint routing
//setting up HTTP pipeline
//implementing an anon inline delegate as middleware