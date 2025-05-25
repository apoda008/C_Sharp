// See https://aka.ms/new-console-template for more information
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

Console.WriteLine($"int uses {sizeof(int)} bytes and can store numbers in the range {int.MinValue:N0} to {int.MaxValue:N0}");
Console.WriteLine($"double uses {sizeof(double)} bytes and can store numers in the range {double.MinValue:N0} to {double.MaxValue:N0}");
Console.WriteLine($"decimal uses {sizeof(decimal)} bytes and can store numbers in teh range {decimal.MinValue:N0} to {decimal.MaxValue:N0}");

Console.WriteLine("Using doubles");
double a = 0.1;
double b = 0.2;
if (a + b == 0.3)
{
    Console.WriteLine($"{a} + {b} equals {0.3}");

}
else
{
    Console.WriteLine($"{a} + {b} does NOT equal {0.3}");

}

Console.WriteLine("Using doubles");
decimal c = 0.1M; //M Suffix means a dec literal value
decimal d = 0.2M;
if (c + d == 0.3M)
{
    Console.WriteLine($"{c} + {d} equals {0.3M}");

}
else
{
    Console.WriteLine($"{c} + {d} does NOT equal {0.3M}");

}
