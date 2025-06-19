
using System; //To use Console.WriteLine
using System.Xml.Linq; //To use Xdocument
using Packt.Shared;

//NAMESPACES, PACKAGES, FRAMEWORKS (.NET)
XDocument doc = new();

string s1 = "Hello";
String s2 = "World";
WriteLine($"{s1} {s2}");

Write("ENtere a color value in hex: "); 
string? hex = Console.ReadLine();
WriteLine("Is {0} a valid color value? {1}", 
    arg0: hex, arg1: hex.IsValidHex());

Write("Enter a XML element: ");
string? xmlTag = ReadLine();
WriteLine("Is {0} a valid XML element? {1}",
    arg0: xmlTag, arg1: xmlTag.IsValidXmlTag());

Write("Enter password: ");
string? password = ReadLine();
WriteLine("Is {0} a valid password? {1}",
    arg0: password, arg1: password.IsValidPassword());

/*GOOD PRACTICE: WHen you have a choice, use the C# keyword instead of the actual type
 because the keywords do not need a namespace to be imported*/