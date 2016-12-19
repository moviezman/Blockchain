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
        SqlDataAdapter LopendeStemmingen = new SqlDataAdapter("SELECT StemmingsNaam FROM Stemming WHERE Actief = 'false'", sqlConnection);
        DataTable dt = new DataTable();
        LopendeStemmingen.Fill(dt);
        foreach (DataRow row in dt.Rows)
        {
            this.LopendeStemmingen += "<p>" + row["StemmingsNaam"] + "<button formaction='TussenPagina.aspx?Stemming=" + row["StemmingsNaam"] + "'>Stop</button></p><br />";
        }
            return this.LopendeStemmingen;
    }
}