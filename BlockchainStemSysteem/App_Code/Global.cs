using System.Collections.Generic;

//Hier worden projecten toegevoegd tijdens het aanmaken van een stemming in de genereerpagina
public static class Global
{
    public static List<string> Projecten;

    static Global()
    {
        Projecten = new List<string>();
    }

    public static void Toevoegen(string value)
    {
        if (!Projecten.Contains(value))
        {
            Projecten.Add(value);
        }
    }

    public static string Return()
    {
        string results = string.Join(", ", Projecten.ToArray());
        return results;
    }

    public static void Clear()
    {
        Projecten.Clear();
    }
}