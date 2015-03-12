/* FileName: PagesController.cs
Project Name: DLUProject
Date Created: 14/11/2014 2:49:19 PM
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
namespace DLUProjectMvc.Areas.Admin.Controllers
{
     
    public class PagesController : BaseController
    {
        private IServices<Pages> _service ;
        public PagesController(IServices<Pages> service)
        {
            this._service = service;
        }       
        public ActionResult Index(int? page, int? pageSize, string queryString)
        {
            int pageIndex = (page ?? 1);
            int pageSize1 = (pageSize ?? 25);
            var myList = _service.Table;
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.FullTextSearch(queryString);              
            }
            var model=myList.ToPagedList(pageIndex, pageSize1);

            if (Request.IsAjaxRequest())
                return PartialView("_ListPartial", model);
            return View(model);
        }
        public ActionResult Create()
        {
            return View(new Pages
            {
                DateCreated = DateTime.Now,
                Image = "/content/images/noimage.png",
                IsPublished = true,
                PageID = -1,
                ParentID = 0,
                IsFrontPage = false,

            });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Pages model)
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
            return View(model);
        }
        public ActionResult Edit(int id = 0)
        {
            var model = _service.Get(id);
            if (model == null)
            {
                return RedirectToAction("Create");
            }
            model.DateModified = DateTime.Now;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Pages model)
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
            int rs = _service.Delete(new Pages { PageID = id });
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

