using System.Numerics;

const int width = 40;

WriteLine("unlong.Maxvlaue vs a 30 digit BigInt");
WriteLine(new string('-', width));

ulong big = ulong.MaxValue;
WriteLine($"{big,width:N0}");

BigInteger bigger = BigInteger.Parse("123456789012345678901234567890");
WriteLine($"{bigger,width:N0}");

/*
 the width constant with the value 40 in the format code means "right-align 40 characters" 
so both numbers are lined up to the right-hand edge. The N0 means "use thousand separators 
and zero decimal places
 */

//COMPLEX NUMBERS
WriteLine();

//     (a + b2) + (c + bi) = (a +c) + (b + d)i
Complex c1 = new(real: 4, imaginary: 2);
Complex c2 = new(real: 3, imaginary: 7);
Complex c3 = c1 + c2;

//output using hte default tostring implementation 
WriteLine($"{c1} added to {c2} is {c3}");

//output using a custom format 
WriteLine("{0} + {1}i added to {2} + {3}i is {4} + {5}i",
    c1.Real, c1.Imaginary, c2.Real, c2.Imaginary, c3.Real, c3.Imaginary);

/*
 .NET6 and earlier used a different default format for complex numbers: (4,2) added to (3,7) 
is (7,9). In .NET7 and later the default formate was changed to use angle brakets and 
semi-colons because some cultures use round brakcets to indicate negative numbers and use 
commas for decimal #s. 
 */

//RANDOM NUMBERS 
/*
 SHared seed values as a secret key, so if you use the same random number generations algo 
with the same seed value in two apps then they can generate the same "random" sequences of 
numbers. Sometimes this is necessary for example, when synchronizing a gps receiver with a 
satelite, or when a game needs to randomly generate the same level. but usually you want to 
keep your seed secret 
 */

Random r = Random.Shared;

//minValue is an inclusive lower bound ie 1 is a possible value 
//maxValue is an exclusive upper bound ie 7 is not a possible value
int dieRoll = r.Next(minValue: 1, maxValue: 7); //returns 1 to 6
WriteLine($"Rolled a die: {dieRoll}");

double randomReal = r.NextDouble(); //returns 0.0 to less than 1.0
WriteLine($"Random double: {randomReal}");

byte[] arrayofBytes = new byte[256];
r.NextBytes(arrayofBytes); //fills array with 256 random bytes 
Write("Random bytes: ");    
for (int i = 0; i < arrayofBytes.Length; i++)
{
    Write($"{arrayofBytes[i]:X2} "); //format each byte as a hex value 
}
WriteLine();

/*
 in scenarios that do need truly random numbers like crypto, there are specialized types for 
that, like RandomNumberGenerator. 

GetItems<T>L this method is passed an array or read only span of any type t of choices and 
the number of items you want to generate, and then it returns that num of items randomly 
from the selected choices

Shuffle<T>: this method is passed an or span of any type T and it shuffles the items in that array or span

 */

string[] beatles = r.GetItems(choices: new[] { "John", "Paul", "George", "Ringo" }, length: 10);

Write("Random Ten beatles: ");
foreach (string beatle in beatles)
{
    Write($"{beatle} ");
}
WriteLine();

r.Shuffle(beatles);

Write("Shuffled beatles: ");

foreach (string beatle in beatles)
{
    Write($"{beatle} ");
}
WriteLine();

//GLOBALLY UNIQUE IDENTIFIERS (GUIDs)
WriteLine();
WriteLine($"Empty GUID: {Guid.Empty}");

Guid g = Guid.NewGuid();
WriteLine($"Random GUID: {g}");

byte[] guidAsBytes = g.ToByteArray();
Write("GUID as byte array: ");  
for (int i = 0; i < guidAsBytes.Length; i++)
{
    Write($"{guidAsBytes[i]:X2} ");
}
WriteLine();









