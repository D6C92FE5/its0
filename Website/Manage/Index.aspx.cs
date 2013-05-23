using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manage_Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ctMyRecent.Visible = DB.GetMyLastArticleID() != 0;
    }
}