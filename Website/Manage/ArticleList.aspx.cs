using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manage_ArticleList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 权限
        if (!_.User.Permission.Article)
        {
            _.ShowMessagePage("对不起，您无权管理文章", "Index.aspx");
            return;
        }

        if (!IsPostBack)
        {
            // 筛选
            ctFilterCategory.ParseAndCheck(s => DB.GetArticleCategory(int.Parse(s)).Name);
            var categoryID = int.Parse(ctFilterCategory.QueryValue ?? "0");
            ctFilterPublisher.ParseAndCheck(s => DB.GetUser(int.Parse(s)).Name);
            var publisherID = int.Parse(ctFilterPublisher.QueryValue ?? "0");

            // 分页
            ctPaginator.ItemCount = DB.GetArticleCount(categoryID, publisherID);

            // 绑定
            ctList.DataSource = DB.GetArticles(
                ctPaginator.CurrentPage, ctPaginator.PageSize,
                categoryID, publisherID);
            ctList.DataBind();
        }
    }
    protected void ctDelete_Command(object sender, CommandEventArgs e)
    {
        var id = 0;
        if (int.TryParse(e.CommandArgument.ToString(), out id))
        {
            DB.DeleteArticle(id);
        }
        _.Refresh();
    }
}