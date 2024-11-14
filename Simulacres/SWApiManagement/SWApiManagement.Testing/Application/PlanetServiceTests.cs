
using Moq;
using SWApiManagement.Application.Contracts.DTOs;
using SWApiManagement.Application.Impl;
using SWApiManagement.Infrastructure.Contracts;
using SWApiManagement.Infrastructure.Contracts.APIEntities;
using SWApiManagement.Infrastructure.Contracts.DBEntities;
using SWApiManagement.XCutting;
using SWApiManagement.XCutting.Enums;

namespace SWApiManagement.Testing.Application
{
    public class PlanetServiceTests
    {

        #region UpdateDatabase
        PlanetListJson planetListJson = new();

        [Fact]
        public async Task UpdateDatabase_DtoHasNoErros_ReturnFalse()
        {
            //Arrange
            Mock<IApiImporter> mockImporter = new();
            mockImporter.Setup(x => x.ImportApiData()).ReturnsAsync(planetListJson);

            Mock<IPlanetsRepository> mockRepository = new();
            mockRepository.Setup(x => x.UpdateDatabase(planetListJson.Planets)).Returns(new List<Planet>());


            PlanetService sut = new(
                mockRepository.Object,
                mockImporter.Object
                );

            //Act
            UpdateResultDTO result = await sut.UpdateDatabaseWithApi();

            //Assert
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task UpdateDatabase_ImporterNull_ReturnTrue()
        {
            //Arrange
            Mock<IPlanetsRepository> mockRepository = new();

            PlanetService sut = new(
                mockRepository.Object,
                null
                );

            //Act
            UpdateResultDTO result = await sut.UpdateDatabaseWithApi();

            //Assert
            Assert.True(result.HasErrors);
			Assert.Equal(ErrorEnum.ImporterNull, result.Error);
			Assert.Equal(GlobalVariables.ERROR_MESSAGE, result.Message);
		}

        [Fact]
        public async Task UpdateDatabase_RepositoryNull_ReturnTrue()
        {
            //Arrange
            Mock<IApiImporter> mockImporter = new();

            PlanetService sut = new(
                null,
				mockImporter.Object
				);

            //Act
            UpdateResultDTO result = await sut.UpdateDatabaseWithApi();

            //Assert
            Assert.True(result.HasErrors);
			Assert.Equal(ErrorEnum.RepositoryNull, result.Error);
			Assert.Equal(GlobalVariables.ERROR_MESSAGE, result.Message);
		}

        [Fact]
        public async Task UpdateDatabase_EntityListNull_ReturnTrue()
        {
			//Arrange
			Mock<IApiImporter> mockImporter = new();
			mockImporter.Setup(x => x.ImportApiData()).ReturnsAsync(planetListJson);

			Mock<IPlanetsRepository> mockRepository = new();


			PlanetService sut = new(
				mockRepository.Object,
				mockImporter.Object
				);

			//Act
			UpdateResultDTO result = await sut.UpdateDatabaseWithApi();

			//Assert
			Assert.True(result.HasErrors);
			Assert.Equal(ErrorEnum.RetrieveFromDatabaseFailed, result.Error);
			Assert.Equal(GlobalVariables.ERROR_MESSAGE, result.Message);
		}
        
        [Fact]
        public async Task UpdateDatabase_JsonListIsNull_ReturnTrue()
        {
			//Arrange
			Mock<IApiImporter> mockImporter = new();

			Mock<IPlanetsRepository> mockRepository = new();

			PlanetService sut = new(
				mockRepository.Object,
				mockImporter.Object
				);

			//Act
			UpdateResultDTO result = await sut.UpdateDatabaseWithApi();

			//Assert
			Assert.True(result.HasErrors);
			Assert.Equal(ErrorEnum.PlanetListIsNull, result.Error);
			Assert.Equal(GlobalVariables.ERROR_MESSAGE, result.Message);
		}

        #endregion

        #region GetResidentsOfPlanet

        string planetName = "Tatooine";
        Planet planet = new();
        PlanetJson planetJson = new();

        [Fact]
        public async Task GetResidentsOfPlanet_DtoHasNoErros_ReturnFalse()
        {
            //Arrange
            Mock<IPlanetsRepository> mockRepository = new();
            mockRepository.Setup(x => x.GetPlanet(planetName)).Returns(planet);

            Mock<IApiImporter> mockImporter = new();
            mockImporter.Setup(x => x.FindPlanetInApi(planet.Url)).ReturnsAsync(planetJson);
            mockImporter.Setup(x => x.FindResidents(planetJson)).ReturnsAsync(new List<string>());

            PlanetService sut = new(
                mockRepository.Object,
                mockImporter.Object
                );

            //Act
            ResidentResultDTO result = await sut.GetResidentsOfPlanet(planetName);

            //Assert
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task GetResidentsOfPlanet_DBPlanetNotFound_ReturnTrue()
        {
            //Arrange
            Mock<IPlanetsRepository> mockRepository = new();

            Mock<IApiImporter> mockImporter = new();

            PlanetService sut = new(
                mockRepository.Object,
                mockImporter.Object
                );

            //Act
            ResidentResultDTO result = await sut.GetResidentsOfPlanet(planetName);

            //Assert
            Assert.True(result.HasErrors);
            Assert.Equal(ErrorEnum.PlanetNotFound, result.Error);
            Assert.Equal(GlobalVariables.PLANET_NOT_FOUND, result.Message);
        }
        
        [Fact]
        public async Task GetResidentsOfPlanet_JsonPlanetNotFound_ReturnTrue()
        {
            //Arrange
            Mock<IPlanetsRepository> mockRepository = new();
			mockRepository.Setup(x => x.GetPlanet(planetName)).Returns(planet);

			Mock<IApiImporter> mockImporter = new();

            PlanetService sut = new(
                mockRepository.Object,
                mockImporter.Object
                );

            //Act
            ResidentResultDTO result = await sut.GetResidentsOfPlanet(planetName);

            //Assert
            Assert.True(result.HasErrors);
            Assert.Equal(ErrorEnum.PlanetNotFound, result.Error);
            Assert.Equal(GlobalVariables.PLANET_NOT_FOUND, result.Message);
        }

        [Fact]
        public async Task GetResidentsOfPlanet_ImporterNull_ReturnTrue()
        {
            //Arrange
            Mock<IPlanetsRepository> mockRepository = new();

            Mock<IApiImporter> mockImporter = new();

            PlanetService sut = new(
                mockRepository.Object,
                null
                );

            //Act
            ResidentResultDTO result = await sut.GetResidentsOfPlanet(planetName);

            //Assert
            Assert.True(result.HasErrors);
            Assert.Equal(ErrorEnum.ImporterNull, result.Error);
            Assert.Equal(GlobalVariables.ERROR_MESSAGE, result.Message);
        }


        #endregion

    }
}
