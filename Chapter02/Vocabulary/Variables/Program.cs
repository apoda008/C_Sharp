// See https://aka.ms/new-console-template for more information
//using static System.Console; nulled to make it global

using System.Xml;
object height = 1.99;
object name = "Amir";
WriteLine($"{name} is {height} metres tall.");

//int length1 = name.Length; //compile error
int length2 = ((string)name).Length; //Cast name to a string 
WriteLine($"{name} has {length2} characters");

dynamic something;

//storing an array of int val in a dynamic object
//an array of any type has a length property
something = new[] { 3, 5, 7 };

//storing an int, DOES NOT have length prop
//something = 12;

//String, HAS length prop
//something = "Ahmed";

//this compiles but might throw exceptions at run time 
WriteLine($"The length of something is {something.Length}");

//Output the type of something var
WriteLine($"something is a {something.GetType()}");

//local vars
int population = 67_000_000;
double weight = 1.88;
decimal price = 4.99M;
string fruit = "Apples";
char letter = 'Z';
bool happy = true;

//inference vars
var population1 = 67_000_000;
var weight1 = 1.88;
var price1 = 4.99M;
var fruit1 = "Apples";
var letter1 = "Z";
var happy1 = true;

//good use of var because it avoids the repeated type
//as show in the more verbose second statement
var xml1 = new XmlDocument(); 
XmlDocument xml2 = new XmlDocument(); //works with all C# versions

//Bad use of var because we cannot tell teh type so we 
//should use a specific type declaration as shown in 2nd statement

var file1 = File.CreateText("something1.txt");
StreamWriter file2 = File.CreateText("something2.txt");

XmlDocument xml3 = new(); //target-typed new in C# 9 or later 

Person Kim = new();
Kim.Birthdate = new(1967, 12, 26); // i.e. new DateTime(1967, 12, 36)

List<Person> people = new()
{
    new() { Firstname = "Alice" }, // Instead of: new Person {....}
    new() { Firstname = "bob"},
    new() { Firstname = "Charlie"}
    
}; //Wont work until Firstname is added to person


//default values 
WriteLine($"default(int) = {default(int)}");
WriteLine($"default(bool) = {default(bool)}");
WriteLine($"default(DateTime) = {default(DateTime)}");
WriteLine($"default(string) = {default(string)}");

int num = 12;
WriteLine($"number set to:  {num}");
num = default;
WriteLine($"number set to: {num}");

//formatting using interpolated strings 
//full syntax of a format item 
//{ index [, alignment ] [ : formatString] }

string applesText = "Apples";
int applesCount = 1234;
string bannasText = "Bananas";
int bananasCount = 56789;

WriteLine();

WriteLine(format: "{0, -10} {1, 6}",
    arg0: "Name", arg1: "Count");

WriteLine(format: "{0, -10} {1,6:N0}",
    arg0: applesText, arg1: applesCount);

WriteLine(format: "{0, -10} {1,6:N0}",
    arg0: bannasText, arg1: bananasCount);

WriteLine("Type your first name and press ENTER");
string? userFirstName = ReadLine(); //? tells compiler that we are expecting a possible null value so it doesnt need to warn 

WriteLine("Type your age and press ENTER");
string age = ReadLine()!; //! is a null-forgiving operator. It tells the compiler that ReadLine will not return null so it can stop warnings
                                
WriteLine($"Hello {userFirstName}, you look good for {age}");

Write("Press any key combination: ");
ConsoleKeyInfo key = ReadKey();
WriteLine();
WriteLine("Key: {0}, Char {1}, Modifiers {2}",
    arg0: key.Key, arg1: key.KeyChar, arg2: key.Modifiers);


//BOTTOM OF PAGE
class Person
{
    public DateTime Birthdate;
    public string Firstname;
}

//the following statement myst be all on one line when using C# 10 
//or earlier. If using C# 11 or Later, we can inclide a line break
//in the middle of an expression but not in the string text.
//WriteLine($"{numberOfApples} apples cost {priceOfApples * numberOfApples:C");