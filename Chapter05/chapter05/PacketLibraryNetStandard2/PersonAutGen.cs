using System;
using System.Collections.Generic;
using System.Text;


//namespace PacketLibraryNetStandard2
namespace Packt.Shared;
//This file simulates an auto-generated class
public partial class Person 
{
    #region Properties: Methods to get/set data or state
    //a readonly prop defined using c# 1 to 5 syntax
    public string Origin
    {
        get 
        {
            return string.Format("{0} was born on {1}.",
                arg0: Name, arg1: HomePlanet);
        }
    }

    //Two readonly prop defined using C#6 or later syntax
    //Lambda Expression body syntax

    public string Greeting => $"{Name} says Hello!";

    public int Age => DateTime.Today.Year - Born.Year;

    //A private backing field to store the prop value 
    private string? _favoritePrimaryColor;

    //A public prop to read and write to the field 
    public string? FavoritePrimaryColor
    {
        get 
        { 
            return _favoritePrimaryColor;
        }
        set 
        {
            switch (value?.ToLower()) 
            {
                case "red":
                case "green":
                case "blue":
                    _favoritePrimaryColor = value;
                    break;
                default:
                    throw new ArgumentException(
                        $"{value} is not a primary color. " + "Choose from; red, green, or blue"
                        );
            }
        }
    }

    private WondersOfTheAncientWorld _favoriteAncientWonder;

    public WondersOfTheAncientWorld FavoriteAncientWonder 
    { 
        get {  return _favoriteAncientWonder; }
        set 
        { 
            string wonderName = value.ToString();
            
            if (wonderName.Contains(',')) 
                {
                throw new ArgumentException(
                    message: "Favorite ancient wonder can only have a single enum value",
                    paramName: nameof(FavoriteAncientWonder));
                };

            if (!Enum.IsDefined(typeof(WondersOfTheAncientWorld), value)) 
                {
                throw new ArgumentException(
                    $"{value} is not a member of the WonderOfAncient enum.",
                    paramName: nameof(FavoriteAncientWonder) );
                }
            _favoriteAncientWonder = value;
            }
        }

    #endregion

    //Indexers
    #region Indexers: Properties that use array syntax to access them 
    public Person this[int index]
    {
        get 
        { 
            return Children[index]; //Pass on to the Lizst<T> indexer
        }
        set 
        { 
            Children[index] = value;
        }
    
    }
    
    //A read-only string indexer
    public Person this[string name]
    {
        get 
        { 
            return Children.Find(p => p.Name == name);
        }
    }

    #endregion
}
