
using System.Runtime.CompilerServices;

namespace Packt.Shared;

public class Person : IComparable<Person?>
{
    #region Properties 
    public string? Name { get; set; }
    public DateTimeOffset Born { get; set; }
    public List<Person> Children = new();

    #endregion

    #region Methods
    public void WriteToConsole()
    {
        WriteLine($"{Name} was born on a {Born:dddd}");
    }

    public void WriteChildrenToConsole()
    {
        string term = Children.Count == 1 ? "child" : "children";
        WriteLine($"{Name} has {Children.Count} {term}");
    }

    #endregion

    //allow multiple spouses to be stored for a person 
    public List<Person> Spouses = new();

    //a read only prop to show if a person is married to anyone 
    public bool Married => Spouses.Count > 0;

    //static method to marry two people 
    public static void Marry(Person p1, Person p2) 
    {
        ArgumentNullException.ThrowIfNull(p1);
        ArgumentNullException.ThrowIfNull(p2);

        if (p1.Spouses.Contains(p2) || p2.Spouses.Contains(p1)) 
        {
            throw new ArgumentException(
                   string.Format("{0} is already married to {1}.",
                   arg0: p1.Name, arg1: p2.Name)
                );
        }

        p1.Spouses.Add(p2);
        p2.Spouses.Add(p1);
    }

    //Instance mthod to marry another person 
    public void Marry(Person partner) 
    {
        Marry(this, partner); //"this" is the current Person
    }

    public void OutputSpouses() 
    {
        if (Married)
        {
            string term = Spouses.Count == 1 ? "person" : "people";

            WriteLine($"{Name} is married to {Spouses.Count} {term}:");

            foreach (Person spouse in Spouses)
            {
                WriteLine($"  {spouse.Name}");
            }
        }
        else 
        {
            WriteLine($"{Name} is a singlton");
        }
    }

    //<summary>
    //Static method to 'multiply' aka procreate and have a child together
    //</summary>
    //<param name ="p1" >Parent 1</param>
    //<param name="p2" >Parent 2</param> 
    //<returns>A Person object that is the child of Parent 1 and Parent 2 </returns>
    //<exception cref="ArgumentNullException">If p1 or p2 are null.</exception>
    //<exception cref="Arumnet Exception"> If p1 and p2 are not married</exception>

    public static Person Procreate(Person p1, Person p2) 
    {
        ArgumentNullException.ThrowIfNull(p1);
        ArgumentNullException.ThrowIfNull(p2);

        if (!p1.Spouses.Contains(p2) && !p2.Spouses.Contains(p1)) 
        {
            throw new ArgumentException(string.Format("{0} must be married to {1} to procreate",
                arg0: p1.Name, arg1: p2.Name));
        }

        Person baby = new()
        {
            Name = $"Baby of {p1.Name} and {p2.Name}",
            Born = DateTimeOffset.Now,
        };

        p1.Children.Add(baby);
        p2.Children.Add(baby);
        return baby;
    }

    //Instance method to 'multiply'
    public Person ProcreateWith(Person partner) 
    {
        return Procreate(this, partner);
    }

    /*======NOTE=========
    Classes are reference types, meaning a reference to the baby obj stored in mem is added, not a clone of the baby obj
    *
    It is a convention to use a different method name for related static and instance methods, for example, Compare(x,y) for the static 
    method name and x.compareTo(y) for the insance method name
    *
    Good practice: a method that creates a new obj or modifies and existing obj should return a reference to that obj so that 
    the caller can access the results.
    */

    #region operators
    //define the + operator to marry 
    public static bool operator +(Person p1, Person p2) 
    {
        Marry(p1, p2);

        //confirm they are both now married 
        return p1.Married && p2.Married;
    }

    //The return type for an operator does not need to match the types passed parameters to the operator
    //but the return cannot be 'void'

    //define the * operator to 'multiply'
    public static Person operator *(Person p1, Person p2) 
    { 
        //Return a reference to the baby that results from multipling 
        return Procreate(p1, p2);
    }
    #endregion

    #region
    //delegate field to define the event 
    public event EventHandler? Shout; //Null initially 

    //data filed related to the event 
    public int AngerLevel;

    //method to trigger the even in certain conditions 
    public void Poke() 
    { 
        AngerLevel++;
        if (AngerLevel < 3) return;

        //if something is listening to the event 
        if (Shout is not null) 
        { 
            Shout(this, EventArgs.Empty);
        }
    }

  
    #endregion
    /*=======NOTE+========
     The 'in' keyword specifies that the type para T is contravariant which 
    means that you can use a less derived type than that specified. for example, 
    if employee derives from person then both can be compared with each other.  
     */  
    
    public int CompareTo(Person? other)
    {
        int position;

        if (other is not null)
        {
            if ((Name is not null) && (other.Name is not null))
            {
                //if both Name values are not null thenuse the string implementation of CompareTO
                position = Name.CompareTo(other.Name);
            }
            else if ((Name is not null) && (other.Name is null))
            {
                position = -1; //this person precedes the other person
            }
            else if ((Name is null) && (other.Name is not null))
            {
                position = 1; //this person follows other person
            }
            else
            {
                position = 0; //this and other are at the same position 
            }
        }
        else if (other is null) 
        {
            position = -1; //this person precedes other Person
        }
        else
        {
            position = 0; // this and other are at same position
        }
        return position;
    }
    //GOOD PRACTICE: if you want to sort an array or collection of instances of your type then implement
    //the Icomparable interface
    #region Override methods 

    public override string ToString()
    {
        return $"{Name} is a {base.ToString()}.";
    }
    /*GOOD PRATICE
    Many real world apis require the properties that you define in your classes to marked as virtual so 
    that they can be overriden. Carefully decide which of your method and property members should marked 
    as virtual
     */

    #endregion
    public void TimeTravel(DateTime when)
    {
        if (when <= Born)
        {
            throw new PersonException("If you travel back in time to a date earlier than your own birth, then the universe will explode!");
        }
        else
        {
            WriteLine($"Welcome to {when:yyyy}!");
        }
    }
} //end

