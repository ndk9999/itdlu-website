using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
namespace DLUProjectFramework.Mvc
{
    public static class SupportOnlineExtensions
    {
        public static MvcHtmlString OnlineYahooSupport(this HtmlHelper htmlHelper, string nickNames, int type)
        {

            if (nickNames.Length == 0)
                return MvcHtmlString.Empty;
            StringBuilder sb = new StringBuilder();
            string[] nickName = nickNames.Split(',');
            foreach (string s in nickName)
            {
                sb.AppendFormat("<a href=\"ymsgr:sendim?{0}\">", s);
                sb.AppendFormat("<img border=\"0\" src=\"http://opi.yahoo.com/online?u={0}&amp;m=g&amp;t=" + type + "&amp;l=us\" alt=\"" + s + "\"></a>", s);
                sb.Append("<br>");
                sb.Append(s);
                sb.Append("<br>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }
        public static MvcHtmlString OnlineSkypeSupport(this HtmlHelper htmlHelper, string nickNames)
        {
            if (nickNames.Length == 0)
                return MvcHtmlString.Empty;
            string[] nickName = nickNames.Split(',');
            StringBuilder sb = new StringBuilder();
            foreach (string s in nickName)
            {
                sb.Append("<script src=\"http://download.skype.com/share/skypebuttons/js/skypeCheck.js\" type=\"text/javascript\"></script>");
                sb.AppendFormat("<a href=\"skype:{0}?chat\">", s);
                sb.Append("<img width=\"164\" height=\"52\" alt=\"Chat with me\" style=\"border: none;\" src=\"http://download.skype.com/share/skypebuttons/buttons/chat_blue_white_164x52.png\"></a>");
                sb.Append("<br>");
                sb.Append(s);
                sb.Append("<br>");

            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}