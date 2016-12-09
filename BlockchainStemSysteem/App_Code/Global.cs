using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Global
/// </summary>
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