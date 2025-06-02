namespace Packt.Shared;

public record struct DisplacementVector 
{ 
    public int X { get; set; }
    public int Y { get; set; }

    public DisplacementVector(int intialX, int intialY) 
    { 
        X = intialX;
        Y = intialY;
    }

    public static DisplacementVector operator +(DisplacementVector vector1, DisplacementVector vector2)
    { 
        return new(vector1.X + vector2.X, vector1.Y + vector2.Y);
    }

}
/*
 with this change, microsoft recommends explicitly specifying class if you want to define a record class, 
 even though the class keyword is optional. as show in the following: 
 public record class ImmutableAnimal(string name)
 *
 Theres a difference between a destructor(finalizer) and destructor method. The method breaks an obj into is constituent parts.
 */

