using Packt.Shared;

int thisCannotBeNull = 4;
//thisCannotBeNull = null; //CS0037 compiler error!
WriteLine(thisCannotBeNull);

int? thisCouldBeNull= null;
WriteLine(thisCouldBeNull);
WriteLine(thisCouldBeNull.GetValueOrDefault());

thisCouldBeNull = 7;

WriteLine(thisCouldBeNull);
WriteLine(thisCouldBeNull.GetValueOrDefault());

//the actual type of int? is Nullable<int>
Nullable<int> thisCouldAlsoBeNull = null;
thisCouldBeNull = 9;
WriteLine(thisCouldAlsoBeNull);

//when you append ? after a struct type, you can change it to a different
//type. for example DateTime? becomes Nullable<DateTime>.

Address address = new(city: "London")
{
    Building = null,
    Street = null!, //warno here but not on building. '?'
    Region = "UK"
};

//WriteLine(address.Building.Length); //warning

if(address.Street is not null) 
{ 
    WriteLine(address.Street.Length);
}

WriteLine(address.Building?.Length);

//NRT Nullable Reference Types

/*
 *Suffixing a reference type with ? does not change the type. This is 
 different from suffixing a value with ?, which changes its type to 
 Nullable<T>. Reference types can already have null values./ ALl you do 
 with NRTs is tell the compiler that you expect it to be null, so the compiler 
 does not need to warn you. However this does not remove the need to perform 
 null checks throughout your code
 */

//CHECKING FOR NULL
/*
 Although you traditionally would use the expression (thisCouldBeNull != 
 Null), this is no longer considered good practice because the dev could have 
 overloaded the != operator to change how it works. using pattern matching 
 with is null and is not null are the only guaranteed ways to check for null. 
 * **/

string authorName = null;
int? authorNameLength;

//the following throws a NullReferenceException 
//authorNameLength = authorName.Length;

//Instead of throwing an exception, null is assigned
authorNameLength = authorName?.Length;

/*
 Sometimes you want to either assing a var to a result or use an alternative 
value such as 3, if the var is null. you do this using the null-coalescing 
operator ?? 
 */

//result will be 25 is authorname?.length is null 
authorNameLength = authorName?.Length ?? 25;


