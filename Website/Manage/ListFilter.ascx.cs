using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manage_ListFilter : System.Web.UI.UserControl
{
    /// <summary>
    /// QueryString 中的字段名
    /// </summary>
    public string QueryField
    {
        get;
        set;
    }

    /// <summary>
    /// 经过处理之后的 QueryString 中的值
    /// </summary>
    public string QueryValue
    {
        get;
        private set;
    }

    /// <summary>
    /// 被筛选项目的名字，只被用于呈现给用户
    /// </summary>
    public string Name
    {
        get
        {
            return ctName.Text;
        }
        set
        {
            ctName.Text = value;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            QueryValue = Request.QueryString[QueryField];
            ctContainer.Visible = QueryValue != null;
            ctCancel.NavigateUrl = Url(null);
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 生成对应 queryValue 的 URL
    /// </summary>
    /// <param name="queryValue">QueryString 中对应字段的值</param>
    /// <returns>对应 queryValue 的 URL</returns>
    public string Url(string queryValue)
    {
        return _.UrlWithQuery(QueryField, queryValue);
    }

    /// <summary>
    /// 解析并检查 QueryValue，不通过时会取消筛选
    /// </summary>
    /// <param name="queryValueHandler">解析 QueryValue，返回呈现给用户当前筛选值
    /// 对于无效的 QueryValue 应当返回 null 或抛出异常</param>
    public void ParseAndCheck(Func<string, string> queryValueHandler)
    {
        if (QueryValue != null)
        {
            try
            {
                ctValue.Text = queryValueHandler(QueryValue);
            }
            catch
            {
                _.RedirectWithQuery(QueryField, null, true);
            }
        }
    }
}