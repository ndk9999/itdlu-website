using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLUProject.Services;
using DLUProject.Domain;
using ColorLife.Core.Mvc;
using DLUProjectMvc.ViewModels;
using DLUProjectFramework.Mvc;
namespace DLUProjectMvc.Controllers
{
    public class DocumentController : Controller
    {
        IServices<Document> _noticeService;
        IServices<DocCategory> _categoryService;
        IServices<Files> _fileService;
        IServices<DocFileAttachment> _docFileAttService;
        public DocumentController(IServices<Document> noticeService, IServices<DocCategory> categoryService,
              IServices<Files> fileService, IServices<DocFileAttachment> docFileAttService)
        {
            this._noticeService = noticeService;
            this._categoryService = categoryService;
            this._fileService = fileService;
            this._docFileAttService = docFileAttService;
        }
        [Route("van-ban")]
        // GET: Notice
        public ActionResult Index(int? page, int? pageSize, string queryString)
        {
            int pageIndex = (page ?? 1);
            int pageSize1 = (pageSize ?? 10);
            var myList = _noticeService.All();
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
              //  myList = myList.FullTextSearch(queryString);
            }
            var model = myList.ToPagedList(pageIndex, pageSize1);

            if (Request.IsAjaxRequest())
                return PartialView("_ListPartial", model);
            return View(model);
        }
        [Route("van-ban/danh-muc/{slug}/{id}")]
        public ActionResult Category(int id, int? page, int? pageSize, string queryString)
        {
            int pageIndex = (page ?? 1);
            int pageSize1 = (pageSize ?? 10);
            var cate = _categoryService.All().FirstOrDefault(c => c.CategoryID.Equals(id) && c.IsPublished == true);
            if (cate == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Title = cate.Name;

            var myList = _noticeService.All().Where(c => c.CategoryID.Equals(id) && c.IsPublished == true && c.IsDeleted == false).ToPagedList(pageIndex, pageSize1); // myList.ToPagedList(pageIndex, pageSize1);

            var model = myList.ToPagedList(pageIndex, pageSize1);

            if (Request.IsAjaxRequest())
                return PartialView("_ListPartial", model);
            return View(model);
        }
        [Route("van-ban/{slug}/{id}")]
        public ActionResult Single(int id)
        {
            var model = _noticeService.All().FirstOrDefault(c => c.DocumentID.Equals(id) && c.IsPublished == true);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Title = model.Name;

            var listAtt = _docFileAttService.All().Where(c => c.DocumentID.Equals(id)).ToList();
            var listFile = from item in _fileService.All()
                           join t in listAtt on item.FileId equals t.FileID
                           select item;

            var viewModel = new SingleDocumentViewModel
            {
                Item = model,
                ListRelated = _noticeService.All().Where(c => c.DocumentID != id).Take(20).ToList(),
                ListFiles = listFile.ToList()
            };
            return View(viewModel);
        }
        private string GetVirtualPath(string physicalPath)
        {
            string rootpath = Server.MapPath("~/");

            physicalPath = physicalPath.Replace(rootpath, "");
            physicalPath = physicalPath.Replace("\\", "/");

            return "~/" + physicalPath;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Download(int id)
        {
            var file = _fileService.Get(id);

            return new DownloadResult
            {
                VirtualPath = GetVirtualPath(file.FileUrl),
                FileDownloadName = file.FileName 
            };
        }
    }
}