/*
 Methods that start with AS, like AsEnumerable, cast the sequence into a diff type but do not allocate 
memory so those medthods are fast. Methods that start with TO, like ToList allocate memory for a new 
sequence of items so they can be slow and will always use more memory resources
 */

//A string array is a sequence that implements IEnumerable<string>
string[] names = { "Micheal", "Pam", "Jim", "Deiwght", "Angela", "Kevin", "Toby", "Creed" };

//DeferredExecution(names);

//FILTERING ENTITIES USING WHERE
/*
    Where is an extension  method. It does not exist on teh array type. To make the Where extension 
method availabl, we must import the system.linq namespace. This is implicity imported by defualt in the 
new .NET 6 and later projects.
 */



//FilteringUsingWhere(names);

//SIMPLIFYING THE CODE BY REMOVING THE EXPLICIT DELEGATE INSTANTIATION 
//----See Program.Functions.cs

//Targeting a lambda expression
//----See Program.Functions.cs

//LAMBDA EXPRESSION WITH DEFAULT PARAMETER VALUES 

// var query = names.where((string name = "bob") => name.Length > 4);

//SORTING AND MORE 
// OrderBy and ThenBy for sorting 

//SORTING BY A SINGLE PROPERTY USING OrderBy
//---See var query in Program.Functions.cs
/*
 Extension methods can be chained if they previous method returns another sequence, that is, a type that implements the IEnumerable<T> interface
 */

//SORTING BY A SUBSEQUENT PROPERTY USING ThenBy
//---See var query in Program.Functions.cs

//SORTING BY THE ITEM ITSELF
//---See var query in Program.Functions.cs

//DECLARING A QUERY USING VAR OR A SPECIFIED TYPE
//---See var query in Program.Functions.cs

/*
 With LINQ is it convenient to use var to declare the query object bcause the return type frequently changes as you work on a LINQ expression. For 
example, this query started as an IEnumerable<string> and is currently an IOrderedEnumerable<string>
 
 */

//FILTERING BY TYPE
//FilteringByType();

//WORKING WITH SETS AND BAGS
/*
 Set: is a collection of one or more unique objects 

Multiset, aka bag: is a collection of one or more objects that can have duplicates
 */

WorkingWithSet();

//SO FAR THIS HAS BEEN WORK WITH IN MEMORY OBJECTS NOW IT WILL BE DATABASES FOLLOWING THIS
