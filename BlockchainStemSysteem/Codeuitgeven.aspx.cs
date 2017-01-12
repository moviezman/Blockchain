using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

public partial class Codeuitgeven : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Stemming = Request.QueryString["Stemming"];
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();

        //Checkt of de stemming bestaat
        SqlCommand CheckStemmingBestaat = new SqlCommand("SELECT COUNT(*) From Stemming WHERE StemmingsNaam = '" + Stemming + "'", sqlConnection);
        int StemmingBestaat = (int)CheckStemmingBestaat.ExecuteScalar();

        //Checkt of de stemming actief is
        SqlCommand CheckActief = new SqlCommand("SELECT Actief FROM Stemming WHERE Stemmingsnaam = '" + Stemming + "'", sqlConnection);
        bool StemmingActief = Convert.ToBoolean(CheckActief.ExecuteScalar());
        sqlConnection.Close();

        //Redirect naar de inlogpagina als de stemming niet bestaat
        if(StemmingBestaat == 0)
        {
            Response.Redirect("Inlogpagina");
            return;
        }
        //Redirect naar de resultatenpagina van de stemming als de stemming afgelopen is
        if (StemmingActief == false)
        {
            Response.Redirect("ResultatenPagina.aspx?Stemming=" + Stemming);
            return;
        }
    }

    protected void ImageButtonOkee_Click(object sender, EventArgs e)
    {
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);

        //Haalt de stemming uit de URL en het telefoonnr uit de textbox
        string Stemming = Request.QueryString["Stemming"];
        string Telnr = txtbx_telnr.Text;
        bool TelNietNieuw = false;

        Nummercontrole check = new Nummercontrole();
        SqlCommand AlleNummers = new SqlCommand("SELECT HashTelNr FROM UC", sqlConnection);
        sqlConnection.Open();
        SqlDataAdapter da = new SqlDataAdapter(AlleNummers);
        DataTable dt = new DataTable();
        da.Fill(dt);
        //Voor elke opgeslagen hash van een telnr
        foreach (DataRow dr in dt.Rows)
        {
            //Checkt of het telefoonnr voorkomt in de database
            if (HashGenereren.checkHash(Telnr, dr["HashTelNr"].ToString()))
            {
                TelNietNieuw = true;
            }
        }
        //Als het nummer nog niet gehasht in de database staat
        if (TelNietNieuw == false)
        {
            //Checken of een telefoonnummer wel een geldig nummer is
            if (check.Nummercheck(Convert.ToString("06" + txtbx_telnr.Text)))
            {
                //Checkt of er nog unieke codes zijn voor de gekozen stemming
                SqlCommand NieuweCodeQuery = new SqlCommand("SELECT Top 1 UniekeCode FROM UC WHERE HashTelNr IS NULL AND StemmingsNaam ='" + Stemming + "';", sqlConnection);
                string NieuweCode = Convert.ToString(NieuweCodeQuery.ExecuteScalar());

                if (NieuweCode != "")
                {
                    //Hasht het telefoonnummer en verbindt het met een unieke code
                    Telnr = HashGenereren.Genereer(Telnr);
                    SqlCommand NummerToevoegen = new SqlCommand("UPDATE UC SET HashTelNr = '" + Telnr + "' WHERE UniekeCode = '" + NieuweCode + "';", sqlConnection);
                    NummerToevoegen.ExecuteNonQuery();
                    sqlConnection.Close();

                    //Vult gegevens in van het e-mailadres om mailtjes te sturen naar een e-mail adres
                    //Deze mailtjes worden omgezet naar een SMS'je door het bedrijf van dit e-mailadres
                    //Deze SMS'jes worden vervolgens verstuurd naar het telefoonnummer van de gebruiker
                    string password = "Blockchain123";
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    //De e-mail wordt verstuurd vanaf dit g-mail account
                    mail.From = new MailAddress("winnovationwindesheim@gmail.com");
                    //Het telefoonnummer waar naar wordt verstuurd is het telefoonnummer in de tekstbox
                    mail.To.Add("06" + txtbx_telnr.Text + "@sms.informaxion.nl");
                    //mail.To.Add("personalthijsiedema@gmail.com");
                    mail.Subject = "2410, Winnovation";
                    mail.Body = "Hier is uw code voor de Winnovation: http://xxxxxxxx.ngrok.io/projectenoverzicht?Stemmer=" + NieuweCode;
                    //mail.Body = "Hier is uw code voor de Winnovation: http://www.winnovationexpo.nl/projectenoverzicht?Stemmer=" + NieuweCode;
                    //mail.Body = "Hier is uw code voor de Winnovation: localhost:50512/projectenoverzicht?Stemmer=" + NieuweCode;

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("winnovationwindesheim", password);
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);

                    lbl_Info.Visible = true;
                    lbl_Info.Text = "Uw code is verzonden per SMS";
                    txtbx_telnr.Text = "";
                }
                else
                {
                    lbl_Info.Visible = true;
                    lbl_Info.Text = "Geen stemcodes beschikbaar";
                    txtbx_telnr.Text = "";
                }
            }
            else
            {
                lbl_Info.Visible = true;
                lbl_Info.Text = "Dit nummer is ongeldig";
                txtbx_telnr.Text = "";
            }
        }
        else
        {
            lbl_Info.Visible = true;
            lbl_Info.Text = "Dit nummer is al gebruikt";
            txtbx_telnr.Text = "";
        }
    }

    //Elke button vult een nummer in in de textbox. Als een knop wordt ingedrukt wordt het informatielabel ook onzichtbaar.
    protected void Buttonnr1_Click(object sender, EventArgs e)
    {
        if (txtbx_telnr.Text.Length < 8) { txtbx_telnr.Text = txtbx_telnr.Text += "1"; lbl_Info.Visible = false; }
    }
    protected void Buttonnr2_Click(object sender, EventArgs e)
    {
        if (txtbx_telnr.Text.Length < 8) { txtbx_telnr.Text = txtbx_telnr.Text += "2"; lbl_Info.Visible = false; }
    }
    protected void Buttonnr3_Click(object sender, EventArgs e)
    {
        if (txtbx_telnr.Text.Length < 8) { txtbx_telnr.Text = txtbx_telnr.Text += "3"; lbl_Info.Visible = false; }
    }
    protected void Buttonnr4_Click(object sender, EventArgs e)
    {
        if (txtbx_telnr.Text.Length < 8) { txtbx_telnr.Text = txtbx_telnr.Text += "4"; lbl_Info.Visible = false; }
    }
    protected void Buttonnr5_Click(object sender, EventArgs e)
    {
        if (txtbx_telnr.Text.Length < 8) { txtbx_telnr.Text = txtbx_telnr.Text += "5"; lbl_Info.Visible = false; }
    }
    protected void Buttonnr6_Click(object sender, EventArgs e)
    {
        if (txtbx_telnr.Text.Length < 8) { txtbx_telnr.Text = txtbx_telnr.Text += "6"; lbl_Info.Visible = false; }
    }
    protected void Buttonnr7_Click(object sender, EventArgs e)
    {
        if (txtbx_telnr.Text.Length < 8) { txtbx_telnr.Text = txtbx_telnr.Text += "7"; lbl_Info.Visible = false; }
    }
    protected void Buttonnr8_Click(object sender, EventArgs e)
    {
        if (txtbx_telnr.Text.Length < 8) { txtbx_telnr.Text = txtbx_telnr.Text += "8"; lbl_Info.Visible = false; }
    }
    protected void Buttonnr9_Click(object sender, EventArgs e)
    {
        if (txtbx_telnr.Text.Length < 8) { txtbx_telnr.Text = txtbx_telnr.Text += "9"; lbl_Info.Visible = false; }
    }
    protected void Buttonnr0_Click(object sender, EventArgs e)
    {
        if (txtbx_telnr.Text.Length < 8) { txtbx_telnr.Text = txtbx_telnr.Text += "0"; lbl_Info.Visible = false; }
    }

    //De terugknop verwijdert het laatste nummer uit de tekstbox
    protected void ButtonBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (txtbx_telnr.Text.Length > 0) { txtbx_telnr.Text = txtbx_telnr.Text.Remove(txtbx_telnr.Text.Length - 1); lbl_Info.Visible = false; }
    }
}