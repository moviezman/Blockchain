using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InlogPaginaBeheerder : System.Web.UI.Page
{
    public object DBContext { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {

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
        string Wachtwoord;

        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();
        //Verander de Id naar de id van het wachtwoord dat je wilt als je hem verandert
        SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord WHERE Id = '1'", sqlConnection);
        Wachtwoord = WwChecken.ExecuteScalar().ToString();
        sqlConnection.Close();

        if(HashGenereren.checkHash(txtbx_Login.Text, Wachtwoord))
        {
            Response.Redirect("OverzichtBeheerder.aspx");
        }
        else
        {
            lbl_Hash.Text = "Foutieve Inlogcode";
        }
    }
}