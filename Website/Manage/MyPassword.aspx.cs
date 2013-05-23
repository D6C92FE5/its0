using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manage_MyPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 配置
            ctPassword.MaxLength = Config.UserPasswordMaxLength;
        }
    }
    protected void ctSubmit_Click(object sender, EventArgs e)
    {
        var user = DB.GetUser(_.User.ID);

        user.Password = ctPasswordHashed.Value;

        // 检查数据
        new Validator("修改密码失败")
        .Check(() => ctPassword.Text == "", "请填写密码")
        .Check(() => ctPasswordHashed.Value.Length != 40, "密码无效")
        .Done();

        DB.SetUser(user);

        _.ShowMessagePage("修改密码成功", "UserList.aspx");
    }
}