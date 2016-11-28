using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    static readonly char[] AvailableCharacters =
    {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
        'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
        'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
    };

    internal static string GenerateIdentifier(int length)
    {
        char[] identifier = new char[length];
        byte[] randomData = new byte[length];

        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(randomData);
        }

        for (int idx = 0; idx < identifier.Length; idx++)
        {
            int pos = randomData[idx] % AvailableCharacters.Length;
            identifier[idx] = AvailableCharacters[pos];
        }

        return new string(identifier);
    }

    protected void btn_Genereer_Click(object sender, EventArgs e)
    {
        if(txtbx_Nummer.Text == "")
        {
            return;
        }

        if(Txtbx_StemmingsNaam.Text == "")
        {
            return;
        }

        int hoeveelheidCodes = Convert.ToInt32(txtbx_Nummer.Text);
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();
        //SqlCommand CheckStemmingsNaam = new SqlCommand("SELECT StemmingsNaam FROM UC WHERE StemmingsNaam = '" + Txtbx_StemmingsNaam.Text + "'", sqlConnection);
        //SqlCommand CheckStemmingsNaam = new SqlCommand("SELECT StemmingsNaam FROM UC WHERE StemmingsNaam = 'Test'", sqlConnection);
        //int StemmingBestaat = (int)CheckStemmingsNaam.ExecuteScalar();
        //if(StemmingBestaat > 0)
        //{
        //    lbl_Codes.Text = "Deze Stemming bestaat al, koekwaus";
        //} else
        //{
        if(hoeveelheidCodes < 1001)
        {
            for (int i = 1; i <= hoeveelheidCodes; i++)
            {
                SqlCommand CheckUniekeCode = new SqlCommand("INSERT INTO UC (UniekeCode, StemmingsNaam) VALUES ('" + GenerateIdentifier(10).ToString() + "', '" + Txtbx_StemmingsNaam.Text + "');", sqlConnection);
                CheckUniekeCode.ExecuteNonQuery();
                lbl_Info.Visible = true;
                lbl_Info.Text = "Stemming met de naam " + Txtbx_StemmingsNaam.Text + " aangemaakt.";
            }
        }
        else
        {
            lbl_Info.Visible = true;
            lbl_Info.Text = "Kies 1000 stemcodes of minder.";
        }
            
        //}
        sqlConnection.Close();
    }
}