using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manage_Paginator : System.Web.UI.UserControl
{
    /// <summary>
    /// QueryString 中的字段名
    /// </summary>
    public string QueryField
    {
        get;
        set;
    }

    private int _PageSize;
    /// <summary>
    /// 每页项目数
    /// </summary>
    public int PageSize
    {
        get
        {
            return _PageSize;
        }
        set
        {
            _PageSize = Math.Max(value, 0);
        }
    }

    /// <summary>
    /// 项目总数，只写
    /// </summary>
    public int ItemCount
    {
        set
        {
            PageCount = PageSize == 0 ? 1 : (int)Math.Ceiling((double)value / PageSize);
        }
    }

    private int _PageCount;
    /// <summary>
    /// 页数
    /// </summary>
    public int PageCount
    {
        get
        {
            return _PageCount;
        }
        set
        {
            _PageCount = Math.Max(value, 1);
        }
    }

    private int _CurrentPage;
    /// <summary>
    /// 当前页码
    /// </summary>
    public int CurrentPage
    {
        // 当前页小于 1 时一定无效
        // 当前页大于 PageCount 时可能是因为 PageCount 还没有被设置
        get
        {
            if (_CurrentPage > PageCount)
            {
                _.RedirectWithQuery(QueryField, PageCount.ToString(), true);
            }
            return _CurrentPage;
        }
        private set
        {
            if (value < 1)
            {
                _.RedirectWithQuery(QueryField, "1", true);
            }
            _CurrentPage = value;
        }
    }

    public Manage_Paginator()
    {
        QueryField = "Page";
        PageSize = Config.DefaultPageSize;
        PageCount = 1;
        CurrentPage = 1;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var pageString = Request.QueryString[QueryField];
            if (pageString != null)
            {
                var page = 1;
                if (pageString == "All") // 显示全部页面
                {
                    PageSize = 0;
                }
                else if (int.TryParse(pageString, out page))
                {
                    CurrentPage = page;
                }
                else
                {
                    _.RedirectWithQuery(QueryField, null, true);
                }
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (PageCount == 1 || PageSize == 0) // 只有一页或者不分页
            {
                Paginator.Visible = false;
            }
        }
    }
}