using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Project
/// </summary>
public class Project
{
    public string UniekeCode;
    public string Naam;
    public int AantalStemmen
    {
        get
        {
            return this.AantalStemmen;
        }
        set
        {
            this.AantalStemmen++;
        }
    }
    public Project()
    {
        //
        // TODO: Add constructor logic here
        //
    }


}