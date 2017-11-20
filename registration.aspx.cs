using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;

public partial class registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString);
            //conn.Open();
            Hashtable cond = new Hashtable();
            cond.Add("email",txt_email.Text);
            Database db = new Database();            
            int temp = db.countRows("users", cond);
            if (temp == 1)
            {
                Response.Write("The email " + txt_email.Text + " is already in use");
            }
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        Hashtable cond = new Hashtable();
        cond.Add("email", txt_email.Text);
        Database db = new Database();
        int temp = db.countRows("users", cond);
        if (temp == 0)
        {
            Hashtable dataArr = new Hashtable();
            string confirmationCode = db.generateRandomString();
            string encodedPassword = db.encodePassword(txt_password.Text);
            dataArr.Add("firstname", txt_firstname.Text);
            dataArr.Add("surname", txt_surname.Text);
            dataArr.Add("email", txt_email.Text);
            dataArr.Add("password", encodedPassword);
            dataArr.Add("confirmationCode", confirmationCode);

            db.insertRow("users",dataArr);
            string textMessage = "Hello " + txt_firstname.Text + ", welcome to our site, please confirm your account by clicking <b><a href='http://localhost:62589/confirmAccount.aspx?h=" + confirmationCode + "'>here</a></b>";
            try
            {
                MailMessage message = new MailMessage("m.camilo@gmail.com", txt_email.Text, "Confirmation email", textMessage);
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential("m.camilo@gmail.com", "L%OnCFAux1");
                client.Send(message);
            }
            catch (Exception ex)
            {
            
            }
        }
    }


}