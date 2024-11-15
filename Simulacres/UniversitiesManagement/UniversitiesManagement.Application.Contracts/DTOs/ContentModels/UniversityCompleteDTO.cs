using System.ComponentModel.DataAnnotations;

namespace UniversitiesManagement.Application.Contracts.DTOs.ContentModels
{
	public class UniversityCompleteDTO
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Country { get; set; }
		public string? AlphaTwoCode { get; set; }
		public string? StateProvince { get; set; }
	}
}
