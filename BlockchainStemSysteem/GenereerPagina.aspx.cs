﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
        SqlCommand WwChecken = new SqlCommand("SELECT Hash FROM Wachtwoord ORDER BY Id DESC", sqlConnection);
        string code = (string)(Session["Login"]);
        string Wachtwoord = WwChecken.ExecuteScalar().ToString();
        if (code != Wachtwoord)
        {
            Response.Redirect("Inlogpagina");
        }
        sqlConnection.Close();

        //Maakt de tabel aan voor de projecten
        TableHeaderRow header = new TableHeaderRow();
        Tbl_Projecten.Rows.Add(header);
        TableHeaderCell headerTableCell1 = new TableHeaderCell();
        headerTableCell1.Text = "Teams:";
        header.Cells.Add(headerTableCell1);
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        //Maakt de projectenlist leeg als de gebruiker naar deze pagina komt vanuit een andere pagina (ipv het herladen van de pagina)
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
            //Minimaal 1 code
            if(hoeveelheidCodes >= 1)
            {
                //Maximaal 1000 codes
                if (hoeveelheidCodes <= 1000)
                {
                    if (Global.Projecten.Any())
                    {
                        //Maak een nieuwe stemming aan
                        SqlCommand NieuweStemming = new SqlCommand("INSERT INTO Stemming (StemmingsNaam, Actief) VALUES ('" + Txtbx_StemmingsNaam.Text + "', 'true');", sqlConnection);
                        NieuweStemming.ExecuteNonQuery();
                        for (int i = 1; i <= hoeveelheidCodes; i++)
                        {
                            //Genereer unieke codes met een lengte van 10 aan en zet deze in de database
                            SqlCommand CheckUniekeCode = new SqlCommand("INSERT INTO UC (UniekeCode, StemmingsNaam) VALUES ('" + RandomCodeGenereren.GenerateIdentifier(10).ToString() + "', '" + Txtbx_StemmingsNaam.Text + "');", sqlConnection);
                            CheckUniekeCode.ExecuteNonQuery();
                        }

                        foreach (string project in Global.Projecten)
                        {
                            //Zet de projecten in de database
                            SqlCommand MaakProjecten = new SqlCommand("INSERT INTO Project (Naam, StemmingsNaam) VALUES ('" + project + "', '" + Txtbx_StemmingsNaam.Text + "');", sqlConnection);
                            MaakProjecten.ExecuteNonQuery();
                        }

                        //Maak een genesisblock aan
                        SqlCommand BlockMaken = new SqlCommand("INSERT INTO Block (BlockData, StemmingsNaam) VALUES ('" + RandomCodeGenereren.GenerateIdentifier(50) + "', '" + Txtbx_StemmingsNaam.Text + "')", sqlConnection);
                        BlockMaken.ExecuteNonQuery();

                        //Maak de lijst met projecten leeg en ga naar het overzicht van de beheerder
                        Global.Projecten.Clear();
                        sqlConnection.Close();
                        Response.Redirect("OverzichtBeheerder");
                    }
                    else
                    {
                        lbl_Info.Text = "Een stemming moet teams hebben";
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


    
    
    //Voegt een project toe aan de tabel van projecten
    protected void btn_ProjectToevoegen_Click(object sender, EventArgs e)
    {
        string project = txtbx_Project.Text;
        if (project != "")
        {
            foreach (string projectNaam in Global.Projecten)
            {
                //Checkt of de projectnaam al bestaat
                if (projectNaam.Contains(project))
                {
                    lbl_Info.Text = "Dit team is al toegevoegd";
                    vulTabel();
                    return;
                }
            }
            Global.Toevoegen(txtbx_Project.Text);
            lbl_Info.Text = "";
            vulTabel();
        }
        else
        {
            lbl_Info.Text = "Vul eerst een teamnaam in";
            vulTabel();
        }
        txtbx_Project.Text = string.Empty;
    }

    //Vult de tabel met de lijst Projecten uit de Global klasse
    protected void vulTabel()
    {
        Global.Projecten.Reverse();
        foreach (string project in Global.Projecten)
        {
            TableRow tRow2 = new TableRow();
            Tbl_Projecten.Rows.Add(tRow2);
            TableCell tCell2 = new TableCell();
            tCell2.Text = project;
            tRow2.Cells.Add(tCell2);
        }
        Global.Projecten.Reverse();
    }

    //Redirect naar het overzicht van de beheerder zonder iets op te slaan
    protected void btn_Overzicht_Click(object sender, EventArgs e)
    {
        Global.Projecten.Clear();
        Response.Redirect("OverzichtBeheerder");
    }

    protected void btn_Verwijderen_Click(object sender, EventArgs e)
    {
        //Als er projecten in de lijst staan
        if(Global.Projecten.Count > 0)
        {
            //Verwijder het nieuwste project
            Global.Projecten.RemoveRange(Global.Projecten.Count - 1, 1);
            vulTabel();
        }
    }
}