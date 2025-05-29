using Packt.Shared;

Person harry = new()
{
    Name = "Harry",
    Born = new(year: 2001, month: 3, day: 25, hour: 0, minute: 0, second: 0, offset: TimeSpan.Zero)
};

harry.WriteToConsole();

//Implenting functionality using methods 
Person lamech = new() { Name = "Lamech" };
Person adah = new() { Name = "adah" };
Person zillah = new() { Name = "Zillah" };

//call the instance method to marry Lamech and Adah 
lamech.Marry(adah);

//call the static methid to marry lamech and zillah 
//Person.Marry(lamech, zillah);

if (lamech + zillah) 
{ 
    WriteLine($"{lamech.Name} and {zillah.Name} successfully got married");
}

lamech.OutputSpouses();
adah.OutputSpouses();
zillah.OutputSpouses();

//call the instance method to make a baby 
Person baby1 = lamech.ProcreateWith(adah);
baby1.Name = "Jabal";

WriteLine($"{baby1.Name} was born on {baby1.Born}");

//call the static method to make a baby 
Person baby2 = Person.Procreate(zillah, lamech);
baby2.Name = "Tubalcain";

//Use the * operator to multiply 
Person baby3 = lamech * adah;
baby3.Name = "Jubal";

Person baby4 = zillah * lamech;
baby4.Name = "Naamah";


adah.WriteChildrenToConsole();
zillah.WriteChildrenToConsole();
lamech.WriteChildrenToConsole();

for (int i = 0; i < lamech.Children.Count; i++) 
{
    WriteLine(format: "   {0}'s child #{1} is named \"{2}\".",
        arg0: lamech.Name, arg1: i, arg2: lamech.Children[i].Name
        );
}

//non-generic lookup collection 
System.Collections.Hashtable lookupobj = new();

lookupobj.Add(key: 1, value: "Alpha");
lookupobj.Add(key: 2, value: "Beta");
lookupobj.Add(key: 3, value: "Gamma");
lookupobj.Add(key: harry, value: "Delta");

//NOTE: three items have a unique integer key for lookup. the last item has a person 
//obj as its key for look up. This is valid in a non-generic collection

int key = 2; //look up the value that has 2 as a key 

WriteLine(format: "Key {0} has value: {1}",
    arg0: harry, lookupobj[harry]);

/*GOOD PRACTICE
 Avoid types in the system.collections namespace. Use types in the system.collections.generic
and related namespaces instead. If you need to use a library that uses non-generic types then 
of course you will have to use non-generic types. this is commonly referred to as technical 
debt
*
When a generic type has one definable type, it should be named T, for example List<T> where T 
is the type stored in the list. When a generic type has multiple definable types, it should 
use T as name prefix and have a sensible name, for example Dictionary <TKey, TValue>
*
--Methods-- are often described as actions that an object can perform either on itself or on related
objects
--Events--  are described as actions that happen to an object 
 */

//define a generic lookup collection 
Dictionary<int, string> lookupIntString = new();

lookupIntString.Add(key: 1, value: "Alpha");
lookupIntString.Add(key: 2, value: "Beta");
lookupIntString.Add(key: 3, value: "Delta");
lookupIntString.Add(key: 4, value: "Gamma");

key = 3;
WriteLine(format: "Key {0} has value: {1}",
    arg0: key,
    arg1: lookupIntString[key]);

//assign the method to the -event- Shout Delegate 
harry.Shout += Harry_Shout;

//Assign the methods(s) to the shout event delegate 
//works because its using the same event handler? 
harry.Shout += Harry_Shout_2;

//call the poke method that eventually raises the shout event 
harry.Poke();
harry.Poke();
harry.Poke();
harry.Poke();

Person?[] people =
    {
    null,
    new() {Name = "Simon"},
    new() {Name = "Jenny"},
    new() {Name = "Adam"},
    new() {Name = null},
    new() {Name = "Richard"}
    };

OutputPeopleNames(people, "Inital list of people");

Array.Sort(people);

OutputPeopleNames(people, "After Sorting using Person's IComparable implementation");

