using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class confirmAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string confirmationCode = Request.QueryString["h"];
        //Request["h"]
        if (confirmationCode != null) { 
            Hashtable cond = new Hashtable();
            cond.Add("confirmationCode", confirmationCode);
            Hashtable cols = new Hashtable();
            cols.Add("status", "1");
            Database db = new Database();
            int temp = db.countRows("users", cond);
            if (temp == 1)
            {
                db.updateRows("users", cols, cond);
                Response.Write("<p>< h4 > Thank you for confirming your account, please click < b >< a href = 'http://localhost:62589/registration.aspx?h=" + confirmationCode + "' > here </ a ></ b > to sign in.</ h4 ></ p > ");
            }
        }
    }
}