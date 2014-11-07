using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SywTrends.Domain.Users;

namespace SywTrends.Common.Ioc
{
	public class DomainInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Classes.FromAssembly(typeof(UsersApi).Assembly).Pick()
				   .WithService.DefaultInterfaces()
				   .Configure(s => s.LifestyleSingleton()));

			container.Register(
				Classes.FromThisAssembly().Pick()
				   .WithService.DefaultInterfaces()
				   .Configure(s => s.LifestyleSingleton()));

			container.AddFacility<LoggingFacility>(f => f.UseLog4Net());
			container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel, true));
		}
	}
}