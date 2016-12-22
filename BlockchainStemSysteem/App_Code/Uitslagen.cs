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
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);        
        string Uitslag = "De winnaar is </br>";
        SqlCommand winnaar = new SqlCommand("SELECT GestemdOp, Count(GestemdOp)AS 'AantalGestemd' FROM UC WHERE stemmingsNaam = '" + Stemming + "' GROUP BY GestemdOp ORDER BY AantalGestemd DESC;", sqlConnection);
        sqlConnection.Open();
        Uitslag += winnaar.ExecuteScalar();
        sqlConnection.Close();
        return Uitslag;
    }

    public static string UitslagStemmingBeheerder(string Stemming)
    {
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);

        string Uitslag = "<h1>Uitslagen van " + Stemming + "</h1></br> ";

        SqlCommand winnaar = new SqlCommand("SELECT GestemdOp, Count(GestemdOp)AS 'AantalGestemd' FROM UC WHERE stemmingsNaam = '" + Stemming + "' GROUP BY GestemdOp ORDER BY AantalGestemd DESC;", sqlConnection);
        SqlDataAdapter asd = new SqlDataAdapter("SELECT Project.Naam, Count(UC.GestemdOp) as aantal_stemmen FROM(Project LEFT JOIN UC on Project.Naam = UC.GestemdOp) WHERE Project.StemmingsNaam = '" + Stemming + "' GROUP BY Project.Naam", sqlConnection);
        sqlConnection.Open();
        Uitslag += "<h1>De winnaar is: " + winnaar.ExecuteScalar() + "</h1><br />";
        DataTable dt = new DataTable();
        asd.Fill(dt);
        foreach (DataRow row in dt.Rows)
        {
            Uitslag += row["Naam"] + " " + row["aantal_stemmen"] + "<br />";
        }
        sqlConnection.Close();
        return Uitslag;
    }
}