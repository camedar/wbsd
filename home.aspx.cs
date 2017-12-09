using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class home : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("it comes from this page " + Request.Headers["Referer"]);
        Response.Write("Welcome " + this.SessionManagement.UserFirstname + " " + this.SessionManagement.UserSurname);
    }
}