using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLUProject.Domain;
using DLUProject.Services;
using ColorLife.Core.Helper;

namespace DLUProjectMvc.Controllers
{
    public class DepartmentController : Controller
    {
        IServices<Content> _contentService;
        IServices<Department> _departmentService;
        IServices<Notice> _noticeService;
        public DepartmentController(IServices<Content> contentService, IServices<Department> departmentService, IServices<Notice> noticeService)
        {
            this._contentService = contentService;
            this._noticeService = noticeService;
            this._departmentService = departmentService;
        }
        // GET: Department
        public ActionResult Index()
        {
            return View();
        }
    }
}