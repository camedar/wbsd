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
        DatabaseOperations db = new DatabaseOperations();
        int temp = db.countRows("users", cond);
        if (temp > 0)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }

    
}