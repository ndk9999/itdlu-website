using DLUProject.Domain;
using DLUProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ColorLife.Core.Helper;
namespace DLUProjectMvc.Areas.Security.Controllers
{
    public class PermissionController : Controller
    {

        private IServices<AccountGroup> _service ;
        private AccountGroupExtension _accountGroupExt;
        private IServices<Systems> _systemService;
        public PermissionController(IServices<AccountGroup> service, AccountGroupExtension accountGroupExt, IServices<Systems> systemService)
        {
            this._service = service;
            this._accountGroupExt = accountGroupExt;
            this._systemService = systemService;
        }       

        // GET: Security/Permission
        
        public ActionResult Permission(int id, string SystemID)
        {
            var service = _service.Get(id);
            if (service == null)
            {
                return RedirectToAction("PermissionAll");
            }
            ViewBag.SystemID = _systemService.All();
            ViewBag.AccountGroup = _service.All();
            int sId = -1;
            if (!string.IsNullOrEmpty(SystemID)) sId = SystemID.ToInt();
            var model = _accountGroupExt.AccountGroupFunctionModel(id, sId);

            ViewBag.Title = "Phân quyền - " + service.Name;
            if (Request.IsAjaxRequest())
                return PartialView("_PermissionPartial", model);
            return View(model);
        }
        public ActionResult Index(string GroupID, string SystemID)
        {
            ViewBag.SystemID = _systemService.All();
            ViewBag.AccountGroup = _service.All();
            int gId = -1;
            if (!string.IsNullOrEmpty(GroupID)) { gId = GroupID.ToInt(); }
            int sId = -1;
            if (!string.IsNullOrEmpty(SystemID)) { sId = SystemID.ToInt(); }

            var model = _accountGroupExt.AccountGroupFunctionModel(gId, sId);
            if (Request.IsAjaxRequest())
                return PartialView("_PermissionPartial", model);
            return View(model);
        }
    }
}