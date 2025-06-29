using System.Security.Cryptography.X509Certificates;

partial class Program 
{
    private static void DeferredExecution(string[] names) 
    {
        SectionTitle("Deferred Execution");

        //Question: Which names end with an M 
        //(Using a LINQ extension method)
        var query1 = names.Where(name => name.EndsWith("m"));

        //Question: which names with an M
        //(Using LINQ query comprehension syntax)
        var query2 = from name in names where name.EndsWith("m") select name;

        //Answer returned as an array of strings containing Pam and Jim
        string[] result1 = query1.ToArray();

        //answer returned as alist of strings contain Pam and Jim 
        List<string> result2 = query2.ToList();

        //Answer returned as we enumerate over the results 
        foreach (string name in query1)
        {
            WriteLine(name); //outputs pam
            names[2] = "Jimmy"; //Change jim to Jimmy 
            //on the second iteration jimmy does not 
            //end iwth an 'm' so it does not get output
        }
    }

    private static void FilteringUsingWhere(string[] names) 
    {
        //var query = names.Where(new Func<string, bool>(NameLongerThanFour));

        //The compiler creates the delegate automatically 
        //var query = names.Where(NameLongerThanFour);

        //using lambda expressions instead of a named method
        
        //var query = names.Where(names => names.Length > 4).OrderBy(name => name.Length).ThenBy(name => name);
        // ^- ThenBy; when values are the same from the 'OrderBy' call it is 'then' sorted by the next
        // expression, in this case alphabetical. 

        //before NET 7: you would have to call the orderby meth and pass a lambda that selects the items themselves
        //-- var query = names.OrderBy(name => name);

        //After Net7: 
        //-- var query names.Order()

        IOrderedEnumerable<string> query = names
            .Where(name => name.Length > 4)
            .OrderBy(name => name.Length)
            .ThenBy(name => name);

        foreach (string item in query) 
        {

            WriteLine(item);
        }
    }

    static bool NameLongerThanFour(string name) 
    { 
        //returns true for a name longer than four characters
        return name.Length > 4;
    }

    static void FilteringByType() 
    {
        SectionTitle("Filtering by type");
        List<Exception> exceptoins = new()
        {
            new ArgumentException(), new SystemException(),
            new IndexOutOfRangeException(), new InvalidOperationException(),
            new NullReferenceException(), new InvalidCastException(),
            new OverflowException(), new DivideByZeroException(),
            new ApplicationException()
        };

        IEnumerable<ArithmeticException> arithmeticExceptionsQuery =
            exceptoins.OfType<ArithmeticException>();

        foreach (ArithmeticException exception in arithmeticExceptionsQuery) 
        {
            WriteLine(exception);
        }
    }

    static void Output(IEnumerable<string> cohort, string description = "") 
    {
        if (!string.IsNullOrEmpty(description)) 
        { 
            WriteLine(description);
        }

        Write(" ");
        WriteLine(string.Join(", ", cohort.ToArray()));
        WriteLine();
    }

    static void WorkingWithSet()
    {
        string[] cohort1 = { "Rachel", "Gareth", "Jonathan", "George" };

        string[] cohort2 = { "Jack", "Stephen", "Daniel", "Jack", "Jared" };

        string[] cohort3 = { "Declan", "Jack", "Jack", "Jasmine", "Conor" };

        SectionTitle("The Cohorts");

        Output(cohort1, "Chohort 1");
        Output(cohort2, "Cohort 2");
        Output(cohort3, "Cohort 3");

        SectionTitle("Set Operatios");

        Output(cohort2.Distinct(), "cohort2.Distinct()");
        Output(cohort2.DistinctBy(name => name.Substring(0, 2)),
            "cohort2.DistinctBy(name => name.Substring(0,2))");
        Output(cohort2.Union(cohort3), "cohort2.Union(cohort3)");
        Output(cohort2.Concat(cohort3), "cohort2.Concat(cohort3)");
        Output(cohort2.Intersect(cohort3), "cohort2.Intersect(cohort3)");
        Output(cohort2.Except(cohort3), "cohort2.Except(cohort3)");
        Output(cohort1.Zip(cohort2, (c1, c2) => $"{c1} matched with {c2}"), "cohort1.zip(cohort2)");
        /*
         With zip if there an unequal numbers of items in the two sequences, then some items will not 
        have a matching partner. Those without a partner, like Jared, will not be included in the result
         */
    }

}