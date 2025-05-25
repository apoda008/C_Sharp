namespace Packt.Shared;

public class Book
{
    //needs NET7 or later as well as C#11 or later
    public required string? Isbn;
    public required string? Title;

    //works with any version of .net
    public string? Author;
    public int PageCount;
}
