﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

            //string hashTeam = HashGenereren.Genereer(Team);
            //string StemToevoegen = "UPDATE UC SET GestemdOp='" + hashTeam + "' WHERE UniekeCode = '" + StemCode + "';";
            string StemToevoegen = "UPDATE UC SET GestemdOp='" + Team + "' WHERE UniekeCode = '" + StemCode + "';";
            Session["Stemcode"] = string.Empty;
            Session["Team"] = string.Empty;

            //Checkt of de stemming nog actief is (om het veranderen van de URL op te vangen)
            SqlCommand CheckActief = new SqlCommand("SELECT Actief FROM Stemming WHERE Stemmingsnaam IN (SELECT StemmingsNaam FROM UC WHERE UniekeCode = '" + StemCode + "');", sqlConnection);

            //Stem toevoegen
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
                //Ingevulde StemCode deactiveren 
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



    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Projectenoverzicht.aspx");
    }
}