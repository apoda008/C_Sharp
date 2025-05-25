using Packt.Shared;

//alias
//using tx = Texas where Texas is the imported namespace

ConfigureConsole(); //sets current culture to US ENGLISH

//Alternatives :
//ConfigureConsole(useComputer: true);
//configureConsole(culture: "fr-FR")


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

WriteLine(format: "{0} was born on {1:D}.",
    arg0: bob.Name, arg1: bob.Born);

bob.FavoriteAncientWonder = WondersOfTheAncientWorld.StatueOfZeusAtOlympia;

WriteLine(
    format: "{0}'s favorite wonder is {1}. Its integer is {2}",
    arg0: bob.Name,
    arg1: bob.FavoriteAncientWonder,
    arg2: (int)bob.FavoriteAncientWonder);

bob.BucketList = WondersOfTheAncientWorld.HangingGardensOfBabylon | WondersOfTheAncientWorld.MausoleumAtHalicarnassus;

//bob.BucketList = (WondersOfTheAncientWorld)18;

WriteLine($"{bob.Name}'s bucket list is {bob.BucketList}");

Book book = new()
{ 
    Isbn = "978-1803237800",
    Title = "C# 12 and .NET 8 - Modern ..."

};

WriteLine("{0}: {1} written by {2} has {3:N0} pages",
    book.Isbn, book.Title, book.Author, book.PageCount);