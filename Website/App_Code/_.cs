using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Web.Caching;

/// <summary>
/// Helper!
/// </summary>
public static class _
{
    #region 页面上下文中的对象们

    /// <summary>
    /// 当前正在执行的页面
    /// </summary>
    private static Page Page
    {
        get
        {
            return HttpContext.Current.CurrentHandler as Page ?? new Page();
        }
    }

    /// <summary>
    /// 获取当前 HTTP 请求的 HttpRequest 对象
    /// </summary>
    private static HttpRequest Request
    {
        get
        {
            return HttpContext.Current.Request;
        }
    }

    /// <summary>
    /// 获取当前 HTTP 响应的 HttpResponse 对象
    /// </summary>
    private static HttpResponse Response
    {
        get
        {
            return HttpContext.Current.Response;
        }
    }

    /// <summary>
    /// 为当前 HTTP 请求获取 HttpApplicationState 对象
    /// </summary>
    private static HttpApplicationState Application
    {
        get
        {
            return HttpContext.Current.Application;
        }
    }

    /// <summary>
    /// 为当前 HTTP 请求获取 HttpSessionState 对象
    /// </summary>
    private static HttpSessionState Session
    {
        get
        {
            return HttpContext.Current.Session;
        }
    }

    /// <summary>
    /// 获取当前应用程序域的 Cache 对象
    /// </summary>
    private static Cache Cache
    {
        get
        {
            return HttpContext.Current.Cache;
        }
    }

    #endregion

    /// <summary>
    /// 获取或设置当前登录的用户
    /// </summary>
    public static User User
    {
        get
        {
            return Session["User"] as User;
        }
        set
        {
            Session["User"] = value;
            Session["UserTime"] = DateTime.Now;
        }
    }

    /// <summary>
    /// 获取或设置当前登录的异常日志队列
    /// </summary>
    public static ConcurrentQueue<Exception> ExceptionLogQueue
    {
        get
        {
            return Application["ExceptionLogQueue"] as ConcurrentQueue<Exception>;
        }
        set
        {
            Application["ExceptionLogQueue"] = value;
        }
    }

    /// <summary>
    /// 将对象的字符串表示形式转换为 HTML 编码的字符串，并返回编码的字符串
    /// </summary>
    /// <param name="value">一个对象</param>
    /// <returns>一个已编码的字符串</returns>
    public static string EncodeHtml(object value)
    {
        return HttpUtility.HtmlEncode(value);
    }

    /// <summary>
    /// 对 URL 字符串进行编码
    /// </summary>
    /// <param name="value">一个对象</param>
    /// <returns>一个已编码的字符串</returns>
    public static string EncodeUrl(object value)
    {
        return HttpUtility.UrlPathEncode(value.ToString());
    }

    /// <summary>
    /// 获取添加或修改 Query 参数后的 Url 字符串
    /// </summary>
    /// <param name="field">Query 参数名</param>
    /// <param name="value">Query 参数值</param>
    /// <returns></returns>
    public static string ModifiedQueryString(string field, string value)
    {
        var query = HttpUtility.ParseQueryString(Request.Url.Query);
        if (!string.IsNullOrEmpty(field))
        {
            if (!string.IsNullOrEmpty(value))
            {
                query[field] = value;
            }
            else
            {
                query.Remove(field);
            }
        }
        return string.Format("{0}?{1}", Request.Url.AbsolutePath, query.ToString());
    }

    /// <summary>
    /// 重定向页面并添加或修改 Query 参数
    /// </summary>
    /// <param name="field">Query 参数名</param>
    /// <param name="value">Query 参数值</param>
    public static void RedirectWithQuery(string field, string value, bool endResponse = false)
    {
        Redirect(Request.Url.AbsolutePath + ModifiedQueryString(field, value), endResponse);
    }

    /// <summary>
    /// 将客户端重定向到新的 URL (首先将 URL 转换为在请求客户端可用的 URL)
    /// </summary>
    /// <param name="url">目标位置</param>
    /// <param name="endResponse">如果为 true，则终止当前进程</param>
    public static void Redirect(string url, bool endResponse = false)
    {
        Response.Redirect(Page.ResolveUrl(url), endResponse);
    }

    /// <summary>
    /// 刷新页面
    /// </summary>
    /// <param name="endResponse">如果为 true，则终止当前进程</param>
    public static void Refresh(bool endResponse = false)
    {
        Redirect(Request.RawUrl, endResponse);
    }

    /// <summary>
    /// 跳转到信息显示页面并显示信息
    /// </summary>
    /// <param name="message">信息</param>
    /// <param name="redirect">显示信息后的自动重定向到的页面</param>
    /// <param name="endResponse">如果为 true，则终止当前进程</param>
    public static void ShowMessagePage(string message, string redirect = null, bool endResponse = false)
    {
        var id = _.ComputeHmac(Guid.NewGuid().ToString(), Config.HmacStaticKey).Substring(0, 4);
        Session["Message" + id] = EncodeHtml(message);
        if (redirect != null)
        {
            Session["Redirect" + id] = Page.ResolveUrl(redirect);
        }
        Redirect("Message.aspx?ID=" + id, endResponse);
    }

    /// <summary>
    /// 向 Page 对象注册启动脚本
    /// </summary>
    /// <param name="script">要注册的启动脚本文本</param>
    public static void RegisterStartupScript(string script)
    {
        Page.ClientScript.RegisterStartupScript(Page.GetType(),
            Guid.NewGuid().ToString(), script + ';', true);
    }

    /// <summary>
    /// 显示带有一段消息和一个确认按钮的警告框
    /// </summary>
    /// <param name="message">弹出的对话框中显示的纯文本</param>
    public static void ShowAlert(string message)
    {
        message = message.Replace("'", "\\'");
        RegisterStartupScript(string.Format("alert('{0}')", message));
    }

    /// <summary>
    /// 生成一个引用 Package 目录中的脚本的标签，其中 URL 经过 ResolveUrl 处理
    /// </summary>
    /// <param name="path">要引用的的脚本相对 Package 目录的路径</param>
    /// <returns>内容为 HTML 标签的字符串</returns>
    public static string PackageScriptTag(string path)
    {
        return string.Format("<script src='{0}{1}'></script>",
            Page.ResolveUrl("~/Package/"), path);
    }

    /// <summary>
    /// 计算密钥相关的哈希运算消息认证码字符串 (SHA-1)
    /// </summary>
    /// <param name="s">原字符串</param>
    /// <param name="key">密钥</param>
    /// <returns>密钥相关的哈希运算消息认证码字符串</returns>
    public static string ComputeHmac(string s, string key)
    {
        return BitConverter.ToString(new HMACSHA1(Encoding.UTF8.GetBytes(key))
            .ComputeHash(Encoding.UTF8.GetBytes(s))).Replace("-", "");
    }

    /// <summary>
    /// 用户发生变化（修改、删除）的通知，被已登录用户接收
    /// </summary>
    /// <param name="id">用户 ID</param>
    public static void UserChangeNotify(int id)
    {
        var now = DateTime.Now;
        Cache.Insert("UserChangeNotify" + id, now, null,
            now.AddMinutes(Session.Timeout), Cache.NoSlidingExpiration);
    }

    /// <summary>
    /// 检查 Session 和 Cookies 判断是否已经登录
    /// </summary>
    /// <returns></returns>
    public static bool CheckLogin()
    {
        // 用户发生变化（修改、删除）时重置登录状态
        if (_.User != null && Cache["UserChangeNotify" + _.User.ID] != null)
        {
            var changeTime = (DateTime)Cache["UserChangeNotify" + _.User.ID];
            var loginTime = (DateTime)Session["UserTime"];
            if (loginTime < changeTime && _.User != null)
            {
                var user = DB.GetUser(_.User.ID);
                _.User = (user != null && user.Password == _.User.Password) ? user : null;
            }
        }
        
        if (_.User == null)
        {
            var cookie = Request.Cookies["User"];
            if (cookie != null)
            {
                var idString = cookie["ID"];
                var password = cookie["Key"];
                var id = 0;
                if (int.TryParse(idString, out id))
                {
                    var user = DB.GetUser(id);
                    if (user != null && user.Password == password)
                    {
                        _.User = user;
                    }
                }
            }
        }
        return _.User != null;
    }

    /// <summary>
    /// 生成附加后缀之后的页面标题
    /// </summary>
    /// <param name="title">页面原始标题</param>
    /// <param name="suffix">要附加的后缀</param>
    /// <returns></returns>
    public static string GeneratePageTitle(string title, string suffix)
    {
        if (!title.EndsWith(suffix))
        {
            if (title == "" || title == "首页")
            {
                title = suffix;
            }
            else
            {
                title += " - " + suffix;
            }
        }
        return title;
    }
}