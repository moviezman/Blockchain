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
    public List<string> Projecten = new List<string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        TableHeaderRow header = new TableHeaderRow();
        Tbl_Projecten.Rows.Add(header);
        TableHeaderCell headerTableCell1 = new TableHeaderCell();
        headerTableCell1.Text = "Projecten:";
        header.Cells.Add(headerTableCell1);
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Global.Projecten.Clear();
        }
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
            vulTabel();
            Txtbx_StemmingsNaam.Text = "";
            return;
        }

        if (Txtbx_StemmingsNaam.Text == "")
        {
            lbl_Info.Text = "Vul een stemmingsnaam in";
            vulTabel();
            return;
        }

        int hoeveelheidCodes = Convert.ToInt32(txtbx_Nummer.Text);
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();
        SqlCommand CheckStemmingsNaam = new SqlCommand("SELECT COUNT(*) FROM UC WHERE StemmingsNaam = '" + Txtbx_StemmingsNaam.Text + "'", sqlConnection);
        SqlCommand CheckBestaandeProjecten = new SqlCommand("");
        int StemmingBestaat = (int)CheckStemmingsNaam.ExecuteScalar();

        if (StemmingBestaat > 0)
        {
            lbl_Info.Text = "Deze Stemming bestaat al";
            vulTabel();
        }
        else
        {
            if(hoeveelheidCodes >= 1)
            {
                if (hoeveelheidCodes <= 1000)
                {
                    if (Global.Projecten.Any())
                    {
                        SqlCommand NieuweStemming = new SqlCommand("INSERT INTO Stemming (StemmingsNaam, Actief) VALUES ('" + Txtbx_StemmingsNaam.Text + "', 'true');", sqlConnection);
                        for (int i = 1; i <= hoeveelheidCodes; i++)
                        {
                            SqlCommand CheckUniekeCode = new SqlCommand("INSERT INTO UC (UniekeCode, StemmingsNaam) VALUES ('" + GenerateIdentifier(10).ToString() + "', '" + Txtbx_StemmingsNaam.Text + "');", sqlConnection);
                            CheckUniekeCode.ExecuteNonQuery();
                        }

                        foreach (string project in Global.Projecten)
                        {
                            SqlCommand MaakProjecten = new SqlCommand("INSERT INTO Project (Naam, StemmingsNaam, AantalStemmen) VALUES ('" + project + "', '" + Txtbx_StemmingsNaam.Text + "', '0');", sqlConnection);
                            MaakProjecten.ExecuteNonQuery();
                        }

                        lbl_Info.Text = "Stemming met de naam " + Txtbx_StemmingsNaam.Text + " aangemaakt.";
                        Global.Projecten.Clear();
                    }
                    else
                    {
                        lbl_Info.Text = "Een stemming moet projecten hebben";
                        vulTabel();
                    }

                }
                else
                {
                    lbl_Info.Text = "Kies 1000 stemcodes of minder";
                    vulTabel();
                }
            }
            else
            {
                lbl_Info.Text = "Kies 1 of meer stemcodes";
                vulTabel();
            }
            
        }
        sqlConnection.Close();
    }


    
    

    protected void btn_ProjectToevoegen_Click(object sender, EventArgs e)
    {
        if (txtbx_Project.Text != "")
        {
            Global.Toevoegen(txtbx_Project.Text);
            vulTabel();
        }
        else
        {
            lbl_Info.Text = "Vul eerst een projectnaam in.";
            vulTabel();
        }
    }

    protected void vulTabel()
    {
        foreach (string project in Global.Projecten)
        {
            TableRow tRow2 = new TableRow();
            Tbl_Projecten.Rows.Add(tRow2);
            TableCell tCell2 = new TableCell();
            tCell2.Text = project;
            tRow2.Cells.Add(tCell2);
        }
    }
}