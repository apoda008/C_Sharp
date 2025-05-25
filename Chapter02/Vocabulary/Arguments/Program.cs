// See https://aka.ms/new-console-template for more information
WriteLine($"There are {args.Length} argurments");
foreach (string arg in args)
{
    WriteLine(arg);
}
if (args.Length < 3)
{
    WriteLine("You must specifiy two colors and cursor size, e.g.");
    WriteLine("dotnet run red yellow 50");
    return; //stop running
}

ForegroundColor = (ConsoleColor)Enum.Parse(
    enumType: typeof( ConsoleColor ),
    value: args[0], ignoreCase: true );

BackgroundColor = (ConsoleColor)Enum.Parse(
    enumType: typeof(ConsoleColor),
    value: args[1], ignoreCase: true);
try
{
    CursorSize = int.Parse(args[2]);
}
catch (PlatformNotSupportedException)
{
    WriteLine("The current platform does not support changin the size of the cursor");
}



#if NET7_0_ANDRIOD
    //compile statements that only work in andriod
#elif NET7_IOS
    //compile statments the only work in iOS
#else
    //compile statments that work everywhere else
#endif
