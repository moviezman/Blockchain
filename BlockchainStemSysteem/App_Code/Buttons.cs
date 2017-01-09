using System.Data;
using System.Data.SqlClient;

//Buttons maak knoppen aan voor elke groep. Daar kan op geklikt worden om op een project te stemmen 
public class Buttons
{
    public string TeamButtons;
    string StemmingsNaam;

    public Buttons(string StemCode)
    {
        TeamButtons = "";
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlCommand CheckStemmingsNaam = new SqlCommand("SELECT StemmingsNaam FROM UC WHERE UniekeCode = '" + StemCode + "'", sqlConnection);
        sqlConnection.Open();
        //Haal de naam van de stemming op die hoort bij de stemcode
        SqlDataReader dr = CheckStemmingsNaam.ExecuteReader();

        while (dr.Read())
        {
            this.StemmingsNaam = dr[0].ToString();
        }
        dr.Close();


        SqlDataAdapter asd = new SqlDataAdapter("Select Naam From Project WHERE StemmingsNaam = '" + StemmingsNaam + "'", sqlConnection);
        DataTable dt = new DataTable();
        asd.Fill(dt);
        //Voor elk project uit de stemming
        foreach (DataRow row in dt.Rows)
        {
            //Maak een knop aan met de projectnaam erop die linkt naar de BevestigStem pagina
            this.TeamButtons += "<button formaction='BevestigStem.aspx?GestemdOp=" + row["Naam"] + "'>" + row["Naam"] + "</button><br />";
        }
    }
}