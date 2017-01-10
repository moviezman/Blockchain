using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InlogPaginaBeheerder : System.Web.UI.Page
{
    string Wachtwoord;
    public object DBContext { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Login"] = "";
    }

    protected void btn_Genereer_Click(object sender, EventArgs e)
    {
        string hash = HashGenereren.Genereer(txtbx_Login.Text);

        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();
        SqlCommand WwToevoegen = new SqlCommand("INSERT INTO Wachtwoord (Hash) VALUES ('" + hash + "');", sqlConnection);
        WwToevoegen.ExecuteNonQuery();
        sqlConnection.Close();

        lbl_Hash.Text = "Gegenereerd";
    }

    protected void Button_Login_Click(object sender, EventArgs e)
    {
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord ORDER BY Id DESC", sqlConnection);

        sqlConnection.Open();
        //Leest het laatst toegevoegde wachtwoord
        Wachtwoord = (string)WwChecken.ExecuteScalar();
        sqlConnection.Close();

        if (HashGenereren.checkHash(txtbx_Login.Text, Wachtwoord))
        {
            Session["Login"] = Wachtwoord;
            Response.Redirect("OverzichtBeheerder.aspx");
        }
        else
        {
            lbl_Hash.Text = "Foutieve Inlogcode";
        }
    }
}