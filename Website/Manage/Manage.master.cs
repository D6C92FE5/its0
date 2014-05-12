using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manage_Manage : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!_.CheckLogin())
        {
            _.Redirect("Login.aspx", true);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 权限
            ctArticle.Visible = _.User.Permission.Article;
            ctUser.Visible = _.User.Permission.User;

            ctLog.Visible = Config.LogVisible;
            ctUser.Visible = Config.MultiUser;

            ctArticleCategory.Visible = Config.ArticleCategoryEditable;

            Page.Header.Title = _.GeneratePageTitle(Page.Header.Title, Config.ManageSystemName);
        }
    }
}
