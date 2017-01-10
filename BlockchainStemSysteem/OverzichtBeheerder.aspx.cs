using System;
using System.Data.SqlClient;

public partial class OverzichtBeheerder : System.Web.UI.Page
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
        if (code != Wachtwoord)
        {
            Response.Redirect("Inlogpagina");
        }
        sqlConnection.Close();
    }

    //Linkt naar de genereerpagina
    protected void btn_GenereerPagina_Click(object sender, EventArgs e)
    {
        Response.Redirect("GenereerPagina.aspx");
    }

    //Laadt alle lopende en afgelopen stemmingen met behulp van de klasse 'BeheerderOverzicht'
    public BeheerderOverzicht Overzicht = new BeheerderOverzicht();

    //Maakt de sessievariabele 'Login' leeg en redirect naar de inlogpagina voor de beheerder
    protected void btn_Uitloggen_Click(object sender, EventArgs e)
    {
        Session["Login"] = "";
        Response.Redirect("inlogpaginaBeheerder");
    }

    //Redirect naar de pagina om het wachtwoord van de beheerder aan te passen
    protected void btn_WwWijzigen_Click(object sender, EventArgs e)
    {
        Response.Redirect("NieuwWw");
    }
}