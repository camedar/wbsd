using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text.RegularExpressions;

public partial class confirmAccount : BasePage
{
    private string newEmail = "";
    private string currentEmail = "";
    private string confirmationCode;
    private int confirmationCodeSize = 6;

    protected void Page_Load(object sender, EventArgs e)
    {
        string refererPath = Request.Headers["Referer"];
        if (refererPath != null)
        {
            try
            {
                
                DatabaseOperations db = new DatabaseOperations();
                Security sc = new Security();
                newEmail = Request.Cookies["UserInformationWBSD"]["NewEmail"];
                txt_email.Text = newEmail;
                //Response.Write("this is the email in cookies " + newEmail);

                if (this.isUserSignedIn())
                {
                    currentEmail = this.SessionManagement.UserEmail;
                }
                else
                {
                    currentEmail = newEmail;
                }
                
                refererPath = refererPath.Substring(refererPath.LastIndexOf("/"));
                if (refererPath.StartsWith("/account.aspx") || refererPath.StartsWith("/login.aspx")
                    || refererPath.StartsWith("/signup.aspx"))
                {
                    generateConfirmationCode();
                    sendConfirmationCode(false);
                }
                else if (refererPath.StartsWith("/confirmEmail.aspx"))
                {                    
                }
                else
                {
                    Response.Write("Denied access");
                }
            }
            catch
            {
                Response.Redirect("login.aspx");
            }
        }
        else
        {
            if (this.isUserSignedIn())
            {
                Response.Redirect("home.aspx");
            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }
        
        /*string confirmationCode = Request.QueryString["h"];
        //Request["h"]
        if (confirmationCode != null) { 
            Hashtable cond = new Hashtable();
            cond.Add("confirmationCode", confirmationCode);
            Hashtable cols = new Hashtable();
            cols.Add("status", "1");
            DatabaseOperations db = new DatabaseOperations();
            int temp = db.countRows("users", cond);
            if (temp == 1)
            {
                db.updateRows("users", cols, cond);
                Response.Write("<p>< h4 > Thank you for confirming your account, please click < b >< a href = 'http://localhost:62589/signup.aspx?h=" + confirmationCode + "' > here </ a ></ b > to sign in.</ h4 ></ p > ");
            }
        }*/
    }

    protected void btn_confirm_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            DatabaseOperations db = new DatabaseOperations();
            Security sc = new Security();
            Hashtable cond = new Hashtable();
            cond.Add("confirmationCode", txt_confirmationCode.Text);
            cond.Add("email", currentEmail);
            Hashtable cols = new Hashtable();
            cols.Add("status", "1");
            cols.Add("confirmationCode", "");
            cols.Add("email", newEmail);

            int temp = db.countRows("users", cond);
            if (temp == 1)
            {
                db.updateRows("users", cols, cond);
                if (this.isUserSignedIn())
                {
                    this.SessionManagement.UserEmail = newEmail;
                    Response.Redirect("account.aspx");
                }
                else
                {
                    Response.Redirect("login.aspx");
                }
            }

        }
    }

    protected void lnk_resendCode_Click(object sender, EventArgs e)
    {
        sendConfirmationCode(true);
    }

    protected void generateConfirmationCode()
    {
        string email = (string)currentEmail;
        Security sc = new Security();
        confirmationCode = sc.generateRandomNumber(confirmationCodeSize);
        DatabaseOperations db = new DatabaseOperations();
        Hashtable cond = new Hashtable();
        cond.Add("email", email);
        Hashtable cols = new Hashtable();
        cols.Add("confirmationCode", confirmationCode);

        int temp = db.countRows("users", cond);
        if (temp == 1)
        {
            db.updateRows("users", cols, cond);            
        }
    }

    protected void sendConfirmationCode(bool resend)
    {        
        Hashtable cond = new Hashtable();
        cond.Add("email", currentEmail);
        DatabaseOperations db = new DatabaseOperations();
        int temp = db.countRows("users", cond);
        if (temp > 0)
        {
            string[] fields = { "firstname", "confirmationCode", "email" };
            ArrayList queryData = db.selectRows("users", fields, cond);
            Hashtable a = (Hashtable)queryData[0];
            string confirmationCode = (string)a["confirmationCode"];
            string firstname = (string)a["firstname"];

            string textMessage = "Hello " + firstname + ", thank you for signing up in our site, to confirm your email please use this code <b>" + confirmationCode + "</b>";
            if (this.isUserSignedIn())
            {
                textMessage = "Hello " + firstname + ", To confirm your new email please use the code <b>" + confirmationCode + "</b>";
            }

            Email sendEmail = new Email();
            sendEmail.send(newEmail, "Email confirmation", textMessage);
            if (resend)
            {
                lbl_responseEmail.Text = "The confirmation code has been sent again.";                
            }
            else
            {
                lbl_responseEmail.Text = "An email with your confirmation code has been sent.";
            }

        }
        
    }

    protected void vld_existence_confirmationCode_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string confirmationCode = txt_confirmationCode.Text;
        Hashtable cond = new Hashtable();
        cond.Add("confirmationCode", confirmationCode);
        cond.Add("email", currentEmail);

        DatabaseOperations db = new DatabaseOperations();
        int temp = db.countRows("users", cond);
        if (temp == 0)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }
}