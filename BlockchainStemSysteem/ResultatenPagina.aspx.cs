using System;
using System.Data.SqlClient;

//Resultatenpagina waar alle gebruikers bijkunnen
//Hierin staan de stemmingsnaam en welk project er heeft gewonnen
public partial class ResultatenPagina : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Stemming = Request.QueryString["Stemming"];
        //Laadt de resultaten van de stemming uit de URL
        //Deze pagina is ook te laden als je de URL goed intypt, omdat de resultaten publiek zijn
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