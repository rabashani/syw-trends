using System.Collections.Generic;
using System.Linq;
using Platform.Client;

namespace SywTrends.Domain.Interests
{
	public interface IInterestsApi
	{
		IList<InterestDto> Get();
	}

	public class InterestsApi : ApiBase, IInterestsApi
	{
		protected override string BasePath { get { return "interests"; } }

		public InterestsApi(IPlatformProxy platformProxy):base(platformProxy)
		{
		}

		public IList<InterestDto> Get()
		{
			return Get<IList<InterestDto>>("get");
		}

	}
}
