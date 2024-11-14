using Countries.Infrastructure.Contracts.JsonImport;

namespace Countries.Infrastructure.Contracts
{
	public interface ICountryRepository
	{
		List<CountryImported> GetAllCountries();
		void LoadDatabaseCountries(List<CountryImported> countriesList);

	}
}
