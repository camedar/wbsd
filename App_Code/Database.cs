using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System;

/// <summary>
/// Summary description for dbOperations
/// </summary>
public class Database
{
    public Database()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public SqlConnection openConnection()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString);
        conn.Open();
        return conn;
    }

    public void closeConnection(SqlConnection conn)
    {
        conn.Close();
    }

    public SqlCommand executeQuery(string table,string[] columns, Hashtable conditions)
    {
        SqlConnection conn = openConnection();
        string cols = string.Join(",",columns);
        string cond = "";
        if (conditions.Count > 0)
        {
            cond = " WHERE ";
            foreach (string key in conditions.Keys)
            {
                string val = (string)conditions[key];
                if (val.Contains("%"))
                {
                    cond += key + " LIKE '" + val + "' AND ";
                }
                else
                {
                    cond += key + " = '" + val + "'";
                }

            }
        }
        cond = cond.Substring(0,cond.Length - 5);
        string sql_check_user = "SELECT " + cols + " FROM " + table + cond;
        SqlCommand com = new SqlCommand(sql_check_user, conn);
        closeConnection(conn);

        return com;
    }

    public int countRows(string table, Hashtable conditions)
    {
        SqlConnection conn = openConnection();
        string cond = "";
        if (conditions.Count > 0)
        {
            cond = " WHERE ";
            foreach (string key in conditions.Keys)
            {
                string val = (string)conditions[key];
                cond += key + " = '" + val + "' AND ";
            }
        }
        cond = cond.Substring(0, cond.Length - 5);
        string sql_check_user = "SELECT COUNT(1) AS numrows FROM " + table + cond;
        Console.WriteLine(sql_check_user);
        SqlCommand com = new SqlCommand(sql_check_user, conn);

        int i = Convert.ToInt32(com.ExecuteScalar().ToString());

        closeConnection(conn);
        return i;
    }

    public void updateRows(string table, Hashtable columns, Hashtable conditions)
    {
        string cols = "";
        string cond = "";
        if (conditions.Count > 0)
        {
            cond = " WHERE ";
            foreach (string key in conditions.Keys)
            {
                string val = (string)conditions[key];
                if (val.Contains("%"))
                {
                    cond += key + " LIKE '" + val + "' AND ";
                }
                else
                {
                    cond += key + " = '" + val + "' AND ";
                }

            }
        }
        cond = cond.Substring(0, cond.Length - 5);

        if (columns.Count > 0)
        {
            foreach (string key_ in columns.Keys)
            {
                string val_ = (string)columns[key_];
                cols += key_ + " = '" + val_ + "',";
            }
        }
        cols = cols.Substring(0, cols.Length - 1);

        try
        {
            SqlConnection conn = openConnection();
            string sql_update_person = "UPDATE " + table + " SET " + cols + cond;
            SqlCommand com = new SqlCommand(sql_update_person, conn);
            //com.Parameters.AddWithValue("@firstname", txt_firstname.Text);
            //com.Parameters.AddWithValue("@surname", txt_surname.Text);
            //com.Parameters.AddWithValue("@username", txt_username.Text);
            //com.Parameters.AddWithValue("@email", txt_email.Text);
            //com.Parameters.AddWithValue("@password", txt_password.Text);
            com.ExecuteNonQuery();
            closeConnection(conn);
        }
        catch (Exception ex)
        {
            //return ex.ToString();
        }
    }

    public void insertRow(string table, Hashtable columns)
    {
        string cols = "", vals = "";
        if (columns.Count > 0)
        {
            foreach (string key_ in columns.Keys)
            {
                string val_ = (string)columns[key_];
                cols += key_ + ",";
                vals += "'" + val_ + "',";
            }
        }
        cols = cols.Substring(0, cols.Length - 1);
        vals = vals.Substring(0, vals.Length - 1);

        try
        {
            SqlConnection conn = openConnection();
            string sql_update_person = "INSERT INTO " + table + "(" + cols + ") VALUES(" + vals + ")";
            SqlCommand com = new SqlCommand(sql_update_person, conn);
            //com.Parameters.AddWithValue("@firstname", txt_firstname.Text);
            //com.Parameters.AddWithValue("@surname", txt_surname.Text);
            //com.Parameters.AddWithValue("@username", txt_username.Text);
            //com.Parameters.AddWithValue("@email", txt_email.Text);
            //com.Parameters.AddWithValue("@password", txt_password.Text);
            com.ExecuteNonQuery();
            closeConnection(conn);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    public string generateRandomString()
    {
        string chars = "ABCEDEFGHIJKLMNOPQRSTVWXYZabcdefghijklmnopqrstvwxyz0123456789";
        string randomStr = "";
        Random RandomClass = new Random();
        int len = chars.Length - 1;
        for(int i=0; i<16; i++)
        {
            int pos = RandomClass.Next(0, len);
            randomStr += chars.Substring(pos,1);
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