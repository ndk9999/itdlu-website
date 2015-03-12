using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLUProject.Services;
namespace DLUProjectFramework.Infrastructure
{
    public class AccessRequestRoleAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionName = string.Empty;
            var controllerName = string.Empty;
            var controller = filterContext.Controller as BaseController;
            var routeValues = filterContext.RequestContext.RouteData.Values;
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

                var account = filterContext.HttpContext.Session.GetAccountSession();
                bool canAccess = account.IsAllowAccess(controllerName.ToLower(), actionName.ToLower());
                if (!canAccess)
                    filterContext.Result = controller.RedirectToAccessDeny();
            }
        }
    }
}