
using Moq;
using UniversitiesManagement.Application.Contracts.DTOs.Results;
using UniversitiesManagement.Application.Impl;
using UniversitiesManagement.Infrastructure.Contracts;
using UniversitiesManagement.Infrastructure.Contracts.APIEntities;
using UniversitiesManagement.Infrastructure.Contracts.DBEntities;

namespace UniversitiesManagement.Testing.UnitTests.Application
{
	public class UniversityServiceTests
	{
		UniversityListJson universityListjson = new();

		[Fact]
		public async Task UpdateDatabase_DtoHasNoErros_ReturnFalse()
		{
			//Arrange
			Mock<IApiRepository> mockImporter = new();
			mockImporter.Setup(x => x.ImportApiData()).ReturnsAsync(universityListjson);

			Mock<IDbUniversityRepository> mockRepository = new();
			mockRepository.Setup(x => x.UpdateDatabaseWithApiData(universityListjson.Universities)).Returns(new List<University>());


			UniversityService sut = new(
				mockRepository.Object,
				mockImporter.Object
				);

			//Act
			GetDatabaseResultDTO result = await sut.UpdateDatabase();

			//Assert
			Assert.False(result.HasErrors);
		}
	}
}
