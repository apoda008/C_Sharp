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

//using the other compare 
Array.Sort(people, new PersonComparer());

OutputPeopleNames(people, "After sorting using person comparere's ICOMPARER implementation");

/* MEMORY REVIEW
 * Stack mem is faster to work with but limited in size/ It is fast becuz is it managed directly 
 * by the CPU and uses a last-in, first-out mechanism so it is more likely to have data in its 
 * L1 and L2 cache. Heap mem is slower but much more plentiful
 
 *default stack size is 1MB. three keywords to define obj types: class, record, and struct. One 
 *difference is how mem is allocated 
 *--When you define a type record or class, you define a REFERENCE typ. Means that all the mem 
 *for the obj itself is allocated on the heap, and only the mem addr of the obj is stored on the 
 *stack. 
 *--When you define a record struct or struct you define a VALUE typ. Means the mem for the obj 
 *itself is allocated to the stack. If a struct uses field types that are not of the struct type, 
 *then those fields will be stored on the heap. meaning the data for that ohn is stored in both 
 *the stack and the heap
 
 *Apart from the differences in terms of where me the data for a type is stored, the other major 
 *differences are that you cannot inherit from a struct, and struct obj are compared for equality 
 *using values instead of mem addresses
 
 *When the method completes, all the stack mem is automatically released from the top of the stack 
 
 *BOXING in C# is when a value type is moved to heap mem and wrapped inside a System.Object 
 *instance. Unboxing is when that value is moved back onto the stack. Unboxing happens explicitly. 
 *Boxing happens implicitly, so it can happen without the dev realizing. Boxing can take up to 20 
 *times longer than without boxing
 *EXAMPLE:
 
    int n = 3;
    object o = n; //boxing happens implicitly 
    n = (int)o; //Unboxing only happens explicitly
 */


//Equality of types
int a = 3;
int b = 3;

WriteLine($"a: {a}. b: {b}");
WriteLine($"a == b: {a==b}");

Person p1 = new() { Name = "Kevin" };
Person p2 = new() { Name = "Kevin" };

WriteLine($"p1: {p1}, p2: {p2}");
WriteLine($"p1.name: {p1.Name}, p2.name {p2.Name}");
WriteLine($"p1 == p2: {p1 == p2}");

//this fails because they are not the same obj. if both vars literally pointed to the same obj on the
//heap then it would be equal 

Person p3 = p1;
WriteLine($"p1: {p1}, p2: {p3}");
WriteLine($"p1.name: {p1.Name}, p2.name {p3.Name}");
WriteLine($"p1 == p2: {p1 == p3}");

//strings do not follow this convention 

//Alternatively use record class because one of its benefits is that it implements this equality behavior

//defining struct types 

DisplacementVector dv1 = new(3, 5);
DisplacementVector dv2 = new(-2, 7);
DisplacementVector dv3 = dv1 + dv2;

WriteLine($"({ dv1.X}, {dv1.Y}) +({ dv2.X}, {dv2.Y}) = ({dv3.X}, {dv3.Y})");

DisplacementVector dv4 = new();
WriteLine($"({dv4.X}, {dv4.Y})");

DisplacementVector dv5 = new(3, 5);
WriteLine($"dv1.equals(dv5): {dv1.Equals(dv5)}");
WriteLine($"dv1 === dv2: {dv1 == dv5}");

//GOOD PRACTICE: if the total mem used by all the fields in your type is 16 bytes or less, your 
//type only uses value types for its fields, and you will never want to derive from your type, then 
//microsoft recommends that you use struct. if you type uses more than 16 of stack mem, it uses
//reference types for its fields or you might want to inhereit from it using class

//INHERITING FROM CLASSES 
Employee john = new()
{
    Name = "John Jones",
    Born = new(year: 1990, month: 7, day: 28, hour: 0, minute: 0, second: 0, offset: TimeSpan.Zero)
};
WriteLine();
WriteLine("===============================================");
john.WriteToConsole();

john.EmployeeCode = "JJ001";
john.HireDate = new(year: 2014, month: 11, day: 23);

WriteLine($"{john.Name} was hired on {john.HireDate:yyyy-MM-dd}");

//overloading
WriteLine(john.ToString());
