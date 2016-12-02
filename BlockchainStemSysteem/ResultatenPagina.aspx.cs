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
            string Team = Request.QueryString["GestemdOp"];
            string StemCode = Request.QueryString["StemCode"];

            DatabaseConnectie dbconnect = new DatabaseConnectie();
            SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);

            string Query = "UPDATE Project SET AantalStemmen = AantalStemmen + 1 WHERE Naam = '" + Team + "';";

            SqlCommand UpdateTeam = new SqlCommand(Query, sqlConnection);

            //Deactiveer een gebruikte code 
            //Stemcode moet nog worden doorgevoerd
            //SqlCommand CodeDeactiveren = new SqlCommand("UPDATE UC SET ingezet = 1 WHERE UniekeCode = '" + StemCode + "';", sqlConnection);

            sqlConnection.Open();

            UpdateTeam.ExecuteNonQuery();
            //Ingevulde StemCode deactiveren 
            //CodeDeactiveren.ExecuteNonQuery();

            sqlConnection.Close();

            lbl_GestemdOp.Text = Team;
        }

        

    }

    

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Projectenoverzicht.aspx");
    }
}