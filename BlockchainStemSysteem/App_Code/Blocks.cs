using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Blocks
/// </summary>
public static class Blocks
{
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
                blockstring += HashGenereren.Genereer((string)row["UniekeCode"]) + "&" + HashGenereren.Genereer((string)row["GestemdOp"]) + "&";
            }

            //Selecteer de string blockdata van het nieuwste blok
            SqlCommand NieuwsteBlock = new SqlCommand("SELECT Blockdata FROM Block ORDER BY Id DESC", sqlConnection);

            //Voeg de eerste 16 karakters van het vorige blok toe aan het nieuwe blok
            string VorigeBlock = (string)NieuwsteBlock.ExecuteScalar();
            blockstring += VorigeBlock.Substring(0, 16);

            //Zet Inblock op 'true' zodat deze stemmen niet nog een keer worden meegenomen in een block
            SqlCommand UpdateGestemdOp = new SqlCommand("UPDATE Top(5) UC SET InBlock='True' WHERE GestemdOp IS NOT NULL AND InBlock = 'False' AND StemmingsNaam = 'Stemming1'", sqlConnection);
            UpdateGestemdOp.ExecuteScalar();
            SqlCommand BlockToevoegen = new SqlCommand("INSERT INTO Block (Blockdata) VALUES ('" + blockstring + "')", sqlConnection);
            BlockToevoegen.ExecuteScalar();
        }

        sqlConnection.Close();
    }
}