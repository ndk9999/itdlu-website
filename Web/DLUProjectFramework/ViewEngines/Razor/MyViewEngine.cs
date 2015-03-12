using System.Linq;
using System.Web.Mvc;

namespace DLUProjectFramework.ViewEngines.Razor
{
    public class MyViewEngine : RazorViewEngine
    {
        public MyViewEngine()
        {
            var newLocationFormat = new[] {
                "~/Views/Shared/MyModule/{0}.cshtml"
            };
            PartialViewLocationFormats = PartialViewLocationFormats.Union(newLocationFormat).ToArray();
        }
    }
}
//MyForumViewEngine