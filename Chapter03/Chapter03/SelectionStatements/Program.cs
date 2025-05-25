// See https://aka.ms/new-console-template for more information
object o = 3;
int j = 4;

if (o is int i)
{
    WriteLine($"{i} x {j} = { i * j }");

}
else 
{
    WriteLine("o is not an int so it cannot multiply");
}

//Inclusive lower bound but exclusive upper bound. 
int number = Random.Shared.Next(minValue: 1, maxValue: 7);
WriteLine($"My randon number is {number}");

switch (number)
{
    case 1:
        WriteLine("One");
        break;
    case 2:
        WriteLine("Two");
        goto case 1;
    case 3: //multiple case section
    case 4:
        WriteLine("Three or Four");
        goto case 1;
    case 5:
        goto A_label;
    default:
        WriteLine("Default");
        break;
} //end of switch
WriteLine("After end of switch");
A_label:
WriteLine($"After A_label");

var animals = new Animal?[]
    {
        new Cat {Name = "Karen", Born = new(year: 2022, month: 8, day: 23), Legs = 4, IsDomestic = true},
        null,
        new Cat {Name = "Mufasa", Born = new(year: 1994, month: 6, day: 12) },
        new Spider {Name = "sid vicious", Born = DateTime.Today, IsPoisonous = true},
        new Spider {Name = "Captain Furry", Born= DateTime.Today, }
    };

foreach (Animal? animal in animals)
{
    string message;

    message = animal switch
    {
        Cat fourLeggedCat when fourLeggedCat.Legs == 4
            => $"the cat name {fourLeggedCat.Name}",
        Cat wildCat when wildCat.IsDomestic == false    
            => $"the non-domestic cat is named {wildCat.Name}",
        Cat cat 
            => $"The cat is named {cat.Name}",
        Spider spider when spider.IsPoisonous
            => $"The {spider.Name} is posionous",
        null
            => "the animal is null",
        _
            =>$"{animal.Name} is a {animal.GetType().Name} "
    };
    WriteLine($"Switch expession: {message}");




    switch (animal)
    {
        case Cat fourLeggedCat when fourLeggedCat.Legs == 4:
            message = $"The cat name {fourLeggedCat.Name} has four legs";
            break;
        case Cat wildCat when wildCat.IsDomestic == false:
            message = $"The non-domestic cat is named {wildCat.Name}.";
            break;
        case Cat cat:
            message = $"The cat is name {cat.Name}";
            break;
        default: 
            message = $"{animal.Name} is a {animal.GetType().Name}.";
            break;
        case Spider spider when spider.IsPoisonous:
            message = $"The {spider.Name} spide is poisonous. Run!";
            break;
        case null:
            message = "the animal is null.";
            break;
    }
    WriteLine($"switch statement: {message}");
}

//switch expresions

string[,] grid1 =
    {
        {"Alpha", "Beta", "Gamma", "Delta" },
        {"Annie", "Ben", "Charlie", "Doug" },
        {"Aardvark", "Bear", "Cat", "Dog" }
    };

for (int row = 0; row <= grid1.GetUpperBound(0); row++)
{
    for (int col = 0; col <= grid1.GetUpperBound(1); col++)
    { 
    WriteLine($"Row {row}, Column {col}: {grid1[row, col]}");
    }
}