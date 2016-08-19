using System;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace NewsPortal.Windsor_Utils
{
	public class ControllersInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(FindControllers().LifestyleTransient());
		}

		private static BasedOnDescriptor FindControllers()
		{
			return Classes.FromThisAssembly().BasedOn<IController>().
				If(t => t.Name.EndsWith("Controller"));
		}
	}
}