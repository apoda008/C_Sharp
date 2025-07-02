using Microsoft.AspNetCore.Mvc.RazorPages; // to use Pagemodel 

namespace Northwind.Web.Pages;

public class SuppliersModel : PageModel 
{ 
    public IEnumerable<string>? Suppliers { get; set; }

    public void OnGet() 
    {
        ViewData["Title"] = "Northwind B2B - Suppliers";

        Suppliers = new[]
        {
            "Aplha Co", "Beta Limited", "Gamma Corp"
        };
    }
        
}