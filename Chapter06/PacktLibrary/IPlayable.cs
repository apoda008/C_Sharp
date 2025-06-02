namespace Packt.Shared;

public interface IPlayable 
{
    void Play();
    void Pause();
    void Stop() //default interface implementation 
    {
        WriteLine("Default implementation of Stop.");
    }
}

//an interface declares what a class should have 
//an inheriting class defines "how it should do it"
//new C# says fuck that 