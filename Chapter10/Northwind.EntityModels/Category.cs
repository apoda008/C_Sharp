using System.ComponentModel.DataAnnotations.Schema; //to use [Column]
namespace Northwind.EntityModels;

public class Category 
{ 
    //these props map to columns in the database
    public int CategoryID { get; set; } //primary key
    public string CategoryName { get; set; } = null!;

    [Column(TypeName = "ntext")] 
    public string? Description { get; set; } 

    //defines a navigation prop for related rows
    public virtual ICollection<Product> Products { get; set; } 
    //to enable devs to add procuts to a category, we must initialize the
    //navigation prop to an empty collection. this also avoids an
    //exceptions if we get a member like count
    = new HashSet<Product>();
}