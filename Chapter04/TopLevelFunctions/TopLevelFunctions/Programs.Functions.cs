using static System.Console;

partial class Program
{
    static void WhatsMyNameSpace()
    {
        WriteLine("Namespace of Program class: {0}",
            arg0: typeof(Program).Namespace ?? "null");
    
    }
}