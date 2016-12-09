using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Threading;
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
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);

        // query ophalen laatste code
        SqlCommand NieuweCodeQuery = new SqlCommand("SELECT Top 1 UniekeCode FROM UC WHERE Ingezet = 'false'", sqlConnection);
        // nummer toevoegen aan database
        SqlCommand UpdateNummer = new SqlCommand("INSERT INTO Stemmer VALUES(" + TextBox1.Text + ", 'true');", sqlConnection);
        // Kijkt of telefoonnnummer al is ingevoerd
        SqlCommand ZoekNummer = new SqlCommand("SELECT COUNT (HeeftStem) as Aantal FROM Stemmer WHERE TelefoonNummer = '" + TextBox1.Text + "';", sqlConnection);

        Nummercontrole check = new Nummercontrole();

        //Checken of een telefoonnummer wel een geldig nummer is
        if (check.Nummercheck(Convert.ToString("06" + TextBox1.Text)))
        //hieronder een tijdelijke check 
        //if (TextBox1.Text != "")
        {

            sqlConnection.Open();

            int Bestaandnummer = Convert.ToInt32(ZoekNummer.ExecuteScalar());

            //kijken of een nummer al gebruikt is
            if (Bestaandnummer == 0)
            {

                UpdateNummer.ExecuteNonQuery();

                string password = "Blockchain123";



                string NieuweCode = Convert.ToString(NieuweCodeQuery.ExecuteScalar());

                sqlConnection.Close();

                if (NieuweCode != null)
                {

                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("winnovationwindesheim@gmail.com");
                    //mail.To.Add(TextBox1.Text + "@sms.informaxion.nl");
                    mail.To.Add("personalthijsiedema@gmail.com");
                    mail.Subject = "2410, Winnovation";
                    //mail.Body = "Hier is uw code voor de Winnovation: http://www.winnovationwindesheim.nl/projectenoverzicht?" + NieuweCode;
                    mail.Body = "Hier is uw code voor de Winnovation: localhost:50512/projectenoverzicht?Stemmer=" + NieuweCode;

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("winnovationwindesheim", password);
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                    Label4.Text = "mail Send";

                    Label4.Visible = true;
                    Label4.Text += " Hier is uw code voor de Winnovation: localhost:50512/projectenoverzicht?Stemmer=" + NieuweCode;
                    TextBox1.Text = "";


                }
            }
            else
            {
                Label4.Visible = true;
                Label4.Text = "Dit nummer is al gebruikt";
                TextBox1.Text = "";


            }
        }

        else
        {
            Label4.Visible = true;
            Label4.Text = "Het nummer is ongeldig";
            TextBox1.Text = "";
        }

    }


    protected void Buttonnr1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "1"; }
    }
    protected void Buttonnr2_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "2"; }
    }
    protected void Buttonnr3_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "3"; }
    }
    protected void Buttonnr4_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "4"; }
    }
    protected void Buttonnr5_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "5"; }
    }
    protected void Buttonnr6_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "6"; }
    }
    protected void Buttonnr7_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "7"; }
    }
    protected void Buttonnr8_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "8"; }
    }
    protected void Buttonnr9_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "9"; }
    }
    protected void Buttonnr0_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "0"; }
    }
    protected void ButtonnrB_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text.Length > 0) { TextBox1.Text = TextBox1.Text.Remove(TextBox1.Text.Length - 1); }
    }
}