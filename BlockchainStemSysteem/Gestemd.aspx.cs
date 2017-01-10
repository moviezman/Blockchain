using System;
using System.Data.SqlClient;

//Pagina waar de gebruiker op komt als hij/zij gestemd heeft
//De stem wordt hier definitief voor de gebruiker
public partial class Gestemd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string Team = (string)Session["Team"];
            string StemCode = (string)Session["Stemcode"];
            string Standaardpagina = "Inlogpagina";

            //Vangt veranderen van de URL op
            if (string.IsNullOrEmpty((string)Session["Stemcode"]) || string.IsNullOrEmpty((string)Session["Team"]))
            {
                Response.Redirect(Standaardpagina);
            }

            //Databaseconnectie maken
            DatabaseConnectie dbconnect = new DatabaseConnectie();
            SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);

            //Zet het team waarop gestemd is vast bij de unieke code van de gebruiker die ingelogd is
            string StemToevoegen = "UPDATE UC SET GestemdOp='" + Team + "' WHERE UniekeCode = '" + StemCode + "';";
            //Maakt de sessievariabelen voor stemcode en team leeg
            //zodat de pagina niet opnieuw geladen kan worden
            //met een veranderde URL om de stem aan te passen
            Session["Stemcode"] = string.Empty;
            Session["Team"] = string.Empty;

            //Checkt of de stemming nog actief is (om het veranderen van de URL op te vangen)
            SqlCommand CheckActief = new SqlCommand("SELECT Actief FROM Stemming WHERE Stemmingsnaam IN (SELECT StemmingsNaam FROM UC WHERE UniekeCode = '" + StemCode + "');", sqlConnection);

            //Stem toevoegen en daarbij deactiveren (een unieke code kan niet nog een keer stemmen als hij al op een project heeft gestemd
            SqlCommand CodeDeactiveren = new SqlCommand(StemToevoegen, sqlConnection);

            //Verbinding maken met database
            sqlConnection.Open();

            bool StemmingActief = Convert.ToBoolean(CheckActief.ExecuteScalar());
            if(StemmingActief == false)
            {
                //Redirect naar de inlogpagina als de stemming al afgelopen is
                Response.Redirect("Inlogpagina");
            }
            else
            {
                //Stem toevoegen en ingelogde stemcode deactiveren
                //Een unieke code kan niet opnieuw stemmen als hij al op een project gestemd heeft
                CodeDeactiveren.ExecuteNonQuery();

                //Als het aantal stemmen dat niet in een block staat groter dan 4 is, maak dan een nieuw blok aan.
                SqlCommand AantalNietInBlock = new SqlCommand("SELECT COUNT(*) FROM UC WHERE InBlock = 'False' AND GestemdOp IS NOT NULL AND  Stemmingsnaam IN (SELECT StemmingsNaam FROM UC WHERE UniekeCode = '" + StemCode + "')", sqlConnection);
                SqlCommand Stemming = new SqlCommand("SELECT StemmingsNaam FROM UC WHERE UniekeCode = '" + StemCode + "'", sqlConnection);
                if ((int)AantalNietInBlock.ExecuteScalar() > 4)
                {
                    Blocks.MaakBlock((string)Stemming.ExecuteScalar());
                }
            }
            sqlConnection.Close();

            lbl_GestemdOp.Text = "Bedankt voor uw stem";
            Label1.Text = Team;
        }
    }

    //Knop om terug te gaan naar de inlogpagina
    //Deze knop is in de applicatie uitgezet
    //omdat ervan wordt uitgegaan dat maar 1 persoon per device stemt
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Projectenoverzicht.aspx");
    }
}