namespace UniversitiesManagement.Application.Contracts.DTOs.Requests
{
	public class CreateUniversityRequestDTO
	{
		public string? Name { get; set; }
		public string? AlphaTwoCode { get; set; }
		public string? StateProvince { get; set; }
		public string? Country { get; set; }
	}
}
