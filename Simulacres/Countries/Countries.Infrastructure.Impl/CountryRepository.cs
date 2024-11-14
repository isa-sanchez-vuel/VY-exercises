using Countries.Infrastructure.Contracts;
using Countries.Infrastructure.Contracts.JsonImport;
using Countries.Infrastructure.Impl.Context;

namespace Countries.Infrastructure.Impl
{
	public class CountryRepository : ICountryRepository
	{
		//private readonly CountryPopulationContext _context;
		private readonly List<CountryImported> _countries;

		public CountryRepository(CountryPopulationContext context)
		{
			_countries = new List<CountryImported>();
			//_context = context;
		}

		public List<CountryImported> GetAllCountries()
		{
			return _countries;
			//return _context.Countries.ToList();
		}

		public void LoadDatabaseCountries(List<CountryImported> countriesList)
		{
			foreach (var country in countriesList)
			{
				_countries.Add(country);
				//_context.Countries.Add(country);
			}
		}

	}
}
