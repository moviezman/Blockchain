using System;
using System.Security.Cryptography;

//HashGenereren biedt het genereren van een hash met salt uit een string en het controleren van een string met een hash met salt
public static class HashGenereren
{
    //Genereert een hash met salt uit een string
    public static string Genereer(string teHashen)
    {
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
        var pbkdf2 = new Rfc2898DeriveBytes(teHashen, salt, 10000);
        byte[] hash = pbkdf2.GetBytes(20);
        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);
        string savedPasswordHash = Convert.ToBase64String(hashBytes);
        return savedPasswordHash;
    }

    //Checkt of een string en een hash overeenkomen
    public static bool checkHash(string wachtwoord, string wwhash)
    {
        if (wwhash.Length > 47)
        {
            string savedPasswordHash = wwhash;
            //Haal de bytes uit de wwhash
            byte[] hashBytes = Convert.FromBase64String(wwhash);
            //Haal de salt op
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            //Bereken de hash van het wachtwoord dat is meegegeven
            var pbkdf2 = new Rfc2898DeriveBytes(wachtwoord, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            //Vergelijk de resultaten
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] == hash[i])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
        return false;
    }
}