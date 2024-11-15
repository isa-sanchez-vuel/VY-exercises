
using UniversitiesManagement.Domain;

namespace UniversitiesManagement.Testing.UnitTests.Domain
{
	public class UniversitymodelTests
	{
		[Fact]
		public void CheckAlphaCode_True_ReturnTrue()
		{
			//Arrange
			string testString = "AA";

			//Act
			bool result = UniversityModel.CheckAlphaCode(testString);

			//Assert
			Assert.True(result);
		}

		[Fact]
		public void CheckAlphaCode_LengthWorng_ReturnFalse()
		{
			//Arrange
			string testString = "AAA";

			//Act
			bool result = UniversityModel.CheckAlphaCode(testString);

			//Assert
			Assert.False(result);
		}

		[Fact]
		public void CheckAlphaCode_Null_ReturnFalse()
		{
			//Arrange
			string? testString = null;

			//Act
			bool result = UniversityModel.CheckAlphaCode(testString);

			//Assert
			Assert.False(result);
		}

		[Fact]
		public void CheckAlphaCode_Empty_ReturnFalse()
		{
			//Arrange
			string testString = "";

			//Act
			bool result = UniversityModel.CheckAlphaCode(testString);

			//Assert
			Assert.False(result);
		}
	}
}
