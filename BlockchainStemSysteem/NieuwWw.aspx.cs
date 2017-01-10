using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NieuwWw : System.Web.UI.Page
{
    string Wachtwoord;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Checkt of de gebruiker ingelogd is als beheerder
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();
        SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord ORDER BY Id DESC", sqlConnection);
        string code = (string)(Session["Login"]);
        string Wachtwoord = WwChecken.ExecuteScalar().ToString();
        if (code != Wachtwoord)
        {
            Response.Redirect("Inlogpagina");
        }
        sqlConnection.Close();
    }

    protected void btn_Opslaan_Click(object sender, EventArgs e)
    {
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord ORDER BY Id DESC", sqlConnection);

        sqlConnection.Open();
        //Leest het laatst toegevoegde wachtwoord
        Wachtwoord = (string)WwChecken.ExecuteScalar();
        sqlConnection.Close();

        //Checkt of het oude wachtwoord goed is
        if (HashGenereren.checkHash(txtbx_OudWw.Text, Wachtwoord))
        {
            //Checkt of de nieuw ingevoerde wachtwoorden overeenkomen
            if(txtbx_NieuwWw1.Text == txtbx_NieuwWw2.Text)
            {
                if(txtbx_NieuwWw1.Text.Length > 5)
                {
                    //Hasht het nieuwe wachtwoord en Voegt het toe in de database
                    string hash = HashGenereren.Genereer(txtbx_NieuwWw1.Text);
                    sqlConnection.Open();
                    SqlCommand WwToevoegen = new SqlCommand("INSERT INTO Wachtwoord (Hash) VALUES ('" + hash + "');", sqlConnection);
                    WwToevoegen.ExecuteNonQuery();
                    sqlConnection.Close();
                    lbl_Info.Text = "Wachtwoord Verandert!";
                    Response.Redirect("InlogpaginaBeheerder");
                    Session["Login"] = "";
                    txtbx_OudWw.Text = "";
                    txtbx_NieuwWw1.Text = "";
                    txtbx_NieuwWw2.Text = "";
                }
                else
                {
                    lbl_Info.Text = "Uw nieuwe wachtwoord moet minimaal 6 karakters bevatten";
                }
            }
            else
            {
                lbl_Info.Text = "De velden bij Nieuwe Wachtwoord komen niet overeen";
            }
        }
        else
        {
            lbl_Info.Text = "Het oude wachtwoord is onjuist ingevoerd";
        }
    }

    protected void btn_Terug_Click(object sender, EventArgs e)
    {
        Response.Redirect("OverzichtBeheerder");
    }
}