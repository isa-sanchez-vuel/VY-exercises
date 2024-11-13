using Countries.Infrastructure.Contracts;
using Countries.Infrastructure.Contracts.Entities;
using Countries.Infrastructure.Contracts.JsonImport;
using Countries.Infrastructure.Impl.Context;
using System.Reflection.Metadata.Ecma335;

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

		public List<CountryImported>? GetCountriesByInitial(string initial) //TODO change made
		{
			if (_countries == null) return null;

			var result = _countries.FindAll(x => x.Name.ToLower().StartsWith(initial.ToLower()));
			if (_countries.Count > 0) return result;
			else return null;
		}

		public int GetPopulationByYear(string code, string year) //TODO change made
		{
			PopulationCountImported? populationFiltered;
			CountryImported country = _countries.First(x => x.Code == code);

			populationFiltered = country.PopulationCounts.Find(x => x.Year.ToString().Equals(year));

			if (populationFiltered == null) return -1;

            return (int)populationFiltered!.Count;
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
