string name = "Samantha Jones";

//Getting the lengths of the first and last names. 

int lengthOfFirstName = name.IndexOf(' ');
int lengthOfLastName = name.Length - lengthOfFirstName - 1;

//using substring 
string firstName = name.Substring(
    startIndex: 0,
    length: lengthOfFirstName);

string lastName = name.Substring(
    startIndex: name.Length - lengthOfLastName,
    length: lengthOfLastName);

WriteLine($"First: {firstName}, Last: {lastName}");

//using spans 
ReadOnlySpan<char> nameAsSpan = name.AsSpan();
ReadOnlySpan<char> firstNameSpan = nameAsSpan[0..lengthOfFirstName];
ReadOnlySpan<char> lastNameSpan = nameAsSpan[^lengthOfLastName..];

WriteLine($"First: {firstNameSpan}, last: {lastNameSpan}"); 