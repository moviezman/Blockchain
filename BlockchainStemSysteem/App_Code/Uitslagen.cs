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
        string Uitslag = "De winnaar van " + Stemming + " is geworden: ";
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlCommand winnaar = new SqlCommand("SELECT Naam FROM Project WHERE AantalStemmen = (SELECT Max(AantalStemmen) FROM Project WHERE StemmingsNaam = '" + Stemming + "')", sqlConnection);
        sqlConnection.Open();
        Uitslag += winnaar.ExecuteScalar();
        sqlConnection.Close();
        return Uitslag;
    }

    public static string UitslagStemmingBeheerder(string Stemming)
    {
        string Uitslag = "<h1>De winnaar van " + Stemming + " is geworden: ";
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlCommand winnaar = new SqlCommand("SELECT Naam FROM Project WHERE AantalStemmen = (SELECT Max(AantalStemmen) FROM Project WHERE StemmingsNaam = '" + Stemming + "')", sqlConnection);
        SqlDataAdapter asd = new SqlDataAdapter("Select Naam, AantalStemmen From Project WHERE StemmingsNaam = '" + Stemming + "'", sqlConnection);
        sqlConnection.Open();
        Uitslag += winnaar.ExecuteScalar() + "</h1><br />";
        DataTable dt = new DataTable();
        asd.Fill(dt);
        foreach (DataRow row in dt.Rows)
        {
            Uitslag += row["Naam"] + " " + row["AantalStemmen"] + "<br />";
        }
        sqlConnection.Close();
        return Uitslag;
    }
}