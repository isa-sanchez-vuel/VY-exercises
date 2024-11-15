using UniversitiesManagement.Infrastructure.Contracts.APIEntities;
using UniversitiesManagement.Infrastructure.Contracts.DBEntities;

namespace UniversitiesManagement.Infrastructure.Contracts
{
	public interface IDbUniversityRepository
	{
		List<University>? UpdateDatabaseWithApiData(List<UniversityJson> newUniversities);
		List<University>? GetAllUniversities();
		University? GetUniversity(int code);
		University? CreateUniversity(University newUni);
		University? UpdateUniversity(University updatedUni);
		University DeleteUniversity(int code);
	}
}
