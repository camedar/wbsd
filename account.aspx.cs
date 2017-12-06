using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class account : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
     
    }

    protected void userForm_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        Security sec = new Security();
        e.NewValues["password"] = sec.encodePassword((string)e.NewValues["password"]);
    }

    protected void userForm_DataBound(object sender, EventArgs e)
    {
        Security sc = new Security();
        TextBox password_input = (TextBox)userForm.FindControl("txt_password");
        password_input.Text = sc.decodePassword((string)password_input.Text);



    }
}