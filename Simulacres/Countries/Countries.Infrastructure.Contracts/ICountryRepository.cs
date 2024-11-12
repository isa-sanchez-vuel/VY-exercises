using Countries.Infrastructure.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace Countries.Infrastructure.Contracts
{
	public interface ICountryRepository
	{

		public List<Country> GetAllCountries();
		public List<PopulationCount> GetAllPopulationCount();
		public void LoadDatabaseCountries(List<Country> countriesList);
		public void LoadDatabasePopulationCounts(List<PopulationCount> populationList);
	}
}
