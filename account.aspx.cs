using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Net;
using System.Web.UI.HtmlControls;

public partial class account : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string refererPath = Request.Headers["Referer"];

        if (!IsPostBack)
        {
            showSectionAccount(0);
        }
    }

    protected void showSectionAccount(int idSection)
    {
        userForm.Visible = false;
        pnl_newEmail.Visible = false;
        pnl_newPassword.Visible = false;
        pnl_newUserName.Visible = false;
        
        li_profile.Attributes.Add("class", "list-group-item");
        lnk_edit_profile.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2C699E");
        li_newemail.Attributes.Add("class", "list-group-item");
        lnk_edit_email.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2C699E");
        li_newpassword.Attributes.Add("class", "list-group-item");
        lnk_edit_password.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2C699E");
        li_newusername.Attributes.Add("class", "list-group-item");
        lnk_edit_username.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2C699E");
        if (idSection == 0)
        {
            userForm.Visible = true;
            li_profile.Attributes.Add("class", "list-group-item active");
            lnk_edit_profile.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        }
        else if (idSection == 1)//200,84,84  35 153 213
        {
            pnl_newEmail.Visible = true;
            li_newemail.Attributes.Add("class", "list-group-item active");            
            lnk_edit_email.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        }
        else if (idSection == 2)
        {
            pnl_newPassword.Visible = true;
            li_newpassword.Attributes.Add("class", "list-group-item active");
            lnk_edit_password.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        }
        else if (idSection == 3)
        {
            pnl_newUserName.Visible = true;
            li_newusername.Attributes.Add("class", "list-group-item active");
            lnk_edit_username.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        }
    }

    protected void userForm_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        //Security sec = new Security();
        //e.NewValues["password"] = sec.encodePassword((string)e.NewValues["password"]);
    }

    protected void userForm_DataBound(object sender, EventArgs e)
    {
        //Security sc = new Security();
        //TextBox password_input = (TextBox)userForm.FindControl("txt_password");
        //password_input.Text = sc.decodePassword((string)password_input.Text);



    }

    protected void vld_existence_txt_newUsername_ServerValidate(object source, ServerValidateEventArgs args)
    {
        DatabaseOperations dbOp = new DatabaseOperations();
               
        bool usernameExists = dbOp.verifyExistenceUsernameInDB((string)txt_newUsername.Text, this.SessionManagement.UserId);
        
        if (usernameExists)
        {
            args.IsValid = false;
        }
        
    }

    protected void btn_newUsername_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            string email = (string)this.SessionManagement.UserEmail;
            //string oldUsername = (string)this.SessionManagement.UserName;
            string newUsername = (string)txt_newUsername.Text;

            DatabaseOperations db = new DatabaseOperations();
            //Security sc = new Security();
            Hashtable cond = new Hashtable();
            cond.Add("email", email);
            Hashtable cols = new Hashtable();
            cols.Add("username", newUsername);

            int temp = db.countRows("users", cond);
            if (temp == 1)
            {
                db.updateRows("users", cols, cond);
                //Response.Write("Login");
            }
        }
    }

    protected void btn_newPassword_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            DatabaseOperations db = new DatabaseOperations();
            Security sc = new Security();
            string email = (string)this.SessionManagement.UserEmail;
            //string oldUsername = (string)this.SessionManagement.UserName;
            string newPassword = (string)txt_password1.Text;
            newPassword = sc.encodePassword(newPassword);

            
            //Security sc = new Security();
            Hashtable cond = new Hashtable();
            cond.Add("email", email);
            Hashtable cols = new Hashtable();
            cols.Add("password", newPassword);

            int temp = db.countRows("users", cond);
            if (temp == 1)
            {
                db.updateRows("users", cols, cond);
                //Response.Write("Login");
            }
        }
    }

    protected void vld_verify_txt_currentPassword_ServerValidate(object source, ServerValidateEventArgs args)
    {
        Security sc = new Security();
        Hashtable cond = new Hashtable();
        string encodedPassword = sc.encodePassword(txt_currentPassword.Text.Trim());
        cond.Add("email", this.SessionManagement.UserEmail);
        cond.Add("password", encodedPassword);
        DatabaseOperations db = new DatabaseOperations();
        int temp = db.countRows("users", cond);
        if (temp > 0)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }
    }

    protected void vld_existence_txt_newEmail_ServerValidate(object source, ServerValidateEventArgs args)
    {
        DatabaseOperations dbOp = new DatabaseOperations();

        bool emailExists = dbOp.verifyExistenceEmailInDB((string)txt_newEmail.Text, this.SessionManagement.UserId);

        if (emailExists)
        {
            args.IsValid = false;
        }
    }

    protected void btn_NewEmail_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            Response.Cookies["UserInformationWBSD"]["NewEmail"] = txt_newEmail.Text;
            Response.Cookies["UserInformation"].Expires = DateTime.Now.AddHours(1);
            Response.Redirect("confirmEmail.aspx");
        }
    }

    protected void lnk_edit_email_Click(object sender, EventArgs e)
    {   
        //string a = liNewemail.ToString();
        //Response.Write("This is the end  " + a);
        showSectionAccount(1);
    }

    protected void lnk_edit_password_Click(object sender, EventArgs e)
    {
        showSectionAccount(2);
    }

    protected void lnk_edit_username_Click(object sender, EventArgs e)
    {
        showSectionAccount(3);
    }

    protected void lnk_edit_profile_Click(object sender, EventArgs e)
    {
        
        showSectionAccount(0);
    }
}