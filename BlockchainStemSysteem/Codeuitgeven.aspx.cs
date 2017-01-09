using System;
using System.Collections.Generic;
using System.Data;
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
        string Stemming = Request.QueryString["Stemming"];
        string Telnr = TextBox1.Text;
        bool TelNietNieuw = false;

        SqlCommand NieuweCodeQuery = new SqlCommand("SELECT Top 1 UniekeCode FROM UC WHERE HashTelNr IS NULL AND StemmingsNaam ='" + Stemming + "';", sqlConnection);
        // nummer toevoegen aan database
        SqlCommand UpdateNummer = new SqlCommand("INSERT INTO Stemmer VALUES(" + TextBox1.Text + ", 'true');", sqlConnection);

        Nummercontrole check = new Nummercontrole();
        SqlCommand AlleNummers = new SqlCommand("SELECT HashTelNr FROM UC", sqlConnection);
        sqlConnection.Open();
        SqlDataAdapter da = new SqlDataAdapter(AlleNummers);
        DataTable dt = new DataTable();
        da.Fill(dt);
        foreach (DataRow dr in dt.Rows)
        {
            //Checkt per telnr of een hash ervan al voorkomt in de database
            if (HashGenereren.checkHash(Telnr, dr["HashTelNr"].ToString()))
            {
                TelNietNieuw = true;
            }
        }
        //Kijken of het ingevulde nummer al gebruikt is
        if (TelNietNieuw == false)
        {
            //Checken of een telefoonnummer wel een geldig nummer is
            if (check.Nummercheck(Convert.ToString("06" + TextBox1.Text)))
            {
                string NieuweCode = Convert.ToString(NieuweCodeQuery.ExecuteScalar());

                if (NieuweCode != null)
                {
                    Telnr = HashGenereren.Genereer(Telnr);
                    SqlCommand NummerToevoegen = new SqlCommand("UPDATE UC SET HashTelNr = '" + Telnr + "' WHERE UniekeCode = '" + NieuweCode + "';", sqlConnection);
                    NummerToevoegen.ExecuteNonQuery();
                    sqlConnection.Close();

                    string password = "Blockchain123";
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

                    Label4.Visible = true;
                    Label4.Text = "Uw code is verzonden per SMS";
                    //Label4.Text = "Hier is uw code voor de Winnovation: localhost:50512/projectenoverzicht?Stemmer=" + NieuweCode;
                    TextBox1.Text = "";
                }
            }
            else
            {
                Label4.Visible = true;
                Label4.Text = "Dit nummer is ongeldig";
                TextBox1.Text = "";
            }
        }
        else
        {
            Label4.Visible = true;
            Label4.Text = "Dit nummer is al gebruikt";
        }
    }


    protected void Buttonnr1_Click(object sender, EventArgs e)
{
    if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "1"; Label4.Visible = false; }
}
protected void Buttonnr2_Click(object sender, EventArgs e)
{
    if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "2"; Label4.Visible = false; }
}
protected void Buttonnr3_Click(object sender, EventArgs e)
{
    if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "3"; Label4.Visible = false; }
}
protected void Buttonnr4_Click(object sender, EventArgs e)
{
    if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "4"; Label4.Visible = false; }
}
protected void Buttonnr5_Click(object sender, EventArgs e)
{
    if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "5"; Label4.Visible = false; }
}
protected void Buttonnr6_Click(object sender, EventArgs e)
{
    if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "6"; Label4.Visible = false; }
}
protected void Buttonnr7_Click(object sender, EventArgs e)
{
    if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "7"; Label4.Visible = false; }
}
protected void Buttonnr8_Click(object sender, EventArgs e)
{
    if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "8"; Label4.Visible = false; }
}
protected void Buttonnr9_Click(object sender, EventArgs e)
{
    if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "9"; Label4.Visible = false; }
}
protected void Buttonnr0_Click(object sender, EventArgs e)
{
    if (TextBox1.Text.Length < 8) { TextBox1.Text = TextBox1.Text += "0"; Label4.Visible = false; }
}
protected void ButtonnrB_Click(object sender, EventArgs e)
{
    if (TextBox1.Text.Length > 0) { TextBox1.Text = TextBox1.Text.Remove(TextBox1.Text.Length - 1); Label4.Visible = false; }
}
}