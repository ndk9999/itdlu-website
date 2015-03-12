/* FileName: ContentController.cs
Project Name: DLUProject
Date Created: 18/11/2014 10:53:57 AM
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
     
    public class ContentController : BaseController
    {
        private IServices<Content> _service ;
        private CategoryExtension _cateExt;
        private ContentExtension _contentExt;
        private DepartmentExtension _departmentExt;
        public ContentController(IServices<Content> service, CategoryExtension cateExt, ContentExtension contentExt,
              DepartmentExtension departmentExt)
        {
            this._service = service;
            this._cateExt = cateExt;
            this._contentExt = contentExt;
            this._departmentExt = departmentExt;
        }
        void InitData()
        {
            ViewBag.Categories = _cateExt.GetByParent(new List<Category>(), 0, true);
            ViewBag.Departments = _departmentExt.GetAllDropdownList();
        }
        public ActionResult Index(int? page, int? pageSize, string DisplayFlag, string CategoryID, string queryString)
        {
            int pageIndex = (page ?? 1);
            int pageSize1 = (pageSize ?? 25);
            int cId = CategoryID.ToInt();

            var model = _contentExt.GetAllContent(pageIndex, pageSize1, DisplayFlag, cId, queryString, false);
            InitData();
            if (Request.IsAjaxRequest())
                return PartialView("_ListPartial", model);
            return View(model);
        }
        public ActionResult Create()
        {
            InitData();
            return View(new Content
            {
                ContentID = 1,
                DateCreated = DateTime.Now,
                Image = "/content/images/noimage.png",
                IsPublished = true,
                Hits = 1,
                State = 1,
                DatePublished = DateTime.Now
            });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Content model)
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
            } InitData();
            return View(model);
        }
        public ActionResult Edit(int id = 0)
        {
            var model = _service.Get(id);
            if (model == null)
            {
                return RedirectToAction("Create");
            } InitData();
            return View(model);
        }
        [HttpPost]
		 [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Content model)
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
            } InitData();
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

