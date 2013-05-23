using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manage_UserEdit : System.Web.UI.Page
{
    /// <summary>
    /// 用户ID
    /// </summary>
    private int userId;

    /// <summary>
    /// 是否为新用户（是添加用户还是修改用户）
    /// </summary>
    protected bool isNewUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        // 权限
        if (!_.User.Permission.User)
        {
            _.ShowMessagePage("对不起，您无权管理用户", "Index.aspx");
            return;
        }

        // 解析用户ID
        var userIdString = Request.QueryString["ID"];
        isNewUser = string.IsNullOrEmpty(userIdString);
        if (!isNewUser)
        {
            if (!int.TryParse(userIdString, out userId) || DB.GetUser(userId) == null)
            {
                _.ShowMessagePage("您访问的用户不存在", "UserList.aspx");
                return;
            }
        }

        if (!IsPostBack)
        {
            // 标题
            Title = isNewUser ? "添加用户" : "编辑用户";
            
            // 配置
            ctName.MaxLength = Config.UserNameMaxLength;
            ctPassword.MaxLength = Config.UserPasswordMaxLength;

            // 加载权限
            foreach (var permission in DB.GetUserPermissions())
            {
                ctPermission.Items.Add(new ListItem(permission.Name, permission.ID.ToString()));
            }
            
            // 加载用户
            if (!isNewUser)
            {
                var user = DB.GetUser(userId);
                ctName.Text = user.Name;
                ctPermission.SelectedValue = user.Permission.ID.ToString();
                ctPassword.Attributes["placeholder"] = "不修改时请留空本行";
            }
        }
    }
    protected void ctSubmit_Click(object sender, EventArgs e)
    {
        var user = isNewUser ? new User() : DB.GetUser(userId);

        var permissionID = 0;
        int.TryParse(ctPermission.SelectedValue, out permissionID);
        user.Permission = DB.GetUserPermission(permissionID);
        user.Name = ctName.Text;
        if (ctPassword.Text != "") // 为空表示不修改密码
        {
            user.Password = ctPasswordHashed.Value;
        }

        // 检查数据
        new Validator(isNewUser ? "添加用户失败" : "修改用户失败")
        .Check(() => user.Name == "", "请填写用户名")
        .Check(() => isNewUser && ctPassword.Text == "", "请填写密码")
        .Check(() => user.Name.Length > Config.UserNameMaxLength, "用户名过长")
        .Check(() => ctPasswordHashed.Value.Length != 40, "密码无效")
        .Check(() => DB.IsUserNameExist(user.Name, user.ID), "用户名已存在")
        .Check(() => !user.Permission.User && !DB.IsOtherUserHasUserPermission(user.ID),
            "此用户是当前唯一具有用户管理权限的用户，您不能取消此权限")
        .Done();

        DB.SetUser(user);

        _.ShowMessagePage(isNewUser ? "添加用户成功" : "修改用户成功", "UserList.aspx");
    }
}