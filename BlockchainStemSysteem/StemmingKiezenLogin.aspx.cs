using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StemmingKiezenLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["StemmingLogin"] = string.Empty;
    }

    protected void btn_Login_Click(object sender, EventArgs e)
    {
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord ORDER BY Id DESC", sqlConnection);

        sqlConnection.Open();
        //Checkt of het wachtwoord overeenkomt met de gehashte versie uit de database
        string Wachtwoord = (string)WwChecken.ExecuteScalar();
        sqlConnection.Close();

        if (HashGenereren.checkHash(txtbx_Login.Text, Wachtwoord))
        {
            lbl_Info.Text = string.Empty;
            Session["StemmingLogin"] = Wachtwoord;
            Response.Redirect("StemmingKiezen.aspx");
        }
        else
        {
            lbl_Info.Text = "Dit wachtwoord is fout";
        }
    }
}