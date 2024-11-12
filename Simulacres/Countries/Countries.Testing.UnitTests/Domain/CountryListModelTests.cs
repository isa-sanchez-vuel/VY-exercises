using Countries.Domain;
using Moq;

namespace Countries.Testing.UnitTests.Domain
{
	public class CountryListModelTests
	{

		[Fact]
		public void GetCountriesByInitial_CharFoundListReturned_ReturnNotNull()
		{
			//Arrange
			CountryListModel model = new();
			model.CountryList = new()
			{
				new()
				{
					Name = "Test",
				}
			};

			char initial = 't';

			//Act
			List<CountryModel>? list = model.GetCountriesByInitial(initial);


			//Assert
			Assert.NotNull(list);

		}

		[Fact]
		public void GetCountriesByInitial_CharNotFound_ReturnFalse()
		{
			//Arrange
			CountryListModel model = new();
			model.CountryList = new()
			{
				new()
				{
					Name = "Test",
				}
			};

			char initial = 'c';

			//Act
			List<CountryModel>? list = model.GetCountriesByInitial(initial);


			//Assert
			Assert.NotNull(list);
			Assert.False(list.Count > 0);

		}

		[Fact]
		public void GetCountriesByInitial_ListEmpty_ReturnNull()
		{
			//Arrange
			CountryListModel model = new();

			char initial = 'T';

			//Act
			List<CountryModel>? list = model.GetCountriesByInitial(initial);


			//Assert
			Assert.Null(list);

		}
	}
}
