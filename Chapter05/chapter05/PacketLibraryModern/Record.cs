namespace Packt.Shared;

public class ImmutablePerson 
{ 
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
}
//Idea is that you create new records from existing ones. The new record has the changed state. AKA
//non-destructive mutation.  used with the keyword 'with'
public record ImmutableVehicle 
{ 
    public int Wheels { get; init; }
    public string? Color { get; init; }
    public string? Brand { get; init; }
}

//Simpler syntax to define a record that auto-generates the 
//properties, constructor, and deconstructor

public record ImmutableAnimal(string Name, string Species);