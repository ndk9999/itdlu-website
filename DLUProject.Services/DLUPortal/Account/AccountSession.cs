using DLUProject.Domain;
using DLUProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace DLUProject.Services
{
    public static class AccountSessionHelper
    {
        public static void SetAccountSession(this HttpSessionStateBase session, AccountSessionModel u)
        {
            session["AccountSession"] = u;
        }
        public static AccountSessionModel GetAccountSession(this HttpSessionStateBase session)
        {
            return session["AccountSession"] as AccountSessionModel;
        }
        public static void RemoveAccountSession(this HttpSessionStateBase session)
        {
            session.Remove("AccountSession");
        }
    }
}
