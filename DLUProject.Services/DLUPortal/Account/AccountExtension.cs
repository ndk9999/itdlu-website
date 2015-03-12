/* 
FileName: AccountExtension.cs
Project Name: ColorLife.Data
Date Created: 7/5/2014 8:44:44 AM
Description: Auto-generated
Version: 1.0.0.0
Author:	Lê Thanh Tuấn
Author Email: tuanitpro@gmail.com
Author Mobile: 0976060432
Author URI: http://tuanitpro.com
License: 

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DLUProject.Data;
using DLUProject.Domain;

using System.Web;
using DLUProject.Model;
using ColorLife.Core.Mvc;
using ColorLife.Core.Helper;
namespace DLUProject.Services
{
    public class AccountExtension
    {
        //YOUR CODE HERE
        private IRepository<Account> _objectProxy;
        private IRepository<AccountLog> _logProxy;
        private IRepository<AccountInGroup> _accountInGroup;
        private IRepository<AccountGroup> _accountGroupProxy;
        private IRepository<AccountGroupFunction> _groupFunctionProxy;
        private IRepository<Function> _functionProxy;
        private IRepository<WorkGroup> _workGroupProxy;
        private IRepository<Systems> _systemProxy;

        public AccountExtension(IRepository<Account> proxy, IRepository<AccountLog> logProxy,
            IRepository<AccountInGroup> accountInGroupProxy, IRepository<AccountGroup> accountGroupProxy,
            IRepository<AccountGroupFunction> groupFunctionProxy, IRepository<Function> functionProxy,
            IRepository<WorkGroup> workGroupProxy, IRepository<Systems> systemProxy)
        {
            this._objectProxy = proxy;
            this._logProxy = logProxy;
            this._accountInGroup = accountInGroupProxy;
            this._accountGroupProxy = accountGroupProxy;
            this._functionProxy = functionProxy;
            this._workGroupProxy = workGroupProxy;
            this._systemProxy = systemProxy;
            this._groupFunctionProxy = groupFunctionProxy;
        }
        public List<AccountGroup> AccountGroups()
        {
            return _accountGroupProxy.All();
        }
        public Account Get(object id)
        {

            return _objectProxy.Get(id);
        }
        public PagedList<Account> GetAllAccounts(int pageIndex, int pageSize, int groupId, string queryString, string sortOrder)
        {
            var myList = _objectProxy.All();
            if (groupId > 0)
            {
                var groups = _accountInGroup.Table.Where(c => c.GroupID == groupId);
                var query = from item in myList
                            join t in groups on item.AccountID equals t.AccountID
                            select item;

                myList = query.ToList();
            }
            if (!String.IsNullOrEmpty(queryString))
            {
                queryString = queryString.ToLower();
                //   myList = myList.AsQueryable().FullTextSearch(queryString).ToList();
                myList = myList.Where(c => string.Format("{0} {1} {2}", c.FirstName, c.LastName, c.Email).ToLower().Contains(queryString)).ToList();
                // myList = myList.Search(c => c.FirstName, c => c.LastName, c => c.Email).Containing(queryString).ToList();
            }
            if (groupId > 0 && !string.IsNullOrEmpty(queryString))
            {
                var groups = _accountInGroup.Table.Where(c => c.GroupID == groupId);
                var query = from item in myList
                            join t in groups on item.AccountID equals t.AccountID
                            select item;
                queryString = queryString.ToLower();
                myList = myList.Where(c => string.Format("{0} {1} {2}", c.FirstName, c.LastName, c.Email).ToLower().Contains(queryString)).ToList();

            }
            switch (sortOrder)
            {
                case "email_desc":
                    myList = myList.OrderBy(s => s.Email).ToList();
                    break;
                case "first_name":
                    myList = myList.OrderBy(s => s.FirstName).ToList();
                    break;
                case "last_name":
                    myList = myList.OrderBy(s => s.LastName).ToList();
                    break;
                default:
                    myList = myList.OrderBy(s => s.AccountID).ToList();
                    break;
            }
            var model = myList.ToPagedList(pageIndex, pageSize);
            return model;
        }
        public Account GetByEmail(string userNameOrEmail)
        {
            if (string.IsNullOrWhiteSpace(userNameOrEmail))
                return null;
            bool isEmail = userNameOrEmail.IsValidEmail();
            if (isEmail)
                return _objectProxy.Table.FirstOrDefault(c => c.Email == userNameOrEmail);
            return null;
        }
        public AccountResult Insert(Account entity, out int accountId)
        {
            HttpSessionStateBase sessionBase = new HttpSessionStateWrapper(HttpContext.Current.Session);
            accountId = -1;
            var account = sessionBase.GetAccountSession(); // HttpContext.Current.Session.GetAccountSession();
            if (account == null)
            {
                return AccountResult.Fail;
            }

            string detail = "";
            var exits = _objectProxy.Get(c => c.Email == entity.Email);
            if (exits == null)
            {
                if (!entity.Email.IsValidEmail())
                    return AccountResult.InvalidEmail;
                var rs = _objectProxy.Insert(entity);

                if (rs > 0)
                {
                    accountId = rs;
                    detail = "Thành công";
                    return AccountResult.Success;
                }
                else
                {
                    detail = "Thất bại";
                    return AccountResult.Fail;
                }
            }
            detail = "Email trùng";
            var log = new AccountLog
            {
                AccountID = account.Account.AccountID,
                ComputerName = Environment.UserDomainName,
                LevelError = LevelErrorEnum.Insert,
                LoggedDate = DateTime.Now,
                Source = "Thêm Tài Khoản",
                Url = HttpContext.Current.Request.Url.ToString(),
                IPAddress = HttpContext.Current.Request.UserHostAddress,
                MACAddress = "",
                Detail = string.Format("{0} đã thêm tài khoản #{1} {2}", account.FullName, accountId, detail)
            };
            _logProxy.Insert(log);


            return AccountResult.DuplicateEmail;
        }
        public int Update(Account entity)
        {
            HttpSessionStateBase sessionBase = new HttpSessionStateWrapper(HttpContext.Current.Session);
            int kq = -1;
            var account = sessionBase.GetAccountSession(); //  HttpContext.Current.Session.GetAccountSession()  ;
            if (account == null)
            {
                return -1;
            }
            kq = _objectProxy.Update(entity);
            string detail = kq > 0 ? "Thành công" : "Thất bại";
            var log = new AccountLog
            {
                AccountID = account.Account.AccountID,
                ComputerName = Environment.UserDomainName,
                LevelError = LevelErrorEnum.Update,
                LoggedDate = DateTime.Now,
                Source = "Cập Nhật Tài Khoản",
                Url = HttpContext.Current.Request.Url.ToString(),
                IPAddress = HttpContext.Current.Request.UserHostAddress,
                MACAddress = "",
                Detail = string.Format("{0} đã cập nhật tài khoản #{1} {2}", account.FullName, entity.Email, detail)
            };
            _logProxy.Insert(log);
            return kq;
        }
        public Account Login(string userNameOrEmail, string password)
        {
            bool isEmail = userNameOrEmail.IsValidEmail();
            Account account = null;
            if (isEmail)
            {
                account = _objectProxy.Table.FirstOrDefault(
                   c => c.Email == userNameOrEmail && c.Password == password.EncodePassword()
                   && c.IsApproved == true && c.IsLockedOut == false);
            }
            if (account != null)
            {
                account.LoginFailedCount = 0;
                _objectProxy.Update(account);

                var log = new AccountLog
                {
                    AccountID = account.AccountID,
                    ComputerName = Environment.UserDomainName,
                    LevelError = LevelErrorEnum.Update,
                    LoggedDate = DateTime.Now,
                    Source = "Đăng nhập",
                    Url = HttpContext.Current.Request.Url.ToString(),
                    IPAddress = HttpContext.Current.Request.UserHostAddress,
                    MACAddress = ""
                };
                _logProxy.Insert(log);
            }
            return account;
        }

        public Account Login(string userNameOrEmail, string password, ref LoginResult errorCode)
        {
            Account account = GetByEmail(userNameOrEmail);
            if (account != null)
            {
                if (account.IsApproved == false)
                {
                    errorCode = LoginResult.NotApproved;  // Tai khoan chua duoc kich hoat
                    return account;
                }

                if (account.IsLockedOut == true)
                {
                    errorCode = LoginResult.IsLockedOut;  // Tai khoan dang bi khoa
                    return account;
                }
                if (account.Password.Trim() != password.EncodePassword())
                {
                    errorCode = LoginResult.InvalidPassword;  // Mat khau khong dung
                    account.LoginFailedCount += 1;
                    if (account.LoginFailedCount >= 10)
                        account.IsLockedOut = true;
                    _objectProxy.Update(account);

                    var log = new AccountLog
                    {
                        AccountID = account.AccountID,
                        ComputerName = Environment.UserDomainName,
                        LevelError = LevelErrorEnum.Update,
                        LoggedDate = DateTime.Now,
                        Source = "Đăng nhập thất bại",
                        Url = HttpContext.Current.Request.Url.ToString(),
                        IPAddress = HttpContext.Current.Request.UserHostAddress,
                        UserAgent = HttpContext.Current.Request.UserAgent,
                        MACAddress = "",
                        Detail = account.Email + " đã đăng nhập sai mật khẩu lúc " + DateTime.Now
                    };
                    _logProxy.Insert(log);

                    return account;
                }
                if (account.Password == password.EncodePassword() && account.IsApproved == true && account.IsLockedOut == false)
                {
                    errorCode = LoginResult.Success;
                    account.LoginFailedCount = 0;
                    account.LastLoginDate = DateTime.Now;
                    _objectProxy.Update(account);

                    var log = new AccountLog
                    {
                        AccountID = account.AccountID,
                        ComputerName = Environment.UserDomainName,
                        LevelError = LevelErrorEnum.Login,
                        LoggedDate = DateTime.Now,
                        Source = "Đăng Nhập",
                        Url = HttpContext.Current.Request.Url.ToString(),
                        IPAddress = HttpContext.Current.Request.UserHostAddress,
                        UserAgent = HttpContext.Current.Request.UserAgent,
                        MACAddress = "",
                        Detail = account.Email + " đã đăng nhập lúc " + DateTime.Now
                    };
                    _logProxy.Insert(log);
                    var accountSession = new AccountSessionModel
                    {
                        Account = account,
                        InFunction = FunctionByAccount(account),
                        InGroup = InGroup(account),
                        InWorkgroup = WorkGroupByAccount(account),
                        InSystem = SystemByAccount(account),
                        IsAuthenticated = true,
                    };
                    HttpSessionStateBase sessionBase = new HttpSessionStateWrapper(HttpContext.Current.Session);
                    sessionBase.SetAccountSession(accountSession);
                }
                return account;
            }
            else
            {
                errorCode = LoginResult.LoginFaild; // Ten dang nhap hoac email khong dung
                return account;
            }
        }
        public void Logoff()
        {
            HttpSessionStateBase sessionBase = new HttpSessionStateWrapper(HttpContext.Current.Session);
            var logged = sessionBase.GetAccountSession(); // HttpContext.Current.Session.GetAccountSession();
            if (logged != null)
            {
                var log = new AccountLog
                {
                    AccountID = logged.Account.AccountID,
                    ComputerName = Environment.UserDomainName,
                    LevelError = LevelErrorEnum.Logout,
                    LoggedDate = DateTime.Now,
                    Source = "Đăng xuất",
                    Url = HttpContext.Current.Request.Url.ToString(),
                    IPAddress = HttpContext.Current.Request.UserHostAddress,
                    UserAgent = HttpContext.Current.Request.UserAgent,
                    MACAddress = "",
                    Detail = logged.Account.Email + " đã đăng xuất lúc " + DateTime.Now
                };
                _logProxy.Insert(log);
                // HttpContext.Current.Session.RemoveAccountSession();
                HttpContext.Current.Session.Abandon();
            }
        }
        public int ApprovedAccount(int accountId)
        {
            var account = _objectProxy.Get(accountId);
            if (account != null)
            {
                bool isApproved = account.IsApproved;
                account.IsApproved = !isApproved;
                return _objectProxy.Update(account);
            }
            return -1;
        }

        public int LockedOutAccount(int accountId)
        {
            var account = _objectProxy.Get(accountId);
            if (account != null)
            {
                bool isLocked = account.IsLockedOut;
                account.IsLockedOut = !isLocked;
                return _objectProxy.Update(account);
            }
            return -1;
        }


        public int ForgotPassword(Account profile, out string passwordNew)
        {
            passwordNew = "";

            string newPassword = StringHelper.RandomString(8);
            profile.Password = newPassword.EncodePassword();

            passwordNew = newPassword;
            int kq = _objectProxy.Update(profile);
            if (kq > 0)
            {
                var log = new AccountLog
                {
                    AccountID = profile.AccountID,
                    ComputerName = Environment.UserDomainName,
                    LevelError = LevelErrorEnum.Update,
                    LoggedDate = DateTime.Now,
                    Source = "Quên mật khẩu",
                    Url = HttpContext.Current.Request.Url.ToString(),
                    IPAddress = HttpContext.Current.Request.UserHostAddress,
                    UserAgent = HttpContext.Current.Request.UserAgent,
                    MACAddress = "",
                    Detail = profile.Email + " đã gửi yêu cầu lấy lại mật khẩu lúc" + DateTime.Now
                };
                _logProxy.Insert(log);
            }
            return kq;
        }

        public int ChangePassword(string email, string password, string passwordNew)
        {
            var profile = _objectProxy.Table.SingleOrDefault(c => c.Email == email && c.Password == password.EncodePassword());
            if (profile != null)
            {

                profile.Password = passwordNew.EncodePassword();
                int kq = _objectProxy.Update(profile);
                if (kq > 0)
                {
                    var log = new AccountLog
                    {
                        AccountID = profile.AccountID,
                        ComputerName = Environment.UserDomainName,
                        LevelError = LevelErrorEnum.Update,
                        LoggedDate = DateTime.Now,
                        Source = "Thay đổi mật khẩu",
                        Url = HttpContext.Current.Request.Url.ToString(),
                        IPAddress = HttpContext.Current.Request.UserHostAddress,
                        UserAgent = HttpContext.Current.Request.UserAgent,
                        MACAddress = "",
                        Detail = profile.Email + " đã thay đổi mật khẩu lúc " + DateTime.Now
                    };
                    _logProxy.Insert(log);
                }
                return kq;
            }
            return -1;
        }

        public int ResetPassword(string email, string passwordNew)
        {
            if (passwordNew.Length > 0)
            {
                var profile = GetByEmail(email);
                if (profile != null)
                {
                    string newPassword = StringHelper.RandomString(8);
                    profile.Password = newPassword.EncodePassword();

                    passwordNew = newPassword;
                    return _objectProxy.Update(profile);
                }
            }
            return -1;
        }
        public List<AccountGroup> InGroup(Account c)
        {
            var query = from item in _accountGroupProxy.Table
                        join t in _accountInGroup.Table.Where(x => x.AccountID == c.AccountID)
                        on item.GroupID equals t.GroupID
                        select new AccountGroup
                        {
                            GroupID = item.GroupID,
                            Name = item.Name
                        };
            return query.ToList();
        }
        public int AddToGroup(int accountId, int groupId)
        {
            var exists = _accountInGroup.Get(c => c.AccountID == accountId && c.GroupID == groupId);
            if (exists == null)
            {
                return _accountInGroup.Insert(new AccountInGroup { AccountID = accountId, GroupID = groupId });
            }
            return -1;
        }
        public int RemoveFromGroup(int accountId, int groupId)
        {
            var a = _accountInGroup.Table.Where(c => c.AccountID == accountId && c.GroupID == groupId);
            int kq = _accountInGroup.Delete(a);
            return kq;
        }
        public int RemoveFromGroup(int accountId)
        {
            var a = _accountInGroup.Table.Where(c => c.AccountID == accountId);
            int kq = _accountInGroup.Delete(a);
            return kq;
        }
        #region Account Session Helper
        List<Function> FunctionByAccount(Account c)
        {
            var accGroup = InGroup(c);
            if (accGroup.Count == 0)
                return new List<Function>();
            var query = from item in _functionProxy.All()
                        join t in _groupFunctionProxy.All()
                        on item.FunctionID equals t.FunctionID
                        join n in accGroup on t.GroupID equals n.GroupID
                        select new Function
                        {
                            WorkGroupID = item.WorkGroupID,
                            WorkGroup = _workGroupProxy.Get(item.WorkGroupID),
                            Url = item.Url,
                            FunctionID = item.FunctionID,
                            Image = item.Image,
                            Name = item.Name,
                            SortOrder = item.SortOrder,
                            IsEnabled = item.IsEnabled
                        };
            return query.GroupBy(x => x.FunctionID).Select(y => y.First()).OrderBy(x => x.SortOrder).ToList();
        }
        List<WorkGroup> WorkGroupByAccount(Account c)
        {
            var funcs = FunctionByAccount(c);
            if (funcs.Count == 0) return new List<WorkGroup>();

            var query = from item in _workGroupProxy.All().Where(x => x.IsEnabled == true)
                        join x in _workGroupProxy.All() on item.WorkGroupID equals x.ParentID
                        join t in funcs on x.WorkGroupID equals t.WorkGroupID

                        //  join t in funcs on item.WorkGroupID equals t.WorkGroupID
                        select new WorkGroup
                        {
                            Systems = _systemProxy.Get(item.SystemID),
                            WorkGroupID = item.WorkGroupID,
                            DisplayFlags = item.DisplayFlags,
                            Image = item.Image,
                            IsEnabled = item.IsEnabled,
                            Name = item.Name,
                            ParentID = item.ParentID,
                            SortOrder = item.SortOrder,
                            SystemID = item.SystemID,
                            Url = item.Url,

                        };


            var query2 = from item in _workGroupProxy.All()
                         join t in funcs on item.WorkGroupID equals t.WorkGroupID
                         select new WorkGroup
                         {
                             Systems = _systemProxy.Get(item.SystemID),
                             WorkGroupID = item.WorkGroupID,
                             DisplayFlags = item.DisplayFlags,
                             Image = item.Image,
                             IsEnabled = item.IsEnabled,
                             Name = item.Name,
                             ParentID = item.ParentID,
                             SortOrder = item.SortOrder,
                             SystemID = item.SystemID,
                             Url = item.Url,

                         };


            var list = query.Union(query2);
            return list.GroupBy(x => x.WorkGroupID).Select(y => y.First()).OrderBy(x => x.SortOrder).ToList();
        }
        List<Systems> SystemByAccount(Account c)
        {
            var wk = WorkGroupByAccount(c);
            if (wk.Count == 0) return new List<Systems>();
            var query = from item in _systemProxy.All()
                        join t in wk on item.SystemID equals t.SystemID
                        select item;
            return query.GroupBy(x => x.SystemID).Select(y => y.First()).ToList();
        }
        #endregion
    }
}
