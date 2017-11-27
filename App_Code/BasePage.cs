using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
    private SessionManagement _SessionManagement;
    public BasePage()
    {
        this.Load += new EventHandler(this.Page_Load);
    }
    
    public SessionManagement SessionManagement
    {
        get
        {
            if (!isUserSignedIn())
            {
                _SessionManagement = new SessionManagement();
                Session["SessionManagement"] = _SessionManagement;
            }
            else
            {
                _SessionManagement = Session["SessionManagement"] as SessionManagement;
            }
            return _SessionManagement;
        }
    }

    private bool isUserSignedIn()
    {
        if(Session["SessionManagement"] == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // get data that's common to all implementors of FactsheetBase 
        // and store the values in FactsheetBase's properties 
        //this.Data = ExtractPageData(Request.QueryString["data"]);
        if(isUserSignedIn())
        {
            Response.Write("The user is signed on");
        }
        else
        {
            Response.Write("The user is not signed on");
        }
    }

}