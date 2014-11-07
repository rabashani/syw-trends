using System;
using System.Web;

namespace SywTrends.Domain.Context
{
	public class RequestLevelPropertyContext<T> where T : class
	{
		private const string KeyFormat = "request-level-context:{0}";

		private readonly string _propertyName;

		public RequestLevelPropertyContext(string propertyName, T propertyValue)
		{
			if (string.IsNullOrEmpty(propertyName))
				throw new ArgumentNullException("propertyName");

			_propertyName = propertyName;

			if (HttpContext.Current != null)
				HttpContext.Current.Items[GetPropertyName()] = propertyValue;
		}

		public override string ToString()
		{
			if (HttpContext.Current != null)
			{
				var item = HttpContext.Current.Items[GetPropertyName()];

				return item != null ? item.ToString() : null;
			}

			return null;
		}

		private string GetPropertyName()
		{
			return string.Format(KeyFormat, _propertyName);
		}
	}
}