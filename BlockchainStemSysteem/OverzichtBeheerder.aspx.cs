﻿using System;
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
        //Checkt of URL aangepast is
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();
        SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord WHERE Id = '1'", sqlConnection);
        string code = Request.QueryString["Login"];
        string Wachtwoord = WwChecken.ExecuteScalar().ToString().Replace(" ", string.Empty);
        Wachtwoord = Wachtwoord.Replace("+", " ");
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

        Response.Redirect("GenereerPagina.aspx?Login=" + Wachtwoord);
    }

    public BeheerderOverzicht Overzicht = new BeheerderOverzicht();
    //public Buttons Team = new Buttons(Convert.ToString(HttpContext.Current.Request.QueryString["Stemmer"]));

    protected void btn_Uitloggen_Click(object sender, EventArgs e)
    {
        Response.Redirect("inlogpaginaBeheerder");
    }
}