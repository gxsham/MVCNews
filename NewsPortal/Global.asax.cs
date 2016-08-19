using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using NewsPortal.Windsor_Utils;

namespace NewsPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

			var container = new WindsorContainer().Install(FromAssembly.This());
			
			ServiceLocator.RegisterAll(container.Kernel);
			ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container.Kernel));
		}
    }
}
