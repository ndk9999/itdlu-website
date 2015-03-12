/* FileName: FoldersController.cs
Project Name: DLUProject
Date Created: 21/11/2014 10:52:16 AM
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
     
    public class FoldersController : BaseController
    {
        private IServices<Folders> _service ;
        public FoldersController(IServices<Folders> service)
        {
            this._service = service;
        }
        public ActionResult Index(int? page, int? pageSize, string queryString)
        {
            int pageIndex = (page ?? 1);
            int pageSize1 = (pageSize ?? 25);
            var myList = _service.All();
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                myList = myList.Where(c => string.Format("{0}", c.FolderPath).ToLower().Contains(queryString)).ToList();
            }
            var model = myList.ToPagedList(pageIndex, pageSize1);

            if (Request.IsAjaxRequest())
                return PartialView("_ListPartial", model);
            return View(model);
        }
        void InitData()
        {
            var items = _service.Table.ToList();           
            ViewBag.ListFolder = items;
        }
        public ActionResult Create()
        {
            InitData();
            return View(new Folders
            {
                FolderID = -1,
                DateCreated = DateTime.Now,
                PortalID = 0,
                StorageLocation = 0,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Folders model)
        {
            if (ModelState.IsValid)
            {
                string folderPath = "";
                bool created = false;
                IFileRepository f = new FileRepository();

              

                // FileSystems/TaiNguyen/Folder1
                folderPath = MyFileFolderHelper.GetFolderFileSystem + model.FolderPath;
                created = f.CreateNewFolder(Server.MapPath(folderPath));
               
                if (created)
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
                else
                {
                    ModelState.AddModelError("", "Không thể tạo thư mục.");
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
            model.DateModified = DateTime.Now;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Folders model)
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
            InitData();
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

