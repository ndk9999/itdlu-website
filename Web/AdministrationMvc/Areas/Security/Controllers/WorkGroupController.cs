/* FileName: WorkGroupController.cs
Project Name: DLUProject
Date Created: 7/11/2014 12:04:30 AM
Description: Auto-generated
Version: 1.0.0.0
Author:	Lê Thanh Tuấn - Khoa CNTT
Author Email: tuanitpro@gmail.com
Author Mobile: 0976060432
Author URI: http://tuanitpro.com
License: 

*/
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ColorLife.Core.Mvc;
using DLUProject.Domain;
using DLUProject.Data;
using DLUProject.Services;
using DLUProjectFramework.Mvc;
using DLUProjectFramework.Infrastructure;
using ColorLife.Core.Helper;
namespace DLUProjectMvc.Areas.Security.Controllers
{
     
    public class WorkGroupController : BaseController
    {
        private IServices<WorkGroup> _service ;
        private WorkGroupExtension _workGroupExt;
        private IServices<Systems> _sysService;
        public WorkGroupController(IServices<WorkGroup> service, WorkGroupExtension wkExt, IServices<Systems>sysService)
        {
            this._service = service;
            this._workGroupExt = wkExt;
            this._sysService = sysService;
        }       
        public ActionResult Index(int? page, int? pageSize, string SystemID, string queryString)
        {
            int pageIndex = (page ?? 1);
            int pageSize1 = (pageSize ?? 25);
            int sId = -1;
            if (!string.IsNullOrEmpty(SystemID)) sId = SystemID.ToInt();

            var model = _workGroupExt.GetAllWorkGroup(pageIndex, pageSize1, queryString, sId, false);
            InitData();
            if (Request.IsAjaxRequest())
                return PartialView("_ListPartial", model);
            return View(model);
        }
        void InitData()
        {            
            ViewBag.SystemsList = _sysService.All();
            ViewBag.WorkGroups = _workGroupExt.GetAllDropdownList(false);
        }
        public ActionResult Create()
        {
            InitData();
            return View(new WorkGroup
            {
                WorkGroupID = -1,
                IsEnabled = true,
                ParentID = 0,
                Image = "/content/images/noimage.png"
            });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(WorkGroup model)
        {
			if (ModelState.IsValid)
            {
                var rs = _service.Insert(model);
                if (rs > 0)
                {
                    var notification = new Notification { Fail = rs > 0, Message = "Thêm dữ liệu thành công.", Exception = null };
                    TempData["Notification"] = notification;// "Cập nhật dữ liệu thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    if (rs == -9999)
                    {
                        ModelState.AddModelError("", "Dữ liệu đã tồn tại, vui lòng nhập lại.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Có lỗi trong quá trình xử lý. Vui lòng thử lại.");
                    }
                }
            }
            InitData();
            return View(model);
        }
        public ActionResult Edit(int id = 0)
        {
            var model = _service.Get(id);
            if (model == null)
            {
                return RedirectToAction("Create");
            }
            InitData();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(WorkGroup model)
        {
             if (ModelState.IsValid)
            {
                var rs = _service.Update(model);
                if (rs > 0)
                {
                    var notification = new Notification { Fail = rs > 0, Message = "Cập nhật dữ liệu thành công.", Exception = null };
                    TempData["Notification"] = notification;// "Cập nhật dữ liệu thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật dữ liệu thất bại!.");
                }
            }
             InitData();
            ModelState.AddModelError("", "Dữ liệu nhập vào không hợp lệ!.");
            return View(model);
        }        
        public ActionResult Delete(int id = 0)
        {
            var model = _service.Get(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int rs = _service.Delete(new WorkGroup { WorkGroupID = id });
            if (rs > 0)
            {
                var notification = new Notification { Fail = rs > 0, Message = "Xóa dữ liệu thành công.", Exception = null };
                TempData["Notification"] = notification;// "Cập nhật dữ liệu thành công";
                return RedirectToAction("Index");
            }
            return View();
        }	 
    }
}

