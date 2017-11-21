using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Security
/// </summary>
public class Security
{
    private string digits = "0123456789";
    private string upperLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string lowerLetters = "abcdefghijklmnopqrstuvwxyz";
    public Security()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string generateRandomString(int size)
    {
        string chars = digits + upperLetters+ lowerLetters;
        string randomStr = "";
        Random RandomClass = new Random();
        int len = chars.Length - 1;
        for (int i = 0; i < size; i++)
        {
            int pos = RandomClass.Next(0, len);
            randomStr += chars.Substring(pos, 1);
        }
        return randomStr;
    }

    public string generateRandomNumber(int size)
    {
        string chars = digits;
        string randomStr = "";
        Random RandomClass = new Random();
        int len = chars.Length - 1;
        for (int i = 0; i < size; i++)
        {
            int pos = RandomClass.Next(0, len);
            randomStr += chars.Substring(pos, 1);
        }
        return randomStr;
    }

    public string encodePassword(string decodedPass)
    {
        var decodedPassBytes = System.Text.Encoding.UTF8.GetBytes(decodedPass);
        string encodedPass = Convert.ToBase64String(decodedPassBytes);

        return encodedPass;
    }

    public string decodePassword(string encodedPass)
    {
        var encodedPassBytes = Convert.FromBase64String(encodedPass);
        string decodedPassword = System.Text.Encoding.UTF8.GetString(encodedPassBytes);
        return decodedPassword;
    }

}