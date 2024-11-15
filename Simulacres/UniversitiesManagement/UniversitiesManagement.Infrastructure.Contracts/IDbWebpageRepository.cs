using UniversitiesManagement.Infrastructure.Contracts.DBEntities;

namespace UniversitiesManagement.Infrastructure.Contracts
{
	public interface IDbWebpageRepository
	{
		bool CreateWebpage(WebPage newWeb);
	}
}
