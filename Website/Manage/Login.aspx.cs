using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;

public partial class Manage_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (_.CheckLogin())
        {
            _.Redirect("Index.aspx");
        }
        
        if (!IsPostBack)
        {
            // 配置
            ctName.MaxLength = Config.UserNameMaxLength;
            ctPassword.MaxLength = Config.UserPasswordMaxLength;

            // 标题
            Page.Header.Title += (Page.Header.Title != "" ? " - " : "") + 
                Config.ManageSystemName;
        }
    }
    protected void ctSubmit_Click(object sender, EventArgs e)
    {
        // 限制单 IP 对单用户名短时间内多次尝试登录
        var name = ctName.Text;
        var ip = Request.UserHostAddress;
        var key = "LoginAttempts_" + _.ComputeHmac(ip, name);
        if (Cache[key] == null)
        {
            // 尝试记录保存一天
            Cache.Insert(key, new ConcurrentQueue<DateTime>(), null, 
                DateTime.Today.AddDays(1), Cache.NoSlidingExpiration);
        }
        var attempts = Cache[key] as ConcurrentQueue<DateTime>;
        var timestart = DateTime.Now.AddMinutes(-10);
        if (attempts.Where(d => d > timestart).Count() >= 5 || attempts.Count() >= 20)
        {
            // 10分钟内5次或者当天20次失败后拒绝登录请求
            ctError.Text = "失败次数到达限制，请稍后再试";
            return;
        }
        else
        {
            attempts.Enqueue(DateTime.Now);
        }

        var user = DB.GetUser(ctName.Text);
        if (user != null && 
            user.Password == _.ComputeHmac(ctPasswordHashed.Value, user.PasswordSalt))
        {
            // 登录
            _.User = user;

            // 清空尝试记录
            var tmpTime = new DateTime();
            while (attempts.TryDequeue(out tmpTime)) ;

            // 记录 Cookie
            if (ctRemember.Checked)
            {
                var cookie = Response.Cookies["User"];
                cookie.HttpOnly = true;
                cookie.Expires = DateTime.Now.AddYears(50);
                cookie.Path = ResolveUrl("~/Manage/");
                cookie["ID"] = user.ID.ToString();
                cookie["Key"] = user.Password;
            }
            _.Redirect("~/Manage/Index.aspx");
        }
        else
        {
            ctError.Text = "用户名或密码错误";
        }
    }
}