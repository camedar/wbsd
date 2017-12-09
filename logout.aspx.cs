using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class logout : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.logOut();
        Response.Redirect("login.aspx");
    }
}