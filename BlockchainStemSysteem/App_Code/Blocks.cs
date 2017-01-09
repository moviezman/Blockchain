using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

// Het genereren van blocks voor de blockchain gebeurt op deze manier
public static class Blocks
{
    public static List<string> HashGebruikteCodes;
    public static List<string> GestemdOp;

    static Blocks()
    {
        //Initiliseer de lijsten zodat deze later gebruikt kunnen worden
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
        //Checkt of er nog stemmen zijn die niet in een block staan
        if (dt.Rows.Count > 0)
        {
            //Voor elke stem die nog niet in een block staat
            foreach (DataRow row in dt.Rows)
            {
                //Hash de UniekeCode en op wie gestemd is en voeg dit toe aan de blockstring
                //Voeg '#' tussen de gehaste 'UniekeCode' en gehashte 'GestemdOp' toe en '&' na 'GestemdOp'
                blockstring += HashGenereren.Genereer((string)row["UniekeCode"]) + "#" + HashGenereren.Genereer((string)row["GestemdOp"]) + "&";
            }

            //Selecteer de string blockdata van het nieuwste blok met de juiste stemming erbij
            SqlCommand NieuwsteBlock = new SqlCommand("SELECT Blockdata FROM Block WHERE StemmingsNaam = '"+ Stemming + "' ORDER BY Id DESC", sqlConnection);

            //Voeg de eerste 16 karakters van het vorige blok toe aan het nieuwe blok
            string VorigeBlock = (string)NieuwsteBlock.ExecuteScalar();
            blockstring += VorigeBlock.Substring(0, 16);

            //Zet Inblock op 'true' voor de stemmen die in de block worden gezet
            //zodat deze stemmen niet nog een keer kunnen worden meegenomen in een block
            SqlCommand UpdateGestemdOp = new SqlCommand("UPDATE Top(5) UC SET InBlock='True' WHERE GestemdOp IS NOT NULL AND InBlock = 'False' AND StemmingsNaam = '"+ Stemming + "'", sqlConnection);
            UpdateGestemdOp.ExecuteScalar();
            //Voeg de blockstring toe aan de database
            SqlCommand BlockToevoegen = new SqlCommand("INSERT INTO Block (Blockdata, StemmingsNaam) VALUES ('" + blockstring + "', '" + Stemming + "')", sqlConnection);
            BlockToevoegen.ExecuteScalar();
        }
        //Sluit de connectie
        sqlConnection.Close();
    }

    public static void Decodeer(string Stemming)
    {
        //Maak de lijsten leeg
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
                    //Check of het genesisblock is (lengte 50)
                    //Als het block een genesisblok is (dus geen stemdata)
                    //Sla het block dan over
                    if (Block.Length != 50)
                    {
                        //Check of de laatste paar karakters overeenkomen met het begin van de vorige string
                        





                        //Verwijder de laatste 16 karakters (eerste 16 karakters van vorige block)
                        Block = Block.Remove(Block.Length - 16);
                        //Block Ontleden in delen en toevoegen aan List

                        //Zolang er nog stemmen in het blok staan
                        while (Block.Length > 0)
                        {
                            //Haal de eerste stem op uit de blockstring
                            Stem = Block.Substring(0, Block.IndexOf("&"));
                            //Verwijder de eerste stemstring uit de blockstring
                            Block = Block.Substring(Stem.Length + 1, Block.Length - Stem.Length - 1);
                            //Haal de hash van de unieke code uit de stemstring
                            HashUniekeCode = Stem.Substring(0, Stem.IndexOf("#"));
                            //Haal de hash van het project waarop gestemd is op
                            HashGestemdOp = Stem.Substring(Stem.IndexOf("#") + 1);
                            
                            //Check of de UniekeCode al gebruikt is
                            foreach(string code in HashGebruikteCodes)
                            {
                                if(HashUniekeCode == code)
                                {
                                    HashUniekeCodeUniek = false;
                                }
                            }

                            //Als de UniekeCode nog niet gebruikt is
                            if (HashUniekeCodeUniek)
                            {
                                //Voegt de hash van de unieke code toe aan een lijst. Hierdoor kan hij niet nog een keer gebruikt worden.
                                HashGebruikteCodes.Add(HashUniekeCode);
                                SqlDataAdapter ucs = new SqlDataAdapter("SELECT UniekeCode FROM UC WHERE StemmingsNaam = '" + Stemming + "'", sqlConnection);
                                DataTable dtucs = new DataTable();
                                ucs.Fill(dtucs);

                                //Check of er unieke codes bij de stemming zijn
                                if (dtucs.Rows.Count > 0)
                                {
                                    //Voor elke UniekeCode
                                    foreach (DataRow ucsrow in dtucs.Rows)
                                    {
                                        //Als de hash overeenkomt met de UniekeCode uit de database
                                        if (HashGenereren.checkHash((string)ucsrow["UniekeCode"], HashUniekeCode))
                                        {
                                            SqlDataAdapter Projecten = new SqlDataAdapter("SELECT Naam FROM Project WHERE StemmingsNaam = '" + Stemming + "'", sqlConnection);
                                            DataTable dtprojecten = new DataTable();
                                            Projecten.Fill(dtprojecten);

                                            //Checkt of het project bestaat
                                            if (dtprojecten.Rows.Count > 0)
                                            {
                                                //Voor elk project
                                                foreach (DataRow projectrow in dtprojecten.Rows)
                                                {
                                                    //Check of de hash overeenkomt met 1 van de projecten uit de database van die stemming
                                                    if (HashGenereren.checkHash((string)projectrow["Naam"], HashGestemdOp))
                                                    {
                                                        //Voeg de projectnaam toe aan de GestemdOp lijst
                                                        GestemdOp.Add((string)projectrow["Naam"]);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //Maakt de lijst leeg zodat bij een volgende stemming er geen problemen met duplicate hashes zijn.
                HashGebruikteCodes.Clear();
            }
        }
        //Sluit de connectie
        sqlConnection.Close();
    }
}