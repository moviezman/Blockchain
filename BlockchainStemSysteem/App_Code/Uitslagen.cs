using System.Data;
using System.Data.SqlClient;

//Haalt de resultaten op voor zowel de gebruiker als de beheerder
public static class Uitslagen
{
    //Haalt de resultaten op voor de gebruiker (toont alleen de winnaar)
    public static string UitslagStemming(string Stemming)
    {
        ////Met blockchain:
        //Blocks.Decodeer(Stemming);

        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);        
        string Uitslag = "<h2>" + "De winnaar is</br>" + "</h2>";
        
        //Checkt hoeveel projecten de meeste stemmen hebben
        SqlCommand CheckWinnaar = new SqlCommand("SELECT COUNT(*) FROM (SELECT GestemdOp FROM UC WHERE StemmingsNaam = '" + Stemming + "' AND GestemdOp = (SELECT Max(GestemdOp) FROM UC)) x", sqlConnection);
        //Haalt de winnaar op
        SqlCommand winnaar = new SqlCommand("SELECT GestemdOp, Count(GestemdOp)AS 'AantalGestemd' FROM UC WHERE StemmingsNaam = '" + Stemming + "' GROUP BY GestemdOp ORDER BY AantalGestemd DESC;", sqlConnection);
        sqlConnection.Open();

        ////Met blockchain:
        ////string Winnaar = (from x in Blocks.GestemdOp select x).Count().Max();
        ////Winnaar ophalen met LINQ werkt nog niet goed
        //var maxValue = Blocks.GestemdOp.Max(x => x);
        //var Winnaar = Blocks.GestemdOp.Select(x => x == maxValue);
        ////string Winnaar = ((from x in Blocks.GestemdOp select x.Count())).Max();
        //Uitslag += Winnaar;

        //Zonder blockchain:
        //Selecteert het project met de meeste stemmen
        if((int)CheckWinnaar.ExecuteScalar() == 1)
        {
            Uitslag += "<h4>" + winnaar.ExecuteScalar() + "</h4>";
        }
        else
        {
            Uitslag = "<h4>Gelijkspel</h4>";
        }
        

        sqlConnection.Close();
        //Returnt een string met daarin de winnaar
        return Uitslag;
    }
    
    //Haalt de resultaten op voor de beheerder (toont zowel de winnaar als het aantal stemmen per team)
    public static string UitslagStemmingBeheerder(string Stemming)
    {
        ////Met blockchain:
        //Blocks.Decodeer(Stemming);

        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);

        string Uitslag = "<h1>Uitslagen van " + Stemming + "</h1></br> ";
        
        //Checkt hoeveel projecten de meeste stemmen hebben
        SqlCommand CheckWinnaar = new SqlCommand("SELECT COUNT(*) FROM (SELECT GestemdOp FROM UC WHERE StemmingsNaam = '" + Stemming + "' AND GestemdOp = (SELECT Max(GestemdOp) FROM UC)) x", sqlConnection);
        //Haalt de winnaar op
        SqlCommand winnaar = new SqlCommand("SELECT GestemdOp, Count(GestemdOp)AS 'AantalGestemd' FROM UC WHERE StemmingsNaam = '" + Stemming + "' GROUP BY GestemdOp ORDER BY AantalGestemd DESC;", sqlConnection);

        ////Met blockchain:
        ////Winnaar ophalen met LINQ werkt nog niet goed
        //var maxValue = Blocks.GestemdOp.Max(x => x);
        //var Winnaar = Blocks.GestemdOp.Select(x => x == maxValue);
        ////string Winnaar = ((from x in Blocks.GestemdOp select x.Count())).Max();
        //Console.WriteLine(Winnaar);
        //Uitslag += Winnaar;

        
        //Gesorteerd op hoeveelheid stemmen
        SqlDataAdapter asd = new SqlDataAdapter("SELECT project.naam, Count(UC.GestemdOp) as aantal_stemmen FROM project LEFT JOIN(SELECT gestemdOp FROM UC WHERE UC.stemmingsnaam = '" + Stemming + "') as UC ON project.naam = UC.gestemdOp WHERE project.stemmingsnaam = '" + Stemming + "' GROUP BY project.naam ORDER BY aantal_stemmen DESC", sqlConnection);

        //Gesorteerd op naam
        //SqlDataAdapter asd = new SqlDataAdapter("SELECT project.naam, Count(UC.GestemdOp) as aantal_stemmen FROM project LEFT JOIN(SELECT gestemdOp FROM UC WHERE UC.stemmingsnaam = '" + Stemming + "') as UC ON project.naam = UC.gestemdOp WHERE project.stemmingsnaam = '" + Stemming + "' GROUP BY project.naam", sqlConnection);
        sqlConnection.Open();

        ////Met blockchain:
        //Uitslag += "<h1>De winnaar is: " + Winnaar + "</h1><br />";

        //Zonder blockchain:
        //Selecteert het project met de meeste stemmen
        if ((int)CheckWinnaar.ExecuteScalar() == 1)
        {
            Uitslag += "<h4>Winnaar:<br />" + winnaar.ExecuteScalar() + "</h4><br />";
        }
        else
        {
            Uitslag += "<h4>Gelijkspel</h4><br />";
        }

        DataTable dt = new DataTable();
        asd.Fill(dt);

        //Haalt op hoeveel unieke codes er bij deze stemming zijn aangemaakt
        SqlCommand AantalUniekeCodes = new SqlCommand("SELECT COUNT(UniekeCode) FROM UC WHERE StemmingsNaam = '" + Stemming + "'", sqlConnection);
        int AantalCodes = (int)AantalUniekeCodes.ExecuteScalar();

        //Haalt op hoeveel unieke codes er bij deze stemming met een telefoonnummer gelinkt zijn
        SqlCommand UitgegevenUniekeCodes = new SqlCommand("SELECT COUNT(UniekeCode) FROM UC WHERE StemmingsNaam = '" + Stemming + "' AND HashTelNr IS NOT NULL", sqlConnection);
        int UitgegevenCodes = (int)UitgegevenUniekeCodes.ExecuteScalar();

        //Haalt op hoe vaak er gestemd is:
        SqlCommand AantalKeerGestemd = new SqlCommand("SELECT COUNT(UniekeCode) FROM UC WHERE StemmingsNaam = '" + Stemming + "' AND GestemdOp IS NOT NULL", sqlConnection);
        int KeerGestemd = (int)AantalKeerGestemd.ExecuteScalar();

        Uitslag += "<h5>Bij deze stemming zijn " + AantalCodes + " unieke codes aangemaakt<br />";
        Uitslag += "Daarvan zijn er " + UitgegevenCodes + " gelinkt aan een telefoonnummer<br />";
        Uitslag += "Uiteindelijk zijn er " + KeerGestemd + " gebruikt om mee te stemmen<br /><h5>";

        Uitslag += "<table>";
        foreach (DataRow row in dt.Rows)
        {
            ////Met blockchain:
            ////Haalt het aantal keer dat op een project gestemd is op
            //int AantalStemmen = 0;
            //AantalStemmen = (from x in Blocks.GestemdOp where x == (string)row["Naam"] select x).Count();
            //Uitslag += row["Naam"] + " " + AantalStemmen + "<br />";

            //Zonder blockchain:
            Uitslag += "<tr><td>" + row["Naam"] + "</td><td>" + row["aantal_stemmen"] + "</td></tr>";
        }
        Uitslag += "</table>";
        sqlConnection.Close();
        //Returnt een string met daarin de winnaar en daaronder per project het aantal stemmen
        return Uitslag;
    }
}