namespace Packt.Shared;

public class Employee : Person 
{ 
    //Extending class to add functionality 
    public string? EmployeeCode { get; set; }
    public DateOnly HireDate { get; set; }

    //hiding members
    public new void WriteToConsole() 
    {
        WriteLine(format: 
            "{0} was born {1:dd/MM/yy} and hired on {2:dd/MM/yy}.",
            arg0: Name, arg1: Born, arg2: HireDate);
    }

    public override string ToString()
    {
        return $"{Name}'s code is {EmployeeCode}";
    }
    //SHould use virtual and override rather than new to change the implementation of
    //an inherited method whenever possible. 
}