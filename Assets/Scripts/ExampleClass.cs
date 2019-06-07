// Klasse
public class Fahrzeug
{
    // Variable
    string Hersteller;

    // Property
    public string Type { get; protected set; }

    public Fahrzeug(string hersteller, string type)
    {
        Hersteller = hersteller;
        Type = type;
    }

    // Methode
    public string Beschreibe()
    {
        return string.Format(
            "Dieses Auto vom Typ {0} ist vom Hersteller {1}", 
            Type, Hersteller);
    }
}
