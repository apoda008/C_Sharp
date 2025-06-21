//Define an alias for a dictionary with string key and string value
using StringDictionary = System.Collections.Generic.Dictionary<string, string>;



//Simple syntax for creating a List and adding three items
List<string> cities = new();
cities.Add("London");
cities.Add("Paris");
cities.Add("Milan");

/* Alternative syntax that is converted by the comiler into the three Add method calls above.
 * List<string> cities = new() { "London", "Paris", "Milan" };
 */

/* Alternative syntax that passes an array of strin values to AddRange method.
 List<string> cities = new();
cities.AddRange(new[] { "London", "Paris", "Milan" });
 */

OutputCollection("Inital list", cities);
WriteLine($"The first city is {cities[0]}");
WriteLine($"The last city is {cities[^1]}"); // ^1 is the last item in the list

cities.Insert(0, "Sydney");
OutputCollection("After inserting Sydney at the start", cities);

cities.RemoveAt(1);
cities.Remove("Milan");
OutputCollection("After removing Paris and Milan", cities);

//DICTIONARIES
WriteLine("-------------------------------------------");
//declare a dictionary without an alias 
// Dictionary<string, string> capitals = new();

//Use the alias to delcare a dictionary
StringDictionary keywords = new();

//add using named parameters 
keywords.Add(key: "int", value: "32-bit interger data type");

//add using positional parameters 
keywords.Add("long", "64-bit interger data type");
keywords.Add("float", "Sing precision floating point data type");

/* Alternative syntace; compiler converts this to calls to Add method
 * Dictionary<string, string> capitals = new()
 * {
 *  {     "int", "32-bit interger data type" },
 *  {    "long", "64-bit interger data type" },
 *  {    "float", "Sing precision floating point data type" }
 * };
 */

/* Alternative syntax; compiler converts this to call to Add method.
 * Dictionary<string, string> capitals = new()
 * {
 *  ["int"] = "32-bit interger data type",
 *  ["long"] = "64-bit interger data type",
 *  ["float"] = "Sing precision floating point data type"
 * };
 */

OutputCollection("Dictionary Keys", keywords.Keys);
OutputCollection("Dictionary Values", keywords.Values);

WriteLine("Keywords and their definitios: ");
foreach (KeyValuePair<string, string> item in keywords)
{
    WriteLine($"   {item.Key} = {item.Value}");
}

//look up a value using a key 
string key = "long";
WriteLine($"The definition of {key} is {keywords[key]}");

/*
 Trailing commas after the third item is added to the dictionary are optional 
and the compiler will not complain about them. this is convenient so that you
can change the order of the three items without having to delete and add 
commas in the right places
 */

//SETS, STACKS AND QUEUES
WriteLine("-------------------------------------------");
HashSet<string> names = new();

foreach (string name in new[] { "adam", "barry", "charlie", "barry" }) 
{ 
    bool added = names.Add(name);
    WriteLine($"{name} was added: {added}");
}

WriteLine($"names set: {string.Join(", ", names)}");
