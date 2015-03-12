using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLUProject.Domain;
using DLUProject.Services;
using DLUProject.Model;
using ColorLife.Core.Helper;
using System.Text;
namespace DLUProjectMvc.Areas.Admin.Controllers
{
    public class Login2Controller : Controller
    {
        // GET: Admin/Login
        AccountExtension _accountService;
        public Login2Controller(AccountExtension accountService)
        {
            this._accountService = accountService;
        }
        //
        // GET: /Admin/Account/

        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model, string returnUrl)
        {
            LoginResult errorCode = LoginResult.LoginFaild;
            if (ModelState.IsValid)
            {

                var rs = _accountService.Login(model.Email, model.Password, ref  errorCode);
                switch (errorCode)
                {
                    case LoginResult.Success:
                        // FormsAuthentication.SetAuthCookie(rs.Email, model.RememberMe);                         
                        return RedirectToLocal(returnUrl);
                    case LoginResult.LoginFaild:
                        ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu.");
                        break;
                    case LoginResult.InvalidPassword:
                        ModelState.AddModelError("", "Sai mật khẩu. Bạn còn " + (10 - rs.LoginFailedCount) + " lần đăng nhập.");
                        // ghi log
                        break;
                    case LoginResult.NotApproved:
                        ModelState.AddModelError("", "Tài khoản chưa được kích hoạt.");

                        break;
                    case LoginResult.IsLockedOut:
                        ModelState.AddModelError("", "Tài khoản đang bị khóa.");
                        break;
                    default:
                        ModelState.AddModelError("", "OK." + errorCode);
                        break;
                }
                return View(model);
            }
            ModelState.AddModelError("", "Đăng nhập thất bại. Vui lòng thử lại.");
            return View(model);
        }


         
        
        public ActionResult Recovery()
        {
            return View();
        }
        string EmailRecoveryTemplate()
        {
            string text = "";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(Server.MapPath("/Content/Templates/email-recovery.html")))
            {
                text = sr.ReadToEnd();
            }
            return text;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Recovery(LoginModel model)
        {
            var userExists = _accountService.GetByEmail(model.Email);
            if (userExists != null)
            {

                string newPassword = "";
                bool rs = _accountService.ForgotPassword(userExists, out newPassword) > 0;
                if (rs)
                {
                    string smtpHost = "smtp.gmail.com"; // SettingManager.Config("SMTP_SERVER");
                    string smtpPassword = "zgoerigfjndorwiv"; // SettingManager.Config("SMTP_PASSWORD");
                    int smtpPort = 25; // SettingManager.Int("SMTP_PORT");
                    string smtpUsername = "tuanitpro@gmail.com";// SettingManager.Config("SMTP_EMAIL");


                    var sbody = new StringBuilder();
                    sbody.Append("Quên mật khẩu<br/><br/>");

                    sbody.AppendFormat("<p> Chào : {0} </p><br/>", userExists.FullName);
                    sbody.AppendFormat("<p> Mật khẩu mới đã được tạo lại     : {0} </p><br/>", newPassword);
                    sbody.AppendFormat("<p> Ngày giờ : {0} </p><br/>", DateTime.Now.ToString());

                    //  string body = EmailRecoveryTemplate().Replace("[username]", userExists.Email);
                    // body = body.Replace("[password]", newPassword);

                    IMailService mailService = new MailService
                    {
                        SmtpHost = smtpHost,
                        SmtpPort = smtpPort,
                        SmtpUserName = smtpUsername,
                        SmtpPassword = smtpPassword,
                        EnableSsl = true,
                        FromEmail = smtpUsername,
                        ToEmail = new string[] { model.Email },
                        FromName = "TUANITPRO",

                        Subject = "Khôi phục mật khẩu",
                        Body = sbody.ToString()
                    };
                    mailService.Send();
                }

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Khôi phục mật khẩu thất bại.");
            return View();
        }

        public ActionResult Logoff()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }
        }
        #endregion
    }
}