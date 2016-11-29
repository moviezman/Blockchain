using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Projectenoverzicht : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Vangt veranderen van de URL op
        if (string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Stemmer"])))
        {
            Response.Redirect("Inlogpagina");
        }

        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();
        SqlCommand CheckUniekeCode = new SqlCommand("Select COUNT(*) From UC WHERE ([UniekeCode] = @UniekeCode)", sqlConnection);
        CheckUniekeCode.Parameters.AddWithValue("@UniekeCode", Request.QueryString["Stemmer"]);
        int CodeBestaat = (int)CheckUniekeCode.ExecuteScalar();

        if (CodeBestaat > 0)
        {

        }
        else
        //Redirect naar de loginpagina als de unieke code niet bestaat.
        {
            Response.Redirect("Inlogpagina");
        }
        sqlConnection.Close();
        this.lbl_IngelogdAls.Text = Request.QueryString["Stemmer"];
    }
}