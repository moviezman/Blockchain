using System;
using System.Data.SqlClient;
using System.Web;

//Op deze pagina worden alle projecten uit de stemming waar de gebruiker
//met de unieke code waarmee hij/zij ingelogd is toegang tot heeft getoond
public partial class Projectenoverzicht : System.Web.UI.Page
{
    //Bereid de nieuwe knoppen voor voor de teams
    public Buttons Team = new Buttons(Convert.ToString(HttpContext.Current.Request.QueryString["Stemmer"]), ZoekResultaat);

    //Slaat het zoekresultaat op
    public static string ZoekResultaat = "";

    //Dit is de pagina waar de gebruiker heen wordt gestuurd als hij/zij de URL verandert
    string Standaardpagina = "Inlogpagina";

    //Dit is waar de gebruiker heen wordt gestuurd als de stemming al is afgelopen
    string Resultatenpagina = "ResultatenPagina";

    protected void btn_ZoekResultaat_Click(object sender, EventArgs e)
    {
        ZoekResultaat = txtbx_ZoekResultaat.Text;
        Team = new Buttons(Convert.ToString(HttpContext.Current.Request.QueryString["Stemmer"]), ZoekResultaat);
        txtbx_ZoekResultaat.Text = String.Empty;
        ZoekResultaat = String.Empty;
    }

    protected void btn_ResetResultaat_Click(object sender, EventArgs e)
    {
        txtbx_ZoekResultaat.Text = String.Empty;
        ZoekResultaat = txtbx_ZoekResultaat.Text;
        Team = new Buttons(Convert.ToString(HttpContext.Current.Request.QueryString["Stemmer"]), ZoekResultaat);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Ingevulde Stemcode ophalen en opslaan in string StemCode
        string StemCode = Request.QueryString["Stemmer"];
        //Redirect de gebruiker naar de inlogpagina als hij/zij de URL aanpast
        if (string.IsNullOrEmpty(Convert.ToString(StemCode)))
        {
            Response.Redirect(Standaardpagina);
        }

        //Database connectie
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);

        //Controleren of de stemming actief is
        SqlCommand CheckActief = new SqlCommand("SELECT Actief FROM Stemming WHERE Stemmingsnaam IN (SELECT StemmingsNaam FROM UC WHERE UniekeCode = '" + StemCode + "');", sqlConnection);
        //Controleren of een StemCode al is gebruikt
        SqlCommand GetStatusCode = new SqlCommand("SELECT GestemdOp FROM UC WHERE UniekeCode = '" + StemCode + "';", sqlConnection);
        //Controleer of de ingevulde unieke code in de database staat
        SqlCommand CheckUniekeCode = new SqlCommand("SELECT COUNT(*) From UC WHERE ([UniekeCode] = '" + StemCode + "' COLLATE SQL_Latin1_General_CP1_CS_AS)", sqlConnection);


        //Database verbinding openen
        sqlConnection.Open();
        bool ActieveCode;
        if (GetStatusCode.ExecuteScalar() is DBNull)
        {
            ActieveCode = false;
        }
        else
        {
            ActieveCode = true;
        }
        //ActieveCode = Convert.ToBoolean(GetStatusCode.ExecuteScalar());
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
                StemCode = Request.QueryString["Stemmer"];
                Response.Redirect("ResultatenPagina.aspx?Stemming=" + dr[0].ToString());
            }
            dr.Close();
            sqlConnection.Close();
        }
        else
        {
            //Als de code al is gebruikt of als de URL is aangepast dan wordt de gebruiker geredirect naar de inlogpagina 
            if (ActieveCode == true || CodeBestaat != 1)
            {
                Response.Redirect(Standaardpagina);
            }
            else
            {
                Session["Stemcode"] = StemCode;
            }
        }
    }

}