using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View_List : System.Web.UI.Page
{
    private readonly int articlePerPage = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        var category = Request.QueryString["Category"];
        if (!IsPostBack)
        {
            ctCategory.Text = _.EncodeHtml(category) ?? "全部文章";
            Page.Title = ctCategory.Text;

            ctPaginator.PageSize = articlePerPage;
            ctPaginator.ItemCount = DB.GetArticleCountByCategoryName(category, true);

            ctList.DataSource = DB.GetArticlesByCategoryName(ctPaginator.CurrentPage,
                ctPaginator.PageSize, category, true);
            ctList.DataBind();
        }
    }
}