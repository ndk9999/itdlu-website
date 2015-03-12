using System.Linq;
using System.Web;
using System.Text;
using System.Collections.Generic;
using StructureMap;
 
using System.Web.Mvc;
using DLUProject.Domain;
using DLUProject.Services;
using DLUProjectFramework.DependencyResolution;


/// <summary>
/// Summary description for MenuHelper
/// </summary>
/// 
public static class MenuExtensions
{

    private static object syncLock = new object();
    private static IServices<Menus> _instance;
    public static IServices<Menus> Instance
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
                        _instance = container.GetInstance<MenusService>();
                    }
                }
            }
            return _instance;
        }
    }
    public static MvcHtmlString MenuSingle(this HtmlHelper helper, int menuId, bool? showIcon)
    {
        bool f = (showIcon ?? false);
        var item = Instance.All().Where(c => c.IsPublished).SingleOrDefault(x => x.MenuID.Equals(menuId));
        if (item == null) return MvcHtmlString.Empty;
        StringBuilder html = new StringBuilder();
        html.AppendLine("<a href='" + item.Url + "'>");
        if (!f)
            html.AppendLine(item.Name);
        else html.AppendLine("<img src='" + item.Image + "' width='100%'/>");
        html.AppendLine("</a>");
        return MvcHtmlString.Create(html.ToString());
    }
    private static List<Menus> GetDLUMainMenu(int parentId)
    {
        var data = Instance.Table.Where(c => c.IsPublished == true && c.ParentID == parentId).Select(c => new Menus
        {
            MenuID = c.MenuID,
            Name = c.Name,
            ParentID = c.ParentID,
            Url = c.Url,
            DisplayFlags = c.DisplayFlags,
            SortOrder = c.SortOrder
        }).OrderBy(c => c.SortOrder).ToList();
        var list = data.Where(c => c.IsDisplayFlag(DisplayFlagMenuEnum.MainMenu));
        return list.ToList();
    }
    public static StringBuilder GetDLUMainMenu1(int parentId)
    {
        var list = GetDLUMainMenu(parentId);
        StringBuilder html = new StringBuilder();
        html.AppendLine("<ul>");
        foreach (var item in list.Where(c => c.ParentID == parentId))
        {
            html.AppendLine("<li>");
            html.AppendLine("<a href=" + item.Url + ">" + item.Name + "</a>");
            var subMenu = list.Where(c => c.ParentID == item.MenuID);
           // if (subMenu.Count() > 0 && !item.MenuID.Equals(item.ParentID))
           // {
                html.AppendFormat(GetDLUMainMenu1(item.MenuID).ToString());
           // }
            html.AppendLine("</li>");
        }
        html.AppendLine("</ul>");
       
        return html;
    }
    //private string GenerateUL(DataRow[] menu, DataTable table, StringBuilder sb)
    //{
    //    sb.AppendLine("<ul>");

    //    if (menu.Length > 0)
    //    {
    //        foreach (DataRow dr in menu)
    //        {
    //            string handler = dr["Handler"].ToString();
    //            string menuText = dr["MENU"].ToString();
    //            string line = String.Format(@"<li><a href=""{0}"">{1}</a>", handler, menuText);
    //            sb.Append(line);

    //            string pid = dr["PID"].ToString();
    //            string parentId = dr["ParentId"].ToString();

    //            DataRow[] subMenu = table.Select(String.Format("ParentId = {0}", pid));
    //            if (subMenu.Length > 0 && !pid.Equals(parentId))
    //            {
    //                var subMenuBuilder = new StringBuilder();
    //                sb.Append(GenerateUL(subMenu, table, subMenuBuilder));
    //            }
    //            sb.Append("</li>");
    //        }
    //    }
    //    sb.Append("</ul>");
    //    return sb.ToString();
    //}
    private static string GenerateUL(List<Menus> menu, List<Menus> table, StringBuilder sb)
    {
        sb.AppendLine("<ul>");

        if (menu.Count > 0)
        {
            foreach (var item in menu)
            {
                string line = string.Format(@"<li><a href=""{0}"">{1}</a>", item.Url, item.Name);
                sb.Append(line);
                var subMenu = table.Where(c => c.ParentID == item.MenuID);
                if (subMenu.Count() > 0 && !item.MenuID.Equals(item.ParentID))
                {
                    var subMenuBuilder = new StringBuilder();
                    sb.Append(GenerateUL(subMenu.ToList(), table, subMenuBuilder));
                }
                sb.Append("</li>");
            }
        }
        sb.Append("</ul>");
        return sb.ToString();
    }
    public static MvcHtmlString GetDLUMainMenu(this HtmlHelper helper)
    {
        var data = Instance.Table.Where(c => c.IsPublished == true).Select(c => new Menus
        {
            MenuID = c.MenuID,
            Name = c.Name,
            ParentID = c.ParentID,
            Url = c.Url,
            DisplayFlags = c.DisplayFlags,
            SortOrder = c.SortOrder
        }).OrderBy(c => c.SortOrder).ToList();
        var list = data.Where(c => c.IsDisplayFlag(DisplayFlagMenuEnum.MainMenu));
        var parentMenus = list.Where(c => c.ParentID == 0);
        var sb = new StringBuilder();
        string unorderedList = GenerateUL(parentMenus.ToList(), list.ToList(), sb);

        return MvcHtmlString.Create(unorderedList.ToString());
    }
    public static MvcHtmlString GetMainMenu(this HtmlHelper helper)
    {
        var list = Instance.All().Where(c => c.IsPublished == true && c.IsDisplayFlag(DisplayFlagMenuEnum.MainMenu));
        StringBuilder html = new StringBuilder();
        html.AppendLine(" <ul class=\"nav navbar-nav collapse navbar-collapse\">");
        foreach (var item in list.Where(c => c.ParentID == 0))
        {
            if (hasChild(list.ToList(), item))
            {
                html.AppendLine("<li class=\"dropdown\">");
                html.AppendLine("<a href=" + item.Url + ">" + item.Name + "<i class=\"fa fa-angle-down\"></i></a>");
                html.AppendLine("<ul role=\"menu\" class=\"sub-menu\">");
                foreach (var item2 in Instance.All().Where(c => c.ParentID.Equals(item.MenuID)))
                {
                    html.AppendLine(" <li><a href=" + item2.Url + ">" + item2.Name + "</a></li>");
                }
                html.AppendLine("</ul>");
                html.AppendLine("</li>");
            }
            else
                html.AppendLine("<li><a href=" + item.Url + " class=" + CurrentUrl(item.Url) + ">" + item.Name + "</a></li>");
        }
        html.AppendLine("</ul>");

        return MvcHtmlString.Create(html.ToString());
    }
    private static bool hasChild(List<Menus> list, Menus item)
    {
        var a = list.Where(c => c.ParentID == item.MenuID);
        return a.Count() > 0;
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
                cls = "active";
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

    public static MvcHtmlString GetFooterMenu(this HtmlHelper helper, DisplayFlagMenuEnum flags)
    {
        StringBuilder html = new StringBuilder();
        var list = Instance.All().Where(c => c.IsPublished == true && c.IsDisplayFlag(flags));
        html.AppendLine("<ul class=\"nav nav-pills nav-stacked\">");
        foreach (var item in list)
        {
            html.AppendLine("<li><a href=" + item.Url + ">" + item.Name + "</a></li>");
        }
        html.AppendLine("</ul>");
        return MvcHtmlString.Create(html.ToString());
    }
}