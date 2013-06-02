using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// WebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    /// <summary>
    /// 判断用户名是否已经被使用
    /// </summary>
    /// <param name="name">用户名</param>
    /// <returns>用户名是否已经被使用</returns>
    [WebMethod(EnableSession=true)]
    public bool IsUserNameNotUsed(string name) {
        return _.CheckLogin() && !DB.IsUserNameExist(name);
    }

    /// <summary>
    /// 判断文章分类名是否已存在
    /// </summary>
    /// <param name="name">文章分类名</param>
    /// <returns>文章分类名是否已存在</returns>
    [WebMethod(EnableSession = true)]
    public bool IsArticleCategoryExist(string name)
    {
        return _.CheckLogin() && !DB.IsArticleCategoryExist(name);
    }
}
