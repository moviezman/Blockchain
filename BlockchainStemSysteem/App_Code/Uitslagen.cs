using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Uitslagen
/// </summary>
public static class Uitslagen
{
    public static string UitslagStemming(string Stemming)
    {
        Blocks.Decodeer(Stemming);
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);        
        string Uitslag = "De winnaar is </br>";
        SqlCommand winnaar = new SqlCommand("SELECT GestemdOp, Count(GestemdOp)AS 'AantalGestemd' FROM UC WHERE StemmingsNaam = '" + Stemming + "' GROUP BY GestemdOp ORDER BY AantalGestemd DESC;", sqlConnection);
        sqlConnection.Open();
        //string Winnaar = (from x in Blocks.GestemdOp select x).Count().Max();
        var maxValue = Blocks.GestemdOp.Max(x => x);
        var Winnaar = Blocks.GestemdOp.Select(x => x == maxValue);
        
        //string Winnaar = ((from x in Blocks.GestemdOp select x.Count())).Max();
        Uitslag += Winnaar;
        sqlConnection.Close();
        return Uitslag;
    }

    public static string UitslagStemmingBeheerder(string Stemming)
    {
        Blocks.Decodeer(Stemming);
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);

        string Uitslag = "<h1>Uitslagen van " + Stemming + "</h1></br> ";

        SqlCommand winnaar = new SqlCommand("SELECT GestemdOp, Count(GestemdOp)AS 'AantalGestemd' FROM UC WHERE StemmingsNaam = '" + Stemming + "' GROUP BY GestemdOp ORDER BY AantalGestemd DESC;", sqlConnection);
        var maxValue = Blocks.GestemdOp.Max(x => x);
        var Winnaar = Blocks.GestemdOp.Select(x => x == maxValue);
        //string Winnaar = ((from x in Blocks.GestemdOp select x.Count())).Max();
        Console.WriteLine(Winnaar);
        Uitslag += Winnaar;
        SqlDataAdapter asd = new SqlDataAdapter("SELECT project.naam, Count(UC.GestemdOp) as aantal_stemmen FROM project LEFT JOIN(SELECT gestemdOp FROM UC WHERE UC.stemmingsnaam = '" + Stemming + "') as UC ON project.naam = UC.gestemdOp WHERE project.stemmingsnaam = '" + Stemming + "' GROUP BY project.naam", sqlConnection);
        sqlConnection.Open();
        Uitslag += "<h1>De winnaar is: " + Winnaar + "</h1><br />";
        DataTable dt = new DataTable();
        asd.Fill(dt);
        foreach (DataRow row in dt.Rows)
        {
            //Haalt het aantal keer dat op een project gestemd is op
            int AantalStemmen = 0;
            AantalStemmen = (from x in Blocks.GestemdOp where x == (string)row["Naam"] select x).Count();
            Uitslag += row["Naam"] + " " + AantalStemmen + "<br />";
        }
        sqlConnection.Close();
        return Uitslag;
    }
}