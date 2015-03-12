using System.Linq;
using System.Web.Mvc;

namespace DLUProjectFramework.ViewEngines.Razor
{
    public class MyPortalViewEngine : RazorViewEngine
    {
        public MyPortalViewEngine()
        {
            var newLocationFormat = new[] {
                "~/Views/Shared/Portal/{0}.cshtml"
            };
            PartialViewLocationFormats = PartialViewLocationFormats.Union(newLocationFormat).ToArray();
        }
    }
}