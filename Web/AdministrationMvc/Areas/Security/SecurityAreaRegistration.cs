using System.Web.Mvc;

namespace DLUProjectMvc.Areas.Security
{
    public class SecurityAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Security";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
               "Security_default",
               "Security/{controller}/{action}/{id}",
               new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional },
               new[] { "DLUProjectMvc.Areas.Security.Controllers" }
           ); 
        }
    }
}