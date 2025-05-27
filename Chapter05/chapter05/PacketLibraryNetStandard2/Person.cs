namespace Packt.Shared;

public class Person : object
{
    #region Fields: Data or Stat for this person 

    public string? Name; //? means it can be null
    public DateTimeOffset Born;

    #endregion

    #region read-only fields: values that can be set at runtime.
    
    public readonly string HomePlanet = "Earth";
    public readonly DateTime Instantiated;

    #endregion

    public WondersOfTheAncientWorld FavoriteAncientWonder;

    public WondersOfTheAncientWorld BucketList;

    public List<Person> Children = new();

    #region Constructors: Called when using new to instantiate a type

    public Person() {
        //Constructors can set default values for fields 
        //including any read-only fields like Instantiate
        Name = "Unknown";
        Instantiated = DateTime.Now;
    }

    #endregion
    //defining multiple constructors
    public Person(string initialName, string homePlanet) { 
        Name = initialName;
        HomePlanet = homePlanet;
        Instantiated = DateTime.Now;
    }

}
