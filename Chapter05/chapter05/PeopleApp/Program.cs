using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;
using Packt.Shared; //to use Person

//alias
//using tx = Texas where Texas is the imported namespace

ConfigureConsole(); //sets current culture to US ENGLISH

//Alternatives :
//ConfigureConsole(useComputer: true);
//configureConsole(culture: "fr-FR")



/*
 Example of aliasing:

using France;
using tx = Texas; //Tx become alias for Texas

Paris p1 = new(); //comes from France, makes France.Paris
Tx.Paris = new(); //Ccomes from Texas, makes Texas.Paris
 
 
 */
// Person bob = new Person() //C#1 or later 
// var bob = new Person() //C#4 or later 

Person bob = new(); //C#9 or later

WriteLine(bob); //implicit call to ToString()
//WriteLine(bob.ToString()); //does same thing 

bob.Name = "Bob Smith";


bob.Born = new DateTimeOffset(
    year: 1965, month: 12, day: 22,
    hour: 16, minute: 28, second: 0,
    offset: TimeSpan.FromHours(-5));

WriteLine(format: "{0} was born on {1:D}.", //long Date
    arg0: bob.Name, arg1: bob.Born);

//Object Initializer 
Person Alice = new() {
    Name = "Alice Jones",
    Born = new(1998, 3, 7, 16, 28, 0,
    //this is an optional offset from the UTC time zone
    TimeSpan.Zero)
};

WriteLine(format: "{0} was born on {1:d}.", 
    arg0: Alice.Name, arg1: Alice.Born);

bob.FavoriteAncientWonder = WondersOfTheAncientWorld.StatueOfZeusAtOlympia;

WriteLine(
    format: "{0}'s favorite wonder is {1}. Its integer is {2}",
    arg0: bob.Name,
    arg1: bob.FavoriteAncientWonder,
    arg2: (int)bob.FavoriteAncientWonder);

bob.BucketList = WondersOfTheAncientWorld.HangingGardensOfBabylon | WondersOfTheAncientWorld.MausoleumAtHalicarnassus;

//bob.BucketList = (WondersOfTheAncientWorld)18;

WriteLine($"{bob.Name}'s bucket list is {bob.BucketList}");

/*
Book book = new()
{ 
    Isbn = "978-1803237800",
    Title = "C# 12 and .NET 8 - Modern ..."

};
*/

Book book = new(isbn: "978-1803237800",
    title: "C# 12 and .NET 8 - Modern ...")
{
    Author = "Mark J. Price",
    PageCount = 821
};

WriteLine("{0}: {1} written by {2} has {3:N0} pages",
    book.Isbn, book.Title, book.Author, book.PageCount);

Person blankPerson = new();

WriteLine(format:
    "{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.",
    arg0: blankPerson.Name,
    arg1: blankPerson.HomePlanet,
    arg2: blankPerson.Instantiated);

//defining multiple structures
Person gunny = new(initialName: "Gunny", homePlanet: "Mars");

WriteLine(format:
        "{0} of {1} was created at {2:hh:mm:ss} on a {2:dd}",
        arg0: gunny.Name,
        arg1: gunny.HomePlanet,
        arg2: gunny.Instantiated
        );

bob.WriteToConsole();
WriteLine(bob.GetOrigin());

//overloading
WriteLine(bob.SayHello());
WriteLine(bob.SayHello("Emily"));

//optional para 
WriteLine(bob.OptionalParamaters(3));
WriteLine(bob.OptionalParamaters(3, "Jump!", 98.5));

//when naming, order of declaration doesnt matter
WriteLine(bob.OptionalParamaters(3, number: 52.7, command: "Hide!"));
WriteLine(bob.OptionalParamaters(3, "Poke!", active: false));

//para passing 
int a = 10;
int b = 20;
int c = 30;
int d = 40;

WriteLine($"Before: a ={a}, b = {b}, c = {c}, d = {d}");

bob.PassingParameters(a, b, ref c, out d);

WriteLine($"After: a ={a}, b = {b}, c = {c}, d = {d}");

int e = 50;
int f = 60;
int g = 70;

WriteLine($"Before: e = {e}, f = {f}, g = {g}");

//simplified C#7 of later syntax for the out para 
bob.PassingParameters(e, f, ref g, out int h);
WriteLine($"Before: e = {e}, f = {f}, g = {g}");

(string, int) fruit = bob.GetFruit();

WriteLine($"{fruit.Item1}, {fruit.Item2} there are.");

var fruitNamed = bob.GetNamedFriut();
WriteLine($"There are {fruitNamed.Number} {fruitNamed.Name}.");

//to deconstruct tuples 
(string name, int num) = bob.GetNamedFriut();

WriteLine($"Name: {name} number: {num}");

//deconstructor 
var (name1, dob1) = bob; //Implicitly calls the deconstructor
WriteLine($"Deconstructed person: {name1}, {dob1}");

var (name2, dob2, fav2) = bob;
WriteLine($"Deconstructed person: {name2}, {dob2}, {fav2}");

//local funcs 
//change to -1 to make the exept handling 

int number = -5;

try
{
    WriteLine($"{number}! is {Person.Factorial(number)}");
}
catch (Exception ex) 
{
    WriteLine($"{ex.GetType()} says: {ex.Message} number was {number}.");
}

//NEW PARTIAL CLASS SECTION 
WriteLine();
WriteLine("=================PARTIAL======================");
WriteLine();

Person Sam = new()
{
    Name = "Sam",
    Born = new(1969, 6, 25, 0, 0, 0, TimeSpan.Zero)
};

WriteLine(Sam.Origin);
WriteLine(Sam.Greeting);
WriteLine(Sam.Age);

string color = "Red";

try
{
    Sam.FavoritePrimaryColor = color;
    WriteLine($"Sam's fav prim color is {Sam.FavoritePrimaryColor}.");
}
catch (Exception ex)
{
    WriteLine("Tried to set {0} to '{1}':{2}", nameof(Sam.FavoritePrimaryColor), color, ex.Message);
}

//bob.FavoriteAncientWonder =
//   WondersOfTheAncientWorld.StatueOfZeusAtOlympia |
//   WondersOfTheAncientWorld.GreatPyramidOfGiza;

//bob.FavoriteAncientWonder = (WondersOfTheAncientWorld)128;

bob.FavoriteAncientWonder = WondersOfTheAncientWorld.StatueOfZeusAtOlympia;

Sam.Children.Add(new()
{
    Name = "Charlie",
    Born = new(2010, 3, 18, 0 , 0, 0, TimeSpan.Zero)
});

Sam.Children.Add(new()
{
    Name = "Ella",
    Born = new(2020, 12, 24, 0, 0, 0, TimeSpan.Zero)
});

//Get using Children list \
WriteLine($"Sam's first child is {Sam.Children[0].Name}");
WriteLine($"Sam's second child is {Sam.Children[1].Name}");

//get using indexer
WriteLine($"Sam's first child is {Sam[0].Name}");
WriteLine($"Sam's first child is {Sam[1].Name}");

//Get using string index
WriteLine($"Sam's child name ella is {Sam["Ella"].Age} years old.");

//sn array containing a mix of passengers 
Passenger[] passengers = {
    new FirstClassPassenger {AirMiles = 1_419, Name = "Suman" },
    new FirstClassPassenger {AirMiles = 16_562, Name = "Lucy"},
    new BusinessClassPassenger {Name = "Dave"},
    new CoachClassPassenger {CarryOnKG = 25.7, Name = "Dave"},
    new CoachClassPassenger {CarryOnKG = 0, Name = "Amit" },
};

foreach (Passenger passenger in passengers) 
{
    decimal flightCost = passenger switch
    {
        /* C# 8 Syntax
        FirstClassPassenger p when p.AirMiles > 35_000 => 1_500M,
        FirstClassPassenger p when p.AirMiles > 15_000 => 1_750,
        FirstClassPassenger _ => 2_000M, */
        //C# 9 or later syntax
        FirstClassPassenger p => p.AirMiles switch
        {
            > 35_000 => 1_500M,
            > 15_000 => 1_750M,
            _ => 2_000
        },
        
        /*Can Also use to avoid switch statement 
         FirstClassPassenger {AirMiles: > 35_000 } => 1500M
         FirstClassPassenger {AirMiles: > 15_000 } => 1750M
         FirstClassPassenger => 1500M
         */

        BusinessClassPassenger _ => 1_000M,
        CoachClassPassenger p when p.CarryOnKG < 10.0 => 500M,
        CoachClassPassenger _ => 650M,
        _ => 800M
    };
    WriteLine($"Flights costs {flightCost:C} for {passenger}");
}

//init and immutable initialization 
ImmutablePerson jeff = new()
{
    FirstName = "Jeff",
    LastName = "Winger"
};

//jeff.FirstName = "Geoff";

//Records and immutablility

ImmutableVehicle car = new() 
{ 
    Brand = "Mazda MX-5 RF",
    Color = "Soul Red Crystal Metallic",
    Wheels = 4
};

ImmutableVehicle repaintedCar = car
    with { Color = "Polymetal grey" };

WriteLine($"Original car color was {car.Color}");
WriteLine($"New car color is {repaintedCar.Color}");

//NOTE Could release the memory for the car var and the repaintedcar would still exist

//equality compare between class and record 
AnimalClass ac1 = new() { Name = "rex" };
AnimalClass ac2 = new() { Name = "rex" };

WriteLine($"ac1 === ac2: {ac1 == ac2}");
AnimalRecord ar1 = new() { Name = "rex" };
AnimalRecord ar2 = new() { Name = "rex" };
WriteLine($"ac1 === ac2: {ar1 == ar2}");

ImmutableAnimal oscar = new("Oscar", "Lab");

var (who, what) = oscar; //Calls the deconstructor method 
WriteLine($"{who} is a {what}");

Headset vp = new("Apple", "Vision Pro");

WriteLine($"{vp.ProductName} is made by {vp.Manufacturer}");

Headset holo = new();
WriteLine($"{holo.ProductName} is made by {holo.Manufacturer}");

Headset mq = new() {Manufacturer = "Meta", ProductName = "Quest 3"};

WriteLine($"{mq.ProductName} is made by {mq.Manufacturer}");
