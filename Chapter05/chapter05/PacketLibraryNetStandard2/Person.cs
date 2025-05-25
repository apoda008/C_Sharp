namespace Packt.Shared;

public class Person : Object
{
    #region Fields: Data or Stat for this person 

    public string? Name; //? means it can be null
    public DateTimeOffset Born;

    #endregion

    public WondersOfTheAncientWorld FavoriteAncientWonder;

    public WondersOfTheAncientWorld BucketList;

    public List<Person> Children = new();
}