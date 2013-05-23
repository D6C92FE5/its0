using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExceptionLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ctList.DataSource =
            from ex in _.ExceptionLogQueue
            orderby (DateTime)ex.Data["__its_exception_occur_time__"] descending
            select new
            {
                ID = "ex" + ex.GetHashCode().ToString(),
                Time = ex.Data["__its_exception_occur_time__"],
                Type = ex.GetType(),
                Url = ex.Data["__its_exception_url__"],
                Message = _.PrepareForHtml(ex.Message),
                StackTrace = _.PrepareForHtml(ex.StackTrace),
            };
        ctList.DataBind();
    }
}