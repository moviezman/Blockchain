using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProjectenResultaten
/// </summary>
public class ProjectenResultaten
{
    public string resultaten;

    public ProjectenResultaten(string Stemming)
    {
        Blocks.Decodeer(Stemming);
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();
        SqlDataAdapter asd = new SqlDataAdapter("Select Naam, AantalStemmen From Project WHERE StemmingsNaam = '" + Stemming + "'", sqlConnection);
        DataTable dt = new DataTable();
        asd.Fill(dt);
        foreach (DataRow row in dt.Rows)
        {
            this.resultaten += row["Naam"] + " " + row["AantalStemmen"] + "<br />";
            
        }
        sqlConnection.Close();
    }
}