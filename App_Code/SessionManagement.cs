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

    public string UserEmail
    {
        get
        {
            return HttpContext.Current.Session["UserEmail"] == null ? string.Empty : HttpContext.Current.Session["UserEmail"].ToString();
        }
        set { HttpContext.Current.Session["UserEmail"] = value; }
    }

    public string UserFirstname
    {
        get
        {
            return HttpContext.Current.Session["UserFirstname"] == null ? string.Empty : HttpContext.Current.Session["UserFirstname"].ToString();
        }
        set { HttpContext.Current.Session["UserFirstname"] = value; }
    }

    public string UserSurname
    {
        get
        {
            return HttpContext.Current.Session["UserSurname"] == null ? string.Empty : HttpContext.Current.Session["UserSurname"].ToString();
        }
        set { HttpContext.Current.Session["UserSurname"] = value; }
    }

    public string UserId
    {
        get
        {
            return HttpContext.Current.Session["UserId"] == null ? string.Empty : HttpContext.Current.Session["UserId"].ToString();
        }
        set { HttpContext.Current.Session["UserId"] = value; }
    }
}