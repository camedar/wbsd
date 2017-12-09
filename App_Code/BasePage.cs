using System;
using System.Activities.Expressions;
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

    protected bool isUserSignedIn()
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
        string path = HttpContext.Current.Request.Url.AbsolutePath;
        path = path.Substring(path.LastIndexOf("/"));
        //if (!isUserSignedIn() && path != "/login.aspx")        
        if (!isUserSignedIn() && !path.StartsWith("/login.aspx") && !path.StartsWith("/confirmEmail.aspx") 
            && !path.StartsWith("/logout.aspx") && !path.StartsWith("/signup.aspx"))
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void logOut()
    {
        SessionManagement sm = new SessionManagement();
        sm.signOut();
    }

}