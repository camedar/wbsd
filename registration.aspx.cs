using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString);
            conn.Open();
            string sql_check_user = "SELECT COUNT(1) FROM people WHERE username = '" + txt_username.Text + "'";
            SqlCommand com = new SqlCommand(sql_check_user,conn);
            int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
            if(temp == 1)
            {
                Response.Write("The username is already in use");
            }
            conn.Close();
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString);
            conn.Open();
            string sql_insert_person = "INSERT INTO people(firstname,surname,username,email,password) VALUES(@firstname,@surname,@username,@email,@password)";
            SqlCommand com = new SqlCommand(sql_insert_person, conn);
            com.Parameters.AddWithValue("@firstname",txt_firstname.Text);
            com.Parameters.AddWithValue("@surname", txt_surname.Text);
            com.Parameters.AddWithValue("@username", txt_username.Text);
            com.Parameters.AddWithValue("@email", txt_email.Text);
            com.Parameters.AddWithValue("@password", txt_password.Text);
            com.ExecuteNonQuery();
            Response.Write("The registration has been successful");
            conn.Close();
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }


}