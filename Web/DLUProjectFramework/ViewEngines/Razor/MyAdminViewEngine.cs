using System.Linq;
using System.Web.Mvc;

namespace DLUProjectFramework.ViewEngines.Razor
{
    public class MyAdminViewEngine : RazorViewEngine
    {
        public MyAdminViewEngine()
        {
            var newLocationFormat = new[] {
                "~/Views/Shared/MyAdmin/{0}.cshtml"
            };
            PartialViewLocationFormats = PartialViewLocationFormats.Union(newLocationFormat).ToArray();
        }
    }
    public class AdministrationViewEngine : RazorViewEngine
    {
        public AdministrationViewEngine()
        {
            var newLocationFormat = new[] {
                "~/Areas/Shared/{0}.cshtml"
            };
            PartialViewLocationFormats = PartialViewLocationFormats.Union(newLocationFormat).ToArray();
        }
    }
}