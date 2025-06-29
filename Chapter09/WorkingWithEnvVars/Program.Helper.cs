﻿partial class Program
{
    private static void SectionTitle(string title)
    {
        WriteLine();
        ConsoleColor previousColor = ForegroundColor;
        //Usa a color that stands out on your system 
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine($"*** {title} ***");
        ForegroundColor = previousColor;
    }

    private static void DictionaryToTable(IDictionary dictionary) 
    {
        Table table = new();
        table.AddColumn("Key");
        table.AddColumn("Value");

        foreach (string key in dictionary.Keys) 
        { 
            table.AddRow(key, dictionary[key]!.ToString()!);
        }

        AnsiConsole.Write(table);
    }
}