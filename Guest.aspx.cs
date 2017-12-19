using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Guest : System.Web.UI.Page
{
    string name;
    protected void Page_Load(object sender, EventArgs e)
    {
        name = (string)(Session["uname"]);
        usernamelabel.Text = "Hello," + " " + name;
        
    }



    protected void logoutevent(object sender, EventArgs e)
    {
        Session["uname"] = null;
        Session.Abandon();
        Response.BufferOutput = true;
        Response.Redirect("Default.aspx",false);

    }
}