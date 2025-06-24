using System.Text;

WriteLine("ENcodings");
WriteLine("[1] ASCII");
WriteLine("[2] UTF-7");
WriteLine("[3] UTF-8");
WriteLine("[4] UTF-16 (UNICODE)");
WriteLine("[5] UTF-32");
WriteLine("[6] Latin1");
WriteLine("[any other key] default encoding");
WriteLine();

Write("Press a number to choose an encoding");
ConsoleKey number = ReadKey(intercept: true).Key;
WriteLine(); WriteLine();

Encoding encoder = number switch
{
    ConsoleKey.D1 or ConsoleKey.NumPad1 => Encoding.ASCII,
    ConsoleKey.D2 or ConsoleKey.NumPad2 => Encoding.UTF7,
    ConsoleKey.D3 or ConsoleKey.NumPad3 => Encoding.UTF8,
    ConsoleKey.D4 or ConsoleKey.NumPad4 => Encoding.Unicode,
    ConsoleKey.D5 or ConsoleKey.NumPad5 => Encoding.UTF32,
    ConsoleKey.D6 or ConsoleKey.NumPad6 => Encoding.Latin1,
    _ => Encoding.Default
};

//define a string to encode 
string message = "CAFE 4.39";
WriteLine($"Text to encode: {message} Characters: {message.Length}");

//define the string into a byte array 
byte[] encoded = encoder.GetBytes(message);

//check how many bytes the encoding needed 
WriteLine("{0} used {1:N0} bytes.",
    encoder.GetType().Name, encoded.Length);
WriteLine();

//Enumerate each byte 
WriteLine("BYTE | HEX | CHAR");
foreach(byte b in encoded)
{
    //print the byte, its hex value and its char representation
    WriteLine($"{b,4} | {b, 3:X} | {(char)b, 4}");
}

//Decode the byte array back into a string a display it 
string decoded = encoder.GetString(encoded);
WriteLine($"Decoded: {decoded}");

//ENCODING AND DECODING TXT IN FILE 
/*
Often, you wont have the choice of which encoding to use because you 
will generate a file for use by another system. However if you do 
pick one that uses the least number of bytes but can store every 
character you need.  
 */

