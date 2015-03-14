using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DLUProjectFramework.Infrastructure
{
    public static class FileIconHelper
    {
        public static MvcHtmlString IconMyFile(this HtmlHelper helper, string fileName)
        {
            string html = "";
            string fullName = HttpContext.Current.Server.MapPath(fileName);
            if (!System.IO.File.Exists(fullName))
                html = "<img src='/content/file-icons/no_preview.png' class='img-responsive'/>";
            else
            {
                var ext = System.IO.Path.GetExtension(fullName);

                if (IsExtensionImage(ext))
                {
                    html = "<img src='" + fileName + "' class='img-responsive'/>";
                }
                else
                    html = "<img src='/content/file-icons/" + ext.Replace(".", string.Empty) + ".png' ' class='img-responsive'/>";
            }
            return MvcHtmlString.Create(html);
        }
        public static bool IsExtensionImage(this string fileExtension)
        {
            string allowedImageExt = "^.jpg|.jpeg|.png$";
            return ColorLife.Core.FileManager.FileHelper.IsExtensionAllowed(fileExtension, allowedImageExt);
        }
    }
}
