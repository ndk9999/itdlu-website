using System.Linq;
using System.Web.Mvc;

namespace DLUProjectFramework.ViewEngines.Razor
{
    public class MyForumViewEngine : RazorViewEngine
    {
        public MyForumViewEngine()
        {
            var newLocationFormat = new[] {
                "~/Views/Shared/MyForum/{0}.cshtml"
            };
            PartialViewLocationFormats = PartialViewLocationFormats.Union(newLocationFormat).ToArray();
        }
    }
}