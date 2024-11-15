using UniversitiesManagement.Application.Contracts.DTOs.ContentModels;
using UniversitiesManagement.XCutting;

namespace UniversitiesManagement.Application.Contracts.DTOs.Results
{
	public class GetDatabaseResultDTO
	{
		public bool HasErrors { get; set; }
		public ErrorEnum? Error {  get; set; } 
		public List<ErrorEnum>? ErrorMessages { get; set; }
		public List<UniversityDTO>? Universities { get; set; }
	}
}
