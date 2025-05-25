// See https://aka.ms/new-console-template for more information
Console.Write("a");
Console.Write("b");
Console.Write("c");


int numberOfApples = 12;
decimal priceOfApples = .35M;

Console.WriteLine(
    format: "{0} apples cost {1:C}",
    arg0: numberOfApples,
    arg1: priceOfApples * numberOfApples);

string formatted = string.Format(
    format: "{0} apples cost {1:C}",
    arg0: numberOfApples,
    arg1: priceOfApples * numberOfApples);

//WriteToFile(formatted); //write the string to a file.

//3 para vals can use named arguments 
Console.WriteLine("{0} {1} lived in {2}",
    arg0: "Roger", arg1: "Cevung", arg2: "Stockholm"
    );

//4 or more parameters cannot use named arguments 
Console.WriteLine(
    "{0} {1} lived in {2} and worked in the {3} team at {4}.",
    "Roger", "Cevung", "Stockholm", "Education", "Optimizely");