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
    public List<Project> Projecten = new List<Project>();
    public List<Stemmer> Stemmers = new List<Stemmer>();
    public Boolean Actief;

    public void nieuwProject(Project project)
    {
        Projecten.Add(project);
    }

    public void nieuweStemmer(Stemmer stemmer)
    {
        Stemmers.Add(stemmer);
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

    public Stemming(string naam, int aantalS)
    {
        // Alleen aan te maken door een admin
        
        this.Naam = naam;
        this.aantalStemmers = aantalS;
        for (int i = 1; i <= aantalS; i++)
        {
            Stemmer Test = new Stemmer(i.ToString());
            nieuweStemmer(Test);
            //RandomString rString = new RandomString();
            //Stemmer i = new Stemmer(rString)
            //genereer de random code met RNGCryptoServiceProvider
        }
        Project project1 = new Project("1", "Blockchain");
        Project project2 = new Project("2", "Ahrma");
        this.Projecten.Add(project1);
        this.Projecten.Add(project2);
    }
}