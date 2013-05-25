using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

/// <summary>
/// 数据库
/// </summary>
public static class DB
{
    #region 文章
    public static Article GetArticle(int id)
    {
        using (var dc = new MainDataContext())
        {
            dc.ObjectTrackingEnabled = false;
            var dlo = new DataLoadOptions();
            dlo.LoadWith<Article>(a => a.Publisher);
            dlo.LoadWith<Article>(a => a.Category);
            dc.LoadOptions = dlo;
            return dc.Article.SingleOrDefault(a => a.ID == id);
        }
    }
    public static Article[] GetArticles(
        int page = 1, int pageSize = 0,
        int categoryID = 0,
        int publisherID = 0)
    {
        using (var dc = new MainDataContext())
        {
            dc.ObjectTrackingEnabled = false;
            var dlo = new DataLoadOptions();
            dlo.LoadWith<Article>(a => a.Publisher);
            dlo.LoadWith<Article>(a => a.Category);
            dc.LoadOptions = dlo;
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
    }
    public static int GetArticleCount(
        int categoryID = 0,
        int publisherID = 0)
    {
        using (var dc = new MainDataContext())
        {
            dc.ObjectTrackingEnabled = false;
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
    }
    public static int GetMyLastArticleID()
    {
        using (var dc = new MainDataContext())
        {
            dc.ObjectTrackingEnabled = false;
            return (from a in dc.Article
                    where a.PublisherID == _.User.ID
                    orderby a.EditDate descending
                    select a.ID)
                    .FirstOrDefault();
        }
    }
    public static void SetArticle(Article article)
    {
        var description = article.ID == 0 ? "发布文章" : "修改文章";
        using (var dc = new MainDataContext())
        {
            var now = DateTime.Now;
            if (article.ID == 0) // 新文章
            {
                article.PublisherID = _.User.ID;
                article.PostDate = now;
                article.ViewCount = 0;
                dc.Article.InsertOnSubmit(article);
            }
            else
            {
                dc.Article.Attach(article, true);
            }
            dc.ArticleCategory.Attach(article.Category, false);
            article.EditDate = now;
            dc.SubmitChanges();
        }
        AddLog(article.ID, "Article", description);
    }
    public static void DelectArticle(int id)
    {
        using (var dc = new MainDataContext())
        {
            var article = dc.Article.SingleOrDefault(a => a.ID == id);
            if (article != null)
            {
                dc.Article.DeleteOnSubmit(article);
                dc.SubmitChanges();
            }
        }
        AddLog(id, "Article", "删除文章");
    }
    #endregion

    #region 文章分类
    public static ArticleCategory GetArticleCategory(int id)
    {
        using (var dc = new MainDataContext())
        {
            dc.ObjectTrackingEnabled = false;
            return dc.ArticleCategory.SingleOrDefault(a => a.ID == id);
        }
    }
    public static ArticleCategory[] GetArticleCategories()
    {
        using (var dc = new MainDataContext())
        {
            dc.ObjectTrackingEnabled = false;
            return dc.ArticleCategory.OrderBy(a => a.Name).ToArray();
        }
    }
    public static bool IsArticleCategoryHasArticle(int id)
    {
        using (var dc = new MainDataContext())
        {
            dc.ObjectTrackingEnabled = false;
            return dc.Article.Where(a => a.CategoryID == id).Count() > 0;
        }
    }
    public static void SetArticleCategory(ArticleCategory category)
    {
        var description = category.ID == 0 ? "添加文章分类" : "修改文章分类";
        using (var dc = new MainDataContext())
        {
            if (category.ID == 0) // 新文章分类
            {
                dc.ArticleCategory.InsertOnSubmit(category);
            }
            else
            {
                dc.ArticleCategory.Attach(category, true);
            }
            dc.SubmitChanges();
        }
        AddLog(category.ID, "ArticleCategory", description);
    }
    public static void DeleteArticleCategory(int id)
    {
        using (var dc = new MainDataContext())
        {
            var category = dc.ArticleCategory.SingleOrDefault(u => u.ID == id);
            if (category != null)
            {
                dc.ArticleCategory.DeleteOnSubmit(category);
                dc.SubmitChanges();
            }
        }
        AddLog(id, "ArticleCategory", "删除文章分类");
    }
    #endregion

    #region 用户
    public static User GetUser(int id)
    {
        using (var dc = new MainDataContext())
        {
            dc.ObjectTrackingEnabled = false;
            var dlo = new DataLoadOptions();
            dlo.LoadWith<User>(a => a.Permission);
            dc.LoadOptions = dlo;
            return dc.User.SingleOrDefault(u => u.ID == id);
        }
    }
    public static User GetUser(string name)
    {
        using (var dc = new MainDataContext())
        {
            dc.ObjectTrackingEnabled = false;
            var dlo = new DataLoadOptions();
            dlo.LoadWith<User>(a => a.Permission);
            dc.LoadOptions = dlo;
            return dc.User.SingleOrDefault(u => u.Name == name);
        }
    }
    public static User[] GetUsers(
        int page = 1, int pageSize = 0,
        int permissionID = 0)
    {
        using (var dc = new MainDataContext())
        {
            dc.ObjectTrackingEnabled = false;
            var dlo = new DataLoadOptions();
            dlo.LoadWith<User>(a => a.Permission);
            dc.LoadOptions = dlo;
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
    }
    public static int GetUserCount(
        int permissionID = 0)
    {
        using (var dc = new MainDataContext())
        {
            dc.ObjectTrackingEnabled = false;
            var users = dc.User.AsQueryable();
            if (permissionID != 0)
            {
                users = users.Where(u => u.PermissionID == permissionID);
            }
            return users.Count();
        }
    }
    public static bool IsUserNameExist(string name, int ignoredUser = 0)
    {
        using (var dc = new MainDataContext())
        {
            dc.ObjectTrackingEnabled = false;
            var users = dc.User.AsQueryable();
            users = users.Where(u => u.ID != ignoredUser);
            users = users.Where(u => u.Name == name);
            return users.Count() > 0;
        }
    }
    public static bool IsOtherUserHasUserPermission(int id)
    {
        using (var dc = new MainDataContext())
        {
            dc.ObjectTrackingEnabled = false;
            var users = dc.User.AsQueryable();
            users = users.Where(u => u.ID != id);
            users = users.Where(u => u.Permission.User);
            return users.Count() > 0;
        }
    }
    public static bool IsUserHasArticle(int id)
    {
        using (var dc = new MainDataContext())
        {
            dc.ObjectTrackingEnabled = false;
            return dc.Article.Where(a => a.PublisherID == id).Count() > 0;
        }
    }
    public static void SetUser(User user)
    {
        var description = user.ID == 0 ? "添加用户" : "修改用户";
        using (var dc = new MainDataContext())
        {
            if (user.ID == 0) // 新用户
            {
                dc.User.InsertOnSubmit(user);
            }
            else
            {
                dc.User.Attach(user, true);
                if (user.Password != DB.GetUser(user.ID).Password)
                {
                    user.PasswordSalt = null;
                }
            }
            dc.UserPermission.Attach(user.Permission, false);
            if (user.PasswordSalt == null)
            {
                user.PasswordSalt = Guid.NewGuid().ToString("N").ToUpper();
                user.Password = _.ComputeHmac(user.Password, user.PasswordSalt);
            }
            dc.SubmitChanges();
        }
        AddLog(user.ID, "User", description);
    }
    public static void DelectUser(int id)
    {
        using (var dc = new MainDataContext())
        {
            User user = dc.User.SingleOrDefault(u => u.ID == id);
            if (user != null)
            {
                dc.User.DeleteOnSubmit(user);
                dc.SubmitChanges();
            }
        }
        AddLog(id, "User", "删除用户");
    }
    #endregion

    #region 用户权限
    public static UserPermission GetUserPermission(int id)
    {
        using (var dc = new MainDataContext())
        {
            dc.ObjectTrackingEnabled = false;
            return dc.UserPermission.SingleOrDefault(u => u.ID == id);
        }
    }
    public static UserPermission[] GetUserPermissions()
    {
        using (var dc = new MainDataContext())
        {
            dc.ObjectTrackingEnabled = false;
            return dc.UserPermission.ToArray();
        }
    }
    #endregion

    #region 日志
    public static Log[] GetLogs(
        int page = 1, int pageSize = 0,
        int userID = 0,
        int targetID = 0,
        string targetType = null)
    {
        using (var dc = new MainDataContext())
        {
            dc.ObjectTrackingEnabled = false;
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
    }
    public static int GetLogCount(
        int userID = 0,
        int targetID = 0,
        string targetType = null)
    {
        using (var dc = new MainDataContext())
        {
            dc.ObjectTrackingEnabled = false;
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
    }
    public static void AddLog(int targetID, string targetType, string description)
    {
        using (var dc = new MainDataContext())
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
    }
    #endregion
}