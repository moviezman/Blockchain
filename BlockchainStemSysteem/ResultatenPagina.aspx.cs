using System;
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
        string Team = Request.QueryString["GestemdOp"];
        this.lbl_GestemdOp.Text = Team;
        string code = ("UPDATE Project SET " + Team + "SET  ");

        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();
        SqlCommand CheckUniekeCode = new SqlCommand("UPDATE CODE HIER", sqlConnection);
        CheckUniekeCode.Parameters.AddWithValue("@UniekeCode", TextBox1.Text);
        int CodeBestaat = (int)CheckUniekeCode.ExecuteScalar();

        if (CodeBestaat > 0)
        {
            Response.Redirect("Projectenoverzicht.aspx?Stemmer=" + TextBox1.Text);
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "false";
        }
        sqlConnection.Close();



    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Projectenoverzicht.aspx");
    }
}