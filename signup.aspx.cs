using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Drawing;
using System.Drawing.Imaging;

public partial class signup : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.logOut();
        if (IsPostBack)
        {
            string Password = txt_password.Text;
            txt_password.Attributes.Add("value", Password);
            string Password2 = txt_password2.Text;
            txt_password2.Attributes.Add("value", Password2);

        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            DatabaseOperations db = new DatabaseOperations();
            Security sc = new Security();
            Hashtable dataArr = new Hashtable();
            string confirmationCode = sc.generateRandomNumber(6);
            string encodedPassword = sc.encodePassword(txt_password.Text.Trim());
            string firstname = txt_firstname.Text.Trim();
            dataArr.Add("firstname", txt_firstname.Text.Trim());
            dataArr.Add("surname", txt_surname.Text.Trim());
            dataArr.Add("email", txt_email.Text.Trim());
            dataArr.Add("password", encodedPassword);
            //dataArr.Add("confirmationCode", confirmationCode);

            string[] resp = db.insertRow("users",dataArr);
            if (resp[0] == "1")
            {
                //string textMessage = "Hello " + firstname + ", thank you for signing up in our site, your confirmation code is <b>" + confirmationCode + "</a></b>";
                //Email newEmail = new Email();
                //newEmail.send(txt_email.Text, "Confirmation email", textMessage);
                Response.Cookies["UserInformationWBSD"]["NewEmail"] = txt_email.Text.Trim();
                Response.Cookies["UserInformation"].Expires = DateTime.Now.AddHours(1);
                Response.Redirect("confirmEmail.aspx");
            }
            else
            {
                Response.Write("The account couldn't be created");
            }
            
        }
    }
  
    protected void vld_email_existence_ServerValidate(object source, ServerValidateEventArgs args)
    {
        Hashtable cond = new Hashtable();
        cond.Add("email", txt_email.Text);
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

    /*protected void upd_captcha_DataBinding(object sender, EventArgs e)
    {
        Security sc = new Security();

        Font f = new Font("Forte", 20, FontStyle.Italic);
        // image width and height
        Bitmap b = new Bitmap(150, 50);
        Graphics g = Graphics.FromImage(b);

        // setting background color
        g.Clear(Color.Yellow);

        String CaptchaString = sc.generateRandomString(6);

        g.DrawString(CaptchaString, f, Brushes.Red, 10, 10);

        Response.ContentType = "image/GIF";
        b.Save(Response.OutputStream, ImageFormat.Gif);
        Response.Write(DateTime.Now.ToString());

        
        
        f.Dispose();
        b.Dispose();
        g.Dispose();
    }*/
}