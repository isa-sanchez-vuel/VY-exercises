
using Countries.Application.Contracts.DTOs;
using Countries.Application.Impl;
using Countries.Domain;
using Countries.Infrastructure.Contracts;
using Countries.XCutting.GlobalVariables;
using Moq;

namespace Countries.Testing.UnitTests.Application
{
	public class CountryServiceTests
	{


		[Fact]
		public void GetCountriesByInitialAndYearPopulation_ResultHasNotErrors_ReturnFalse()
		{
			//Arrange
			CountryInitialYearRqtDTO request = new()
			{
				CountryFirstLetter = "t",
				Year = DateTime.Now.Year.ToString(),
			};

			Mock<IApiImporter> importerMock = new();
			importerMock.Setup(x => x.ImportData()).Returns(Task.FromResult(GlobalVariables.TEST_JSON));

			Mock<ICountryRepository> repositoryMock = new();

			Mock<CountryModel> modelMock = new();

			CountryService sut = new(
				repositoryMock.Object,
				importerMock.Object
				);

			//Act
			Task<CountryInitialYearResultDTO> result = sut.GetCountriesByInitialAndYearPopulation(request);


			//Assert
			Assert.False(result.Result.HasErrors);
		}
		/*
		[Fact]
		public void GetCountriesByInitialAndYearPopulation_RequestNull_ReturnTrue()
		{
			//Arrange
			CountryInitialYearRqtDTO request = new()
			{
				CountryFirstLetter = "t",
				Year = DateTime.Now.Year.ToString(),
			};

			Mock<IApiImporter> importerMock = new();
			importerMock.Setup(x => x.ImportData()).Returns(Task.FromResult(GlobalVariables.TEST_JSON));

			Mock<ICountryRepository> repositoryMock = new();

			CountryService sut = new(
				repositoryMock.Object,
				importerMock.Object
				);

			//Act
			Task<CountryInitialYearResultDTO> result = sut.GetCountriesByInitialAndYearPopulation(request);


			//Assert
			Assert.True(result.Result.HasErrors);
		}*/
	}
}
