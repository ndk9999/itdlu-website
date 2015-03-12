using DLUProject.Data;
using DLUProject.Domain;
using DLUProject.Services;
using DLUProjectFramework.Kendoui;
using DLUProjectFramework.Mvc;
using DLUProjectMvc.Areas.Security.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using DLUProjectFramework.Infrastructure;
using ColorLife.Core.Mvc;
using ColorLife.Core.Helper;
namespace DLUProjectMvc.Areas.Security.Controllers
{
    public class AccountController : Controller
    {
        private IServices<Account> _accountService;
        private IServices<AccountGroup> _accountGroup;
        private AccountExtension _accountExt;
        public AccountController(IServices<Account> accountService, IServices<AccountGroup> accountGroup, AccountExtension accountExt)
        {
            this._accountService = accountService;
            this._accountGroup = accountGroup;
            this._accountExt = accountExt;
        }
        // GET: Security/Account


        public ActionResult Index(int? page, int? pageSize, string GroupID, string queryString, string sortOrder)
        {
            ViewBag.EmailSortParm = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "first_name" : "";
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "last_name" : "";
            int pageIndex = (page ?? 1);
            int pageSize1 = (pageSize ?? 25);

            int gId = -1;
            if (!string.IsNullOrEmpty(GroupID)) gId = GroupID.ToInt();

            InitData();
            var model = _accountExt.GetAllAccounts(pageIndex, pageSize1, gId, queryString, sortOrder);    
            if (Request.IsAjaxRequest())
                return PartialView("_ListPartial", model);
            return View(model);
        }
        void InitData()
        {
            ViewBag.AccountGroups = _accountGroup.All();
        }
        public ActionResult Create()
        {
            InitData();
            return View(new Account { DateCreated = DateTime.Now, LoginFailedCount = 0, AccountID = -1 });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Account model, int[] groupIds)
        {
            if (ModelState.IsValid)
            {
                model.Password = model.Password.EncodePassword();
                var rs = _accountService.Insert(model);
                if (rs > 0)
                {
                    if (groupIds != null)
                    {
                        foreach (var item in groupIds)
                        {
                            _accountExt.AddToGroup(rs, item);
                        }
                    }

                    var notification = new Notification { Fail = rs > 0, Message = "Thêm mới dữ liệu thành công.", Exception = null };
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
            ModelState.AddModelError("", "Dữ liệu nhập vào không hợp lệ!.");
            return View(model);
        }
        public ActionResult Edit(int id = 0)
        {
            var model = _accountService.Get(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            InitData();
            ViewBag.AccountInGroup = _accountExt.InGroup(model);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Account model, int[] groupIds)
        {
            if (ModelState.IsValid)
            {
                var rs = _accountService.Update(model);
                if (rs > 0)
                {
                    if (groupIds != null)
                    {
                        _accountExt.RemoveFromGroup(model.AccountID);
                        foreach (var item in groupIds)
                        {
                            _accountExt.AddToGroup(model.AccountID, item);
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
            ViewBag.AccountInGroup = _accountExt.InGroup(model);
            InitData();
            ModelState.AddModelError("", "Dữ liệu nhập vào không hợp lệ!.");
            return View(model);
        }
        public ActionResult Detail(int id = 0)
        {
            var model = _accountService.Get(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Title = model.FullName;
            ViewBag.AccountInGroup = _accountExt.InGroup(model);
            InitData();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Detail(int id, Account model)
        {
            if (ModelState.IsValid)
            {
                var acc = _accountService.Get(id);
                acc.FirstName = model.FirstName;
                acc.LastName = model.LastName;
                acc.IsApproved = model.IsApproved;
                acc.IsLockedOut = model.IsLockedOut;
                if (model.Password != null) acc.Password = model.Password.EncodePassword();
                var rs = _accountService.Update(acc);
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
            InitData();
            ModelState.AddModelError("", "Dữ liệu nhập vào không hợp lệ!.");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ChangePassword(Account model)
        {
            if (ModelState.IsValid)
            {
                var acc = _accountService.Get(model.AccountID);                
                if (model.Password != null) acc.Password = model.Password.EncodePassword();
                var rs = _accountService.Update(acc);
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
            InitData();
            ModelState.AddModelError("", "Dữ liệu nhập vào không hợp lệ!.");
            return View(model);
        }
        public ActionResult Delete(int id = 0)
        {
            var model = _accountService.Get(id);
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
            int rs = _accountService.Delete(id);
            if (rs > 0)
            {
                var notification = new Notification { Fail = rs > 0, Message = "Xóa dữ liệu thành công.", Exception = null };
                TempData["Notification"] = notification;// "Cập nhật dữ liệu thành công";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Xóa dữ liệu thất bại!.");
            }
            return RedirectToAction("Delete", new { id = id });
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, AccountListModel model)
        {
            var query = _accountService.Table;

            if (!String.IsNullOrWhiteSpace(model.SearchCategoryName))
                query = query.Where(c => c.FirstName.Contains(model.SearchCategoryName));
            query = query.OrderBy(c => c.AccountID).ThenBy(c => c.FirstName);

            int total = query.Count();
            var categories = PagingHelper.Paged(query, command.Page, command.PageSize, c => c.Email, false, out total);
            // var categories = _categoryService.GetAllCategories(model.SearchCategoryName, command.Page - 1, command.PageSize, true);
            var gridModel = new DataSourceResult
            {
                Data = categories.Select(x => new Account
                {
                    Email = x.Email,
                    AccountID = x.AccountID,
                    FirstName = x.FirstName,
                    DateCreated = x.DateCreated,
                    IsApproved = x.IsApproved,
                    IsLockedOut = x.IsLockedOut,
                    LastLoginDate = x.LastLoginDate,
                    LastName = x.LastName,
                    LoginFailedCount = x.LoginFailedCount,
                    Password = x.Password
                }),
                Total = total
            };
            return Json(gridModel);
        }
    }
}