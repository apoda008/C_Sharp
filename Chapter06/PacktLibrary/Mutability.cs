namespace Packt.Shared;

//A mutable record class
public record class C1 
{ 
    public string? Name { get; set; }
}

//an immutable record class 
public record class C2(string? Name);

//a mutable record struct
public record struct S2(string? Name);

//another mutable record struct
public record struct S1 
{ 
    public string? Name {  set; get; }
}

//A immutable record struct
public readonly record struct S3(string? Name);