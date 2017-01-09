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
        string Uitslag = "De winnaar is</br>";
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
        Uitslag += "<h3>" + winnaar.ExecuteScalar() + "</h3>";

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

        SqlCommand winnaar = new SqlCommand("SELECT GestemdOp, Count(GestemdOp)AS 'AantalGestemd' FROM UC WHERE StemmingsNaam = '" + Stemming + "' GROUP BY GestemdOp ORDER BY AantalGestemd DESC;", sqlConnection);

        ////Met blockchain:
        ////Winnaar ophalen met LINQ werkt nog niet goed
        //var maxValue = Blocks.GestemdOp.Max(x => x);
        //var Winnaar = Blocks.GestemdOp.Select(x => x == maxValue);
        ////string Winnaar = ((from x in Blocks.GestemdOp select x.Count())).Max();
        //Console.WriteLine(Winnaar);
        //Uitslag += Winnaar;

        SqlDataAdapter asd = new SqlDataAdapter("SELECT project.naam, Count(UC.GestemdOp) as aantal_stemmen FROM project LEFT JOIN(SELECT gestemdOp FROM UC WHERE UC.stemmingsnaam = '" + Stemming + "') as UC ON project.naam = UC.gestemdOp WHERE project.stemmingsnaam = '" + Stemming + "' GROUP BY project.naam", sqlConnection);
        sqlConnection.Open();

        ////Met blockchain:
        //Uitslag += "<h1>De winnaar is: " + Winnaar + "</h1><br />";

        //Zonder blockchain:
        //Selecteert het project met de meeste stemmen
        Uitslag += "<h1>De winnaar is: " + winnaar.ExecuteScalar() + "</h1><br />";

        DataTable dt = new DataTable();
        asd.Fill(dt);
        foreach (DataRow row in dt.Rows)
        {
            ////Met blockchain:
            ////Haalt het aantal keer dat op een project gestemd is op
            //int AantalStemmen = 0;
            //AantalStemmen = (from x in Blocks.GestemdOp where x == (string)row["Naam"] select x).Count();
            //Uitslag += row["Naam"] + " " + AantalStemmen + "<br />";

            //Zonder blockchain:
            Uitslag += row["Naam"] + " " + row["aantal_stemmen"] + "<br />";
        }
        sqlConnection.Close();
        //Returnt een string met daarin de winnaar en daaronder per project het aantal stemmen
        return Uitslag;
    }
}