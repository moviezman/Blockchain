using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Genereerd een hash uit het telnr op het moment dat een stemcode wordt opgevraagd
/// Er wordt gecontroleerd of hetzelfde stemcode en nummer wordt gebruikt bij het toevoegen van de stem
/// </summary>
public class HashToBlock
{

    public int TelefoonNummer;
    public string StemCode;
    public string Hash;
    //Hier de naam van de datatable invullen
    public string naamDataTable = "";
    //Hier de naam van de tabel invullen, tabel moet string bevatten 
    public string tabelInDataTable = "";

    public HashToBlock(string tel)
    {
        //Hier wordt de hash gegenereerd, in de vorm van een sha256 hash
        //Er is gekozen voor 256 bits versleuteling omdat deze niet te ontsleutelen is
        //Daarnaasts is deze snel uit te rekenen om te versleutelen 
        SHA256 sha256 = SHA256Managed.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(tel);
        byte[] hash = sha256.ComputeHash(bytes);
        string hashString = Convert.ToString(hash);


        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);

        string hashToevoegen = "INSERT INTO " + naamDataTable + " VALUES ('" + hashString + "');";
        SqlCommand insertHash = new SqlCommand(hashToevoegen, sqlConnection);

        sqlConnection.Open();
        insertHash.ExecuteNonQuery();
        sqlConnection.Close();
    }

    public bool CheckHash(string tel)
    {
        //Hier wordt de hash opnieuw gegenereerd, in de vorm van een sha256 hash
        //De hash wordt vergeleken met de hash in de database
        //Klopt deze hash niet dan wordt de gebruiker terug gestuurd
        SHA256 sha256 = SHA256Managed.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(tel);
        byte[] hash = sha256.ComputeHash(bytes);
        string hashString = Convert.ToString(hash);


        DatabaseConnectie dbconnect = new DatabaseConnectie();
        SqlConnection sqlConnection = new SqlConnection(dbconnect.dbConnectie);

        string hashSelecteren = "SELECT " + tabelInDataTable + " FROM " + naamDataTable + " WHERE " + tabelInDataTable + " = " + hashString + ";";
        SqlCommand selectHash = new SqlCommand(hashSelecteren, sqlConnection);

        sqlConnection.Open();
        string returnHash = Convert.ToString(selectHash.ExecuteScalar());
        sqlConnection.Close();
        
        if(returnHash == hashString)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void MineHash(string hashVanTel)
    {
        for (int i = 0610000000; i < 0689999999; i++)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(Convert.ToString(i));
            byte[] hash = sha256.ComputeHash(bytes);
            string hashString = Convert.ToString(hash);

            if(hashVanTel == hashString)
            {
                return;
            }
        }
        // sql code om stem te verweideren die te maken heeft met deze code
        // kan zijn dat hier een te nummer bij moet of team waarop gestemd is om hier een goede check op te hebben 

        
    }
}