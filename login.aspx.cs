using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(this.isUserSignedIn())
        {
            Response.Redirect("home.aspx");
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            DatabaseOperations db = new DatabaseOperations();
            Security sc = new Security();
            
            Hashtable cond = new Hashtable();
            cond.Add("email", txt_username.Text.Trim());

            string[] fields = { "userId", "username","firstname", "surname", "email" };
            ArrayList queryData = db.selectRows("users", fields, cond);
            Hashtable a = (Hashtable)queryData[0];
            this.SessionManagement.UserId = (string)a["userId"];
            this.SessionManagement.UserName = (string)a["username"];
            this.SessionManagement.UserFirstname = (string)a["firstname"];
            this.SessionManagement.UserSurname = (string)a["surname"];
            this.SessionManagement.UserEmail = (string)a["email"];

            Response.Redirect("home.aspx");
        }
    }

    protected void vld_login_credentials_ServerValidate(object source, ServerValidateEventArgs args)
    {
        Security sc = new Security();
        Hashtable cond = new Hashtable();
        string encodedPassword = sc.encodePassword(txt_password.Text.Trim());
        cond.Add("email", txt_username.Text);
        cond.Add("password", encodedPassword);
        //cond.Add("status", "1");
        DatabaseOperations db = new DatabaseOperations();
        //int temp = db.countRows("users", cond);
 

        string[] fields = { "firstname", "status", "email" };
        ArrayList queryData = db.selectRows("users", fields, cond);

        if (queryData.Count > 0)
        {
            args.IsValid = true;
            Hashtable a = (Hashtable)queryData[0];
            string status = (string)a["status"];
            if (status == "0") {
                string email = (string)a["email"];

                Response.Cookies["UserInformationWBSD"]["NewEmail"] = email;
                Response.Cookies["UserInformation"].Expires = DateTime.Now.AddHours(1);
                Response.Redirect("confirmEmail.aspx");
            }
        }
        else
        {
            args.IsValid = false;
        }
    }

    protected void lnk_singup_Click(object sender, EventArgs e)
    {
        Response.Redirect("signup.aspx");
    }

    protected void lnk_resetpassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("resetpassword.aspx");
    }
}