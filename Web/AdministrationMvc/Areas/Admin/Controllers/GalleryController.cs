﻿/* FileName: GalleryController.cs
Project Name: DLUProject
Date Created: 28/11/2014 2:03:13 PM
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
     
    public class GalleryController : BaseController
    {
        private IServices<Gallery> _service ;
         private GalleryCategoryExtension _categoryService;
         public GalleryController(IServices<Gallery> service, GalleryCategoryExtension categoryService)
        {
            this._service = service;
            this._categoryService = categoryService;
        }
        public ActionResult Index(int? page, int? pageSize, string CategoryID, string queryString)
        {
            int pageIndex = (page ?? 1);
            int pageSize1 = (pageSize ?? 25);
            var myList = _service.All();
            int catId = CategoryID.ToInt();
            if (catId > 0)
            {
                myList = myList.Where(c => c.CategoryID.Equals(catId)).ToList();
            }
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => string.Format("{0} {1}", c.Name, c.Description).ToLower().Contains(queryString)).ToList();
            }
            if (catId > 0 && !string.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => c.CategoryID.Equals(catId) && string.Format("{0} {1}", c.Name, c.Description).ToLower().Contains(queryString)).ToList();
            }
            var model = myList.ToPagedList(pageIndex, pageSize1);
            InitData();
            if (Request.IsAjaxRequest())
                return PartialView("_ListPartial", model);
            return View(model);
        }
        void InitData()
        {
            ViewBag.Categories = _categoryService.GetAllDropdownList(false);
        }
        public ActionResult Create()
        {
            InitData();
            return View(new Gallery
            {
                DateCreated = DateTime.Now,
                GalleryID  = -1,
                Image = "/content/images/noimage.png",
                SortOrder = 1
            });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Gallery model)
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
		 [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Gallery model)
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

