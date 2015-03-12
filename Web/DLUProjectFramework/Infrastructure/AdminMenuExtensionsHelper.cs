using DLUProject.Domain;
using DLUProject.Model;
using DLUProject.Services;
using DLUProjectFramework.DependencyResolution;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
 


public static class AdminMenuExtensionsHelper
{
    private static object syncLock = new object();
    private static IServices<WorkGroup> _instance;
   // private static AccountSessionModel accountSession = null;
    public static IServices<WorkGroup> Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        var container = new Container(c => c.AddRegistry<DefaultRegistry>());
                        _instance = container.GetInstance<WorkGroupService>();
                    }
                }
            }
            return _instance;
        }
    }
    public static HtmlString WriteLeftSubMenu(this HtmlHelper htmlHelper, int parentID)
    {
        HttpSessionStateBase sessionBase = new HttpSessionStateWrapper(HttpContext.Current.Session);
        var accountSession = sessionBase.GetAccountSession();
        if (accountSession == null)
            return MvcHtmlString.Create(string.Empty);


        StringBuilder html = new StringBuilder();
        var db = accountSession.InWorkgroup.Where(c => c.IsEnabled == true && c.IsDisplayFlag(DisplayFlagMenu.Left)).ToList();
      //  var db = Instance.All().Where(c => c.IsEnabled == true).ToList();
        html.AppendLine("<ul class=\"nav nav-list\">");
        foreach (var item in db.Where(x => x.ParentID == parentID))
        {
            html.AppendLine(" <li class=\"active1\">");

            bool hasLv1 = hasSubLevel1(db, item);
            if (hasLv1)
            {

                html.AppendLine("<a href=\"/Admin\" class=\"dropdown-toggle\">");
                html.AppendLine("<i class=\"menu-icon fa fa-desktop\"></i>");
                html.AppendLine("<span class=\"menu-text\">" + item.Name + "</span>");
                html.AppendLine("<b class=\"arrow fa fa-angle-down\"></b></a>");
                html.AppendLine("<b class=\"arrow\"></b>");

                html.AppendLine("<ul class=\"submenu\">");
                foreach (var item2 in db.Where(c => c.ParentID == item.WorkGroupID))
                {
                    var funcs = accountSession.InFunction.Where(c => c.WorkGroupID.Equals(item2.WorkGroupID) &&c.IsEnabled==true);
                    if (funcs.Count() > 0)
                    {
                        html.AppendLine("<li class=\"\">");
                        html.AppendLine("<a href=\"/" + item2.Systems.Url + "/" + item2.Url + "\" class=\"dropdown-toggle\"><i class=\"menu-icon fa fa-caret-right\"></i>" + item2.Name + "</a><b class=\"arrow fa fa-angle-down\"></b>");
                        html.AppendLine("<b class=\"arrow\"></b>");
                      html.AppendLine(" <ul class=\"submenu\" style=\"display: block;\">");
                        foreach (var itemx in funcs)
                        {
                            html.AppendLine("<li class=\"\"><a href=\"/" + item2.Systems.Url + "/" +item2.Url + "/" + itemx.Url + "\" ><i class=\"menu-icon fa fa-caret-right\"></i>  " + itemx.Name + "  </a>         </li>");
                        }
                        html.AppendLine("</ul>");
                        html.AppendLine("</li>");
                    }
                    else
                        html.AppendLine("<li class=\"s\"><a href=\"/" + item2.Systems.Url + "/" + item2.Url + "\"><i class=\"menu-icon fa fa-caret-right\"></i>" + item2.Name + "</a><b class=\"arrow\"></b></li>");
                }
                html.AppendLine("</ul>");
            }
            else
            {
                html.AppendLine("<a href=\"/Admin\">");
                html.AppendLine("<i class=\"menu-icon fa fa-tachometer\"></i>");
                html.AppendLine("<span class=\"menu-text\">" + item.Name + "</span>");
                html.AppendLine("</a>");
            }
            html.AppendLine("</li>");
        }
        html.AppendLine("</ul>");
        return MvcHtmlString.Create(html.ToString());
    }
    private static bool hasSubLevel1(List<WorkGroup> list, WorkGroup item)
    {
        var a = list.Where(c => c.ParentID == item.WorkGroupID);
        return a.Count() > 0;
    }
    private static string SubMenu(List<WorkGroup> db, int parentID)
    {
        var e = db.FindAll(x =>  x.ParentID == parentID);
        string html = "";
        html += "<ul class='subnav-menu'>";
        foreach (var item in e)
        {
            html += " <li " + GetSubmenuChild(db, item.WorkGroupID) + ">";
            html += "   <a href='" + item.Url + "'>" + item.Name + "</a>";
            html += SubMenu(db, item.WorkGroupID);

            html += "</li>";
        }

        html += "</ul>";

        return html;
    }
    public static string GetSubmenuChild(List<WorkGroup > db, int parentID)
    {
        var e = db.FindAll(x =>  x.ParentID == parentID);
        string html = "class='dropdown-menu1'";
        if (e.Count > 0)
            return html;
        return string.Empty;
    }
    private static string GetCaret(List<WorkGroup> db, int parentID)
    {
        var e = db.FindAll(x =>  x.ParentID == parentID);
        string html = "<span class='caret'></span>";
        if (e.Count > 0)
            return html;
        return string.Empty;
    }
    static string CountSubMenu(List<WorkGroup> db, int parentID)
    {
        var e = db.FindAll(x =>  x.ParentID == parentID);
        string html = " data-toggle='dropdown' class='dropdown-toggle' ";
        if (e.Count > 0)
            return html;
        return string.Empty;
    }
    static string CurrentUrl(string pg)
    {
        var cls = "";
        var nodeUrl = pg;
        var startIndx = nodeUrl.LastIndexOf("/Admin/") > 0 ? nodeUrl.LastIndexOf("/Admin/") : 0;
        var endIndx = pg.LastIndexOf(".") > 0 ? pg.LastIndexOf(".") : pg.Length;
        var nodeDir = pg.Substring(startIndx, endIndx - startIndx);

        if (HttpContext.Current.Request.RawUrl.IndexOf(nodeUrl, StringComparison.OrdinalIgnoreCase) != -1 ||
            (HttpContext.Current.Request.RawUrl.IndexOf(nodeDir, StringComparison.OrdinalIgnoreCase) != -1) &&
            !nodeDir.EndsWith("/Admin"))
        {
            cls = "active";
        }

        // if "page" has its own subfolder (comments, extensions etc.) 
        // select parent tab when navigating through child tabs
        startIndx = nodeDir.LastIndexOf("/");
        if (startIndx > 0)
        {
            nodeDir = nodeDir.Substring(0, startIndx);
            if (HttpContext.Current.Request.RawUrl.ToLower().Contains(nodeDir.ToLower() + "/") && nodeDir.Substring(1, nodeDir.Length - 1).IndexOf("/") > 0)
            {
                cls = "active";
            }
        }
        return cls;
    }

    public static string WriteLeftMenu(int parentId)
    {
        string html = "";
        html += " <div class='subnav'>";
        html += "<div class='subnav-title'>";
        html += "<a href='" + parentId + "' class='toggle-subnav'><i class='icon-angle-down'></i><span>" + parentId + "</span></a>";
        html += "</div>";

        html += "</div>";
        return html;
    }
}
