/* FileName: StaffController.cs
Project Name: DLUProject
Date Created: 22/11/2014 8:50:50 PM
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
namespace DLUProjectMvc.Areas.Admin.Controllers
{
     
    public class StaffController : BaseController
    {
        private IServices<Staff> _service ;
        private IServices<StaffDepartment> _staffDepartment;
        private DepartmentExtension _departmentExt;
        private StaffExtension _staffExt;
        public StaffController(IServices<Staff> service,IServices<StaffDepartment>staffDepartment,
            DepartmentExtension departmentExt, StaffExtension staffExt)
        {
            this._service = service;
            this._staffDepartment = staffDepartment;
            this._departmentExt = departmentExt;
            this._staffExt = staffExt;
        }
        void InitData()
        {
            ViewBag.Departments = _departmentExt.GetByParent(new List<Department>(), 0);
        }
        public ActionResult Index(int? page, int? pageSize, string DepartmentID, string queryString)
        {
            int pageIndex = (page ?? 1);
            int pageSize1 = (pageSize ?? 25);
            int deptId = DepartmentID.ToInt();

            var model =   _staffExt.GetAllStaffs(pageIndex, pageSize1, deptId, queryString); //_staffExt.GetAllStaffDepartment().ToPagedList(pageIndex, pageSize1);//
            InitData();
            if (Request.IsAjaxRequest())
                return PartialView("_ListPartial", model);
            return View(model);
        }
        public ActionResult Create()
        {
            InitData();
            return View(new Staff
            {
                StaffID = 0,
                Image = "/content/images/noimage.png",
                DOB = DateTime.Parse("01/01/1900")
            });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Staff model, int[] deptIds)
        {
			if (ModelState.IsValid)
            {
                var rs = _service.Insert(model);
                if (rs > 0)
                {
                    if (deptIds != null)
                    {                        
                        foreach (var item in deptIds)
                        {
                            _staffExt.AddToDepartment(rs, item);
                        }
                    }
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
            } InitData();
            return View(model);
        }
        public ActionResult Edit(int id = 0)
        {
            var model = _service.Get(id);
            if (model == null)
            {
                return RedirectToAction("Create");
            }
            ViewBag.StaffInDepartment = _staffExt.InDepartment(model);
            InitData();
            return View(model);
        }
        [HttpPost]
		 [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Staff model,int[] deptIds)
        {
             if (ModelState.IsValid)
            {
                var rs = _service.Update(model);
                if (rs > 0)
                {
                    if (deptIds != null)
                    {
                        _staffExt.RemoveFromDepartment(model.StaffID);
                        foreach (var item in deptIds)
                        {
                            _staffExt.AddToDepartment(model.StaffID, item);
                        }
                    }
                    var notification = new Notification { Fail = rs > 0, Message = "Cập nhật dữ liệu thành công.", Exception = null };
                    TempData["Notification"] = notification;// "Cập nhật dữ liệu thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật dữ liệu thất bại!.");
                }
            }
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
            int rs = _service.Delete(id);
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

