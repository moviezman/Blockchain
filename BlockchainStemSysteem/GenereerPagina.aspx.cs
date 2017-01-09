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
        //Checkt of de gebruiker ingelogd is als beheerder
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();
        SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord WHERE Id = '1'", sqlConnection);
        string code = (string)(Session["Login"]);
        string Wachtwoord = WwChecken.ExecuteScalar().ToString();
        if (code != Wachtwoord)
        {
            Response.Redirect("Inlogpagina");
        }
        sqlConnection.Close();


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

        //verbinden met Database
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
            //maximaal 1000 codes mogelijk
            if(hoeveelheidCodes >= 1)
            {
                if (hoeveelheidCodes <= 1000)
                {
                    if (Global.Projecten.Any())
                    {
                        SqlCommand NieuweStemming = new SqlCommand("INSERT INTO Stemming (StemmingsNaam, Actief) VALUES ('" + Txtbx_StemmingsNaam.Text + "', 'true');", sqlConnection);
                        NieuweStemming.ExecuteNonQuery();
                        for (int i = 1; i <= hoeveelheidCodes; i++)
                        {
                            SqlCommand CheckUniekeCode = new SqlCommand("INSERT INTO UC (UniekeCode, StemmingsNaam) VALUES ('" + RandomCodeGenereren.GenerateIdentifier(10).ToString() + "', '" + Txtbx_StemmingsNaam.Text + "');", sqlConnection);
                            CheckUniekeCode.ExecuteNonQuery();
                        }

                        foreach (string project in Global.Projecten)
                        {
                            SqlCommand MaakProjecten = new SqlCommand("INSERT INTO Project (Naam, StemmingsNaam) VALUES ('" + project + "', '" + Txtbx_StemmingsNaam.Text + "');", sqlConnection);
                            MaakProjecten.ExecuteNonQuery();
                        }

                        SqlCommand BlockMaken = new SqlCommand("INSERT INTO Block (BlockData, StemmingsNaam) VALUES ('" + RandomCodeGenereren.GenerateIdentifier(50) + "', '" + Txtbx_StemmingsNaam.Text + "')", sqlConnection);
                        BlockMaken.ExecuteNonQuery();

                        string Wachtwoord;

                        //Verander de Id naar de id van het wachtwoord dat je wilt als je hem verandert
                        SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord WHERE Id = '1'", sqlConnection);
                        Wachtwoord = WwChecken.ExecuteScalar().ToString();
                        sqlConnection.Close();

                        Global.Projecten.Clear();
                        Response.Redirect("OverzichtBeheerder");
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


    
    
    //project toevoegen
    protected void btn_ProjectToevoegen_Click(object sender, EventArgs e)
    {
        string project = txtbx_Project.Text;
        if (project != "")
        {
            foreach (string projectNaam in Global.Projecten)
            {
                if (projectNaam.Contains(project))
                {
                    lbl_Info.Text = "Dit project is al toegevoegd";
                    vulTabel();
                    return;
                }
            }
            Global.Toevoegen(txtbx_Project.Text);
            vulTabel();
            
        }
        else
        {
            lbl_Info.Text = "Vul eerst een projectnaam in";
            vulTabel();
        }
        txtbx_Project.Text = string.Empty;
        
    }

    //tabel vullen
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

    protected void btn_Overzicht_Click(object sender, EventArgs e)
    {
        Global.Projecten.Clear();
        Response.Redirect("OverzichtBeheerder");
    }
}