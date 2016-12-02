using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Buttons
/// </summary>
public class Buttons
{
    public string TeamButtons;


    public Buttons(string StemCode)
    {
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlDataAdapter asd = new SqlDataAdapter("Select Naam From Project", sqlConnection);
        DataTable dt = new DataTable();
        asd.Fill(dt);
        foreach (DataRow row in dt.Rows)
        {
            this.TeamButtons += "<button formaction='ResultatenPagina.aspx?GestemdOp=" + row["Naam"] + "&StemCode=" + StemCode + "' style='width:200px'>" + row["Naam"] + "</button><br />"; ;
        }
    }

    
}