using System.Diagnostics.CodeAnalysis; //to use [StringSyntax] attribute

partial class Program 
{
    [StringSyntax(StringSyntaxAttribute.Regex)]
    private const string DigitsOnlyText = @"^\d+$";

    [StringSyntax(StringSyntaxAttribute.Regex)]
    private const string CommaSeperatorText = "(?:^|,)(?=[^\"]|(\")?)\"?((?(1)[^\"]*|[^,\"]*))\"?(?=,|$)";

    [StringSyntax(StringSyntaxAttribute.DateTimeFormat)]
    private const string fullDateTime = "dddd, d MMMM yyyy";

    /*
     The [StringSyntax] attribute is a feature introduced in .NET 7. It depends on your code editor whether 
    it is recognized. The .NET BCL has more than 350 parameters, properties, and fields that are now 
    decorated with this attribute
     */
}