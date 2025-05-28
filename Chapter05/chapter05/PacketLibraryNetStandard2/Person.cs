using System.Reflection;
using System.Security.Cryptography.X509Certificates;
//aliasing tuples 
using Fruit = (string Name, int Number);

namespace Packt.Shared;

public partial class Person : object
{
    #region Fields: Data or Stat for this person 

    public string? Name; //? means it can be null
    public DateTimeOffset Born;

    #endregion

    #region read-only fields: values that can be set at runtime.
    
    public readonly string HomePlanet = "Earth";
    public readonly DateTime Instantiated;

    #endregion
    //This has been moved to PersonAutoGen as a prop
    //public WondersOfTheAncientWorld FavoriteAncientWonder;

    public WondersOfTheAncientWorld BucketList;

    public List<Person> Children = new();

    #region Constructors: Called when using new to instantiate a type

    public Person() {
        //Constructors can set default values for fields 
        //including any read-only fields like Instantiate
        Name = "Unknown";
        Instantiated = DateTime.Now;
    }

    #endregion
    //defining multiple constructors
    public Person(string initialName, string homePlanet) { 
        Name = initialName;
        HomePlanet = homePlanet;
        Instantiated = DateTime.Now;
    }

    #region Methods: Actions the type can perform
    public void WriteToConsole() 
    {
        WriteLine($"{Name} was born on a {Born:dddd}.");
    }

    public string GetOrigin() 
    { 
        return $"{Name} was born on {HomePlanet}";
    }

    public string SayHello() {
        return $"{Name} says Hello!";
    }

    public string SayHello(string name) {
        return $"{Name} says 'Hello, {name}!'";
    }

    //optional para 
    public string OptionalParamaters(int count, string command = "Run!", double number = 0.0, bool active = true) 
    {
        return string.Format(
            format: "command is {0}, num is {1}, active is {2}",
            arg0: command,
            arg1: number,
            arg2: active
           );    
    }

    //how para are passed 
    public void PassingParameters(int w, in int x, ref int y, out int z) 
    {
        //out para cannout have a defautl and they must be initialized inside the method 
        z = 100;
        //increment each para except the read only x 
        w++;
        y++;
        z++;

        WriteLine($"In the method: w={w}, x={x}, y={y}, z={z}");
    }

    //method that returns a tuple: (string, int)
    public (string, int) GetFruit() 
    {
        return ("APPLES", 5);    
    }

    //tuple that returns a tuple with named fields
    public (string Name, int Number) GetNamedFriut() 
    {
        return (Name: "Apples", Number: 5);
    }

    //DECONSTRUCTORS: break down this object into parts
    public void Deconstruct(out string? name, out DateTimeOffset dob) 
    {
        name = Name;
        dob = Born;
    }

    public void Deconstruct(out string? name, out DateTimeOffset dob, out WondersOfTheAncientWorld fav) 
    {
        name = Name; 
        dob = Born;
        fav = FavoriteAncientWonder;
    }

    //method with a local func 
    public static int Factorial(int number) 
    {
        if (number < 0) 
        {
            throw new ArgumentException($"{nameof(number)} cannot be less than Zero");
        }
        return localFactorial(number);

        int localFactorial(int local) //local func
        {
            if (local == 0) return 1;
            return local * localFactorial(local - 1);
        }
    }
    #endregion
    
}
