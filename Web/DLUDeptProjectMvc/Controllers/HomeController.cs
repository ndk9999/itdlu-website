using DLUProject.Domain;
using DLUProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceModel.Syndication;
using System.Xml;
using ColorLife.Core.Helper;
using DLUProject.Model;
namespace DLUDeptProjectMvc.Controllers
{
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
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

       

        public ActionResult Demo()
        {
            return View();
        }
        public ActionResult Popup()
        {
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