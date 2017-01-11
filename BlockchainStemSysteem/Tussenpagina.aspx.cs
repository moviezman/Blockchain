using System;
using System.Data.SqlClient;

//Deze pagina wordt aangeroepen tijdens het stoppen van een stemming door een beheerder
//Deze pagina wordt niet zichtbaar voor de beheerder
public partial class Tussenpagina : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Checkt of de gebruiker ingelogd is als beheerder
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();
        SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord ORDER BY Id DESC", sqlConnection);
        string code = (string)(Session["Login"]);
        string Wachtwoord = WwChecken.ExecuteScalar().ToString();
        sqlConnection.Close();
        if (code != Wachtwoord)
        {
            Response.Redirect("Inlogpagina");
        }
        else
        {
            //Stopt de stemming en verwijdert alle opgeslagen telefoonnrhashes uit de database.
            string StemmingsNaam = Request.QueryString["Stemming"];
            Blocks.MaakBlock(StemmingsNaam);
            SqlCommand StopStemming = new SqlCommand("UPDATE Stemming SET Actief = 'false' WHERE StemmingsNaam = '" + StemmingsNaam + "'", sqlConnection);
            //Verandert alle gehashte telefoonnummers in '1'
            //De hoeveelheid ingevulde telefoonnummers worden later opgehaald in de resultatenpaginabeheerder
            //Daarom worden ze niet verandert in 'NULL'
            SqlCommand Verwijdertelnr = new SqlCommand("UPDATE UC SET HashTelNr = '1' WHERE StemmingsNaam = '" + StemmingsNaam + "' AND HashTelNr IS NOT NULL", sqlConnection);
            sqlConnection.Open();
            //Stopt de stemming
            StopStemming.ExecuteScalar();
            //Verwijdert alle opgeslagen gehashte telefoonnummers van de gestopte stemming uit de database
            Verwijdertelnr.ExecuteScalar();
            sqlConnection.Close();
            Response.Redirect("OverzichtBeheerder");
        }        
    }
}