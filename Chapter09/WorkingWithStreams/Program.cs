using System.Xml; //To use XmlWriter and so on
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
//Close on the stream writer helper will call Close on the underlying stream. 
// This in turn calls Dispose to release unmanaged file resources

OutputFileInfo(textFile); //Display file info

//WRITING TO XML ELEMENTS 
SectionTitle("Writing to XML streams");

//define a file path to write to
string xmlFile = Combine(CurrentDirectory, "streams.xml");

//Declare variables for the filestream and XML writer
FileStream? xmlFileStream = null;
XmlWriter? xml = null;

try
{
    xmlFileStream = File.Create(xmlFile);

    //wrap the file stream in an XML writer helper and tell it to automatically indent nested elements
    xml = XmlWriter.Create(xmlFileStream, new XmlWriterSettings { Indent = true });

    //write the xml declaration 
    xml.WriteStartDocument();

    //write root element 
    xml.WriteStartElement("Callsigns");

    //Enumerate the strings, writing each one to the stream 
    foreach (string item in Viper.CallSigns)
    {
        //write an element for each callsign
        xml.WriteElementString("Callsign", item);
    }

    //close the root element
    xml.WriteEndElement();
}
catch (Exception ex)
{
    //if the path doesn't exist the exception will be caught 
    WriteLine($"{ex.GetType()} says {ex.Message}");

}
finally 
{
    if (xml is not null) 
    {
        xml.Close();
        WriteLine("The XML writer's unmanaged resources have been released.");
    }

    if (xmlFileStream is not null) 
    { 
        xmlFileStream.Close();
        WriteLine("The file stream's unmanaged resources have been released.");
    }
}

OutputFileInfo(xmlFile); //Display file info

/*
The close and Dispose(bool) methmods are virtual in the Stream class because they are designed to be 
overridden in a derived class, like FileStream, to do the work of releasing unmanaged resources

GOOD PRACTICE: Before calling the Dispose method, check that the object is not null 
 */

//SIMPLIFY DISPOSAL WITH STATMENTS 'using'

//COMPRESSING STREAMS
SectionTitle("Compressing XML streams");
Compress(algorithm: "gzip");
Compress(algorithm: "brotli");

// As well as choosing a compression mode, you can also choose a compression level. 

//READING AND WRITING WITH RANDOM ACCESS HANDLES 