
using Countries.Application.Contracts.DTOs;
using Countries.Application.Impl;
using Countries.Domain.Models;
using Countries.Infrastructure.Contracts;
using Countries.Infrastructure.Contracts.JsonImport;
using Countries.XCutting.Enums;
using Countries.XCutting.GlobalVariables;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Countries.Testing.UnitTests.Application
{
	public class CountryServiceTests
	{
		public const string TEST_JSON_SUCCESS = GlobalVariables.TEST_JSON_SUCCESS;
		public const string TEST_JSON_ERROR = GlobalVariables.TEST_JSON_ERROR;

		private const string COUNTRY_NAME = GlobalVariables.COUNTRY_NAME;
		private const string COUNTRY_CODE = GlobalVariables.COUNTRY_CODE;

		private const int COUNT_RESULT = GlobalVariables.COUNT_RESULT;

		private const string CORRECT_COUNTRY_CHAR = GlobalVariables.CORRECT_COUNTRY_CHAR;
		private const string CORRECT_POPULATION_YEAR = GlobalVariables.CORRECT_POPULATION_YEAR;

		private const string WRONG_COUNTRY_CHAR = GlobalVariables.WRONG_COUNTRY_CHAR;
		private const string WRONG_POPULATION_YEAR = GlobalVariables.WRONG_POPULATION_YEAR;

		private readonly List<CountryImported> CountriesCorrect = new()
			{
				new()
				{
					Name = COUNTRY_NAME,
					Code = COUNTRY_CODE,
					PopulationCounts = new()
					{
						new()
						{
							Year = int.Parse(CORRECT_POPULATION_YEAR),
							Count = COUNT_RESULT,
						}
					}
				}
			};

		private readonly CountryInitialYearRqtDTO RequestCorrect = new()
		{
			Year = CORRECT_POPULATION_YEAR,
			CountryFirstLetter = CORRECT_COUNTRY_CHAR
		};

		private readonly CountryInitialYearRqtDTO RequestError = new()
		{
			Year = WRONG_POPULATION_YEAR,
			CountryFirstLetter = WRONG_COUNTRY_CHAR,
		};

		[Fact]
		public void GetCountriesByInitialAndYearPopulation_ResultHasNotErrors_ReturnFalse()
		{
			//Arrange
			Mock<IApiImporter> importerMock = new();
			importerMock.Setup(x => x.ImportData()).ReturnsAsync(TEST_JSON_SUCCESS);

			Mock<ICountryRepository> repositoryMock = new();
			repositoryMock.Setup(x => x.LoadDatabaseCountries(CountriesCorrect));
			repositoryMock.Setup(x => x.GetCountriesByInitial(CORRECT_COUNTRY_CHAR)).Returns(CountriesCorrect);
			repositoryMock.Setup(x => x.GetPopulationByYear(COUNTRY_CODE, CORRECT_POPULATION_YEAR)).Returns(COUNT_RESULT);

			Mock<CountryModel> modelMock = new();

			CountryService sut = new(
				repositoryMock.Object,
				importerMock.Object
				);

			//Act
			Task<CountryInitialYearResultDTO> result = sut.GetCountriesByInitialAndYearPopulation(RequestCorrect);


			//Assert
			Assert.False(result.Result.HasErrors);
		}


		[Fact]
		public void GetCountriesByInitialAndYearPopulation_JsonErrorTrue_ReturnTrue()
		{
			//Arrange
			Mock<IApiImporter> importerMock = new();
			importerMock.Setup(x => x.ImportData()).ReturnsAsync(TEST_JSON_ERROR);

			Mock<ICountryRepository> repositoryMock = new();
			//repositoryMock.Setup(x => x.LoadDatabaseCountries(CountriesCorrect));
			//repositoryMock.Setup(x => x.GetCountriesByInitial(CORRECT_COUNTRY_CHAR)).Returns(CountriesCorrect);
			//repositoryMock.Setup(x => x.GetPopulationByYear(COUNTRY_CODE, CORRECT_POPULATION_YEAR)).Returns(COUNT_RESULT);

			Mock<CountryModel> modelMock = new();

			CountryService sut = new(
				repositoryMock.Object,
				importerMock.Object
				);

			//Act
			Task<CountryInitialYearResultDTO> result = sut.GetCountriesByInitialAndYearPopulation(RequestCorrect);


			//Assert
			Assert.True(result.Result.HasErrors);
			Assert.Equal(CountryInitialYearErrorEnum.ApiDataImportError, result.Result.Error);
		}


	}
}
