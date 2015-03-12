using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DLUProjectFramework.Infrastructure
{
    public class CustomerActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            Controller controller = filterContext.Controller as Controller;

            if (controller != null)
            {
                if (session["Customer"] == null)
                {
                    filterContext.Result =
                 new RedirectToRouteResult(
                 new RouteValueDictionary{{ "controller", "Customer" },
                                          { "action", "Login" },
                                          {"returnUrl", controller.HttpContext.Request.Url.PathAndQuery}
                                         });
                    // controller.HttpContext.Response.Redirect("./Login");
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
