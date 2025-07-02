#region Configure the web server hose and services
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

#endregion


#region Configure the HTTP pipeline and routes]


//ENABLING STRONGER SECURITY
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

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
 */



//SSL Cert: pg 662

//FOR GIT 
//BASIC HTML AND HTTPS CONTROL
//RAZOR PAGES
//add code to razor pages 
//using shared layout w/ razor pages
//Configuring Files included in an ASP.NET Core Project