using OOPBankMultiuser.Domain.Models;

namespace OOPBankMultiuser.Testing.UnitTesting.OOPBankMultiuser.Domain
{
	public class AccountUnitTest
	{

		#region ValidateIncome

		[Fact]
		public void ValidateIncome_WhenValidInput_ReturnTrue()
		{
			//Arrange
			int income = 500;
			AccountModel model = new();

			//Act
			bool result = model.ValidateIncome(income);

			//Assert
			Assert.True(result);
		}

		[Fact]
		public void ValidateIncome_WhenNegativeInput_ReturnFalse()
		{
			//Arrange
			int income = -500;
			AccountModel model = new();

			//Act
			bool result = model.ValidateIncome(income);

			//Assert
			Assert.False(result);
		}

		[Fact]
		public void ValidateIncome_WhenOverMaxInput_ReturnFalse()
		{
			//Arrange
			int income = 10000;
			AccountModel model = new();

			//Act
			bool result = model.ValidateIncome(income);

			//Assert
			Assert.False(result);
		}

		#endregion

		#region ValidateOutcome
		[Fact]
		public void ValidateOutcome_WhenValidInput_ReturnTrue()
		{
			//Arrange
			int outcome = 500;
			AccountModel model = new()
			{
				TotalBalance = 500
			};

			//Act
			bool result = model.ValidateOutcome(outcome);

			//Assert
			Assert.True(result);
		}

		[Fact]
		public void ValidateOutcome_WhenNegativeInput_ReturnFalse()
		{
			//Arrange
			int outcome = -500;
			AccountModel model = new();

			//Act
			bool result = model.ValidateOutcome(outcome);

			//Assert
			Assert.False(result);
		}

		[Fact]
		public void ValidateOutcome_WhenOverMaxInput_ReturnFalse()
		{
			//Arrange
			int outcome = 10000;
			AccountModel model = new();

			//Act
			bool result = model.ValidateOutcome(outcome);

			//Assert
			Assert.False(result);
		}

		[Fact]
		public void ValidateOutcome_WhenOverBalanceInput_ReturnFalse()
		{
			//Arrange
			int outcome = 2000;
			AccountModel model = new()
			{
				TotalBalance = 100,
			};

			//Act
			bool result = model.ValidateOutcome(outcome);

			//Assert
			Assert.False(result);
		}
		#endregion

		#region ValidateNumber

		[Fact]
		public void ValidateNumber_WhenValidInput_ReturnTrue()
		{
			//Arrange
			string number = "1234567890";
			AccountModel model = new();

			//Act
			bool result = model.ValidateNumber(number);

			//Assert
			Assert.True(result);
		}

		[Fact]
		public void ValidateNumber_WhenWrongSize_ReturnFalse()
		{
			//Arrange
			string number = "123456789";
			AccountModel model = new();

			//Act
			bool result = model.ValidateNumber(number);

			//Assert
			Assert.False(result);
		}

		[Fact]
		public void ValidateNumber_WhenWrongFormat_ReturnFalse()
		{
			//Arrange
			string number = "eeeeeeeeee";
			AccountModel model = new();

			//Act
			bool result = model.ValidateNumber(number);

			//Assert
			Assert.False(result);
		}

		#endregion

		#region ValidatePin
		[Fact]
		public void ValidatePin_WhenValidInput_ReturnTrue()
		{
			//Arrange
			string pin = "1234";
			AccountModel model = new();

			//Act
			bool result = model.ValidatePin(pin);

			//Assert
			Assert.True(result);
		}

		[Fact]
		public void ValidatePin_WhenWrongSize_ReturnFalse()
		{
			//Arrange
			string pin = "12345";
			AccountModel model = new();

			//Act
			bool result = model.ValidatePin(pin);

			//Assert
			Assert.False(result);
		}

		[Fact]
		public void ValidatePin_WhenWrongFormat_ReturnFalse()
		{
			//Arrange
			string pin = "eeee";
			AccountModel model = new();

			//Act
			bool result = model.ValidatePin(pin);

			//Assert
			Assert.False(result);
		}
		#endregion


	}
}
