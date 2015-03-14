 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLUProject.Services;
using DLUProject.Domain;
using DLUProject.Model;
using ColorLife.Core.Helper;
using DLUProjectMvc.ViewModels;
using DLUProjectFramework.Widget;
using DLUProjectMvc.ViewModels.Widget;
namespace DLUProjectMvc.Controllers
{
    //[OutputCache(Duration=3600, VaryByParam="None")]
    public class HomeController : Controller
    {
        IServices<Pages> _pageService;
        IServices<Contact> _contactService;
        IServices<Content> _contentService;
        public HomeController(IServices<Pages> pageService, IServices<Contact> contactService,
             IServices<Content> contentService)
        {
            this._pageService = pageService;
            this._contactService = contactService;
            this._contentService = contentService;
        }
        public ActionResult Index()
        {
            var model = new ContentHomePageViewModel
            {
                ListContent = _contentService.Table.Select(c => new Content
                {
                    Image = c.Image,
                    Alias = c.Alias,
                    Name = c.Name,
                    Description = c.Description,
                    ContentID = c.ContentID,
                }).OrderByDescending(c => c.ContentID).Take(4).ToList()
            };
            
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Route("lien-he")]
        public ActionResult Contact()
        {
            return View(new ContactModel { DateCreated = DateTime.Now, IsRead = false });
        }
        [Route("lien-he")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                var contact = AutoMapper.Mapper.Map<Contact>(model);
                int kq = _contactService.Insert(contact);
                if (kq > 0)
                {
                    return RedirectToAction("Contact");
                }
                return View(model);
            }
            ModelState.AddModelError("", "Dữ liệu không hợp lệ, vui lòng kiểm tra lại");
            return View(model);
        }
        [Route("trang/{id}")]
        public ActionResult Pages(string id)
        {
            var alias = id.RemoveDiacriticsURL().ToLower();
            var model = _pageService.Table.FirstOrDefault(c => c.Alias.ToLower().Equals(alias));
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Title = model.Name;
            return View(model);
        }

   
    }
}