using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class sendPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            DatabaseOperations db = new DatabaseOperations();
            Security sc = new Security();
            
            string email = txt_email.Text;
            Hashtable cond = new Hashtable();
            cond.Add("email",email);
            string[] columns = { "firstname","password" };
            ArrayList resp = db.selectRows("users", columns,cond);
            if(resp.Count > 0)
            {
                Hashtable respRow = (Hashtable)resp[0];
                string password = sc.decodePassword((string)respRow["password"]);
                string firstname = (string)respRow["firstname"];

                string textMessage = "Hello " + firstname + ", as requested this is the password of your account <b>" + password + "</b>";                

                Email sendEmail = new Email();
                sendEmail.send(email, "Password reminder", textMessage);
                lbl_success_sendPassword.Text = "The email has been sent";
            }
        }
    }

    protected void vld_existence_txt_email_ServerValidate(object source, ServerValidateEventArgs args)
    {
        DatabaseOperations db = new DatabaseOperations();
        Hashtable cond = new Hashtable();
        string email = txt_email.Text;
        cond.Add( "email", email);
        int numRows = db.countRows("users",cond);
        if(numRows == 0)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }

    }
}