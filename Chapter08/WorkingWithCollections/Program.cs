//Define an alias for a dictionary with string key and string value
using StringDictionary = System.Collections.Generic.Dictionary<string, string>;
using System.Collections.Immutable;
using System.Collections.Frozen;

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


//QUEUES 
WriteLine();
Queue<string> coffee = new();

coffee.Enqueue("Latte"); // front of queue
coffee.Enqueue("Cappuccino");
coffee.Enqueue("Espresso");
coffee.Enqueue("Mocha");
coffee.Enqueue("Americano"); // back of queue

OutputCollection("Initial queue from front to the back", coffee);

//server handles next person in queue 
string served = coffee.Dequeue();
WriteLine($"Served: {served}");
OutputCollection("Queue after serving one person", coffee);

//server handles next person in queue
served = coffee.Dequeue();
WriteLine($"Served: {served}");
OutputCollection("Queue after serving another person", coffee);

WriteLine($"{coffee.Peek()} is next in line");
OutputCollection("Current queue from front to back", coffee);

PriorityQueue<string, int> vaccine = new();

//add some people
// 1 = High prio people in their 70s or poor health 
// 2 = Medium prio e.g. middle-aged
// 3 = low prio e.g. teens and twenties

vaccine.Enqueue("Pamela", 1);
vaccine.Enqueue("Rebecca", 3);
vaccine.Enqueue("Juliet", 2);
vaccine.Enqueue("Ian", 1);

OutputPQ("Current queue for vaccine", vaccine.UnorderedItems);

WriteLine($"{ vaccine.Dequeue()} has been vaccinated");
WriteLine($"{vaccine.Dequeue()} has been vaccinated");
OutputPQ("Current queue for vaccine", vaccine.UnorderedItems);

WriteLine($"{vaccine.Dequeue()} has been vaccinated");

WriteLine("Adding mark to queue w/ prio 2");
vaccine.Enqueue("Mark", 2);

WriteLine($"{vaccine.Peek()} is next in line for vaccination");
OutputPQ("current queue for vaccine", vaccine.UnorderedItems);

//SPECIALIZED COLLECTIONS
//READONLY, IMMUTABLE 
WriteLine("-------------------------------------------");

//useDictionary(keywords);
//useDictionary(keywords.AsReadOnly());

useDictionary(keywords.ToImmutableDictionary());
//Immutables retunrs a new immutable collection w/ the new item in it. The
//original immutable collection still only has the original items

ImmutableDictionary<string, string> immutableKeywords = keywords.ToImmutableDictionary();

//call the add method w/ a return value
ImmutableDictionary<string, string> newDictionary = immutableKeywords.Add(
    key: Guid.NewGuid().ToString(),
    value: Guid.NewGuid().ToString());

OutputCollection("Immutebale keywords dict: ", immutableKeywords);
OutputCollection("new keywords dict: ", newDictionary);

/*
 Newly added items will not always appear at the top of the dictionary. Internally, the order
is defined by the hash of the key. This is why dictionaries are sometimes called hash tables

To improve performance, many apps store a shared copy of commonly accessed objs in a central 
cache. To safely allow multiple threads to work with those obj knowing they wont change, you 
should make immutable or use a concurrent collection type
 */

//FROZEN COLLECTIONS
WriteLine();
// Concept of that 95% the time a collection is populated and then never changed. 
//only two frozen collections: FrozenDictionary<TKey, TValue> and FrozenSet<T>

//creating a frozen collection has an overhead to perfrom the sometimes complex optimizations 
FrozenDictionary<string, string> frozenKeywords = keywords.ToFrozenDictionary();

OutputCollection("Frozen keywords dictionary", frozenKeywords);

//lookups are faster in a frozen dictionary 
WriteLine($"Define long: {frozenKeywords["long"]}");

//Initializing collections using collection expressions 
//int[] numarray12 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
//List<string> names = ["Adam", "Barry", "Charlie"];
//Span<int> numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

