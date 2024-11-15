using UniversitiesManagement.Application.Contracts.DTOs.ContentModels;
using UniversitiesManagement.XCutting;

namespace UniversitiesManagement.Application.Contracts.DTOs.Results
{
	public class InsertUniversityResultDTO
	{
		public bool HasErrors { get; set; }
		public ErrorEnum? Error { get; set; }
		public List<ErrorEnum>? ErrorMessages { get; set; }
		public UniversityCompleteDTO? OldUniversity { get; set; }
		public UniversityCompleteDTO? UpdatedUniversity { get; set; }
	}
}
