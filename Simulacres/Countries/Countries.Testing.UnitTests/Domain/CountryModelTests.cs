using Countries.Domain.Models;
using Moq;

namespace Countries.Testing.UnitTests.Domain
{
    public class CountryModelTests
	{

		[Fact]
		public void GetPopulationFromYear_YearFoundPopulationAboveZero_ReturnTrue()
		{
			//Arrange
			CountryModel model = new();
			model.PopulationCounts = new()
			{
				new()
				{
					Year = "2000",
					Counter = 1000,
				}
			};

			string year = "2000"; //setting min value to get the same year

			//Act
			int result = model.GetPopulationFromYear(year);


			//Assert
			Assert.True(result > 0);
		}

		[Fact]
		public void GetPopulationFromYear_PopulationNull_ReturnFalse()
		{
			//Arrange
			CountryModel model = new();
			model.PopulationCounts = new()
			{
				new()
				{
					Year = "2000",
					Counter = 1000,
				}
			};

			string year = "2000"; //setting max value to get a different year

			//Act
			int result = model.GetPopulationFromYear(year);


			//Assert
			Assert.False(result > 0);
		}

	}
}
