using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Platform.Client;

namespace SywTrends.Common.Ioc
{
	public class PlatformClientInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Classes.FromAssembly(typeof(PlatformProxy).Assembly).Pick()
				   .WithService.DefaultInterfaces()
				   .Configure(s => s.LifestyleSingleton()));
		}
	}
}