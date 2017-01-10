using System;
using System.Data.SqlClient;
using System.Web;

//De Resultatenpagina van de beheerder
public partial class ResultatenPagina : System.Web.UI.Page
{
    string Stemming = HttpContext.Current.Request.QueryString["Stemming"];
    protected void Page_Load(object sender, EventArgs e)
    {
        //Checkt of de stemming actief is
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
        SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord ORDER BY Id DESC", sqlConnection);
        string code = (string)(Session["Login"]);
        string Wachtwoord = WwChecken.ExecuteScalar().ToString();
        if (code != Wachtwoord)
        {
            Response.Redirect("Inlogpagina");
        }
        sqlConnection.Close();
    }    
}