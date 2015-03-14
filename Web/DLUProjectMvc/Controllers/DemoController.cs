using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using System.Web.Mvc;
using DLUProject.Services;
using DLUProject.Domain;
using System.Configuration;
using DLUProjectFramework.Widget;
using DLUProjectMvc.ViewModels.Widget;
using System.Text.RegularExpressions;
namespace DLUProjectMvc.Controllers
{
    public class DemoController : Controller
    {


        IServices<Menus> _menuService;
        AccountExtension _acc;
        public DemoController(IServices<Menus> menuService, AccountExtension acc)
        {
            this._menuService = menuService;
            this._acc = acc;
        }
        public ActionResult Index()
        {
            string Youtube = @"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)";
            string Vimeo = @"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)";


            string url = "https://www.youtube.com/watch?v=UwBpBRa0d-E"; //url goes here!

            Match youtubeMatch = Regex.Match(url, Youtube);
            // Match vimeoMatch = VimeoVideoRegex.Match(url);

            string id = string.Empty;

            if (youtubeMatch.Success)
                id = youtubeMatch.Groups[1].Value;

            // if (vimeoMatch.Success)
            // id = vimeoMatch.Groups[1].Value;
            ViewBag.ID = ColorLife.Core.Helper.StringHelper.GetYoutubeID(url);
            ViewBag.Widgets = GetWidgetData();
            return View();
        }
        public ActionResult Index2()
        {
            var model = _menuService.Table;
            return View(model);
        }
        public ActionResult Index3()
        {
            LoginResult rx = LoginResult.LoginFaild;
            var ax = _acc.Login("tuanitpro@gmail.com", "123", ref rx);

            var account = HttpContext.Session.GetAccountSession();
            ViewBag.Func = account.InFunction;
            ViewBag.Group = account.InGroup;
            ViewBag.Name = account.Account.FullName;
            bool canAccess = account.IsAllowAccess("Dashboard", "Index");
            if (!canAccess)
                ViewBag.OK = "False";
            else ViewBag.OK = "OK";
            return View();
        }
        public List<IWidget> GetWidgetData()
        {
            var noticeboardWidget = new List<IWidget>
    {
        new NoticeBoard()
        {
         SortOrder = 1, ClassName = "high", 
           HeaderText = "Notice Board", FooterText = "" ,
         SubWidget = new SubWidget { Topic = "Office Time", 
           Description = "Office time will be change next month", WidgetName="_SubWidget" },
            WidgetName = "_Widget"
        },
        
    };

            return noticeboardWidget.OrderBy(p => p.SortOrder).ToList();

        }
    }
}