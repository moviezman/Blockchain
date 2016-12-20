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
        //Checkt of URL aangepast is
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();
        SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord WHERE Id = '1'", sqlConnection);
        string code = Request.QueryString["Login"];
        string Wachtwoord = WwChecken.ExecuteScalar().ToString().Replace(" ", string.Empty);
        Wachtwoord = Wachtwoord.Replace("+", " ");
        sqlConnection.Close();
        if (code != Wachtwoord)
        {
            Response.Redirect("Inlogpagina");
        }
        else
        {
            string StemmingsNaam = Request.QueryString["Stemming"];
            SqlCommand StopStemming = new SqlCommand("UPDATE Stemming SET Actief = 'false' WHERE StemmingsNaam = '" + StemmingsNaam + "'", sqlConnection);
            sqlConnection.Open();
            StopStemming.ExecuteScalar();
            sqlConnection.Close();
            Response.Redirect("OverzichtBeheerder?Login=" + Wachtwoord);
        }
        sqlConnection.Close();
        
    }
}