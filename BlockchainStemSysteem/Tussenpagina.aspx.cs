using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tussenpagina : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string StemmingsNaam = Request.QueryString["Stemming"];
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlCommand StopStemming = new SqlCommand("UPDATE Stemming SET Actief = 'false' WHERE StemmingsNaam = '" + StemmingsNaam + "'", sqlConnection);
        sqlConnection.Open();
        StopStemming.ExecuteScalar();
        sqlConnection.Close();
        Response.Redirect("OverzichtBeheerder");
    }
}