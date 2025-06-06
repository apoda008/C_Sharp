﻿using System.Diagnostics.CodeAnalysis; // [SetsRequiredMembers]

namespace Packt.Shared;

public class Book
{
    //needs NET7 or later as well as C#11 or later
    public required string? Isbn;
    public required string? Title;

    //works with any version of .net
    public string? Author;
    public int PageCount;

    //Constructor for use with object intializer syntax:
    public Book() { }

    //constructor with para to set required fields
    [SetsRequiredMembers]
    public Book(string? isbn, string? title) { 
        Isbn = isbn; ;
        Title = title;
    }
}
