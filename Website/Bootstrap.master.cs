using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Bootstrap : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();

        // 让 IE 使用最新的最新的引擎渲染网页
        Response.AppendHeader("X-UA-Compatible", "IE=edge,chrome=1");
    }
}
