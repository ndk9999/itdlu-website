 

using System.Web.Mvc;


namespace DLUProjectFramework.Infrastructure
{
    [LoginAuthorize]
     [AccessRequestRole]
    public class BaseController : Controller
    {
        public ActionResult RedirectToLogin()
        {
            return RedirectToAction("Index", "Login", new { area = "Admin", returnUrl = Request.Path });
        }
        public ActionResult RedirectToAccessDeny()
        {
            return RedirectToAction("Index", "AccessDeny", new { area = "Admin", returnUrl = Request.Path });
        }
    }
}
