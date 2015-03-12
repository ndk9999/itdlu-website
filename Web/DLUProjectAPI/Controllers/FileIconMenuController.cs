using ColorLife.Core.FileManager;
using ColorLife.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
 

namespace DLUProjectAPI.Controllers
{
    public class FileIconMenuController : ApiController
    {
        [HttpGet]
        public JsonResponse<string> LoadAllMenuIcon(string path)
        {
            StringBuilder sb = new StringBuilder();
            string allowedExtensions = "^.jpg|.gif|.png|.jpeg|.PNG|.JPEG|.GIF$";
           // string path = "/Content/icons/";
            var query = from item in FileManager.Instance.All(path, FileFilter.Files, allowedExtensions)
                        //  orderby item.LastWriteDate
                        select item;
            foreach (var i in query)
            {
                sb.Append("<div class=\"col-sm-12 col-md-2\">");
                sb.Append("<div class=\"thumbnail\">");
                sb.Append("<img src=\"" + path + i.Name + " \" alt=\"" + i.Name + "\" style=\"width: 48px; height: 48px;\" onclick=\"return insertIcon('" + path + i.Name + "')\"/>");
                sb.Append("<div class=\"caption\" style='text-align:center; font-weight:bold'>");
                // sb.Append(i.Name);
                sb.Append("</div>");
                sb.Append("</div></div>");
            }
            return new JsonResponse<string> { Data = sb.ToString(), Success = true, Message = "All File Icon" };
        }
    }
}