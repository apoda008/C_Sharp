using System.Globalization; //to use cultureinfo 

partial class Program 
{
    private static void ConfigureConsole(string culture = "en-US", bool useComputerCulture = false) 
    { 
        //to enable Unicaode Characters 
        OutputEncoding = System.Text.Encoding.UTF8;

        if (!useComputerCulture) 
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
        }

        WriteLine($"CurrentCulture: {CultureInfo.CurrentCulture.DisplayName}");
    }

    private static void SectionTitle(string title) 
    { 
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine($"***  {title} ***");
        ForegroundColor = previousColor;
    }
}