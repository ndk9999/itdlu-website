using Autofac;
using Autofac.Integration.WebApi;
using DLUProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace DLUProjectAPI.App_Start
{
    internal class AutofacWebAPI
    {

        public static void Initialize(HttpConfiguration config)
        {

            config.DependencyResolver = new AutofacWebApiDependencyResolver(
                RegisterServices(new ContainerBuilder())
            );
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            //deal with your dependencies here
            builder.RegisterType<CategoryService>().As<internal class AutofacWebAPI {

    public static void Initialize(HttpConfiguration config) {

        config.DependencyResolver = new AutofacWebApiDependencyResolver(
            RegisterServices(new ContainerBuilder())
        );
    }

    private static IContainer RegisterServices(ContainerBuilder builder) {

        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();

        //deal with your dependencies here
        builder.RegisterType<CarsService>().As<ICarsService>();
        builder.RegisterType<CarsCountService>().As<ICarsCountService>();

        return builder.Build();
    }
}>();
            builder.RegisterType<CarsCountService>().As<ICarsCountService>();

            return builder.Build();
        }
    }
}