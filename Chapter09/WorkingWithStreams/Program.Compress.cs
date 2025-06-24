using Packt.Shared; //to use Viper
using System.IO.Compression; //to use BrotliStream, GzipStream
using System.Xml; //to use XmlWriter and so on

partial class Program 
{
    private static void Compress(string algorithm = "gzip") 
    { 
        //define a file path using the algo as file extension 
        string filePath = Combine(CurrentDirectory, $"streams.{algorithm}");

        FileStream file = File.Create(filePath);

        Stream compressor;
        if (algorithm == "gzip")
        {
            compressor = new GZipStream(file, CompressionMode.Compress);
        }
        else 
        { 
            compressor = new BrotliStream(file, CompressionMode.Compress);
        }

        using (compressor) 
        {
            using (XmlWriter xml = XmlWriter.Create(compressor)) 
            { 
                xml.WriteStartDocument();
                xml.WriteStartElement("Callsigns");
                foreach (string item in Viper.CallSigns)
                {
                    xml.WriteElementString("callsign", item);
                }
            }
        }//also closes the underlying stream 

        OutputFileInfo(filePath);

        //read the compressed file. 
        WriteLine("Reading the compressed XML file: ");
        file = File.Open(filePath, FileMode.Open);
        Stream decompressor; 
        if (algorithm == "gzip")
        {
            decompressor = new GZipStream(file, CompressionMode.Decompress);
        }
        else
        {
            decompressor = new BrotliStream(file, CompressionMode.Decompress);
        }

        using (decompressor) 
        {
            using (XmlReader reader = XmlReader.Create(decompressor))
                while (reader.Read()) 
                { 
                    //check of we are on an element node named callsign 
                    if((reader.NodeType == XmlNodeType.Element) && (reader.Name == "callsign"))
                    {
                        reader.Read(); // move to the text inside the element
                        WriteLine($"{reader.Value}"); //read its value
                    }

                    /* alternative syntax with prop pattern matching 
                     * if (reader is { NodeType: XmlNodeType.Element, Name: "Callsign" })
                     
                     */
                }
        }
    }
}

