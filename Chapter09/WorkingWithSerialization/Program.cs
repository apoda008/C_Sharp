/*
 Serialization: process of converting live obj graph into a sequence of bytes using a specified format
.deserialization: process of converting a sequence of bytes into a live obj graph

JSON is more compact and is best for web and mobile apps. XML is more verbose but is better supported 
in legacy systems. Use JSON to minimize the size of serialized obj graphs. JSON is also a good choice 
when sending obj graphs to web apps and mobile apps because JSON is the native serialization format for 
JS and mobile apps often make calls over limited bandwidth so the # of bytes is important.
 
 */

using System.ComponentModel;
using System.Xml.Serialization; //to use xmlSeralization 
using Packt.Shared; //to use person
using FastJson = System.Text.Json.JsonSerializer; //to use System.Text.Json for JSON serialization

List<Person> people = new()
{
    new(initialSalary: 30_000M)
    {
        FirstName = "Alice",
        LastName = "Smith",
        DateOFBirth = new(year: 1974, month: 3, day: 14)
    },
    new(initialSalary: 40_000M)
    { 
        FirstName = "Bob",
        LastName = "Jones",
        DateOFBirth = new(year: 1969, month: 11, day: 23)
    },
    new(initialSalary: 20_000M)
    { 
        FirstName = "Charlie",
        LastName = "Cox",
        DateOFBirth = new(1984, 5, 4),
        Children = new()
        { 
            new(initialSalary: 0M)
            {
                FirstName = "Sally",
                LastName = "Cox",
                DateOFBirth = new(2012, 7, 12)
            }
        }
    }
};

SectionTitle("Serializing as XML");

//Create Serializer to format a list of Person as XML 
XmlSerializer xs = new(type: people.GetType());

//create a file to write to 
string path = Combine(CurrentDirectory, "people.xml");

using (FileStream stream = File.Create(path)) 
{ 
    //serialize the obj graph to the stream 
    xs.Serialize(stream, people);

}//closes the stream 

OutputFileInfo(path);

//GENERATING COMPACT XML (see Person.cs)

//Deserialization XML files 
SectionTitle("Deserializing from XML");

using (FileStream xmlLoad = File.Open(path, FileMode.Open)) 
{ 
    //deserialize and cast teh obj graph into a list of person 
    List<Person>? loadedPeople = xs.Deserialize(xmlLoad) as List<Person>;

    if (loadedPeople is not null)
    {
        foreach (Person p in loadedPeople) 
        { 
            WriteLine("{0} has {1} children", 
                p.LastName, p.Children?.Count ?? 0);
        } 
    }
}
/*MORE INFORMATION 
 *There are many other attributes defined in the System.Xml.Serialization namespace that can be used 
 *to control the XML generated.
 *
 *When using XmlSerializer, remember that only the public fields and properties are included and the 
 *type must have a parameterless constructor. you can customize the output with attributes
 */

//SERIALIZING AS JSON
SectionTitle("Serializing as JSON");

//create a file to write to 
string jsonPath = Combine(CurrentDirectory, "people.json");

using (StreamWriter jsonStream = File.CreateText(jsonPath))
{
    Newtonsoft.Json.JsonSerializer jss = new();

    //Serialize the obj graph into a string
    jss.Serialize(jsonStream, people);
}//Closes the file stream and releases resources 

OutputFileInfo(jsonPath);

//DESERIALIZING JSON FILES
SectionTitle("Deserializing from JSON");

await using (FileStream jsonLoad = File.Open(jsonPath, FileMode.Open)) 
{ 
    //Deserialize obj graph into a list of Person
    List<Person>? laodedPeople = 
        await FastJson.DeserializeAsync(utf8Json: jsonLoad,
        returnType: typeof(List<Person>)) as List<Person>;

    if (laodedPeople is not null) 
    { 
        foreach(Person p in laodedPeople)
        {
            WriteLine("{0} has {1} children",
                p.LastName, p.Children?.Count ?? 0);
        }
    }
}

