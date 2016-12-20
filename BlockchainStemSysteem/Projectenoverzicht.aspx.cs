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

    //Dit is waar de gebruiker heen wordt gestuurd als de stemming al is afgelopen
    string Resultatenpagina = "ResultatenPagina";

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

        //Hier staan de SQL queries 
        //Controleren of de stemming actief is
        SqlCommand CheckActief = new SqlCommand("SELECT Actief FROM Stemming WHERE Stemmingsnaam IN (SELECT StemmingsNaam FROM UC WHERE UniekeCode = '" + StemCode + "');", sqlConnection);
        //Controleren of een StemCode al is gebruikt query
        SqlCommand GetStatusCode = new SqlCommand("SELECT ingezet FROM UC WHERE UniekeCode = '" + StemCode + "';", sqlConnection);
        //Controleer of een code een unieke code is
        SqlCommand CheckUniekeCode = new SqlCommand("SELECT COUNT(*) From UC WHERE ([UniekeCode] = '" + StemCode + "' COLLATE SQL_Latin1_General_CP1_CS_AS)", sqlConnection);


        //Database verbinding openen
        sqlConnection.Open();

        bool ActieveCode = Convert.ToBoolean(GetStatusCode.ExecuteScalar());
        int CodeBestaat = (int)CheckUniekeCode.ExecuteScalar();
        bool StemmingActief = Convert.ToBoolean(CheckActief.ExecuteScalar());

        //Verbinding met database verbreken 
        sqlConnection.Close();

        //Als de stemming niet actief is wordt automatisch geredirect naar de resultatenpagina
        if (StemmingActief == false)
        {
            SqlCommand CheckStemmingsNaam = new SqlCommand("SELECT StemmingsNaam FROM UC WHERE UniekeCode = '" + StemCode + "'", sqlConnection);
            sqlConnection.Open();
            //Haal string uit CheckStemmingsNaam
            SqlDataReader dr = CheckStemmingsNaam.ExecuteReader();

            while (dr.Read())
            {
                //Response.Redirect(Resultatenpagina + ".aspx?Stemming=" + dr[0].ToString());
                Response.Redirect("ResultatenPagina.aspx?Stemming=" + dr[0].ToString());
            }
            dr.Close();
            sqlConnection.Close();
        }
        else
        {
            //Als de code al is gebruikt dan wordt de gebruiker doorgestuurd naar de inlogpagina 
            //Als de URL wordt aangepast dan wordt de gebruikers teruggestuurd naar de inlogpagina 
            if (ActieveCode == true || CodeBestaat != 1)
            {
                Response.Redirect(Standaardpagina);
            }
        }

        

        //maak een label met tekst erin
        this.lbl_IngelogdAls.Text = StemCode;


    }
    //Bereid de nieuwe knoppen voor voor de teams
    public Buttons Team = new Buttons(Convert.ToString(HttpContext.Current.Request.QueryString["Stemmer"]));
}