using System.Web.Mvc;
using DLUProjectFramework.Infrastructure;
namespace DLUProjectMvc.Areas.Security.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Security/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}