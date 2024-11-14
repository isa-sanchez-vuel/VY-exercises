
using Countries.Application.Contracts;
using Countries.Application.Contracts.DTOs;
using Countries.Domain;
using Countries.Domain.Models;
using Countries.Infrastructure.Contracts;
using Countries.Infrastructure.Contracts.Entities;
using Countries.Infrastructure.Contracts.JsonImport;
using Countries.XCutting.Enums;
using Countries.XCutting.GlobalVariables;
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

			if (!ValidatorModel.ValidateChar(request.CountryFirstLetter)) result.Error = CountryInitialYearErrorEnum.FirstLetterNotAChar;
			else if (!ValidatorModel.ValidateYear(request.Year)) result.Error = CountryInitialYearErrorEnum.InvalidYear;

			else if (request == null) result.Error = CountryInitialYearErrorEnum.RequestNull;
			else if (_importer == null) result.Error = CountryInitialYearErrorEnum.ImporterNull;
			else if (_repository == null) result.Error = CountryInitialYearErrorEnum.RepositoryNull;
			else
			{
				string jsonApi = await _importer.ImportData();
				CountryListImported? importedList = JsonSerializer.Deserialize<CountryListImported>(jsonApi);
				if (importedList == null) result.Error = CountryInitialYearErrorEnum.ApiImportFailed;
				else if (importedList.Error == true) result.Error = CountryInitialYearErrorEnum.ApiDataImportError; //TODO change made
				else
				{
					CountryListModel? countryListModel = MapJsonToModel(importedList);

					if (countryListModel == null) result.Error = CountryInitialYearErrorEnum.ModelMapFailed;
					else
					{
						List<CountryModel>? tempCountries = countryListModel.GetCountriesByInitial(request.CountryFirstLetter);

						if (tempCountries == null) result.Error = CountryInitialYearErrorEnum.CountryListNull;
						else
						{
							result.Countries = tempCountries.Select(x =>
							new CountryPopulationCountDTO()
							{
								Name = x.Name,
								TotalPopulation = x.GetPopulationFromYear(request.Year)
							}).ToList();

							result.HasErrors = false;
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
						Year = y.Year.ToString(),
						Counter = (int)y.Count,
					}).ToList(),

				}).ToList(),
			};
		}
	}
}
