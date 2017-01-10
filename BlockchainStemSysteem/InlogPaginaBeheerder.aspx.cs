using System;
using System.Data.SqlClient;

//Inlogpagina voor de beheerder
public partial class InlogPaginaBeheerder : System.Web.UI.Page
{
    string Wachtwoord;
    public object DBContext { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Login"] = "";
    }

    protected void Button_Login_Click(object sender, EventArgs e)
    {
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord ORDER BY Id DESC", sqlConnection);

        sqlConnection.Open();
        //Checkt of het wachtwoord overeenkomt met de gehashte versie uit de database
        Wachtwoord = (string)WwChecken.ExecuteScalar();
        sqlConnection.Close();

        if (HashGenereren.checkHash(txtbx_Login.Text, Wachtwoord))
        {
            Session["Login"] = Wachtwoord;
            Response.Redirect("OverzichtBeheerder.aspx");
        }
        else
        {
            lbl_Info.Text = "Foutieve Inlogcode";
        }
    }
}