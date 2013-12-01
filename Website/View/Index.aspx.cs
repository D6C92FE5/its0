using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View_Index : System.Web.UI.Page
{
    private readonly int articlePerBlock = 8;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ctList.DataSource = DB.GetArticlesByCategoryName(1, articlePerBlock, null, true);
            ctList.DataBind();
        }
    }
}