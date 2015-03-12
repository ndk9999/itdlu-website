/* FileName: NoticeController.cs
Project Name: DLUProject
Date Created: 18/11/2014 6:54:19 PM
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
     
    public class NoticeController : BaseController
    {
        private IServices<Notice> _service ;
        private NoticeExtension _noticeExt;
        private NoticeCategoryExtension _cateExt;
        private DepartmentExtension _departmentExt;

        private IServices<NoticeFileAttachment> _docFileAtt;
        private IServices<Files> _fileService;
        public NoticeController(IServices<Notice> service, NoticeExtension noticeExt,NoticeCategoryExtension cateExt,
            DepartmentExtension departmentExt, IServices<NoticeFileAttachment>fileAtt, IServices<Files>fileService)
        {
            this._service = service;
            this._noticeExt = noticeExt;
            this._cateExt = cateExt;
            this._departmentExt = departmentExt;
            this._fileService = fileService;
            this._docFileAtt = fileAtt;
        }
        void InitData()
        {
            ViewBag.Categories = _cateExt.GetByParent(new List<NoticeCategory>(), 0, true);
            ViewBag.Departments = _departmentExt.GetAllDropdownList();
        }
        void ListFiles(int id)
        {
            var listAtt = _docFileAtt.All().Where(c => c.NoticeID.Equals(id)).ToList();
            var listFile = from item in _fileService.All()
                           join t in listAtt on item.FileId equals t.FileID
                           select item;

            ViewBag.ListFiles = listFile.ToList();
        }
        public ActionResult Index(int? page, int? pageSize, string CategoryID, string queryString)
        {
            int pageIndex = (page ?? 1);
            int pageSize1 = (pageSize ?? 25);
            int cId = CategoryID.ToInt();
            var model = _noticeExt.GetAll(pageIndex, pageSize1, cId, queryString, false);
            InitData();
            if (Request.IsAjaxRequest())
                return PartialView("_ListPartial", model);
            return View(model);
        }
        public ActionResult Create()
        {
            InitData();
            return View(new Notice
            {
                NoticeID = -1,
                IsPublished = true,
                IsDeleted = false,
                SortOrder = 1,
                Image = "/content/images/noimage.png",
                DateCreated = DateTime.Now,
                CreatedByID = "Trường Đại học Đà Lạt"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Notice model, int[] Files)
        {
            if (ModelState.IsValid)
            {
                var rs = _service.Insert(model);
                if (rs > 0)
                {
                    if (Files != null)
                    {
                        foreach (var item in Files)
                        {
                            _docFileAtt.Insert2(new NoticeFileAttachment { NoticeID = rs, FileID = item });
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
            } InitData();
            ListFiles(id);
            model.DateModified = DateTime.Now;
            return View(model);
        }
        [HttpPost]
		 [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Notice model, int[] Files)
        {
             if (ModelState.IsValid)
            {
                var rs = _service.Update(model);
                if (rs > 0)
                {
                    if (Files != null)
                    {
                        _docFileAtt.Delete(c => c.NoticeID.Equals(model.NoticeID));
                        foreach (var item in Files)
                        {
                            _docFileAtt.Insert2(new NoticeFileAttachment { NoticeID = model.NoticeID, FileID = item });
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
            InitData();
            ListFiles(model.NoticeID);
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

