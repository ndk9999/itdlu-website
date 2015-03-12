/* FileName: FilesController.cs
Project Name: DLUProject
Date Created: 21/11/2014 10:52:52 AM
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
using ColorLife.Core.FileManager;
namespace DLUProjectMvc.Areas.Admin.Controllers
{
    public class FilesController : BaseController
    {
        private IServices<Files> _service;
        private IServices<Folders> _folderService;
        public FilesController(IServices<Files> service, IServices<Folders> folderService)
        {
            this._service = service;
            this._folderService = folderService;
        }
        void InitData()
        {
            ViewBag.ListFolders = _folderService.All();
        }
        public ActionResult Index(int? page, int? pageSize, string queryString)
        {
            int pageIndex = (page ?? 1);
            int pageSize1 = (pageSize ?? 25);
            var myList = _service.All();
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => string.Format("{0} {1} {2}", c.Name, c.FileName, c.Caption).ToLower().Contains(queryString)).ToList();
            }
            InitData();
            var model = myList.ToPagedList(pageIndex, pageSize1);
            if (Request.IsAjaxRequest())
                return PartialView("_ListPartial", model);
            return View(model);
        }
        public ActionResult Create()
        {
            InitData();
            return View(new Files
            {
                DateCreated = DateTime.Now,
                IsPublished = true,

            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Files model)
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
        public ActionResult Edit(Files model)
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
            int rs = 0;
            var file = _service.Get(id);
            if (file != null)
            {
                FileManager.Instance.DeleteFileFolder(file.FileUrl);
                rs = _service.Delete(id);

            }
            if (rs > 0)
            {
                var notification = new Notification { Fail = rs > 0, Message = "Xóa dữ liệu thành công.", Exception = null };
                TempData["Notification"] = notification;// "Cập nhật dữ liệu thành công";
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult CreatePopup()
        {
            InitData();
            return View(new Files
            {
                DateCreated = DateTime.Now,
                IsPublished = true,

            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreatePopup(Files model)
        {
            if (ModelState.IsValid)
            {
                var rs = _service.Insert(model);
                if (rs > 0)
                {
                    var notification = new Notification { Fail = rs > 0, Message = "Thêm dữ liệu thành công.", Exception = null };
                    TempData["Notification"] = notification;// "Cập nhật dữ liệu thành công";
                    return RedirectToAction("FilePopup");
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

        public ActionResult FilePopup(int? page, int? pageSize, string queryString)
        {
            int pageIndex = (page ?? 1);
            int pageSize1 = (pageSize ?? 25);
            var myList = _service.All().Where(c => c.IsPublished == true);
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => string.Format("{0} {1} {2}", c.Name, c.FileName, c.Caption).ToLower().Contains(queryString)).ToList();
            }
            InitData();
            var model = myList.ToPagedList(pageIndex, pageSize1);
            if (Request.IsAjaxRequest())
                return PartialView("_ListPopupPartial", model);
            return View(model);
        }
        [HttpPost]
        public ActionResult AjaxDeleteFile(int id)
        {
            var file = _service.Get(id);
            if (file != null)
            {
                FileManager.Instance.DeleteFileFolder(file.FileUrl);
                int rs = _service.Delete(id);
                return RedirectToAction("FilePopup");
            }
            return RedirectToAction("FilePopup");
        }
    }
}

