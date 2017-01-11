using System;
using System.Data.SqlClient;
using System.Web;

//De Resultatenpagina van de beheerder
public partial class ResultatenPaginaBeheerder : System.Web.UI.Page
{
    string Stemming = HttpContext.Current.Request.QueryString["Stemming"];
    protected void Page_Load(object sender, EventArgs e)
    {
        //Checkt of de stemming actief is
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlCommand StemmingActief = new SqlCommand("SELECT Actief FROM Stemming WHERE Stemmingsnaam = '" + Stemming + "';", sqlConnection);
        sqlConnection.Open();
        bool Actief = Convert.ToBoolean(StemmingActief.ExecuteScalar());
        if (Actief)
        {
            Response.Redirect("Inlogpagina");
        }

        //Checkt of de gebruiker ingelogd is als beheerder
        SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord ORDER BY Id DESC", sqlConnection);
        string code = (string)(Session["Login"]);
        string Wachtwoord = WwChecken.ExecuteScalar().ToString();
        if (code != Wachtwoord)
        {
            Response.Redirect("Inlogpagina");
        }
        sqlConnection.Close();
    }
    protected void btn_Terug_Click(object sender, EventArgs e)
    {
        Response.Redirect("OverzichtBeheerder");
    }
}