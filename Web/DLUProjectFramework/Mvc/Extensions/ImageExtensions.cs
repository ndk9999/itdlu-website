

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Web.Routing;
namespace DLUProjectFramework.Mvc
{
    public static class ImageExtensions
    {
        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string src, int width = 9999, int height = 9999, object htmlAttributes=null)
        {
            var tagBuilder = new TagBuilder("img");
            tagBuilder.MergeAttribute("src", src);
            tagBuilder.MergeAttribute("width", width.ToString());
            tagBuilder.MergeAttribute("height", height.ToString());
            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string src, object htmlAttributes = null)
        {
            var tagBuilder = new TagBuilder("img");
            tagBuilder.MergeAttribute("src", src);
            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            if (IsExtensionImage(src))
                return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
            return MvcHtmlString.Empty;
        }
        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string src)
        {
            var tagBuilder = new TagBuilder("img");
            tagBuilder.MergeAttribute("src", src);
            
            if (IsExtensionImage(src))
                return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
            return MvcHtmlString.Empty;
        }
        public static MvcHtmlString Flash(this HtmlHelper htmlHelper, string src, int width, int height)
        {
            var tagBuilder = new TagBuilder("object");
            tagBuilder.MergeAttribute("classid", "clsid:D27CDB6E-AE6D-11cf-96B8-444553540000");
            tagBuilder.MergeAttribute("codebase", "http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0");

            var tagembed = new TagBuilder("embed");
            tagembed.MergeAttribute("src", src);
            tagembed.MergeAttribute("type", "application/x-shockwave-flash");
            tagembed.MergeAttribute("src", src);
            tagembed.MergeAttribute("wmode", "transparent");
            tagembed.MergeAttribute("quality", "high");
            tagembed.MergeAttribute("pluginspage", "http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash");
            tagembed.MergeAttribute("width", width.ToString());
            tagembed.MergeAttribute("height", height.ToString());

            string embedHelper = tagembed.ToString(TagRenderMode.SelfClosing);
            tagBuilder.InnerHtml = embedHelper;
            if (IsExtensionFlash(src))
                return MvcHtmlString.Create(tagBuilder.ToString());
            return MvcHtmlString.Empty;
        }
        public static MvcHtmlString ImageEx(this HtmlHelper htmlHelper, string src, int width, int height, object htmlAttributes=null)
        {
            if (IsExtensionImage(src))
                return Image(htmlHelper, src, width, height, htmlAttributes);
            else if (IsExtensionFlash(src))
                return Flash(htmlHelper, src, width, height);
            return MvcHtmlString.Empty;
        }
        #region Helper
        static bool IsExtensionAllowed(this string fileExtension, string allowedExtensions)
        {
            bool tempResult = true;
            if (!object.ReferenceEquals(fileExtension, string.Empty))
            {
                try
                {
                    tempResult = Regex.IsMatch(fileExtension, allowedExtensions, RegexOptions.IgnoreCase);
                }
                catch
                {
                    tempResult = false;
                }
            }
            return tempResult;
        }
        /// <summary>
        /// Check Image extention
        /// </summary>
        /// <param name="fileExtension">Image.jpg</param>        
        /// <returns></returns>
        static bool IsExtensionImage(this string fileExtension)
        {
            string allowedImageExt = "^.jpg|.jpeg|.png|.gif|.bmp|.JPG|.JPEG|.PNG|.GIF|.BMP$";
            return IsExtensionAllowed(fileExtension, allowedImageExt);
        }
        static bool IsExtensionFlash(this string fileExtension)
        {
            string allowedFlashExt = "^.flv|.swf$";
            return IsExtensionAllowed(fileExtension, allowedFlashExt);
        }
        #endregion
    }
}
