﻿using System;
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
            string Standaardpagina = "Inlogpagina";
            string StemToevoegen = "UPDATE Project SET AantalStemmen = AantalStemmen + 1 WHERE Naam = '" + Team + "';";
            string DeactiveerCode = "UPDATE UC SET ingezet = 1 WHERE UniekeCode = '" + StemCode + "';";

            //Vangt veranderen van de URL op
            if (string.IsNullOrEmpty(Convert.ToString(StemCode)))
            {
                Response.Redirect(Standaardpagina);
            }

            DatabaseConnectie dbconnect = new DatabaseConnectie();
            SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);



            //Deactiveer een gebruikte code
            SqlCommand CodeDeactiveren = new SqlCommand(DeactiveerCode, sqlConnection);

            //Stem toevoegen 
            SqlCommand UpdateTeam = new SqlCommand(StemToevoegen, sqlConnection);

            //Verbinding maken met database
            sqlConnection.Open();

            //update het aantal stemmen van een team
            UpdateTeam.ExecuteNonQuery();
            //Ingevulde StemCode deactiveren 
            CodeDeactiveren.ExecuteNonQuery();

            sqlConnection.Close();
        }

    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Projectenoverzicht.aspx");
        lbl_StemmingsNaam.Text = Request.QueryString["Stemming"];
    }
    public ProjectenResultaten Resultaten = new ProjectenResultaten(Convert.ToString(HttpContext.Current.Request.QueryString["Stemming"]));
}