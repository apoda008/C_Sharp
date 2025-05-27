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
