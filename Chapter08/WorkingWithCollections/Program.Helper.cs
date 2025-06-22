partial class Program 
{
    private static void OutputCollection<T>(
        string title, IEnumerable<T> collection)
    {
        WriteLine($"{title}:");
        foreach (T item in collection)
        {
            WriteLine($"   {item}");
        }
    }

    private static void OutputPQ<TElement, TPriority>(string title, IEnumerable<(TElement Element, TPriority Priority)> collection) 
    { 
        WriteLine($"{title}:");
        foreach ((TElement, TPriority) item in collection) 
        { 
            WriteLine($"   {item.Item1}: {item.Item2}");
        }
    }
    /*
     Note that the OoutPQ method is generic. You can specifiy the two types used in the tuples that are passed in as collection
     */
    private static void useDictionary(IDictionary<string, string> dictionary) 
    { 
        WriteLine($"Count before is {dictionary.Count}.");
        try
        {
            WriteLine("Adding new item with GUID values");
            // add method with return type of void 
            dictionary.Add(
                key: Guid.NewGuid().ToString(),
                value: Guid.NewGuid().ToString());
        }
        catch (NotSupportedException) 
        { 
            WriteLine("This dictionary does not support the add method.");

        }
        WriteLine($"Count after is {dictionary.Count}.");
    }

    /*
     Note the type of parameter is IDictionary<TKey, TValue>. Using an interface provides more flexibility because we can pass either 
     a Dictionary<TKey, TValue> a ReadOnlyDictionary<Tkey, Tvalue>. or anything else that implements that interface
     */
}