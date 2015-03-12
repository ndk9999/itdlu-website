using System.Linq;
using System.Web.Mvc;

namespace DLUProjectFramework.ViewEngines.Razor
{
    public class MyDLUViewEngine : RazorViewEngine
    {
        public MyDLUViewEngine()
        {
            var newLocationFormat = new[] {
                "~/Views/Shared/MyDLU/{0}.cshtml"
            };
            PartialViewLocationFormats = PartialViewLocationFormats.Union(newLocationFormat).ToArray();
        }
    }
}