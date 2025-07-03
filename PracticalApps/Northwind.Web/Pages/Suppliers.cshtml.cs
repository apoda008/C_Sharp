using Microsoft.AspNetCore.Mvc; //to use [BindProperty], IActionResult
using Microsoft.AspNetCore.Mvc.RazorPages; // to use Pagemodel 
using Northwind.EntityModels;
using Northwind.EntityModels.Sqlite; //to use NorthwindCOntext


namespace Northwind.Web.Pages;

public class SuppliersModel : PageModel 
{ 
    public IEnumerable<Supplier>? Suppliers { get; set; }

    private NorthwindContext _db;

    [BindProperty]
    public Supplier? Supplier { get; set; }

    public IActionResult OnPost() 
    {
        if (Supplier is not null && ModelState.IsValid)
        {
            _db.Suppliers.Add(Supplier);
            _db.SaveChanges();

            return RedirectToPage("/suppliers");

        }
        else 
        {
            return Page(); //Return to original page
        }
    }

    public SuppliersModel(NorthwindContext db) 
    { 
        _db = db;
    }
    public void OnGet() 
    {
        ViewData["Title"] = "Northwind B2B - Suppliers";

        Suppliers = _db.Suppliers
            .OrderBy(c => c.Country)
            .ThenBy(c => c.CompanyName);
    }
        
}