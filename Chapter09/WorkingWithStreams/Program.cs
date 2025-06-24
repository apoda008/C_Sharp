using Packt.Shared; //to use viper

SectionTitle("Writing to text streams");    

//define a file to write to 
string textFile = Combine(CurrentDirectory, "streams.txt");

//create a text file and return a helper writer 
StreamWriter text = File.CreateText(textFile);

//Enumerate the strings, writing each one to the stream 
foreach (string item in Viper.CallSigns)
{
    text.WriteLine(item);
}

text.Close(); //Release unmanaged file resources 

OutputFileInfo(textFile); //Display file info
