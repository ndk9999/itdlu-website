using System.Linq;
using System.Web;
using System.Text;
using ColorLife.Core.FileManager;
 
 
 
using System.Collections.Generic;
using StructureMap;
 
using System.Web.Mvc;
using DLUProject.Domain;
using DLUProject.Data;
using DLUProject.Services;
/// <summary>
/// Summary description for MenuHelper
/// </summary>
public static class AdminMenuHelper
{

    static IServices<WorkGroup> wk = ObjectFactory.GetInstance <WorkGroupService>();
  //  static AccountSessionModel accountSession = HttpContext.Current.Session.GetAccountSession();
     
    private static List<Function> GetByWorkGroupID(int idWorkGroup)
    {
       // return accountSession.InFunction.Where(c => c.IsEnabled == true && c.WorkGroupID == idWorkGroup).ToList();
        return new List<Function>();
        //  return _functionService.All().Where(c => c.IsEnabled == true && c.WorkGroupID == idWorkGroup).ToList();
    }
    public static MvcHtmlString   LoadWorkgroupFunction(this HtmlHelper helper)
    {
        StringBuilder html = new StringBuilder();
        var db = wk.All().Where(c => c.IsEnabled == true && c.IsDisplayFlag(DisplayFlagMenu.Top)).ToList();

        foreach (var item in db.Where(c => c.ParentID == 0))
        {

            html.AppendLine("<div class=\"ribbon-tab\" id=\"tab" + item.WorkGroupID + "\" class=\"ribbon-tab-header sel\">");
            html.AppendLine("<span class=\"ribbon-title\">" + item.Name + "</span>");

            var wkr = db.Where(c => c.ParentID == item.WorkGroupID);
            html.AppendLine("<div class=\"ribbon-section\">");
            html.AppendLine("<span class=\"section-title\">" + item.Name + "</span>");
            foreach (var i in wkr)
            {
                html.AppendLine("<a class=\"ribbon-button ribbon-button-large " + CurrentUrl(i.Url.ToLower()) + "\" href=" + MyUrlHelper.DomainURI+ i.Url + " data-controller='"+i.Url+"'>");
                html.AppendLine("<span class=\"button-title\">" + i.Name + "</span>");
                html.AppendLine("  <span class=\"button-help\">Thống kê danh sách bộ theo đơn vị.</span>");
                html.AppendLine("<img class=\"ribbon-icon ribbon-normal\" src=\"" + i.Image + "\" width=\"32px\"/>");
                html.AppendLine("</a>");

                //   var funs = GetByWorkGroupID(i.WorkGroupID);
                //  html.AppendLine(LoadFunction(funs));
            }
            html.AppendLine("</div>");
            // html.AppendLine("<div class=\"ribbon-section-sep\" unselectable=\"on\"></div>");
            html.AppendLine("</div>");

            //html.Append("<li class='" + CurrentUrl(item.Url.ToLower()) + "'>");
            //html.Append("<a href=\"" + UrlHelper.PortalURL + item.UrlFull + "\" title='" + item.Name + "' " + CountSubMenu(db, item.WorkGroupID) + "><span>" + item.Name + "</span>");
            //html.Append(GetCaret(db, item.WorkGroupID) + "</a>");

            //var functions = GetByWorkGroupID(item.WorkGroupID);
            //html.Append("<ul class='dropdown-menu'>");
            //html.Append(SubMenu(db, item.WorkGroupID));
            //foreach (var i in functions)
            //{
            //    html.Append("<li ><a href= '" + UrlHelper.PortalURL + item.UrlFull + "/Default.aspx?controller=" + i.Url + "' title='" + i.Name + "'>" + i.Name + "</a></li>");
            //}

            //html.Append("</ul>");

            //html.Append("</li>");
        }
        return MvcHtmlString.Create(html.ToString());
    }
   
    private static string LoadFunction(List<Function> list)
    {
        StringBuilder html = new StringBuilder();

        html.AppendLine("<div class=\"ribbon-section\">");
        // html.AppendLine("<span class=\"section-title\">" + item.Name + "</span>");
        foreach (var c in list)
        {
            html.AppendLine("<div class=\"ribbon-button ribbon-button-small \" >");
            html.AppendLine("<a class=\"button-title\" href=" + MyUrlHelper.PortalURL + c.WorkGroup.UrlFull + "/Default.aspx?controller=" + c.Url + ">" + c.Name + "</a>");
            html.AppendLine("  <span class=\"button-help\">Thống kê danh sách bộ theo đơn vị.</span>");
            html.AppendLine("<img class=\"ribbon-icon ribbon-normal\" src=\"" + c.Image + "\" width=\"32px\"/>");
            html.AppendLine("</div>");
        }
        html.AppendLine("</div>");

        return html.ToString();
    }
    static string CurrentUrl(string pg)
    {
        var cls = "";
        var actionName = string.Empty;
        var controllerName = string.Empty;
        var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
        if (routeValues != null)
        {
            if (routeValues.ContainsKey("action"))
            {
                actionName = routeValues["action"].ToString();
                // ViewBag.Action = actionName;
            }
            if (routeValues.ContainsKey("controller"))
            {
                controllerName = routeValues["controller"].ToString();
                // ViewBag.Controller = controllerName;
            }
            if (controllerName.ToLower().Equals(pg))
            {
                cls = "sel";
            }
            return cls;
        }
        return cls;
    }
    static string CurrentPage(string pg)
    {
        var cls = "";

        if (HttpContext.Current.Request.Path.ToLower().Contains(pg.ToLower()))
        {
            cls = "active";
        }
        return cls;
    }
    public static MvcHtmlString LoadAllMenuIcon(this HtmlHelper helper)
    {
        StringBuilder sb = new StringBuilder();
        string allowedExtensions = "^.jpg|.gif|.png|.jpeg|.PNG|.JPEG|.GIF$";
        string path = "/Content/icons/";
        var query = from item in FileManager.Instance.All(path, FileFilter.Files, allowedExtensions)
                    //  orderby item.LastWriteDate
                    select item;
        foreach (var i in query)
        {
            sb.Append("<div class=\"col-sm-12 col-md-2\">");
            sb.Append("<div class=\"thumbnail\">");
            sb.Append("<img src=\"" + path + i.Name + " \" alt=\"" + i.Name + "\" style=\"width: 48px; height: 48px;\" onclick=\"return insertIcon('" + path + i.Name + "')\"/>");
            sb.Append("<div class=\"caption\" style='text-align:center; font-weight:bold'>");
            // sb.Append(i.Name);
            sb.Append("</div>");
            sb.Append("</div></div>");
        }
        return MvcHtmlString.Create(sb.ToString());
    }
}