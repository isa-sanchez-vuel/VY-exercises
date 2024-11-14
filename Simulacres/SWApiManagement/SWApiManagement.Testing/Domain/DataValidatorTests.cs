using SWApiManagement.Domain;

namespace SWApiManagement.Testing.Domain
{
	public class DataValidatorTests
	{

		[Fact]
		public void StringIsValid_True_ReturnTrue()
		{
			//Arrange
			string testString = "Tatooine";

			//Act
			bool result = DataValidator.StringIsValid(testString);

			//Assert
			Assert.True(result);
		}

		[Fact]
		public void StringIsValid_StringEmpty_ReturnFalse()
		{
			//Arrange
			string testString = "";

			//Act
			bool result = DataValidator.StringIsValid(testString);

			//Assert
			Assert.False(result);
		}

		[Fact]
		public void StringIsValid_StringNull_ReturnFalse()
		{
			//Arrange
			string? testString = null;

			//Act
			bool result = DataValidator.StringIsValid(testString);

			//Assert
			Assert.False(result);
		}
	}
}
