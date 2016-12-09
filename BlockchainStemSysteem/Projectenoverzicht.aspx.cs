using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Projectenoverzicht : System.Web.UI.Page
{
    //Dit is de pagina waar de gebruiker heen wordt gestuurd als deze de site beinvloed 
    string Standaardpagina = "Inlogpagina";

    protected void Page_Load(object sender, EventArgs e)
    {

        //Ingevulde Stemcode ophalen en opslaan in string StemCode
        string StemCode = Request.QueryString["Stemmer"];
        //Vangt veranderen van de URL op
        if (string.IsNullOrEmpty(Convert.ToString(StemCode)))
        {
            Response.Redirect(Standaardpagina);
        }

        //Database connectie
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);

        //Hier staan de SQL quiries 
        //Controleren of een StemCode al is gebruikt quiry
        SqlCommand GetStatusCode = new SqlCommand("SELECT ingezet FROM UC WHERE UniekeCode = '" + StemCode + "';", sqlConnection);
        //Controleer of een code een unike code is
        SqlCommand CheckUniekeCode = new SqlCommand("SELECT COUNT(*) From UC WHERE ([UniekeCode] = '" + StemCode + "' COLLATE SQL_Latin1_General_CP1_CS_AS)", sqlConnection);


        //Database verbinding openen
        sqlConnection.Open();

        bool ActiveCode = Convert.ToBoolean(GetStatusCode.ExecuteScalar());

        int CodeBestaat = (int)CheckUniekeCode.ExecuteScalar();

        //Verbinding met database verbreken 
        sqlConnection.Close();

        //Als de code als is gebruikt dan wordt de gebruiker doorgestuurd naar de inlogpagina 
        //Als de URL wordt aangepast dan wordt de gebruikers teruggestuurd naar de inlogpagina 
        if (ActiveCode == true || CodeBestaat != 1)
        {
            Response.Redirect(Standaardpagina);
        }

        //maak een label met tekst erin
        this.lbl_IngelogdAls.Text = StemCode;


    }
    //Bereid de nieuwe knoppen voor voor de teams
    public Buttons Team = new Buttons(Convert.ToString(HttpContext.Current.Request.QueryString["Stemmer"]));
}