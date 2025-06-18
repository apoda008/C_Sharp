
using System; //To use Console.WriteLine
using System.Xml.Linq; //To use Xdocument

//NAMESPACES, PACKAGES, FRAMEWORKS (.NET)
XDocument doc = new();

string s1 = "Hello";
String s2 = "World";
WriteLine($"{s1} {s2}");

/*GOOD PRACTICE: WHen you have a choice, use the C# keyword instead of the actual type
 because the keywords do not need a namespace to be imported*/