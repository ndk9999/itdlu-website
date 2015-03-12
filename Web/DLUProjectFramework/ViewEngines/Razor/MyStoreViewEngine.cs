using System.Linq;
using System.Web.Mvc;

namespace DLUProjectFramework.ViewEngines.Razor
{
    public class MyStoreViewEngine : RazorViewEngine
    {
        public MyStoreViewEngine()
        {
            var newLocationFormat = new[] {
                "~/Views/Shared/MyStore/{0}.cshtml"
            };
            PartialViewLocationFormats = PartialViewLocationFormats.Union(newLocationFormat).ToArray();
        }
    }
}