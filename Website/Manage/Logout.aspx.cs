using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manage_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        _.User = null;
        var cookie = Response.Cookies["User"];
        cookie.Expires = new DateTime(1970, 1, 1);
        cookie.HttpOnly = true;
        cookie.Path = ResolveUrl("~/Manage/");
        _.Redirect("Login.aspx");
    }
}