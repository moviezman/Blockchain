using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ResultatenPagina : System.Web.UI.Page
{
    string Stemming = HttpContext.Current.Request.QueryString["Stemming"];
    protected void Page_Load(object sender, EventArgs e)
    {
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlCommand StemmingActief = new SqlCommand("SELECT Actief FROM Stemming WHERE Stemmingsnaam = '" + Stemming + "';", sqlConnection);
        bool Actief = Convert.ToBoolean(StemmingActief.ExecuteScalar());
        if (!Actief)
        {
            Response.Redirect("InlogpaginaBeheerder");
        }
        if (!IsPostBack)
        {
            string StemCode = Request.QueryString["StemCode"];
            string Standaardpagina = "Inlogpagina";

            //Vangt veranderen van de URL op
            //if (string.IsNullOrEmpty(Convert.ToString(StemCode)))
            //{
            //    Response.Redirect(Standaardpagina);
            //}
        }


    }

    public ProjectenResultaten Resultaten = new ProjectenResultaten(Convert.ToString(HttpContext.Current.Request.QueryString["Stemming"]));
}