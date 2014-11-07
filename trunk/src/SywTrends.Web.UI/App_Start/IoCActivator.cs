using Castle.Windsor;
using Castle.Windsor.Installer;
using System.Web.Http;
using System.Web.Mvc;
using SywTrends.Common.Ioc;
using SywTrends.Web.UI.App_Start;

[assembly: WebActivator.PreApplicationStartMethod(typeof (IoCActivator), "Start")]

namespace SywTrends.Web.UI.App_Start
{
	public static class IoCActivator
	{
		public static IWindsorContainer Container { get; private set; }
		
		public static void Start()
		{
			var container = new WindsorContainer()
				.Install(FromAssembly.This())
				.Install(FromAssembly.Containing<DomainInstaller>());

			Container = container;

			var mvcControllerFactory = new WindsorControllerFactory(container.Kernel);
			ControllerBuilder.Current.SetControllerFactory(mvcControllerFactory);
		}
	}
}