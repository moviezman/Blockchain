using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ResultatenPagina : System.Web.UI.Page
{
    string Stemming = HttpContext.Current.Request.QueryString["Stemming"];
    protected void Page_Load(object sender, EventArgs e)
    {
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlCommand StemmingActief = new SqlCommand("SELECT Actief FROM Stemming WHERE Stemmingsnaam = '" + Stemming + "';", sqlConnection);
        bool Actief = Convert.ToBoolean(StemmingActief.ExecuteScalar());
        if (!Actief)
        {
            Response.Redirect("Inlogpagina");
        }

        //Checkt of de gebruiker ingelogd is als beheerder
        sqlConnection.Open();
        SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord WHERE Id = '1'", sqlConnection);
        string code = (string)(Session["Login"]);
        string Wachtwoord = WwChecken.ExecuteScalar().ToString();
        if (code != Wachtwoord)
        {
            Response.Redirect("Inlogpagina");
        }
        sqlConnection.Close();


    }

    public ProjectenResultaten Resultaten = new ProjectenResultaten(Convert.ToString(HttpContext.Current.Request.QueryString["Stemming"]));
}