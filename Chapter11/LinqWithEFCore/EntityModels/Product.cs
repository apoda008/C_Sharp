using System.ComponentModel.DataAnnotations; //[require] and [stringlength]

using System.ComponentModel.DataAnnotations.Schema; //to use [column]

namespace Northwind.EntityModels;

public class Product 
{ 
    public int ProductId { get; set; }

    [Required]
    [StringLength(40)]
    public string ProductName { get; set; } = null!;

    public int? SupplierId { get; set; }
    public int? CategoryId { get; set; }

    [StringLength(20)]
    public string? QuantityPerUnit { get; set; }

    //Required for SQL server provider 
    [Column(TypeName = "money")]
    public decimal? UnitPrice { get; set; }

    public short? UnitsInStock { get; set; }
    public short? UnitsOnOrder { get; set; }
    public short? ReorderLevel { get; set; }
    public bool Discontinued { get; set; }

    /*
     DEliberately not defined any relationships between Category and Product so that 
    we can see how to manually associate them with each other using LINQ
     */

}