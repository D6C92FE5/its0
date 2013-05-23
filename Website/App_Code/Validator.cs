using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 验证器
/// </summary>
public class Validator
{
    private List<string> errorList = new List<string>();

    /// <summary>
    /// Validator
    /// </summary>
    /// <param name="errorMessage">出错时显示的错误信息</param>
    public Validator(string errorMessage = "")
	{
        errorList.Add(errorMessage);
	}

    /// <summary>
    /// 检测一项
    /// </summary>
    /// <param name="isInvalid">验证不通过时返回 true</param>
    /// <param name="message">错误消息</param>
    /// <returns></returns>
    public Validator Check(Func<bool> isInvalid, string message)
    {
        if (isInvalid())
        {
            errorList.Add(message);
        }
        return this;
    }

    /// <summary>
    /// 根据验证结果执行操作，验证不通过时会跳转到结果页面并显示所有错误消息
    /// </summary>
    public void Done()
    {
        if (errorList.Count() > 1)
        {
            _.ShowMessagePage(string.Join("\n", errorList), endResponse:true);
        }
    }
}