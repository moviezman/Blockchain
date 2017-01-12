using System.Data;
using System.Data.SqlClient;
using System.Linq;

//Haalt de resultaten op voor zowel de gebruiker als de beheerder
public static class Uitslagen
{
    static bool GebruikBlockchain = false;
    //Haalt de resultaten op voor de gebruiker (toont alleen de winnaar)
    public static string UitslagStemming(string Stemming)
    {
        if (GebruikBlockchain) { Blocks.Decodeer(Stemming); }

        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);        
        string Uitslag = "<h2>" + "De winnaar is</br>" + "</h2>";
        
        //Checkt hoeveel projecten de meeste stemmen hebben
        SqlCommand CheckWinnaar = new SqlCommand("SELECT COUNT(*) FROM (SELECT GestemdOp FROM UC WHERE StemmingsNaam = '" + Stemming + "' AND GestemdOp = (SELECT Max(GestemdOp) FROM UC)) x", sqlConnection);
        //Haalt de winnaar op
        SqlCommand winnaar = new SqlCommand("SELECT GestemdOp, Count(GestemdOp)AS 'AantalGestemd' FROM UC WHERE StemmingsNaam = '" + Stemming + "' GROUP BY GestemdOp ORDER BY AantalGestemd DESC;", sqlConnection);
        sqlConnection.Open();

        //Selecteert het project met de meeste stemmen
        if (GebruikBlockchain)
        {
            //Winnaar ophalen met blockchain werkt nog niet!
            //string Winnaar = (from x in Blocks.GestemdOp select x).Count().Max();
            //Winnaar ophalen met LINQ werkt nog niet goed
            //var maxValue = Blocks.GestemdOp.Max(x => x);
            //var Winnaar = Blocks.GestemdOp.Select(x => x == maxValue);
            //string Winnaar = ((from x in Blocks.GestemdOp select x.Count())).Max();
            //Uitslag += Winnaar;

            //Haalt de winnaar op zonder blockchain (omdat met blockchain nog niet werkt):
            Uitslag += "<h4>" + winnaar.ExecuteScalar() + "</h4>";
        }
        else
        {
            Uitslag += "<h4>" + winnaar.ExecuteScalar() + "</h4>";
        }
        sqlConnection.Close();
        //Returnt een string met daarin de winnaar
        return Uitslag;
    }
    
    //Haalt de resultaten op voor de beheerder (toont zowel de winnaar als het aantal stemmen per team)
    public static string UitslagStemmingBeheerder(string Stemming)
    {
        if (GebruikBlockchain) { Blocks.Decodeer(Stemming); }

        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);

        string Uitslag = "<h1>Uitslagen van " + Stemming + "</h1></br> ";
        
        //Haalt de winnaar op
        SqlCommand winnaar = new SqlCommand("SELECT GestemdOp, Count(GestemdOp)AS 'AantalGestemd' FROM UC WHERE StemmingsNaam = '" + Stemming + "' GROUP BY GestemdOp ORDER BY AantalGestemd DESC;", sqlConnection);

        sqlConnection.Open();

        //Selecteert het project met de meeste stemmen
        if (GebruikBlockchain)
        {
            ////Winnaar ophalen met LINQ werkt nog niet goed
            //var maxValue = Blocks.GestemdOp.Max(x => x);
            //var Winnaar = Blocks.GestemdOp.Select(x => x == maxValue);
            ////string Winnaar = ((from x in Blocks.GestemdOp select x.Count())).Max();
            //Console.WriteLine(Winnaar);
            //Uitslag += "<h4>Winnaar:<br />" + Winnaar + "</h4><br />";

            //Haalt de winnaar op zonder blockchain (omdat met blockchain nog niet werkt):
            Uitslag += "<h4>Winnaar:<br />" + winnaar.ExecuteScalar() + "</h4><br />";
        }
        else
        {
            Uitslag += "<h4>Winnaar:<br />" + winnaar.ExecuteScalar() + "</h4><br />";
        }

        //Haalt op hoeveel unieke codes er bij deze stemming zijn aangemaakt
        SqlCommand AantalUniekeCodes = new SqlCommand("SELECT COUNT(UniekeCode) FROM UC WHERE StemmingsNaam = '" + Stemming + "'", sqlConnection);
        int AantalCodes = (int)AantalUniekeCodes.ExecuteScalar();

        //Haalt op hoeveel unieke codes er bij deze stemming met een telefoonnummer gelinkt zijn
        SqlCommand UitgegevenUniekeCodes = new SqlCommand("SELECT COUNT(UniekeCode) FROM UC WHERE StemmingsNaam = '" + Stemming + "' AND HashTelNr IS NOT NULL", sqlConnection);
        int UitgegevenCodes = (int)UitgegevenUniekeCodes.ExecuteScalar();

        //Haalt op hoe vaak er gestemd is:
        SqlCommand AantalKeerGestemd = new SqlCommand("SELECT COUNT(UniekeCode) FROM UC WHERE StemmingsNaam = '" + Stemming + "' AND GestemdOp IS NOT NULL", sqlConnection);
        int KeerGestemd = (int)AantalKeerGestemd.ExecuteScalar();

        Uitslag += "<h5>Voor deze stemming zijn " + AantalCodes + " unieke codes aangemaakt.<br />";
        Uitslag += "Er zijn " + UitgegevenCodes + " SMS'jes verstuurd.<br />";
        Uitslag += "Uiteindelijk is er " + KeerGestemd + " keer gestemd.<br /><h5>";

        //Gesorteerd op hoeveelheid stemmen
        SqlDataAdapter asd = new SqlDataAdapter("SELECT project.naam, Count(UC.GestemdOp) as aantal_stemmen FROM project LEFT JOIN(SELECT gestemdOp FROM UC WHERE UC.stemmingsnaam = '" + Stemming + "') as UC ON project.naam = UC.gestemdOp WHERE project.stemmingsnaam = '" + Stemming + "' GROUP BY project.naam ORDER BY aantal_stemmen DESC", sqlConnection);

        //Gesorteerd op naam
        //SqlDataAdapter asd = new SqlDataAdapter("SELECT project.naam, Count(UC.GestemdOp) as aantal_stemmen FROM project LEFT JOIN(SELECT gestemdOp FROM UC WHERE UC.stemmingsnaam = '" + Stemming + "') as UC ON project.naam = UC.gestemdOp WHERE project.stemmingsnaam = '" + Stemming + "' GROUP BY project.naam", sqlConnection);

        DataTable dt = new DataTable();
        asd.Fill(dt);

        Uitslag += "<table>";
        foreach (DataRow row in dt.Rows)
        {
            //Haalt het aantal keer dat op een project gestemd is op
            if (GebruikBlockchain)
            {
                int AantalStemmen = 0;
                AantalStemmen = (from x in Blocks.GestemdOp where x == (string)row["Naam"] select x).Count();
                Uitslag += "<tr><td>" + row["Naam"] + "</td><td>" + AantalStemmen + "</td></tr>";
            } else
            {
                Uitslag += "<tr><td>" + row["Naam"] + "</td><td>" + row["aantal_stemmen"] + "</td></tr>";
            }            
        }
        Uitslag += "</table>";
        sqlConnection.Close();
        //Returnt een string met daarin de winnaar en daaronder per project het aantal stemmen
        return Uitslag;
    }
}