﻿//Null namespace to merge with autogenerated program 
public partial class Program
{
    private static void SectionTitle(string title)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine($"*** {title} ***");
        ForegroundColor = previousColor;


    }

    private static void OutputFileInfo(string path)
    {
        WriteLine("*** File Info ***");
        WriteLine($"File:  {GetFileName(path)}");
        WriteLine($"Path:  {GetDirectoryName(path)}");
        WriteLine($"Size:  {new FileInfo(path).Length:N0} bytes");
        WriteLine("/--------------------");
        WriteLine(File.ReadAllText(path));
        WriteLine("/--------------------");

    }
}