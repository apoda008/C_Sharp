using System.Text.RegularExpressions; // to use Regex

namespace Packt.Shared;

public static class StringExtensions
{
    public static bool isValidEmail(this string input) 
    {
        //use a simple regular expression to check
        //that the input string is a valid email

        return Regex.IsMatch(input, @"[a-zA-Z0-9\.-_]+@[a-zA-Z0-9\.-_]+");
    }
}
/*these two changes (static in the class and this in the method) tell the 
 * compiler that it should treat the method as one that extends the string*/