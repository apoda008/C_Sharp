ConfigureConsole();

//FILTERING AND SORTING SEQUENCES
//FilterAndSort();

//JOINING, GROUPING, AND LOOKUPS
//JOINING SEQUENCES
//JoinCatagoriesAndProducts();

//GROUP-JOINING SEQUENCES 
//GroupJoinCategoriesAndProducts();

//GROUPING FOR LOOKUPS ======LAST COMMIT=======
//ProductsLookUp();

//AGGREGATING AND PAGING SEQUENCES
//AggregateProducts();

//CHECKING FOR AN EMPTY SEQUENCE
/*
 Call count() and see if it returns zero - worst way to do this 

call LINQ Any() and see if it returns true - better than count() but still not best 

Get the sequence's count prop (if it has one) and see if its greater than zero. 

Get the sequences Length prop (if it has one) and see if its greater than zero,
 */

//BE CAREFUL WITH COUNT
//The short of it: avoid calling it unless its a type that has it

//PAGING WITH LINQ
PagingProducts();

/*
 GOOD PRACTICE: You should always order data before calling Skip and Take if you want 
to implement paging. This is because each time you execute a query, the linq provider 
does not have to guarantee to return the data in the same order unless you have 
specified it. Therefore, if the SQlite provider wanted to, the first time you request 
a page of products they might be in productid order but the next time you request a 
page of products they might be in unitprice oder or a random order and that would 
confuse the user. in practice, at least for relational databases, the defualt order 
is usually by its index on the primary key
 */

//SWEETENING LINQ SYNTAX WITH SYNTACTIC SUGAR
//aka LINQ query comprehension syntax

/*
 
 OG without sugar 
var query = names 
    .Where(name => name.length > 4
    .OrderBy(name => name.length
    .ThenBy(name => name);

With sugar:

var query = from name in names 
    where name.length > 4 
    orderby name.length, name 
    select name;
 
 
 the select keyword is always required for LINQ query comprehension syntax. the select 
extension method is optional when using extension methods and lambda expression because if 
you do not call select, then the whole item is implicitly selected
 
 
 */