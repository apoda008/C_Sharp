namespace Packt.Shared;

public class PersonException : Exception 
{ 
    public PersonException() : base() { }

    public PersonException(string message) : base(message) { }

    public PersonException(string message, Exception innerException) : base(message, innerException) { }

    /*
     Unlike ordinary methods, constructors are not inherited, so we must explicitly declare and explicitly 
    call the base constructor implementations in System. Exception (or whichever class you derived from) 
    to make them available to programmers who might want to use those constructors with our custom exception
     */
}