using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manage_UserList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 权限
        if (!_.User.Permission.User)
        {
            _.ShowMessagePage("对不起，您无权管理用户", "Index.aspx");
            return;
        }

        if (!IsPostBack)
        {
            // 筛选
            ctFilterPermission.ParseAndCheck(s => DB.GetUserPermission(int.Parse(s)).Name);
            var permissionID = int.Parse(ctFilterPermission.QueryValue ?? "0");

            // 分页
            ctPaginator.ItemCount = DB.GetUserCount();

            // 绑定
            ctList.DataSource = DB.GetUsers(
                ctPaginator.CurrentPage, ctPaginator.PageSize,
                permissionID);
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
            .Check(() => DB.IsUserHasArticle(id), "存在此用户发布的文章")
            .Check(() => !DB.IsOtherUserHasUserPermission(id),
                "此用户是当前唯一具有用户管理权限的用户")
            .Done();

            DB.DeleteUser(id);
            _.UserChangeNotify(id);
        }
        _.Refresh();
    }
}