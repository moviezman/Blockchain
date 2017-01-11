using System.Data;
using System.Data.SqlClient;

//Deze pagina zorgt voor het genereren van de code voor lopende en afgelopen stemmingen
public class BeheerderOverzicht
{
    string LopendeStemmingen;
    string AfgelopenStemmingen;
    public BeheerderOverzicht()
    {
        
    }

    public string LopendeStemmingenOphalen()
    {
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlDataAdapter SQLLopendeStemmingen = new SqlDataAdapter("SELECT StemmingsNaam FROM Stemming WHERE Actief = 'true'", sqlConnection);
        DataTable dt = new DataTable();
        SQLLopendeStemmingen.Fill(dt);
        //Voor elke stemming die actief is
        foreach (DataRow row in dt.Rows)
        {
            //Geef de stemmingsnaam met een stopknop ernaast
            //Deze stopknop linkt naar de Tussenpagina, waar de stemming gestopt wordt
            //Ook het gehashte wachtwoord wordt meegegeven naar de tussenpagina, om veranderen van de URL op te vangen
            sqlConnection.Open();
            SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord WHERE Id = '1'", sqlConnection);
            string Wachtwoord = WwChecken.ExecuteScalar().ToString();

            //SqlCommand AantalUniekeCodes = new SqlCommand("SELECT COUNT(UniekeCode) FROM UC WHERE StemmingsNaam = '" + row["StemmingsNaam"] + "'", sqlConnection);
            //int AantalCodes = (int)AantalUniekeCodes.ExecuteScalar();

            //Aantal codes die zijn uitgegeven en aantal codes die nog over zijn
            SqlCommand UitgegevenUniekeCodes = new SqlCommand("SELECT COUNT(UniekeCode) FROM UC WHERE StemmingsNaam = '" + row["StemmingsNaam"] + "' AND HashTelNr IS NOT NULL", sqlConnection);
            int UitgegevenCodes = (int)UitgegevenUniekeCodes.ExecuteScalar();
            SqlCommand BeschikbareUniekeCodes = new SqlCommand("SELECT COUNT(UniekeCode) FROM UC WHERE StemmingsNaam = '" + row["StemmingsNaam"] + "' AND HashTelNr IS NULL", sqlConnection);
            int BeschikbareCodes = (int)BeschikbareUniekeCodes.ExecuteScalar();

            this.LopendeStemmingen += "<h1>" + row["StemmingsNaam"] + "</h1>Uitgegeven codes: " + UitgegevenCodes + "<br />Beschikbare codes: " + BeschikbareCodes + "<br /><button ID=knopStop formaction='TussenPagina.aspx?Stemming=" + row["StemmingsNaam"] + "&Login=" + Wachtwoord + "'>Stop</button></h1>";
            sqlConnection.Close();
        }
        //returnt de code van lopende stemmingen
        return this.LopendeStemmingen;
    }

    public string AfgelopenStemmingenOphalen()
    {
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        SqlDataAdapter SQLAfgelopenStemmingen = new SqlDataAdapter("SELECT StemmingsNaam FROM Stemming WHERE Actief = 'false'", sqlConnection);
        DataTable dt = new DataTable();
        SQLAfgelopenStemmingen.Fill(dt);
        //Voor elke stemming:
        foreach (DataRow row in dt.Rows)
        {
            //Maak een knop aan die linkt naar de beheerdersresultatenpagina van deze stemming
            this.AfgelopenStemmingen += "<button formaction='ResultatenPaginaBeheerder.aspx?Stemming=" + row["StemmingsNaam"] + "'>" + row["StemmingsNaam"] + "</button><br />";
        }
        //returnt de code van afgelopen stemmingen
        return this.AfgelopenStemmingen;
    }
}