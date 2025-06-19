using System.Globalization; //to use cultureinfo
using System.Reflection; //to use AssemblyName
using System.Reflection.Emit; //to use AssemblyBuilder


WriteLine("This is an ahead-of-time (AOT) compiled console application.");
WriteLine("Current culture {0}", CultureInfo.CurrentCulture.DisplayName);
WriteLine("OS version: {0}", Environment.OSVersion);

//AssemblyBuilder ab = AssemblyBuilder.DefineDynamicAssembly( new AssemblyName("MyAssembly"), AssemblyBuilderAccess.Run);

Write("Press any key to exit...");
ReadKey(intercept: true); //do not out the key that was pressed


/*Currently when compiling it is not AOT compiled since it has not yet been published*/

/*
 you could decompile someones elses assemblies for non-learning purposes, like copying their code for use in your own 
prod library or app, but remember that you are viewing their intellectual property and you should not use it without 
their permission.
 */