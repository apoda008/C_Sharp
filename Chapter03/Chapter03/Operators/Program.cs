// See https://aka.ms/new-console-template for more information

//var resultofOperation = firstOperand operator secondOperand

//unary operators 
//var resilofopsAfter = onlyOperand operator
//var resultofopsBefore = operator onlyOperand

//ternary operators works on three operands
//var resultofops = 1stoperand firstOperator 2ndoperand secondOperator 3rdOperand

//Syntax of conditional operator 
//var result = boolean_expression ? value_if_true : value_if_false

//exampl
using System.Security.Cryptography.X509Certificates;

internal class Program
{
    private static void Main(string[] args)
    {
        int x = 0;
        string result2 = x > 3 ? "Greater than 3" : "Less than or equal to 3";

        //above is the same as 
        string result;

        if (x < 3)
        {
            result = "Greater than 3";
        }
        else
        {
            result = "less than 3";
        }

        #region Exploring unary operators

        int a = 3;
        int b = ++a;

        WriteLine($"a is {a}, b is {b}");


        int c = 3;
        int d = a++;

        WriteLine($"a is {c}, b is {d}");

        int e = 11;
        int f = 3;

        WriteLine($"e is {e}, f is {f}");

        //Related operators to the assignment operators are the null-coalescing operators. 
        //Sometimes you want to either assign a variable to a result or if the varuable 
        //is null, then assign an alternative value. 

        string? authorName = ReadLine();

        //the maxlength var will be the length of authorName if it is not null, or 30
        //if authorName is null.

        int maxLength = authorName?.Length ?? 30;

        //the authorName var will be "unknown" if authorName was null 
        authorName ??= "unknown";

        bool p = true;
        bool q = false;

        WriteLine($"AND  |p            |q   ");
        WriteLine($"p    | {p & p,-5} | {p & q,-5}");
        WriteLine($"p    | {q & q,-5}  | {q & q,-5}");
        WriteLine();
        WriteLine($"OR   |p            |q   ");
        WriteLine($"p    | {p | p,-5}  | {p | q,-5}");
        WriteLine($"p    | {q | q,-5}  | {q | q,-5}");
        WriteLine();
        WriteLine($"XOR  |p            |q   ");
        WriteLine($"p    | {p ^ p,-5}  | {p ^ q,-5}");
        WriteLine($"p    | {q ^ q,-5}  | {q ^ q,-5}");
        WriteLine();
        // -5 means left alighn within a fine width column

        #endregion

        #region conditional logical operators 
        WriteLine();
        WriteLine($"p && Dostuff() = {p && DoStuff()}");
        WriteLine($"q && Dostuff() = {q && DoStuff()}");



        static bool DoStuff()
        {
            WriteLine("I AM doing some stuff");
            return true;

        }
    }
}

#region Exploring unary operators

#endregion
#region conditional logical operators 

#endregion

#region bitwise and binary shift


#endregion

