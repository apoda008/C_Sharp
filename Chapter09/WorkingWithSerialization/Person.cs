namespace Packt.Shared;
using System.Xml.Serialization; //to use [xmlattribute] //allows for compact XML 

public class Person
{
    public Person(decimal initialSalary)
    {
        Salary = initialSalary;
    }

    [XmlAttribute("fname")]
    public string? FirstName { get; set; }
    [XmlAttribute("lname")]
    public string? LastName { get; set; }
    [XmlAttribute("dob")]
    public DateTime DateOFBirth { get; set; }
    public HashSet<Person>? Children { get; set; }
    protected decimal Salary { get; set; }

    public Person() { }
    //The constructor does not need to do anything but it must exist
    //so that the XmlSerializer can call it to instantiate new Person
    //instances during the deserialization process
}
