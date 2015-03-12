/* 
FileName: AccountLogExtension.cs
Project Name: ColorLife.Data
Date Created: 5/6/2014 5:12:12 PM
Description: Auto-generated
Version: 1.0.0.0
Author:	Lê Thanh Tuấn - Khoa CNTT
Author Email: tuanitpro@gmail.com
Author Mobile: 0976060432
Author URI: http://tuanitpro.com
License: 

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DLUProject.Domain;
using DLUProject.Data;
using System.Web;

namespace DLUProject.Services
{
    public class AccountLogExtension
    {
        //YOUR CODE HERE

        private IRepository< AccountLog> _logProxy;

        public AccountLogExtension(IRepository<AccountLog> logProxy)
        {
            this._logProxy = logProxy;
        }
        public int WriteLog(LevelErrorEnum logType, Account account, string action)
        {
            if (account != null)
            {
                var log = new AccountLog
                {
                    AccountID = account.AccountID,
                    ComputerName = Environment.UserDomainName,
                    LevelError = logType ,
                    LoggedDate = DateTime.Now,
                    Source = action,
                    Url = HttpContext.Current.Request.Url.ToString(),
                    IPAddress = HttpContext.Current.Request.UserHostAddress,
                    MACAddress = "",
                    Detail = string.Format("{0} đã {1}", account.FullName, action)
                };
                int kq = _logProxy.Insert(log);

                return kq;
            } return -9999;
        }
        public int InsertLog(Account account, string action)
        {
            return WriteLog(LevelErrorEnum.Insert, account, action);
        }
        public int UpdateLog(Account account, string action)
        {
            return WriteLog(LevelErrorEnum.Update, account, action);
        }
        public int DeleteLog(Account account, string action)
        {
            return WriteLog(LevelErrorEnum.Delete, account, action);
        }
        public int ViewLog(Account account, string action)
        {
            return WriteLog(LevelErrorEnum.View, account, action);
        }
        public int LoginLog(Account account, string action)
        {
            return WriteLog(LevelErrorEnum.Login, account, action);
        }
        // YOUR CODE HERE
    }
}




