using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BeheerderOverzicht
/// </summary>
public class BeheerderOverzicht
{
    string LopendeStemmingen;
    string AfgelopenStemmingen;
    public BeheerderOverzicht()
    {
        
    }

    public string LopendeStemmingenOphalen()
    {
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlDataAdapter SQLLopendeStemmingen = new SqlDataAdapter("SELECT StemmingsNaam FROM Stemming WHERE Actief = 'true'", sqlConnection);
        DataTable dt = new DataTable();
        SQLLopendeStemmingen.Fill(dt);
        foreach (DataRow row in dt.Rows)
        {
            sqlConnection.Open();
            SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord WHERE Id = '1'", sqlConnection);
            string Wachtwoord = WwChecken.ExecuteScalar().ToString();
            this.LopendeStemmingen += "<p>" + row["StemmingsNaam"] + "<button formaction='TussenPagina.aspx?Stemming=" + row["StemmingsNaam"] + "&Login=" + Wachtwoord + "'>Stop</button></p><br />";
            sqlConnection.Close();
        }
        return this.LopendeStemmingen;
    }

    public string AfgelopenStemmingenOphalen()
    {
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlDataAdapter SQLAfgelopenStemmingen = new SqlDataAdapter("SELECT StemmingsNaam FROM Stemming WHERE Actief = 'false'", sqlConnection);
        DataTable dt = new DataTable();
        SQLAfgelopenStemmingen.Fill(dt);
        foreach (DataRow row in dt.Rows)
        {
            this.AfgelopenStemmingen += "<button formaction='ResultatenPaginaBeheerder.aspx?Stemming=" + row["StemmingsNaam"] + "'>" + row["StemmingsNaam"] + "</button></p><br />";
        }
        return this.AfgelopenStemmingen;
    }
}