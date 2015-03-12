/* FileName: DocumentController.cs
Project Name: DLUProject
Date Created: 30/11/2014 2:35:31 PM
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
     
    public class DocumentController : BaseController
    {
        private IServices<Document> _service ;
        private IServices<DocType> _docTypeService;
        private DocCategoryExtension _docCateExt;
        private IServices<DocFileAttachment> _docFileAtt;
        private IServices<Files> _fileService;
        public DocumentController(IServices<Document> service, IServices<DocType>docTypeService, DocCategoryExtension docCatExt,
            IServices< DocFileAttachment>docFileAtt, IServices<Files>fileService)
        {
            this._service = service;
            this._docTypeService = docTypeService;
            this._docCateExt = docCatExt;
            this._docFileAtt = docFileAtt;
            this._fileService = fileService;
        }
        void InitData()
        {
            ViewBag.DocTypes = _docTypeService.All();
            ViewBag.Categories = _docCateExt.GetAllDropdownList(false);
            
        }
        void ListFiles(int id)
        {
            var listAtt = _docFileAtt.All().Where(c => c.DocumentID.Equals(id)).ToList();
            var listFile = from item in _fileService.All()
                           join t in listAtt on item.FileId equals t.FileID
                           select item;

            ViewBag.ListFiles = listFile.ToList();
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
            InitData();
            if (Request.IsAjaxRequest())
                return PartialView("_ListPartial", model);
            return View(model);
        }
        public ActionResult Create()
        {
            InitData();
            return View(new Document
            {
                DateCreated = DateTime.Now,
                IsDeleted = false,
                IsPublished = true,
                DateEffected = DateTime.Now,
                DatePublished=DateTime.Now
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Document model, int[] Files)
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
                            _docFileAtt.Insert2(new DocFileAttachment { DocumentID = rs, FileID = item });
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
            }
            ListFiles(id);
            InitData();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Document model, int[] Files)
        {
            if (ModelState.IsValid)
            {
                var rs = _service.Update(model);
                if (rs > 0)
                {
                    if (Files != null)
                    {
                        _docFileAtt.Delete(c => c.DocumentID.Equals(model.DocumentID));
                        foreach (var item in Files)
                        {
                            _docFileAtt.Insert2(new DocFileAttachment { DocumentID = model.DocumentID, FileID = item });
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
            ListFiles(model.DocumentID);
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

