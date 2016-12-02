using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        DataTable dt = new DataTable("Projecten");
        DataColumn column = new DataColumn();
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
        if (txtbx_Nummer.Text == "")
        {
            lbl_Info.Text = "Vul een hoeveelheid codes in";
            return;
        }

        if (Txtbx_StemmingsNaam.Text == "")
        {
            lbl_Info.Text = "Vul een stemmingsnaam in";
            return;
        }

        int hoeveelheidCodes = Convert.ToInt32(txtbx_Nummer.Text);
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();
        SqlCommand CheckStemmingsNaam = new SqlCommand("SELECT COUNT(*) FROM UC WHERE StemmingsNaam = '" + Txtbx_StemmingsNaam.Text + "'", sqlConnection);

        int StemmingBestaat = (int)CheckStemmingsNaam.ExecuteScalar();

        if (StemmingBestaat > 0)
        {
            lbl_Info.Text = "Deze Stemming bestaat al";
        }
        else
        {
            if (hoeveelheidCodes <= 1000)
            {
                for (int i = 1; i <= hoeveelheidCodes; i++)
                {
                    SqlCommand CheckUniekeCode = new SqlCommand("INSERT INTO UC (UniekeCode, StemmingsNaam) VALUES ('" + GenerateIdentifier(10).ToString() + "', '" + Txtbx_StemmingsNaam.Text + "');", sqlConnection);
                    CheckUniekeCode.ExecuteNonQuery();
                    lbl_Info.Text = "Stemming met de naam " + Txtbx_StemmingsNaam.Text + " aangemaakt.";
                }
            }
            else
            {
                lbl_Info.Text = "Kies 1000 stemcodes of minder.";
            }

        }
        sqlConnection.Close();
    }




    List<string> Projecten = new List<string>();
    string test;

    protected void btn_ProjectToevoegen_Click(object sender, EventArgs e)
    {
        if(txtbx_Project.Text != "")
        {
            this.Projecten.Add("test");
            this.Projecten.Add(txtbx_Project.Text);
            lbl_Info.Text = string.Join(", ", this.Projecten.ToArray());
        }
        else
        {
            lbl_Info.Text = "Vul eerst een projectnaam in.";
        }


        //// Total number of rows.
        //int rowCnt;
        //// Current row count.
        //int rowCtr;
        //// Total number of cells per row (columns).
        //int cellCtr;
        //// Current cell counter
        //int cellCnt;

        //rowCnt = int.Parse(TextBox1.Text);
        //cellCnt = int.Parse(TextBox2.Text);

        //for (rowCtr = 1; rowCtr <= rowCnt; rowCtr++)
        //{
        //    // Create new row and add it to the table.
        //    TableRow tRow = new TableRow();
        //    Table1.Rows.Add(tRow);
        //    for (cellCtr = 1; cellCtr <= cellCnt; cellCtr++)
        //    {
        //        // Create a new cell and add it to the row.
        //        TableCell tCell = new TableCell();
        //        tCell.Text = txtbx_Project.Text;
        //        tRow.Cells.Add(tCell);
        //    }
        //}
    }

    protected void GridView1_Load(object sender, EventArgs e)
    {
        int counter = 1;
    }
}