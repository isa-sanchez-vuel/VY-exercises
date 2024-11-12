using Countries.Application.Contracts.DTOs;

namespace Countries.Application.Contracts
{
	public interface ICountryService
	{
		Task<CountryInitialYearResultDTO> GetCountriesByInitialAndYearPopulation(CountryInitialYearRqtDTO request);
	}
}
