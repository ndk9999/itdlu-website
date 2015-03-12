using System;
using System.Web.Mvc;
namespace DLUProjectFramework.Mvc
{
    public class Notification
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public bool Fail { get; set; }

        public static Notification AddNotification(bool fail, string message, Exception ex)
        {
            var notification = new Notification { Fail = fail, Message = message, Exception = ex };
            return notification;
        }
        public static Notification AddNotification(NotifyType status, string message, Exception ex)
        {
            bool fail = true;
            switch (status)
            {
                case NotifyType.InvalidData:
                case NotifyType.Fail:
                case NotifyType.CreateFail:
                case NotifyType.EditFail:
                case NotifyType.DeleteFail:
                case NotifyType.DeleteAllFail:
                case NotifyType.SaveFail:
                    fail = false;
                    break;
            }
            var notification = new Notification { Fail = fail, Message = message, Exception = ex };
            return notification;
        }
        public static Notification AddNotification(Notification data)
        {
            return data;
        }
    }
}
