using log4net;

namespace SywTrends.Domain.Context
{
	public interface ILoggingContext
	{
		void Set<T>(string name, T value) where T : class;
	}

	public class LoggingContext : ILoggingContext
	{
		public void Set<T>(string name, T value) where T : class
		{
			ThreadContext.Properties[name] = new RequestLevelPropertyContext<T>(name, value);
		}
	}
}