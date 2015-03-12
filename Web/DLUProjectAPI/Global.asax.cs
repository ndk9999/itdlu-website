

using DLUProject.API.Controllers;
using DLUProject.Data;
using DLUProject.Domain;
using DLUProject.Services;
using System.Reflection;
using System.Web.Http;

namespace DLUProjectAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            
        }
    }
}
