using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

/// <summary>
/// Summary description for Stemming
/// </summary>
public class Stemming
{
    public string Naam;
    public int aantalStemmers;
    public List<Project> Projecten;
    public List<Stemmer> Stemmers;
    public Boolean Actief;

    public void nieuwProject(Project project)
    {
        Projecten.Add(project);
    }

    public void StopStemming()
    {
        //Alleen voor admin toegankelijk maken
        this.Actief = false;
    }

    public void ZieStemmen()
    {
        //foreach(Project in Projecten)
        //{
        //    return Project.aantalStemmen;
        //}
    }

    public Stemming(string naam, int aantal)
    {
        // Alleen aan te maken door een admin
        this.Naam = naam;
        this.aantalStemmers = aantal;
        for (int i = 1; i <= aantal; i++)
        {
            RandomString rString = new RandomString();
            //Stemmer i = new Stemmer(rString)
                //genereer de random code met RNGCryptoServiceProvider
        }
    }
}