using System.Data;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.Marshalling;

partial class Program
{
    static void TimesTable(byte number, byte size = 12)
    {
        WriteLine($"This is the {number} times table with {size} rows:");
        WriteLine();

        for (int row = 1; row <= size;  row++)
            {
            WriteLine($"{row} x {number} = {row * number}");
            }
        WriteLine();
    }
    //lambdas 
    static int FibImperative(uint term)
    {
        if (term == 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        else if (term == 1)
        {
            return 0;
        }
        else if (term == 2)
        {
            return 1;
        }
        else
        {
            return FibImperative(term - 1) + FibImperative(term - 2);
        }

    }
    static string CardinalToOrdinal(uint number)
    {
        uint lasttwodigits = number % 100;

        switch (lasttwodigits) 
        {
            case 11: //special cases
            case 12:
            case 13:
                return $"{number:N0}th";
            default:
                uint lastDigit = number % 10;

                string suffix = lastDigit switch
                {
                    1 => "st",
                    2 => "nd",
                    3 => "rd",
                    _ => "th"
                };

                return $"{number:N0}{suffix}";
        }
    }


    static void RunFibImperative()
    {
        for (uint i = 1; i <= 30; i++)
        {
            WriteLine("The {0} term of the Fibonacci sequence is {1:N0}.",
            arg0: CardinalToOrdinal(i),
            arg1: FibImperative(term: i));
        }
    }
}

