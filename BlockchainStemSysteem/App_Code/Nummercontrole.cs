using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Hier een functie die een nummer binnenkrijgt en daarvan afweegt of dit nummer valide is
/// </summary>
public class Nummercontrole
{
    public bool Nummercheck(int nummer)
    {
        if (nummer == 5) {
            return true;
        }
        else
        {
            return false;
        }
    }
}