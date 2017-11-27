using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SessionManagement
/// </summary>
public class SessionManagement
{
    public SessionManagement()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string UserName
    {
        get
        {
            return HttpContext.Current.Session["UserName"] == null ? string.Empty : HttpContext.Current.Session["UserName"].ToString();
        }
        set { HttpContext.Current.Session["UserName"] = value; }
    }
}