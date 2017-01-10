using System;
using System.Data;
using System.Data.SqlClient;

//Hier kan de gebruiker een stemming kiezen
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
            //Voegt een knop toe voor elke lopende stemming
            //Deze knop linkt naar de 'CodeUitgeven' pagina
            this.StemButtons += "<button formaction='Codeuitgeven.aspx?Stemming=" + row["Stemmingsnaam"] + "'>" + row["Stemmingsnaam"] + "</button><br />";
        }
    }
}