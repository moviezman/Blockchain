using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Buttons maak knoppen aan voor elke groep. Daar kan op geklikt worden om aan te geven dat je voor een team stemt. 
/// </summary>
public class Buttons
{
    public string TeamButtons;
    string StemmingsNaam;

    public Buttons(string StemCode)
    {
        
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlCommand CheckStemmingsNaam = new SqlCommand("SELECT StemmingsNaam FROM UC WHERE UniekeCode = '" + StemCode + "'", sqlConnection);
        sqlConnection.Open();
        //Haal string uit CheckStemmingsNaam
        SqlDataReader dr = CheckStemmingsNaam.ExecuteReader();

        while (dr.Read())
        {
            this.StemmingsNaam = dr[0].ToString();
        }
        dr.Close();


        SqlDataAdapter asd = new SqlDataAdapter("Select Naam From Project WHERE StemmingsNaam = '" + StemmingsNaam + "'", sqlConnection);
        DataTable dt = new DataTable();
        asd.Fill(dt);
        foreach (DataRow row in dt.Rows)
        {
            this.TeamButtons += "<button formaction='ResultatenPagina.aspx?GestemdOp=" + row["Naam"];

          

        }
    }
}