using Countries.Infrastructure.Contracts;
using Countries.Infrastructure.Contracts.Entities;
using Countries.Infrastructure.Impl.Context;

namespace Countries.Infrastructure.Impl
{
	public class CountryRepository : ICountryRepository
	{
		private readonly CountryPopulationContext _context;

		public CountryRepository(CountryPopulationContext context)
		{
			_context = context;
		}

		public List<Country> GetAllCountries()
		{
			return _context.Countries.ToList();
		}

		public List<PopulationCount> GetAllPopulationCount()
		{
			return _context.PopulationCounts.ToList();
		}

		public void LoadDatabaseCountries(List<Country> countriesList)
		{
			foreach (var country in countriesList)
			{
				_context.Countries.Add(country);
			}
		}

		public void LoadDatabasePopulationCounts(List<PopulationCount> populationList)
		{
			foreach (var populationCount in populationList)
			{
				_context.PopulationCounts.Add(populationCount);
			}
		}
	}
}
