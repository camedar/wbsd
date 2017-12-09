using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for DatabaseOperations
/// </summary>
public class DatabaseOperations : Database
{
    public DatabaseOperations()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int countRows(string table, Hashtable conditions)
    {   
        string cond = "";
        if (conditions.Count > 0)
        {
            cond = " WHERE ";
            foreach (string key in conditions.Keys)
            {
                string val = (string)conditions[key].ToString();
                cond += key + " = '" + val + "' AND ";
            }
        }
        cond = cond.Substring(0, cond.Length - 5);

        string sql_check_user = "SELECT COUNT(1) AS numrows FROM " + table + cond;        
        SqlConnection conn = openConnection();
        SqlCommand com = executeQuery(sql_check_user, conn, false);
        //SqlCommand com = new SqlCommand(sql_check_user, conn);

        int i = Convert.ToInt32(com.ExecuteScalar().ToString());

        closeConnection(conn);
        return i;
    }

    public ArrayList selectRows(string table, string[] columns, Hashtable conditions)
    {
        ArrayList valuesList = new ArrayList();

        string cond = "";
        if (conditions.Count > 0)
        {
            cond = " WHERE ";
            foreach (string key in conditions.Keys)
            {
                string val = (string)conditions[key].ToString();
                cond += key + " = '" + val + "' AND ";
            }
        }
        cond = cond.Substring(0, cond.Length - 5);

        string columnsStr = string.Join(",",columns);

        string sql_check_user = "SELECT " + columnsStr + " FROM " + table + cond;
   
        SqlConnection conn = openConnection();
        SqlCommand com = executeQuery(sql_check_user, conn, false);
        //SqlCommand com = new SqlCommand(sql_check_user, conn);
        SqlDataReader dataReader = com.ExecuteReader();
        int numberFields = dataReader.FieldCount;

        while (dataReader.Read())
        {
            Hashtable data = new Hashtable();
            for (int i=0;i<numberFields;i++)
            {
                string val = dataReader[i].ToString();
                string key = columns[i];
                data.Add(columns[i], val);
            }
            
            valuesList.Add(data);
        }

        //int i = Convert.ToInt32(com.ExecuteScalar().ToString());


        closeConnection(conn);
        return valuesList;
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
                string val = (string)conditions[key].ToString();
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
                string val_ = (string)columns[key_].ToString();
                cols += key_ + " = '" + val_ + "',";
            }
        }
        cols = cols.Substring(0, cols.Length - 1);

        try
        {
            SqlConnection conn = openConnection();
            string sql_update_person = "UPDATE " + table + " SET " + cols + cond;
            SqlCommand com = executeQuery(sql_update_person, conn, false);
            //SqlCommand com = new SqlCommand(sql_update_person, conn);
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

    public string[] insertRow(string table, Hashtable columns)
    {
        string[] reponse = new string[2];
        string cols = "", vals = "";
        if (columns.Count > 0)
        {
            foreach (string key_ in columns.Keys)
            {
                string val_ = (string)columns[key_].ToString();
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
            SqlCommand com = executeQuery(sql_update_person, conn, false);
            //SqlCommand com = new SqlCommand(sql_update_person, conn);
            //com.Parameters.AddWithValue("@firstname", txt_firstname.Text);
            //com.Parameters.AddWithValue("@surname", txt_surname.Text);
            //com.Parameters.AddWithValue("@username", txt_username.Text);
            //com.Parameters.AddWithValue("@email", txt_email.Text);
            //com.Parameters.AddWithValue("@password", txt_password.Text);
            com.ExecuteNonQuery();
            closeConnection(conn);

            reponse[0] = "1";
            reponse[1] = "Record inserted succesfully";
            return reponse;
        }
        catch (Exception ex)
        {
            reponse[0] = "0";
            reponse[1] = ex.Message.ToString();
            Console.Write(ex.Message.ToString());
            return reponse;
        }
    }

    public string[] insertRowTransaction(string table, Hashtable columns)
    {
        string[] reponse = new string[2];
        string cols = "", vals = "";
        if (columns.Count > 0)
        {
            foreach (string key_ in columns.Keys)
            {
                string val_ = (string)columns[key_].ToString();
                cols += key_ + ",";
                vals += "'" + val_ + "',";
            }
        }
        cols = cols.Substring(0, cols.Length - 1);
        vals = vals.Substring(0, vals.Length - 1);

        SqlConnection conn = openConnection();
        string sql_update_person = "INSERT INTO " + table + "(" + cols + ") VALUES(" + vals + ")";
        SqlCommand com = executeQuery(sql_update_person,conn,true);
            
        SqlTransaction transaction;
        transaction = com.Transaction;


        try
        {            
            com.ExecuteNonQuery();

            // Attempt to commit the transaction.
            transaction.Commit();
            Console.WriteLine("Insertion commited.");

            reponse[0] = "1";
            reponse[1] = "Record inserted succesfully";
            closeConnection(conn);
            return reponse;            
        }
        catch (Exception ex)
        {
            Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
            Console.WriteLine("  Message: {0}", ex.Message);

            

            // Attempt to roll back the transaction.
            try
            {
                transaction.Rollback();
            }
            catch (Exception ex2)
            {
                // This catch block will handle any errors that may have occurred
                // on the server that would cause the rollback to fail, such as
                // a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                Console.WriteLine("  Message: {0}", ex2.Message);
            }

            closeConnection(conn);

            reponse[0] = "1";
            reponse[1] = ex.Message.ToString();
            Console.Write(ex.Message.ToString());
            return reponse;
        }
                          
    }

    public ArrayList executeStoredProcedure(string spName, Hashtable conditions)
    {
        ArrayList valuesList = new ArrayList();

        SqlConnection conn = openConnection();
        SqlCommand com = executeQuery(spName, conn, false);
        com.CommandType = CommandType.StoredProcedure;
        if (conditions.Count > 0)
        {
            foreach (string key in conditions.Keys)
            {
                com.Parameters.Add("@" + key, SqlDbType.VarChar).Value = (string)conditions[key].ToString();
            }
        }        
        

        using (SqlDataReader dataReader = com.ExecuteReader())
        {
            int numberFields = dataReader.FieldCount;
            while (dataReader.Read())
            {
                ArrayList data = new ArrayList();
                for (int i = 0; i < numberFields; i++)
                {
                    string val = dataReader[i].ToString();
                    data.Add(val);
                }

                valuesList.Add(data);
            }
        }
        
        closeConnection(conn);
        return valuesList;
    }

    public bool verifyExistenceUsernameInDB(string username,string userId)
    {
        Hashtable param = new Hashtable();
        if (userId != null)
        {
            param.Add("userId", userId);
        }
        param.Add("userName", username);
        ArrayList resp = executeStoredProcedure("verifyExistenceUsername", param);
        ArrayList item = (ArrayList)resp[0];
        int i;
        Int32.TryParse((string)item[0], out i);
        if (i > 0)
        {
            return true;
        }

        return false;
    }

    public bool verifyExistenceEmailInDB(string email, string userId)
    {
        Hashtable param = new Hashtable();
        if (userId != null)
        {
            param.Add("userId", userId);
        }
        param.Add("userEmail", email);
        ArrayList resp = executeStoredProcedure("verifyExistenceEmail", param);
        ArrayList item = (ArrayList)resp[0];
        int i;
        Int32.TryParse((string)item[0], out i);
        if (i > 0)
        {
            return true;
        }

        return false;
    }


}