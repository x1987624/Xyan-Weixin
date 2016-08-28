using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

using Xyan.IBLL;
using Xyan.BLL;

namespace Weixin
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            var containerBuilder = new ContainerBuilder();
            //注入service
            containerBuilder.Register(c => new BaseUserService()).As<IBaseUserService>().SingleInstance();
            containerBuilder.Register(c => new BasePermissionGroupService()).As<IBasePermissionGroupService>().SingleInstance();
            containerBuilder.Register(c => new BasePermissionService()).As<IBasePermissionService>().SingleInstance();
            containerBuilder.Register(c => new BaseRoleService()).As<IBaseRoleService>().SingleInstance();

            containerBuilder.RegisterControllers(Assembly.GetExecutingAssembly());
            containerBuilder.RegisterFilterProvider();
            IContainer container = containerBuilder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}