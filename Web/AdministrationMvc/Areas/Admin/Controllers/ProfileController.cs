using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 
using ColorLife.Core.Helper;
using DLUProjectFramework.Infrastructure;
using DLUProject.Services;
using DLUProject.Domain;
using DLUProjectFramework.Mvc;

namespace DLUProjectMvc.Areas.Admin.Controllers
{
    public class ProfileController : BaseController
    {
        public AccountExtension accountExt;
        public ProfileController(AccountExtension ext)
        {
            accountExt = ext;
        }

        // GET: Admin/Profile
        public ActionResult Index()
        {
            var accountModel = Session.GetAccountSession();

            return View(accountModel.Account);
        }
        [HttpPost]
        public ActionResult Index(Account model)
        {
            var accountModel = Session.GetAccountSession();
            accountModel.Account.FirstName = model.FirstName;
            accountModel.Account.LastName = model.LastName;

            int rs = accountExt.Update(accountModel.Account);
            if (rs > 0)
            {
                var notification = new Notification { Fail = rs > 0, Message = "Thay đổi thông tin thành công.", Exception = null };
                TempData["Notification"] = notification;// "Cập nhật dữ liệu thành công";
                return RedirectToAction("Index");
            }
            else
            {
                var notification = new Notification { Fail = false, Message = "Thay đổi thông tin thất bại.", Exception = null };
                TempData["Notification"] = notification;// "Cập nhật dữ liệu thành công";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string PasswordOld, string PasswordNew)
        {
            var accountModel = Session.GetAccountSession();

            int rs = accountExt.ChangePassword(accountModel.Account.Email, PasswordOld, PasswordNew);
            if (rs > 0)
            {
                var notification = new Notification { Fail = rs > 0, Message = "Thay đổi mật khẩu thành công.", Exception = null };
                TempData["Notification"] = notification;// "Cập nhật dữ liệu thành công";
                return RedirectToAction("Index");
            }
            else
            {
                var notification = new Notification { Fail = false, Message = "Thay đổi mật khẩu thất bại.", Exception = null };
                TempData["Notification"] = notification;// "Cập nhật dữ liệu thành công";
                return RedirectToAction("Index");
            }

        }
    }
}