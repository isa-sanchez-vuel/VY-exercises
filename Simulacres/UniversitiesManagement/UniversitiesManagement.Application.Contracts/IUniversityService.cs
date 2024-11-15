using UniversitiesManagement.Application.Contracts.DTOs.Requests;
using UniversitiesManagement.Application.Contracts.DTOs.Results;

namespace UniversitiesManagement.Application.Contracts
{
	public interface IUniversityService
	{
		Task<GetDatabaseResultDTO> UpdateDatabase();
		GetDatabaseResultDTO ListDatabaseUniversities();
		InsertUniversityResultDTO CreateNewUniversity(CreateUniversityRequestDTO request);
		InsertUniversityResultDTO UpdateOldUniversity(UpdateUniversityRequestDTO request);
		GetUniversityResultDTO FindUniversity(int idRequest);
		DeleteUniversityResultDTO DeleteExistentUniversity(int idRequest);



	}
}
