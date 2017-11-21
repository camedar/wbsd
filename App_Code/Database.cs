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

    protected SqlConnection openConnection()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString);
        conn.Open();
        return conn;
    }

    protected void closeConnection(SqlConnection conn)
    {
        conn.Close();
    }

    protected SqlCommand executeQuery(string sql, SqlConnection conn, bool transactional)
    {
        SqlCommand com = null;
        if (!transactional)
        {
            com = new SqlCommand(sql, conn);
        }
        else
        {
            com = ExecuteQueryTransaction(conn, sql);
        }
        
        

        return com;
    }

    private SqlCommand ExecuteQueryTransaction(SqlConnection connection, string sql)
    {            
        SqlCommand command = connection.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = connection.BeginTransaction("Transaction");

        // Must assign both transaction object and connection
        // to Command object for a pending local transaction
        command.Connection = connection;
        command.Transaction = transaction;
        command.CommandText = sql;

        return command;
    }

    /*protected SqlCommand executeQuery(string table,string[] columns, Hashtable conditions)
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
    }  */
}