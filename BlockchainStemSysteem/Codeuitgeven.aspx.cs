using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Codeuitgeven : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Nummercontrole check = new Nummercontrole();

        if (check.Nummercheck(Convert.ToInt32(TextBox1.Text)))
        {

            DatabaseConnectie dbconnect = new DatabaseConnectie();
            SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);


            // test SELECT Top 1 UniekeCode FROM UC WHERE Ingezet = 'false');
            SqlCommand NieuweCodeQuery = new SqlCommand("SELECT Top 1 UniekeCode FROM UC WHERE Ingezet = 'false'", sqlConnection);

            string NieuweCode = "";

            sqlConnection.Open();

            NieuweCode = Convert.ToString(NieuweCodeQuery.ExecuteScalar());

            sqlConnection.Close();

            if (NieuweCode != null)
            {
                Label4.Visible = true;
                Label4.Text = "Uw code is: " + NieuweCode;
            }
            else
            {
                Label4.Visible = true;
                Label4.Text = "U hebt een ongeldig nummer ingevoerd";
            }
        } else
        {
            Label4.Visible = true;
            Label4.Text = "nummer moet 5 zijn";
        }
        
    }
}