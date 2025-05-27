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