using System.Reflection; // TO USE ASSEMBLY

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        WriteLine($"Computer Named {Env.MachineName} says \"No.\"");
        //get the assembly that is the entry point for this app
        Assembly? myApp = Assembly.GetEntryAssembly();

        //if the prev line retuned nothing then end the app
        if (myApp is null) return;

        //loop through the assembly so we read its details
        foreach (AssemblyName name in myApp.GetReferencedAssemblies())
        {
            //Load the assemble so we can read its details
            Assembly a = Assembly.Load(name);

            //declare a var to count the num of methods 
            int methodCount = 0;

            //Loop through all teh types in the assembly 
            foreach (TypeInfo t in a.DefinedTypes)
            {
                //Add up the counts of all the methods
                methodCount += t.GetMethods().Length;
            
            }

            //Output the count of types and their methods
            WriteLine("{0:N0} types with {1:N0} methods in {2} assembly",
                arg0: a.DefinedTypes.Count(),
                arg1: methodCount,
                arg2: name.Name);

            //Declare some unused Variables using types 
            //in addiotnal assemblies to make them load too 
            System.Data.DataSet ds = new();
            HttpClient client = new();           
        }

        // Let the HeightMetres var become equal to the value 1.88
        double heightInMetres = 1.88;
        Console.WriteLine($"The variable {nameof(heightInMetres)} has the value {heightInMetres}");

        //Verbatim strings 
        string filepath = @"C:\television\sony\bravia.txt";

        //Raw String literals
        var person = new { FirstName = "Alice", Age = 56 };

        string json = $$"""
                {
                    "first name": "{{person.FirstName}}",
                    "age": {{person.Age}},
                    "calculation" : "{{{1 + 2}}}"
                }
                """;
        Console.WriteLine(json);

    }
}