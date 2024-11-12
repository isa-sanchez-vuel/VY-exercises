using Countries.XCutting.Enums;

namespace Countries.Application.Contracts.DTOs
{
	public class CountryInitialYearResultDTO
	{
		public bool HasErrors { get; set; }
		public string? ErrorMessage { get; set; }
		public CountryInitialYearErrorEnum? Error { get; set; }
		public List<CountryPopulationCountDTO>? Countries { get; set; }
	}
}
