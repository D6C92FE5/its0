using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manage_ArticleCategoryEdit : System.Web.UI.Page
{
    /// <summary>
    /// 文章分类ID
    /// </summary>
    private int categoryId;

    /// <summary>
    /// 是否为新文章分类（是添加文章分类还是修改文章分类）
    /// </summary>
    protected bool isNewCategory;

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

        // 解析文章分类ID
        var categoryIdString = Request.QueryString["ID"];
        isNewCategory = string.IsNullOrEmpty(categoryIdString);
        if (!isNewCategory)
        {
            if (!int.TryParse(categoryIdString, out categoryId) || 
                DB.GetArticleCategory(categoryId) == null)
            {
                _.ShowMessagePage("您访问的文章分类不存在", "ArticleCategoryList.aspx");
                return;
            }
        }

        if (!IsPostBack)
        {
            // 标题
            Title = isNewCategory ? "添加文章分类" : "编辑文章分类";

            // 配置
            ctName.MaxLength = Config.ArticleCategoryNameMaxLength;

            // 加载文章分类
            if (!isNewCategory)
            {
                var category = DB.GetArticleCategory(categoryId);
                ctName.Text = category.Name;
            }
        }
    }
    protected void ctSubmit_Click(object sender, EventArgs e)
    {
        var category = isNewCategory ? new ArticleCategory() : DB.GetArticleCategory(categoryId);

        category.Name = ctName.Text;

        // 检查数据
        new Validator(isNewCategory ? "添加文章分类失败" : "修改文章分类失败")
        .Check(() => category.Name == "", "请填写分类名")
        .Check(() => category.Name.Length > Config.ArticleCategoryNameMaxLength, "分类名过长")
        .Check(() => DB.IsArticleCategoryExist(category.Name, category.ID), "文章分类名已存在")
        .Done();

        DB.SetArticleCategory(category);

        _.ShowMessagePage(isNewCategory ? "添加文章分类成功" : "修改文章分类成功", 
            "ArticleCategoryList.aspx");
    }
}