using System.Text.RegularExpressions;

partial class Program 
{
    [GeneratedRegex(DigitsOnlyText, RegexOptions.IgnoreCase)]
    private static partial Regex DigitsOnly();

    [GeneratedRegex(CommaSeperatorText, RegexOptions.IgnoreCase)]
    private static partial Regex CommaSeperator();

}