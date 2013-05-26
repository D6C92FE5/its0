using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using (var dc = new MainDataContext())
        {
            ctInitDatabase.Visible = !dc.DatabaseExists();
        }
    }

    protected void ctInitDatabase_Click(object sender, EventArgs e)
    {
        using (var dc = new MainDataContext())
        {
            if (!dc.DatabaseExists())
            {
                dc.CreateDatabase();

                // 添加权限
                dc.UserPermission.InsertOnSubmit(
                    new UserPermission { Name = "文章和用户", Article = true, User = true });
                dc.UserPermission.InsertOnSubmit(
                    new UserPermission { Name = "文章", Article = true, User = false });
                dc.SubmitChanges();

                // 添加初始用户
                var user = new User();
                user.Name = "ad";
                user.Password = _.ComputeHmac("1", Config.HmacStaticKey);
                user.Permission = dc.UserPermission.First();
                DB.SetUser(user);

                _.Refresh();
            }
        }
    }
}