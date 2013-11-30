using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Manage_Message : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var id = Request.QueryString["ID"];
        if (Session["Message" + id] == null)
        {
            Session["Message" + id] = "未知错误";
            Session["Redirect" + id] = "Index.aspx";
        }

        var redirect = Session["Redirect" + id];
        if (redirect != null)
        {
            var autoRefresh = new HtmlMeta();
            autoRefresh.HttpEquiv = "refresh";
            autoRefresh.Content = "2;url=" + redirect.ToString();
            Page.Header.Controls.Add(autoRefresh);

            ctRedirect.Visible = true;
            ctRedirect.NavigateUrl = redirect.ToString();
        }
        ctMessage.Text = Session["Message" + id].ToString();
    }
}