using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLUProject.Services;

namespace DLUProjectFramework.Infrastructure
{
    public class LoginAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var controller = filterContext.Controller as BaseController;
            if (controller == null)
                return;
            var account = filterContext.HttpContext.Session.GetAccountSession();
            if (account == null)
                filterContext.Result = controller.RedirectToLogin();



            // var exits = InFunction.FirstOrDefault(c => c.WorkGroup.Url.ToLower().Equals(folder) && c.Url.ToLower().Equals(page));
            // return exits != null;
        }
    }
}