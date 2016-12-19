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
        SqlCommand StemmingActief = new SqlCommand("SELECT Actief FROM Stemming WHERE Stemmingsnaam = '" + Stemming + "';", sqlConnection);
        bool Actief = Convert.ToBoolean(StemmingActief.ExecuteScalar());
        
        string Uitslag = "De winnaar van " + Stemming + " is geworden: ";
        SqlCommand winnaar = new SqlCommand("SELECT Top(1) Naam FROM Project WHERE stemmingsNaam = '" + Stemming + "' ORDER BY AantalStemmen DESC;", sqlConnection);
        sqlConnection.Open();
        Uitslag += winnaar.ExecuteScalar();
        sqlConnection.Close();
        return Uitslag;
    }

    public static string UitslagStemmingBeheerder(string Stemming)
    {
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);

        string Uitslag = "<h1>De winnaar van " + Stemming + " is geworden: ";
        
        SqlCommand winnaar = new SqlCommand("SELECT Top(1) Naam FROM Project WHERE stemmingsNaam = '" + Stemming + "' ORDER BY AantalStemmen DESC;", sqlConnection);
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