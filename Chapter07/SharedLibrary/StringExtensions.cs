
/*
 If you need to create types that use new fearures in .NET8, as well as types that only use 
.MET Standard 2.0 features, then you can create two seperate class libraries: one targeting 
.NET Standard 2.0 and the other targeting .NET 8.0. Or multi-target the same class library
 */
using System.Text.RegularExpressions; //to use Regex

namespace Packt.Shared;

public static class  StringExtensions
{
    public static bool IsValidXmlTag(this string input) 
    {
        return Regex.IsMatch(input,
            @"^<([a-z]+)([^<]*(?:>(.*)<\/\1>|\s+\/>)$");
    }

    public static bool IsValidPassword(this string input) 
    {
        //Min of eight valid characters
        return Regex.IsMatch(input, "^[a-zA-Z0-9_]{8,}$");
    }

    public static bool IsValidHex(this string input) 
    {
        //three or six valid hex number characters
        return Regex.IsMatch(input, "^#?([a-fA-F0-9]{3}|[a-fa-F0-9]{6})$");
    }
}