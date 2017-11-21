using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text.RegularExpressions;

public partial class confirmAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
                Response.Write("<p>< h4 > Thank you for confirming your account, please click < b >< a href = 'http://localhost:62589/registration.aspx?h=" + confirmationCode + "' > here </ a ></ b > to sign in.</ h4 ></ p > ");
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
            Hashtable cols = new Hashtable();
            cols.Add("status", "1");
           
            int temp = db.countRows("users", cond);
            if (temp == 1)
            {
                db.updateRows("users", cols, cond);
                Response.Write("Login");
            }

        }
    }

    protected void lnk_resendCode_Click(object sender, EventArgs e)
    {
        string email = txt_email.Text;
        //Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        //Regex regex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        bool match = Regex.IsMatch(email, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        //bool match = Regex.IsMatch(email,
        //          @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        //          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
        //          RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

        if (email == "")
        {
            lbl_responseEmail.Text = "Please provide your email";
        }
        else if (!match)
        {
            lbl_responseEmail.Text = "The email is invalid";
        }
        else
        {
            Hashtable cond = new Hashtable();
            cond.Add("email", email);
            DatabaseOperations db = new DatabaseOperations();
            int temp = db.countRows("users", cond);
            if (temp > 0)
            {
                string[] fields = { "firstname", "confirmationCode", "email" };
                ArrayList queryData = db.selectRows("users", fields, cond);
                Hashtable a = (Hashtable)queryData[0];
                string confirmationCode = (string)a["confirmationCode"];
                string firstname = (string)a["firstname"];

                string textMessage = "Hello " + firstname + ", thank you for signing up in our site, your confirmation code is <b>" + confirmationCode + "</b>";
                Email newEmail = new Email();
                newEmail.send(email, "Confirmation email", textMessage);
                lbl_responseEmail.Text = "An email with your confirmation code was sent.";

            }
            else
            {
                lbl_responseEmail.Text = "This email is not registered";
            }
        }
    }

    protected void vld_existence_confirmationCode_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string confirmationCode = txt_confirmationCode.Text;
        Hashtable cond = new Hashtable();
        cond.Add("confirmationCode", confirmationCode);

        DatabaseOperations db = new DatabaseOperations();
        int temp = db.countRows("users", cond);
        if (temp == 0)
        {
            args.IsValid = false;
        }
    }
}