using System.Text.Json.Serialization; // to use [JsonInclude]

namespace Packt.Shared;

public class Book 
{ 
    //constructor to set non-nullable prop 
    public Book(string title)
    {
        Title = title;
    }

    //properties 
    public string Title { get; set; }
    public string? Author { get; set; }

    //fields
    [JsonInclude] //Include this field in serialization
    public DateTime PublishDate;
    [JsonInclude]
    public DateTimeOffset Created;

    public ushort Pages; 

}