using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manage_Log : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 筛选
        ctFilterUserID.ParseAndCheck(s => int.Parse(s).ToString());
        var userID = int.Parse(ctFilterUserID.QueryValue ?? "0");
        ctFilterTargetID.ParseAndCheck(s => int.Parse(s).ToString());
        var targetID = int.Parse(ctFilterTargetID.QueryValue ?? "0");
        ctFilterTargetType.ParseAndCheck(s => s);
        var targetType = ctFilterTargetType.QueryValue;

        // 分页
        ctPaginator.ItemCount = DB.GetLogCount(userID);

        // 绑定
        ctList.DataSource = DB.GetLogs(
            ctPaginator.CurrentPage, ctPaginator.PageSize,
            userID, targetID, targetType);
        ctList.DataBind();
    }
}