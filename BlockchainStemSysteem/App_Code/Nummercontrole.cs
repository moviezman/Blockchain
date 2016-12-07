using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Hier een functie die een nummer binnenkrijgt en daarvan afweegt of dit nummer valide is
/// </summary>
public class Nummercontrole
{
    public bool Nummercheck(string nummer)
    {
        string NummerStart = nummer.Substring(0, 3);
        //NLNummers beginnen met 061, 062, 063, 064, 065 of 068 en hebben 10 nummers
        if (NummerStart == "061" || NummerStart == "062" || NummerStart == "063" || NummerStart == "064" || NummerStart == "065" || NummerStart == "068")
        {
            if (nummer.Length == 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}