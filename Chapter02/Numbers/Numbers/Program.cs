// See https://aka.ms/new-console-template for more information

//unsigned int is pos
using System.Linq.Expressions;

uint naturalNumber = 23;

//an is neg or pos 
int integerNum = -23;

//float is signle precision 
//the F or f suffix makes the value a float literal 
//the suffix is require to compile
float realNum = 2.3f;

//A double is a double-precision float-point num 
//double is the default for a num val with a decimal point
double anotherRealNumber = 2.3; //a double literal value 

int decimalNotation = 2_000_000;
int binaryNotation = 0b_0001_1110_1000_0100_1000_0000;
int hexNotation = 0x_001E_8480;

//Check the 3 var hace the same value 
Console.WriteLine($"{decimalNotation == binaryNotation}");
Console.WriteLine($"{decimalNotation == hexNotation}");

//Output the variable values in decimal 
Console.WriteLine($"{decimalNotation:N0}");
Console.WriteLine($"{binaryNotation:N0}");
Console.WriteLine($"{hexNotation:N0}");

//Output the variable val in hex 
Console.WriteLine($"{decimalNotation:X}");
Console.WriteLine($"{binaryNotation:X}");
Console.WriteLine($"{hexNotation:X}");