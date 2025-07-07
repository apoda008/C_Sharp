using Microsoft.AspNetCore.Mvc; //to use [route] [apicontroller] controller base and so on 
using Northwind.EntityModels; //to use Customer
using Northwind.WebApi.Repositories; //to use ICustomerRepository

namespace Northwind.WebApi.Controllers;

//Base address: apo/customers
[Route("api/[controller]")]
[ApiController]
public class CustomersController : Controller 
{
    private readonly ICustomerRepository _repo;

    //constructor injects repository registered in Program.cs
    public CustomersController(ICustomerRepository repo) 
    { 
        _repo = repo;
    }
    /*
     * The Controller class registers a route using [Route] attribute that starts with api/ and includes the name of the controller
     * that is, api/customers. the [controller] part is automatically replaced with the class name with the Controller suffix
     * removed. Therefore the base address of the route to the CustomersController is api/cusomters. THe constructor uses dependency 
     * injection to get the registered repository for working with customers.  
     */

    //GET: api/cusomters
    //get api/customers/?coutnry=[country]
    //this will always return a list of customers (but it might be empty)
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
    public async Task<IEnumerable<Customer>> GetCustomers(string? country) 
    {
        if (string.IsNullOrWhiteSpace(country))
        {
            return await _repo.RetrieveAllAsync();
        }
        else 
        { 
            return(await _repo.RetrieveAllAsync())
                .Where(customer => customer.Country == country);
        }
    }

    //GetCustomers method can have a string para passed with a country name. if it is missing, all customers are returned. if it is present
    //it is used to filter customers by country

    //GET: api/customers/[id]
    [HttpGet("{id}", Name = nameof(GetCustomer))] //named route
    [ProducesResponseType(200, Type = typeof(Customer))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCustomer(string id) 
    {
        Customer? c = await _repo.RetrieveAsync(id);
        if (c == null) { 
            return NotFound(); //404 response
        }
        return Ok(c); //200 ok with customer in body 
    }
    //The GETCustomer Method has a route explicitly named getCustomer so that it can be used to generate a URL after inserting a new customer

    //POST: api/customers
    //BODY: Customer (JSON, XML)
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Customer))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] Customer c) 
    {
        if (c == null) 
        {
            return BadRequest(); //400 bad request 
        }
        
        Customer? addedCustomer = await _repo.CreateAsync(c);

        if (addedCustomer == null)
        {
            return BadRequest("repository failed to create customer");
        }
        else 
        { 
            return CreatedAtRoute( //201 created
                routeName: nameof(GetCustomer),
                routeValues: new {id = addedCustomer.CustomerId.ToLower() },
                value: addedCustomer);
        }
    }

    //PUT: api/customers/[id[
    //BODY: Customer (JSON, XML)
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(
        string id, [FromBody] Customer c)
    {
        id = id.ToUpper();
        c.CustomerId = c.CustomerId.ToUpper();
        if (c == null || c.CustomerId != id) 
        {
            return BadRequest(); //400 bad request
        }
        Customer? existing = await _repo.RetrieveAsync(id);
        if (existing == null) 
        { 
            return NotFound(); //404 resource not found
        }
        await _repo.UpdateAsync(c);
        return new NoContentResult(); //204 no content
    }

    //DELETE: api/customers/[id]
    [HttpDelete]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(string id) 
    {
        //Take control of problem details 
        if (id == "bad") 
        {
            ProblemDetails problemDetails = new()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://localhost:5151/customers/failed-to-delete",
                Title = $"Customer ID {id} found but failed to delete",
                Detail = "More details like Company Name, Country and so on",
                Instance = HttpContext.Request.Path
            };
            return BadRequest(problemDetails); //400 bad request
        }

        Customer? existing = await _repo.RetrieveAsync(id);
        if (existing == null) 
        {
            return NotFound(); //404 resource not found
        }
        bool? deleted = await _repo.DeleteAsync(id);
        if (deleted.HasValue && deleted.Value) //short curcuit AND
        {
            return new NoContentResult(); //204 no content
        }
        else 
        {
            return BadRequest(// 400 bad request
                $"Customer {id} was found but failed to delet");
        }
    }
}
    