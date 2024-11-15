
namespace UniversitiesManagement.Application.Contracts.DTOs.ContentModels
{
	public class DeletedUniversityDTO
	{
		public bool IsDeleted { get; set; }
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Country { get; set; }
		public string? AlphaTwoCode { get; set; }
		public string? StateProvince { get; set; }
		public string? SuccessMessage { get; set; }
	}
}
