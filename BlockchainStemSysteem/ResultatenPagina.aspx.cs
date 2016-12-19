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

        if (!IsPostBack)
        {

            //string Team = Request.QueryString["GestemdOp"];
            string StemCode = Request.QueryString["StemCode"];
            string Standaardpagina = "Inlogpagina";

            //Vangt veranderen van de URL op
            //if (string.IsNullOrEmpty(Convert.ToString(StemCode)))
            //{
            //    Response.Redirect(Standaardpagina);
            //}

            DatabaseConnectie dbconnect = new DatabaseConnectie();
            SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);


            //Verbinding maken met database
            sqlConnection.Open();

            sqlConnection.Close();
        }

    }

    public ProjectenResultaten Resultaten = new ProjectenResultaten(Convert.ToString(HttpContext.Current.Request.QueryString["Stemming"]));
}