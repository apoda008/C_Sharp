namespace Packt.Shared;
public class Headset(string manufacturer, string productName)
{
    public string Manufacturer { get; set; } = manufacturer;
    public string ProductName { get; set; } = productName;

    // default para-less constructor calls the primary constructor
    public Headset() : this("Micrsoft", "HoloLens") { }
}