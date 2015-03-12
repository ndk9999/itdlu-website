using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLUProject.Domain;
using DLUProject.Services;
using DLUProjectMvc.ViewModels;
using ColorLife.Core.Mvc;
namespace DLUDeptProjectMvc.Controllers
{
    public class ArticleController : Controller
    {
        IServices<Content> _contentService;
        private IServices<Category> _categoryService;
        public ArticleController(IServices<Content> contentService, IServices<Category>categoryService)
        {
            this._contentService = contentService;
            this._categoryService = categoryService;
        }
       [Route("tin-tuc")]
        public ActionResult Index(int? page, string queryString)
        {
            int pageIndex = (page ?? 1);
            int pageSize1 = 10;

            var model = _contentService.All().Where(c => c.IsPublished == true && c.IsDeleted == false).ToPagedList(pageIndex, pageSize1); // myList.ToPagedList(pageIndex, pageSize1);
            var viewModel = new ContentCategoryViewModel
            {
             
                PagedListContent = model,
                ListTopNew = model.Take(5).ToList(),
                ListTopViews = model.OrderByDescending(x => x.Hits).Take(10).ToList(),
            };
            if (Request.IsAjaxRequest())
                return PartialView("_ListPartial", viewModel);
            return View(viewModel);

        }
        [Route("tin-tuc/{slug}/{id}")]
        public ActionResult Single(int id)
        {
            var model = _contentService.All().FirstOrDefault(c => c.ContentID.Equals(id)&&c.IsPublished==true);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Title = model.Name;
            var viewModel = new SingleContentViewModel
            {
                Item = model,
                LatestContent = _contentService.All().Where(c => !c.ContentID.Equals(id) && c.IsPublished == true && c.IsDeleted == false).Take(7).ToList(),
                RelatedContent = _contentService.All().Where(c => !c.ContentID.Equals(id) && c.IsPublished == true && c.IsDeleted == false && c.CategoryID.Equals(model.CategoryID)).Skip(10).Take(10).ToList(),
                ListTopNew = _contentService.All().Where(c => c.IsPublished == true && c.IsDeleted == false).Take(5).ToList(),
                ListTopViews = _contentService.All().Where(c => c.IsPublished == true && c.IsDeleted == false).OrderByDescending(x=>x.Hits).Take(10).ToList(),                        
            };
            return View(viewModel);
        }
        public ActionResult Print(int id)
        {
            var model = _contentService.All().FirstOrDefault(c => c.ContentID.Equals(id) && c.IsPublished == true);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Route("tin-tuc/danh-muc/{slug}/{id}")]
        public ActionResult Category(int id, int? page, string queryString)
        {
            var cate = _categoryService.All().FirstOrDefault(c => c.CategoryID.Equals(id) && c.IsPublished == true);
            if (cate == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Title = cate.Name;
            int pageIndex = (page ?? 1);
            int pageSize1 = 10;
            

            var model = _contentService.All().Where(c =>c.CategoryID.Equals(id) && c.IsPublished == true && c.IsDeleted == false).ToPagedList(pageIndex, pageSize1); // myList.ToPagedList(pageIndex, pageSize1);
            var viewModel = new ContentCategoryViewModel
            {
                Item=cate,
                 PagedListContent=model,
                ListTopNew = model.Take(5).ToList(),
                ListTopViews = model.OrderByDescending(x => x.Hits).Take(10).ToList(),                        
            };
            if (Request.IsAjaxRequest())
                return PartialView("_ListPartial", viewModel);
            return View(viewModel);
        }
    }
}