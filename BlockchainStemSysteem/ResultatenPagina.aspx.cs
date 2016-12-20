using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ResultatenPagina : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Stemming = Request.QueryString["Stemming"];
        //Vang op of URL aangepast is
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlCommand StemmingActief = new SqlCommand("SELECT Actief FROM Stemming WHERE Stemmingsnaam = '" + Stemming + "';", sqlConnection);
        SqlCommand StemmingBestaat = new SqlCommand("SELECT COUNT(*) From Stemming WHERE Stemmingsnaam = '" + Stemming + "'", sqlConnection);
        sqlConnection.Open();
        bool Bestaat = Convert.ToBoolean(StemmingBestaat.ExecuteScalar());
        bool Actief = Convert.ToBoolean(StemmingActief.ExecuteScalar());
        if (Actief || !Bestaat)
        {
            Response.Redirect("Inlogpagina");
        }
        sqlConnection.Close();
    }
}