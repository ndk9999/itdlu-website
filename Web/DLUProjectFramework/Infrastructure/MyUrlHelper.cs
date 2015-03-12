using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using ColorLife.Core.Mvc;
using DLUProject.Domain;
using DLUProjectFramework.Mvc;
/// <summary>
/// Summary description for UrlHelper
/// </summary>
public static class MyUrlHelper
{
    public static string DomainURI
    {
        get
        {
            var appPath = ColorLife.Core.Helper.WebHelper.FullyQualifiedApplicationPath;
            return appPath;
        }
    }
    public static string PortalURL
    {
        get
        {
            return DomainURI + "Admin/";
        }
    }
    public static string AccessDennyURL
    {
        get
        {
            return PortalURL + "/AccessDeny.aspx";
        }
    }
    public static string GetUnlockText(this HtmlHelper helper)
    {
        string a = "Email is locked, click " + helper.ActionLink("here to unlock.", "unlock");
        return a;
    }
    private static bool IsAllowAccess(AccessFlag action)
    {
        bool ok = false;
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
            //  var acc = HttpContext.Current.Session.GetAccountSession().IsAllowAccess(controllerName, action.ToString());
            ok = true;
        }
        return ok;
    }
    public static MvcHtmlString CreateLink(this HtmlHelper helper)
    {
        var url = helper.ImageActionLink("ribbon-button ribbon-button-small", "fa fa-file-o", "Thêm mới", "Create");
        var canAccess = IsAllowAccess(AccessFlag.Create);
        if (!canAccess) return MvcHtmlString.Empty;
        return MvcHtmlString.Create(url.ToHtmlString());
    }
    public static MvcHtmlString ImportLink(this HtmlHelper helper)
    {
        var url = helper.ImageActionLink("ribbon-button ribbon-button-small", "fa fa-file-excel-o", "Nhập", "Import");
        var canAccess = IsAllowAccess(AccessFlag.Import);
        if (!canAccess) return MvcHtmlString.Empty;
        return MvcHtmlString.Create(url.ToHtmlString());
    }
    public static MvcHtmlString DeleteLink(this HtmlHelper helper, object obj)
    {
        var url = helper.ImageActionLink("red", "ace-icon fa fa-trash-o bigger-130", "", "Delete", new { id = obj });
        var canAccess = IsAllowAccess(AccessFlag.Delete);
        if (!canAccess) return MvcHtmlString.Empty;
        return MvcHtmlString.Create(url.ToHtmlString());
    }
    public static MvcHtmlString EditLink(this HtmlHelper helper, object obj)
    {
        var url = helper.ImageActionLink("green", "ace-icon fa fa-pencil bigger-130", "", "Edit", new { id = obj });
        var canAccess = IsAllowAccess(AccessFlag.Edit);
        if (!canAccess) return MvcHtmlString.Empty;
        return MvcHtmlString.Create(url.ToHtmlString());
    }
    public static MvcHtmlString ButtonCreateLink(this HtmlHelper helper)
    {
        var url = helper.ImageActionLink("btn btn-primary btn-sm", "fa fa-plus", "Thêm mới", "Create");
        var canAccess = IsAllowAccess(AccessFlag.Create);
        if (!canAccess) return MvcHtmlString.Empty;
        return MvcHtmlString.Create(url.ToHtmlString());
    }
    public static MvcHtmlString ButtonCreateLinkModal(this HtmlHelper helper)
    {
        var url = string.Format("<a class=\"ribbon-button ribbon-button-small\" href=\"{0}\" data-toggle=\"modal\" data-target=\"#addNewModal\"><i class=\"fa fa-file-o\"></i>{1}</a>","#","Thêm mới");
        var canAccess = IsAllowAccess(AccessFlag.Create);
        if (!canAccess) return MvcHtmlString.Empty;
        return MvcHtmlString.Create(url.ToString());
    }
    public static MvcHtmlString ButtonImportLink(this HtmlHelper helper)
    {
        var url = helper.ImageActionLink("ribbon-button ribbon-button-small", "fa fa-file-excel-o", "Nhập", "Import");
        var canAccess = IsAllowAccess(AccessFlag.Import);
        if (!canAccess) return MvcHtmlString.Empty;
        return MvcHtmlString.Create(url.ToHtmlString());
    }
    public static MvcHtmlString ButtonImportLinkModal(this HtmlHelper helper)
    {
        var url = string.Format("<a class=\"ribbon-button ribbon-button-small\" href=\"{0}\" data-toggle=\"modal\" data-target=\"#importExcel\"><i class=\"fa fa-file-excel-o\"></i>{1}</a>", "#", "Nhập");
        var canAccess = IsAllowAccess(AccessFlag.Create);
        if (!canAccess) return MvcHtmlString.Empty;
        return MvcHtmlString.Create(url.ToString());
    }
    public static MvcHtmlString ButtonExportLink(this HtmlHelper helper)
    {
        var url = helper.ImageActionLink("ribbon-button ribbon-button-small", "fa fa-file-excel-o", "Xuất", "ExportToExcel");        
        return MvcHtmlString.Create(url.ToHtmlString());
    }
}