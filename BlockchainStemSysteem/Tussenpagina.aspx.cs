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
            SqlCommand Verwijdertelnr = new SqlCommand("UPDATE UC SET HashTelNr = NULL WHERE StemmingsNaam = '" + StemmingsNaam + "'", sqlConnection);
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