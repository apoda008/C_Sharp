using System.Globalization;

OutputEncoding = System.Text.Encoding.UTF8;
//Strings i guess? 
string city = "London";
WriteLine($"{city} is {city.Length} characters long.");

WriteLine($"First char is {city[0]} and fourth is {city[3]}.");

string cities = "Paris,Tehran,Chennai,Sydney,New York,Medellin";

string[] citiesArray = cities.Split(',');
WriteLine($"There are {citiesArray.Length} cities in the list.");

foreach (string c in citiesArray)
{
    WriteLine($"City: {c}");
}
//part of string 
string fullName = "ALan Shire";

int indexOfTheSpace = fullName.IndexOf(' ');

string firstName = fullName.Substring(startIndex: 0, indexOfTheSpace + 1);

string lastName = fullName.Substring(startIndex: indexOfTheSpace + 1);

WriteLine($"Original: {fullName}");
WriteLine($"Swapped: {lastName}, {firstName}");

string company = "Microsoft";
WriteLine($"Text: {company}");
WriteLine("Starts with M: {0}, contains an N:{1}",
    arg0: company.StartsWith("M"),
    arg1: company.Contains("N"));

//COMPARING
WriteLine();
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

string text1 = "Mark";
string text2 = "MARK";

WriteLine($"text1: {text1}, text2: {text2}");

WriteLine("Compare: {0}.", string.Compare(text1 , text2));

WriteLine("Compare (ingoreCase): {0}.",
    string.Compare(text1, text2, ignoreCase: true));

WriteLine("Compare (InvariantCultureIgnoreCase): {0}.",
    string.Compare(text1, text2, StringComparison.InvariantCultureIgnoreCase));

/*
 Some of these methods are static methods. This means that the method can only 
be called from the type, not from the variable instance. 
 */

string recombined = string.Join(" => ", citiesArray);
WriteLine(recombined);

string fruit = "apples";
decimal price = .39M;
DateTime when = DateTime.Today;

WriteLine($"Interpolated: {fruit} cost {price:C} on {when:dddd}.");
WriteLine(string.Format("string.format: {0} cose {1:C} on {2:dddd}.",
    fruit, price, when));

/*
 Use StringBuilder type for large concatenations 
 */

