using SWApiManagement.XCutting.Enums;

namespace SWApiManagement.Application.Contracts.DTOs
{
	public class ResidentResultDTO
	{
		public bool HasErrors { get; set; }
		public ErrorEnum? Error { get; set; }
		public string Message { get; set; }
		public List<string> ResidentNames { get; set; }
	}
}
