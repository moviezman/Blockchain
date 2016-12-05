using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DatabaseConnectie
/// </summary>
public class DatabaseConnectie
{
    //Michiel
    //public string dbConnectie = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Windesheim\Blockchain\C#\BlockchainStemSysteem\App_Data\Database.mdf;Integrated Security=True";

    //Thijs
    public string dbConnectie = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Documenten\Blockchain2\BlockchainStemSysteem\App_Data\Database.mdf;Integrated Security=True;Connect Timeout=30";
    public DatabaseConnectie()
    {
        
    }
}