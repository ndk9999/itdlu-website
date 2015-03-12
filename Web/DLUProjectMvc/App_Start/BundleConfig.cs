using System.Web;
using System.Web.Optimization;

namespace DLUProjectMvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/additional-methods*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862

           
            #region  AUCTION STORE
            bundles.Add(new StyleBundle("~/Content/store").Include(
                   "~/Content/bootstrap.css",
                   "~/Content/font-awesome.css",
                   "~/Content/store/style.css",
                   "~/Content/store/prettyPhoto.css",
                   "~/Content/store/price-range.css",
                   "~/Content/store/animate.css",
                   "~/Content/store/responsive.css"));

             
            bundles.Add(new ScriptBundle("~/Scripts/store").Include(
                 "~/Scripts/store/html5shiv.js",
                "~/Scripts/store/jquery.scrollUp.js",
                "~/Scripts/store/price-range.js",
                 "~/Scripts/store/jquery.prettyPhoto.js",
                  
                  "~/Scripts/store/main.js",
                  "~/Scripts/store/colorlife.src.js"
                ));              
            #endregion


            bundles.Add(new ScriptBundle("~/bundles/AwesomeAngularMVCApp")
    .IncludeDirectory("~/Scripts/Controllers", "*.js")
    .Include("~/Scripts/AwesomeAngularMVCApp.js"));

            BundleTable.EnableOptimizations = false   ;
        }
    }
}
