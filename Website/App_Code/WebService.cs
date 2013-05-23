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
    [WebMethod]
    public bool IsUserNameNotUsed(string name) {
        return !DB.IsUserNameExist(name);
    }

}
