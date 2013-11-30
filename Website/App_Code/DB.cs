using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.Linq;

public partial class User
{
    public string Password
    {
        get
        {
            return this.__Password;
        }
        set
        {
            this.PasswordSalt = Guid.NewGuid().ToString("N").ToUpper();
            this.__Password = _.ComputeHmac(value, this.PasswordSalt);
        }
    }
}

/// <summary>
/// 数据库
/// </summary>
public static class DB
{
    /// <summary>
    /// 获取当前 HTTP 请求的上下文中 Linq to SQL 类的对象
    /// </summary>
    private static MainDataContext dc
    {
        get
        {
            if (HttpContext.Current == null)
            {
                throw new InvalidOperationException("无法在非 HTTP 请求的上下文中通过 DB 类访问数据库");
            }

            var items = HttpContext.Current.Items;
            if (!items.Contains("DataContext"))
            {
                items["DataContext"] = new MainDataContext();
            }

            var dc = (MainDataContext)items["DataContext"];
            if (!dc.DatabaseExists())
            {
                throw new InvalidOperationException("无法访问关联的数据库");
            }

            return dc;
        }
    }

    #region 文章
    public static Article GetArticle(int id)
    {
        return dc.Article.SingleOrDefault(a => a.ID == id);
    }
    public static Article[] GetArticles(
        int page = 1, int pageSize = 0,
        int categoryID = 0,
        int publisherID = 0)
    {
        var articles = dc.Article.AsQueryable();
        articles = articles.OrderByDescending(a => a.PostDate);
        if (categoryID != 0)
        {
            articles = articles.Where(a => a.CategoryID == categoryID);
        }
        if (publisherID != 0)
        {
            articles = articles.Where(a => a.PublisherID == publisherID);
        }
        if (pageSize != 0)
        {
            articles = articles.Skip(pageSize * (page - 1)).Take(pageSize);
        }
        return articles.ToArray();
    }
    public static int GetArticleCount(
        int categoryID = 0,
        int publisherID = 0)
    {
        var articles = dc.Article.AsQueryable();
        if (categoryID != 0)
        {
            articles = articles.Where(a => a.CategoryID == categoryID);
        }
        if (publisherID != 0)
        {
            articles = articles.Where(a => a.PublisherID == publisherID);
        }
        return articles.Count();
    }
    public static int GetMyLastArticleID()
    {
        return (from a in dc.Article
                where a.PublisherID == _.User.ID
                orderby a.EditDate descending
                select a.ID)
                .FirstOrDefault();
    }
    public static void SetArticle(Article article)
    {
        var description = article.ID == 0 ? "发布文章" : "修改文章";
        var now = DateTime.Now;
        if (article.ID == 0) // 新文章
        {
            article.PublisherID = _.User.ID;
            article.PostDate = now;
            article.ViewCount = 0;
            dc.Article.InsertOnSubmit(article);
        }
        article.EditDate = now;
        dc.SubmitChanges();
        AddLog(article.ID, "Article", description);
    }
    public static void DeleteArticle(int id)
    {
        var article = dc.Article.SingleOrDefault(a => a.ID == id);
        if (article != null)
        {
            dc.Article.DeleteOnSubmit(article);
            dc.SubmitChanges();
        }
        AddLog(id, "Article", "删除文章");
    }
    #endregion

    #region 文章分类
    public static ArticleCategory GetArticleCategory(int id)
    {
        return dc.ArticleCategory.SingleOrDefault(a => a.ID == id);
    }
    public static ArticleCategory[] GetArticleCategories()
    {
        return dc.ArticleCategory.OrderBy(a => a.Name).ToArray();
    }
    public static int GetArticleCategoryCount()
    {
        return dc.ArticleCategory.Count();
    }
    public static bool IsArticleCategoryExist(string name, int ignoredCategory = 0)
    {
        var categories = dc.ArticleCategory.AsQueryable();
        categories = categories.Where(a => a.ID != ignoredCategory);
        categories = categories.Where(a => a.Name == name);
        return categories.Count() > 0;
    }
    public static bool IsArticleCategoryHasArticle(int id)
    {
        return dc.Article.Where(a => a.CategoryID == id).Count() > 0;
    }
    public static void SetArticleCategory(ArticleCategory category)
    {
        var description = category.ID == 0 ? "添加文章分类" : "修改文章分类";
        if (category.ID == 0) // 新文章分类
        {
            dc.ArticleCategory.InsertOnSubmit(category);
        }
        dc.SubmitChanges();
        AddLog(category.ID, "ArticleCategory", description);
    }
    public static void DeleteArticleCategory(int id)
    {
        var category = dc.ArticleCategory.SingleOrDefault(u => u.ID == id);
        if (category != null)
        {
            dc.ArticleCategory.DeleteOnSubmit(category);
            dc.SubmitChanges();
        }
        AddLog(id, "ArticleCategory", "删除文章分类");
    }
    #endregion

    #region 用户
    public static User GetUser(int id)
    {
        return dc.User.SingleOrDefault(u => u.ID == id);
    }
    public static User GetUser(string name)
    {
        return dc.User.SingleOrDefault(u => u.Name == name);
    }
    public static User[] GetUsers(
        int page = 1, int pageSize = 0,
        int permissionID = 0)
    {
        var users = dc.User.AsQueryable();
        users = users.OrderBy(u => u.ID);
        if (permissionID != 0)
        {
            users = users.Where(u => u.PermissionID == permissionID);
        }
        if (pageSize != 0)
        {
            users = users.Skip(pageSize * (page - 1)).Take(pageSize);
        }
        return users.ToArray();
    }
    public static int GetUserCount(
        int permissionID = 0)
    {
        var users = dc.User.AsQueryable();
        if (permissionID != 0)
        {
            users = users.Where(u => u.PermissionID == permissionID);
        }
        return users.Count();
    }
    public static bool IsUserNameExist(string name, int ignoredUser = 0)
    {
        var users = dc.User.AsQueryable();
        users = users.Where(u => u.ID != ignoredUser);
        users = users.Where(u => u.Name == name);
        return users.Count() > 0;
    }
    public static bool IsOtherUserHasUserPermission(int id)
    {
        var users = dc.User.AsQueryable();
        users = users.Where(u => u.ID != id);
        users = users.Where(u => u.Permission.User);
        return users.Count() > 0;
    }
    public static bool IsUserHasArticle(int id)
    {
        return dc.Article.Where(a => a.PublisherID == id).Count() > 0;
    }
    public static void SetUser(User user)
    {
        var description = user.ID == 0 ? "添加用户" : "修改用户";
        if (user.ID == 0) // 新用户
        {
            dc.User.InsertOnSubmit(user);
        }
        dc.SubmitChanges();
        AddLog(user.ID, "User", description);
    }
    public static void DeleteUser(int id)
    {
        User user = dc.User.SingleOrDefault(u => u.ID == id);
        if (user != null)
        {
            dc.User.DeleteOnSubmit(user);
            dc.SubmitChanges();
        }
        AddLog(id, "User", "删除用户");
    }
    #endregion

    #region 用户权限
    public static UserPermission GetUserPermission(int id)
    {
        return dc.UserPermission.SingleOrDefault(u => u.ID == id);
    }
    public static UserPermission[] GetUserPermissions()
    {
        return dc.UserPermission.ToArray();
    }
    #endregion

    #region 日志
    public static Log[] GetLogs(
        int page = 1, int pageSize = 0,
        int userID = 0,
        int targetID = 0,
        string targetType = null)
    {
        var logs = dc.Log.AsQueryable();
        logs = logs.OrderByDescending(l => l.Time);
        if (userID != 0)
        {
            logs = logs.Where(l => l.UserID == userID);
        }
        if (targetID != 0)
        {
            logs = logs.Where(l => l.TargetID == targetID);
        }
        if (targetType != null)
        {
            logs = logs.Where(l => l.TargetType == targetType);
        }
        if (pageSize != 0)
        {
            logs = logs.Skip(pageSize * (page - 1)).Take(pageSize);
        }
        return logs.ToArray();
    }
    public static int GetLogCount(
        int userID = 0,
        int targetID = 0,
        string targetType = null)
    {
        var logs = dc.Log.AsQueryable();
        if (userID != 0)
        {
            logs = logs.Where(l => l.UserID == userID);
        }
        if (targetID != 0)
        {
            logs = logs.Where(l => l.TargetID == targetID);
        }
        if (targetType != null)
        {
            logs = logs.Where(l => l.TargetType == targetType);
        }
        return logs.Count();
    }
    public static void AddLog(int targetID, string targetType, string description)
    {
        var log = new Log();
        log.UserID = (_.User ?? new User()).ID;
        log.TargetID = targetID;
        log.TargetType = targetType;
        log.Description = description;
        log.Time = DateTime.Now;
        dc.Log.InsertOnSubmit(log);
        dc.SubmitChanges();
    }
    #endregion
}