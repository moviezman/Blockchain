using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for Blocks
/// </summary>
public static class Blocks
{
    public static List<string> HashGebruikteCodes;
    public static List<string> GestemdOp;

    static Blocks()
    {
        GestemdOp = new List<string>();
        HashGebruikteCodes = new List<string>();
    }

    public static void MaakBlock(string Stemming)
    {
        string blockstring = "";
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();

        //Selecteer maximaal 5 uniekecodes en op wie ze gestemd hebben
        SqlDataAdapter StemData = new SqlDataAdapter("SELECT Top(5) UniekeCode, GestemdOp FROM UC WHERE GestemdOp IS NOT NULL AND InBlock = 'False' AND StemmingsNaam = '" + Stemming + "'", sqlConnection);
        
        //Voeg de gehashte uniekecodes en gestemdop toe aan de blockstring
        DataTable dt = new DataTable();
        StemData.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                blockstring += HashGenereren.Genereer((string)row["UniekeCode"]) + "#" + HashGenereren.Genereer((string)row["GestemdOp"]) + "&";
            }

            //Selecteer de string blockdata van het nieuwste blok
            SqlCommand NieuwsteBlock = new SqlCommand("SELECT Blockdata FROM Block WHERE StemmingsNaam = '"+ Stemming + "' ORDER BY Id DESC", sqlConnection);

            //Voeg de eerste 16 karakters van het vorige blok toe aan het nieuwe blok
            string VorigeBlock = (string)NieuwsteBlock.ExecuteScalar();
            blockstring += VorigeBlock.Substring(0, 16);

            //Zet Inblock op 'true' zodat deze stemmen niet nog een keer worden meegenomen in een block
            SqlCommand UpdateGestemdOp = new SqlCommand("UPDATE Top(5) UC SET InBlock='True' WHERE GestemdOp IS NOT NULL AND InBlock = 'False' AND StemmingsNaam = '"+ Stemming + "'", sqlConnection);
            UpdateGestemdOp.ExecuteScalar();
            SqlCommand BlockToevoegen = new SqlCommand("INSERT INTO Block (Blockdata, StemmingsNaam) VALUES ('" + blockstring + "', '" + Stemming + "')", sqlConnection);
            BlockToevoegen.ExecuteScalar();
        }

        sqlConnection.Close();
    }

    public static void Decodeer(string Stemming)
    {
        //Maak de lists leeg
        HashGebruikteCodes.Clear();
        GestemdOp.Clear();
        string Block = "";
        string Stem = "";
        string HashUniekeCode = "";
        string HashGestemdOp = "";
        bool HashUniekeCodeUniek = true;
        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);
        sqlConnection.Open();

        //Checkt of de stemming wel afgelopen is (zo niet, dan gebeurt er niks)
        SqlCommand CheckAfgelopen = new SqlCommand("SELECT Actief FROM Stemming WHERE StemmingsNaam = '" + Stemming + "'", sqlConnection);
        bool StemmingActief = Convert.ToBoolean(CheckAfgelopen.ExecuteScalar());
        if(StemmingActief == false)
        {
            SqlDataAdapter StemData = new SqlDataAdapter("SELECT BlockData FROM Block WHERE StemmingsNaam = '" + Stemming + "'", sqlConnection);

            //Voor elke string uit de db
            DataTable dt = new DataTable();
            StemData.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    //Haal de blokdata op uit de database
                    Block = (string)row["BlockData"];
                    //Check of het genesisblok is (lengte 50), zo nee, ga door
                    if (Block.Length != 50)
                    {
                        //Check of de laatste paar karakters overeenkomen met het begin van de vorige string

                        //Verwijder de laatste 16 karakters (eerste 16 karakters van vorige block)
                        Block = Block.Remove(Block.Length - 16);
                        //Block Ontleden in delen en toevoegen aan List

                        //Zolang er nog stemmen in het blok staan
                        while (Block.Length > 0)
                        {
                            Stem = Block.Substring(0, Block.IndexOf("&"));
                            Block = Block.Substring(Stem.Length + 1, Block.Length - Stem.Length - 1);
                            HashUniekeCode = Stem.Substring(0, Stem.IndexOf("#"));
                            HashGestemdOp = Stem.Substring(Stem.IndexOf("#") + 1);
                            
                            foreach(string code in HashGebruikteCodes)
                            {
                                if(HashUniekeCode == code)
                                {
                                    HashUniekeCodeUniek = false;
                                }
                            }

                            //Checkt of de unieke code uniek is
                            if (HashUniekeCodeUniek)
                            {
                                //Voegt de hash van de unieke code toe aan een lijst. Hierdoor kan hij niet nog een keer gebruikt worden.
                                HashGebruikteCodes.Add(HashUniekeCode);
                                SqlDataAdapter ucs = new SqlDataAdapter("SELECT UniekeCode FROM UC WHERE StemmingsNaam = '" + Stemming + "'", sqlConnection);
                                DataTable dtucs = new DataTable();
                                ucs.Fill(dtucs);

                                //Checkt of de UniekeCode bestaat
                                if (dtucs.Rows.Count > 0)
                                {
                                    foreach (DataRow ucsrow in dtucs.Rows)
                                    {
                                        if (HashGenereren.checkHash((string)ucsrow["UniekeCode"], HashUniekeCode))
                                        {
                                            SqlDataAdapter Projecten = new SqlDataAdapter("SELECT Naam FROM Project WHERE StemmingsNaam = '" + Stemming + "'", sqlConnection);
                                            DataTable dtprojecten = new DataTable();
                                            Projecten.Fill(dtprojecten);

                                            //Checkt of het project bestaat
                                            if (dtprojecten.Rows.Count > 0)
                                            {
                                                foreach (DataRow projectrow in dtprojecten.Rows)
                                                {
                                                    if (HashGenereren.checkHash((string)projectrow["Naam"], HashGestemdOp))
                                                    {
                                                        GestemdOp.Add((string)projectrow["Naam"]);
                                                        //Lijst meenemen naar Stemming
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            
                            //Checken of de UniekeCode bestaat
                            //Checken of het project waarop gestemd is bestaat
                            //Stem toevoegen aan list
                        }
                    }
                }
                //Maakt de lijst leeg zodat bij een volgende stemming er geen problemen met duplicate hashes zijn.
                HashGebruikteCodes.Clear();
            }
        }
        int count = GestemdOp.Count(s => GestemdOp.Contains(s));
        sqlConnection.Close();
    }
}