
using UniversitiesManagement.Infrastructure.Contracts.APIEntities;

namespace UniversitiesManagement.Infrastructure.Contracts
{
	public interface IApiRepository
	{

		Task<UniversityListJson?> ImportApiData();
		Task<T?> ImportApiData<T>(string url);

	}
}
