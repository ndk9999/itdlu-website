using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLUProjectFramework.Infrastructure;
namespace DLUProjectMvc.Areas.Admin.Controllers
{
    [LoginAuthorize]
    public class AccessDenyController : Controller
    {
        // GET: Admin/AccessDeny
        public ActionResult Index()
        {
            return View();
        }
    }
}