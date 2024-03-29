﻿using DLUProject.Domain;
using DLUProjectFramework.ViewEngines.Razor;
using DLUProjectFramework.AutoMapper;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

 

namespace DLUProjectMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperConfig.AutoMapConfigure();

            GlobalConfiguration.Configure(DLUProjectAPI.WebApiConfig.Register);

            
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new AdministrationViewEngine());
            ViewEngines.Engines.Add(new MyDLUViewEngine());
           
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

           
        }
        
    }
}
