using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manage_ArticleCategoryList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 权限
        if (!_.User.Permission.Article)
        {
            _.ShowMessagePage("对不起，您无权管理文章分类", "Index.aspx");
            return;
        }

        if (!Config.ArticleCategoryEditable)
        {
            _.ShowMessagePage("文章分类管理未启用", "Index.aspx");
            return;
        }

        if (!IsPostBack)
        {
            ctList.DataSource = DB.GetArticleCategories();
            ctList.DataBind();
        }
    }
    protected void ctDelete_Command(object sender, CommandEventArgs e)
    {
        var id = 0;
        if (int.TryParse(e.CommandArgument.ToString(), out id))
        {
            // 检查
            new Validator("删除用户失败")
            .Check(() => DB.IsArticleCategoryHasArticle(id), "存在此分类的文章")
            .Done();

            DB.DeleteArticleCategory(id);
        }
        _.Refresh();
    }
}