using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StemmingKiezen : System.Web.UI.Page
{

    public string StemButtons;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Database connectie
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);

        SqlDataAdapter asd = new SqlDataAdapter("Select Stemmingsnaam From Stemming", sqlConnection);
        DataTable dt = new DataTable();
        asd.Fill(dt);
        foreach (DataRow row in dt.Rows)
        {
            this.StemButtons += "<button formaction='Codeuitgeven.aspx?Stemming=" + row["Stemmingsnaam"] + "'>" + row["Stemmingsnaam"] + "</button><br />";
        }
    }
}