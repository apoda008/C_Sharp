
WriteLine("I can run everywhere!");
WriteLine($"OS Version is {Environment.OSVersion}");

if (OperatingSystem.IsMacOS())
{
    WriteLine("I am macOS");
}
else if (OperatingSystem.IsWindowsVersionAtLeast(major: 10, build: 22000))
{ 
    WriteLine("I am Windows 11 or later");
}

else if (OperatingSystem.IsWindowsVersionAtLeast(major: 10))
{
    WriteLine("I am Windows 10");
}
else
{
    WriteLine("I am an unknown OS");
}

WriteLine("Press any key to stop me...");
ReadKey(intercept: true);

/*
 There are two elements that you can use to specify runtime identifiers. Use 
<RuntimeIdentifier> if you only need to specify one. Use <RuntimeIdentifiers> 
if you need to specify multiple, as we did in the preceding example. If you 
use the wrong one, then the compiler will give an error and it can be 
difficult to understand why with only one character difference 

.Net MAUI projects are not supported for LInux, The team has said they have 
left that work to the open source community. if you need to create a truly 
cross-platform graphical app, then take a look at Avalonia

NATIVE AHEAD-OF-TIME COMPILATION (AOT)
self containted - they can run on anything that does not have the .NET runtime 
                    installed

Ahead-of-time compiled to native code, meaning a faster start up time and a 
                    potentially smaller mem footprint
 
Limitations; 
    No dynamic loading of assemblies
    No runtime code generation
    It requires trimming, which has its own limitations
    They must be self contained, so they must embed any libraries they call 
            which increases size
 
(Reflection) Devs mst annotate their types with [DynamicallyAccessedMembers] to a member 
    that is only dynamically accessed via reflection and should therefore be 
    left untrimmed

WARNING - Cross-platform native AOT publishing is not supported. THis means that 
you must run the publish on the OS that you will deploy to. For example you 
cannot publish native AOT proj on linux to later run on WIndows and vice versa
 */


