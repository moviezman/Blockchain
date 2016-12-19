using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ResultatenPagina : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Vang op of URL aangepast is
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlCommand StemmingActief = new SqlCommand("SELECT Actief FROM Stemming WHERE Stemmingsnaam = '" + Stemming + "';", sqlConnection);
        bool Actief = Convert.ToBoolean(StemmingActief.ExecuteScalar());
        if (!Actief)
        {
            Response.Redirect("Inlogpagina");
        }

    }
}