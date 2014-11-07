using Castle.MicroKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace SywTrends.Common.Ioc
{
	public class WindsorHttpDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver, System.Web.Mvc.IDependencyResolver
	{
		private readonly IKernel kernel;

		public WindsorHttpDependencyResolver(IKernel kernel)
		{
			this.kernel = kernel;
		}

		public IDependencyScope BeginScope()
		{
			return kernel.Resolve<IDependencyScope>(); // instances released suitably (at end of web request)
		}

		public object GetService(Type serviceType)
		{
			// for ModelMetadataProvider and other MVC related types that may have been added to the container
			// check the lifecycle of these registrations
			return kernel.HasComponent(serviceType) ? kernel.Resolve(serviceType) : null;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return kernel.HasComponent(serviceType) ? kernel.ResolveAll(serviceType) as IEnumerable<object> : Enumerable.Empty<object>();
		}

		public void Dispose()
		{
			// Nothing created so nothing to dispose - kernel will take care of its own
		}
	}
}