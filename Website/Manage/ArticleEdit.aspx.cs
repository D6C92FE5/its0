using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Manage_ArticleEdit : System.Web.UI.Page
{
    /// <summary>
    /// 文章ID，发布文章时为0
    /// </summary>
    private int articleId;

    /// <summary>
    /// 是否为新文章（是发布文章还是修改文章）
    /// </summary>
    protected bool isNewArticle;

    protected void Page_Load(object sender, EventArgs e)
    {
        // 权限
        if (!_.User.Permission.Article)
        {
            _.ShowMessagePage("对不起，您无权管理文章", "Index.aspx");
            return;
        }

        // 解析文章ID
        var articleIdString = Request.QueryString["ID"];
        isNewArticle = string.IsNullOrEmpty(articleIdString);
        if (!isNewArticle)
        {
            if (articleIdString == "MyRecent") // 我最近的文章
            {
                articleId = DB.GetMyLastArticleID();
                if (articleId == 0)
                {
                    _.ShowMessagePage("您还没有发布过文章", "ArticleList.aspx");
                }
                else
                {
                    _.RedirectWithQuery("ID", articleId.ToString());
                }
                return;
            }
            if (!int.TryParse(articleIdString, out articleId) ||
                DB.GetArticle(articleId) == null)
            {
                _.ShowMessagePage("您访问的的文章不存在", "ArticleList.aspx");
                return;
            }
        }

        if (isNewArticle && DB.GetArticleCategoryCount() == 0)
        {
            _.ShowMessagePage("请首先添加文章分类");
            return;
        }

        if (!IsPostBack)
        {
            //标题
            Title = isNewArticle ? "发布文章" : "修改文章";

            // 配置
            ctTitle.MaxLength = Config.ArticleTitleMaxLength;
            ctFrom.MaxLength = Config.ArticleFromMaxLength;
            ctTopField.Visible = Config.ArticleTopEnabled;
            ctRecommendField.Visible = Config.ArticleRecommendEnabled;
            ctHideField.Visible = Config.ArticleHideEnabled;
            ctPictureScrollField.Visible = Config.ArticlePictureScrollEnabled;

            //载入分类
            if (isNewArticle)
            {
                ctCategory.Items.Add(new ListItem("分类", ""));
            }
            foreach (var category in DB.GetArticleCategories())
            {
                ctCategory.Items.Add(new ListItem(category.Name, category.ID.ToString()));
            }

            // 载入文章
            if (!isNewArticle)
            {
                var article = DB.GetArticle(articleId);
                ctCategory.SelectedValue = article.Category.ID.ToString();
                ctTitle.Text = article.Title;
                ctFrom.Text = article.From;
                ctTop.Checked = article.IsTop;
                ctRecommend.Checked = article.IsRecommend;
                ctHide.Checked = article.IsHide;
                ctContent.Text = article.Content;
                ctPictureScroll.Checked = article.PictureScroll != null;
                ctPictureScrollUrl.Text = article.PictureScroll ?? "";
            }

            // CKFinder
            var ckfinder = new CKFinder.FileBrowser();
            ckfinder.BasePath = ResolveUrl("~/Package/CKFinder/");
            ckfinder.SetupCKEditor(ctContent);
        }
    }

    protected void ctSubmit_OnClick(object sender, EventArgs e)
    {
        var article = isNewArticle ? new Article() : DB.GetArticle(articleId);

        var categoryID = 0;
        int.TryParse(ctCategory.SelectedValue, out categoryID);
        article.Category = DB.GetArticleCategory(categoryID);
        article.Title = ctTitle.Text;
        article.From = ctFrom.Text;
        article.Content = ctContent.Text;
        article.IsTop = ctTop.Checked;
        article.IsRecommend = ctRecommend.Checked;
        article.IsHide = ctHide.Checked;
        article.PictureScroll = ctPictureScroll.Checked ? ctPictureScrollUrl.Text : null;

        // 检查数据
        new Validator(isNewArticle ? "发布文章失败" : "修改文章失败")
        .Check(() => article.Category == null, "请选择分类")
        .Check(() => article.Title == "", "请填写标题")
        .Check(() => article.Title.Length > Config.ArticleTitleMaxLength, "标题过长")
        .Check(() => article.From.Length > Config.ArticleFromMaxLength, "来源过长")
        .Done();
        
        DB.SetArticle(article);

        _.ShowMessagePage(isNewArticle ? "发布文章成功" : "修改文章成功", "ArticleList.aspx");
    }
}