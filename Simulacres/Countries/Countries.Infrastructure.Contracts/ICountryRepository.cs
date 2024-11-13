using Countries.Infrastructure.Contracts.Entities;
using Countries.Infrastructure.Contracts.JsonImport;
using Microsoft.EntityFrameworkCore;

namespace Countries.Infrastructure.Contracts
{
	public interface ICountryRepository
	{
		List<CountryImported> GetAllCountries();
		List<CountryImported>? GetCountriesByInitial(string initial);
		int GetPopulationByYear(string code, string year);
		void LoadDatabaseCountries(List<CountryImported> countriesList);

	}
}
