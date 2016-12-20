using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OverzichtBeheerder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Checkt of de gebruiker ingelogd is als beheerder
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();
        SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord WHERE Id = '1'", sqlConnection);
        string code = (string)(Session["Login"]);
        string Wachtwoord = WwChecken.ExecuteScalar().ToString();
        if (code != Wachtwoord)
        {
            Response.Redirect("Inlogpagina");
        }
        sqlConnection.Close();
    }

    protected void btn_GenereerPagina_Click(object sender, EventArgs e)
    {
        string Wachtwoord;

        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();
        //Verander de Id naar de id van het wachtwoord dat je wilt als je hem verandert
        SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord WHERE Id = '1'", sqlConnection);
        Wachtwoord = WwChecken.ExecuteScalar().ToString();
        sqlConnection.Close();

        Response.Redirect("GenereerPagina.aspx");
    }

    public BeheerderOverzicht Overzicht = new BeheerderOverzicht();
    //public Buttons Team = new Buttons(Convert.ToString(HttpContext.Current.Request.QueryString["Stemmer"]));

    protected void btn_Uitloggen_Click(object sender, EventArgs e)
    {
        Session["Login"] = "";
        Response.Redirect("inlogpaginaBeheerder");
    }
}