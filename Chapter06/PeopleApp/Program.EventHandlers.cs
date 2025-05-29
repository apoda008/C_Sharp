using Packt.Shared;

//No namespace delcaration so this extends the program class 
//in the null namespace 

partial class Program 
{
    //A method to handle the Shout event received by the harry object 
    private static void Harry_Shout(object? sender, EventArgs e) 
    {
        //if no sender, then do nothing
        if (sender is null) return;

        //if sender is not a Person, then do nothing 
        if (sender is not Person p) return;

        WriteLine($"{p.Name} is this angry: {p.AngerLevel}");
    }
    /*
     Microsoft's convention for method names that hand;e events is 
    ObjectName_EventName. In this project, sender will always be a 
    person instance, so the null checks are not necessary and the 
    event handler could be much simpler with just the WriteLine 
    statement. However it is important to know that these types of 
    null checks make your code more robust in cases of event misuse
     */

    //another method to handle the event received by the harry object 
    private static void Harry_Shout_2(object? sender, EventArgs e) 
    {
        WriteLine("Stop it");
    }


}