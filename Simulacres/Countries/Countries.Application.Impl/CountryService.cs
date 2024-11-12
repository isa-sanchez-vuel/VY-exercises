
using Countries.Application.Contracts;
using Countries.Application.Contracts.DTOs;
using Countries.Domain;
using Countries.Infrastructure.Contracts;
using Countries.Infrastructure.Contracts.Entities;
using Countries.Infrastructure.Contracts.JsonImport;
using Countries.XCutting.Enums;
using System.Globalization;
using System.Text.Json;

namespace Countries.Application.Impl
{
	public class CountryService : ICountryService
	{
		private readonly ICountryRepository _repository;
		private readonly IApiImporter _importer;

		public CountryService(ICountryRepository repository, IApiImporter importer)
		{
			_repository = repository;
			_importer = importer;
		}


		public async Task<CountryInitialYearResultDTO> GetCountriesByInitialAndYearPopulation(CountryInitialYearRqtDTO request)
		{
			CountryInitialYearResultDTO result = new()
			{
				HasErrors = true,
				Error = null,
			};

			if (request == null) result.Error = CountryInitialYearErrorEnum.RequestNull;
			else if (request.CountryFirstLetter.Length != 1) result.Error = CountryInitialYearErrorEnum.FirstLetterNotOneChar;
			else if (!int.TryParse(request.Year, out _)) result.Error = CountryInitialYearErrorEnum.YearWrongFormat;
			else if (_importer == null) result.Error = CountryInitialYearErrorEnum.ImporterNull;
			else if (_repository == null) result.Error = CountryInitialYearErrorEnum.RepositoryNull;
			else
			{
				string jsonApi = await _importer.ImportData();
				CountryListImported? importedList = JsonSerializer.Deserialize<CountryListImported>(jsonApi);
				if (importedList == null) result.Error = CountryInitialYearErrorEnum.ListimportFailed;
				else
				{
					CountryListModel? countryListModel = MapJsonToModel(importedList);

					if (countryListModel == null) result.Error = CountryInitialYearErrorEnum.ModelMapFailed;
					else
					{
						char initial = request.CountryFirstLetter[0];
						List<CountryModel>? tempCountries = countryListModel.GetCountriesByInitial(initial);

						if (tempCountries == null) result.Error = CountryInitialYearErrorEnum.CountryListNull;
						else
						{
							result.Countries = new();

							foreach (var country in tempCountries)
							{
								int population = country.GetPopulationFromYear(request.Year);

								if (population <= 0 && country.PopulationNull) result.Error = CountryInitialYearErrorEnum.YearNotListed;
								else
								{
									result.HasErrors = false;

									CountryPopulationCountDTO dto = new();

									dto.Name = country.Name;
									dto.TotalPopulation = population;

									result.Countries.Add(dto);
								}
							}
						}
					}
				}
			}
			return result;
		}

		private CountryListModel MapJsonToModel(CountryListImported? importedList)
		{
			return new()
			{
				CountryList = importedList!.Countries.Select(x => new CountryModel()
				{
					Name = x.Name,
					Code = x.Code,
					Iso3 = x.Iso3,
					PopulationCounts = x.PopulationCounts.Select(y => new PopulationCountModel()
					{
						Year = DateTime.ParseExact(y.Year.ToString(), "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None),
						Counter = (int)y.Count,
					}).ToList(),

				}).ToList(),
			};
		}
	}
}
