using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions; //to use regex

Write("Enter your age");
string input = ReadLine()!;
//Regex ageChecker = new(@"^\d+$"); // Regular expression to match digits
//Regex ageChecker = new(DigitsOnlyText); // Using a constant for the regex pattern
Regex ageChecker = DigitsOnly();

WriteLine(ageChecker.IsMatch(input) ? "Thank you!" : $"This is not a valid age: {input}");

/*
@ switches off the ability to use escape characters "\t" 
^matches the start of the string
$matches the end of the string
+modify to the meaning to one or more digits

use regular expressions to validate input from the user. 
*/

//splitting a complex comma string 

//C#1 to 10: use escaped double - qoute characters 
//string films = "\"Monsters, Inc.\", "I, Tonya\", \"Lock, Stock and Two Smoking Barrels\"";

//C11 or later: Use """ to start and end a raw string literal 
string films = """
    "Monsters, Inc.", "I, Tonya", "Lock, Stock and Two Smoking Barrels"
    """;

WriteLine($"Films to split: {films}");

string[] filmsDumb = films.Split(',');

WriteLine("Splitting with string.Split() method:");
foreach (string film in filmsDumb)
{
    WriteLine($"   {film}");
}

//Regex csv = new(
//    "(?:^|,)(?=[^\"]|(\")?)\"?((?(1)[^\"]*|[^,\"]*))\"?(?=,|$)"); // Regular expression to match CSV format

//Regex csv = new(CommaSeperatorText);

Regex csv = CommaSeperator();

MatchCollection filmsSmart = csv.Matches(films);

WriteLine("Splitting with regular expressions: ");
foreach (Match film in filmsSmart)
{
    WriteLine($"   {film.Groups[2].Value}");
}

//COLLECTIONS

/*
 The [defaultMember] attribute allows you to specify which member is accessed by default when no name is 
specified. To make IndexOf the default member you would use [DefaultMember("indexOf")]. To specify the indexer 
you use [DefaultMember("Item")]. 

Some devs can get into the poor habit of using List<T> and other collections when an array would be better. 
use arrays instead of collections if the data will not change size after instantiation. You should also use 
lists initially while you are adding and removing items, but then convert them into an array once you are done 
with manipulating them. 
*/