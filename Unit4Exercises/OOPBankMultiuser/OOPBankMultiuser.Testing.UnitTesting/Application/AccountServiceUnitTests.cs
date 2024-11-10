using Moq;
using OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations;
using OOPBankMultiuser.Application.Impl;
using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.Infrastructure.Contracts.Entities;
using OOPBankMultiuser.XCutting.Enums;


namespace OOPBankMultiuser.Testing.UnitTesting.Application
{
	public class AccountServiceUnitTests
	{
		#region DepositMoney

		[Fact]
		public void DepositMoney_WhenValidInput_ReturnHasErrorsFalse()
		{
			//Arrange
			int income = 500;
			int id = It.IsAny<int>();

			Mock<IAccountRepository> mockAccountRepository = new();
			mockAccountRepository.Setup(x => x.GetAccountInfo(id)).Returns(new Account());

			Mock<IMovementRepository> mockMovementRepository = new();
			mockMovementRepository.Setup(x => x.GetMovements(id)).Returns(new List<Movement>());

			AccountService sut = new(
				mockAccountRepository.Object,
				mockMovementRepository.Object
				);

			//Act
			IncomeResultDTO result = sut.AddMoney(income, id);

			//Assert
			Assert.False(result.ResultHasErrors);
		}

		[Fact]
		public void DepositMoney_WhenIncomeNegative_ReturnHasErrorsTrue()
		{
			//Arrange
			int income = -500;
			int id = It.IsAny<int>();

			Mock<IAccountRepository> mockAccountRepository = new();
			mockAccountRepository.Setup(x => x.GetAccountInfo(id)).Returns(new Account());

			Mock<IMovementRepository> mockMovementRepository = new();
			mockMovementRepository.Setup(x => x.GetMovements(id)).Returns(new List<Movement>());

			AccountService sut = new(
				mockAccountRepository.Object,
				mockMovementRepository.Object
				);

			//Act
			IncomeResultDTO result = sut.AddMoney(income, id);

			//Assert
			Assert.True(result.ResultHasErrors);
			Assert.True(result.Error == IncomeErrorEnum.NegativeValue);
		}

		[Fact]
		public void DepositMoney_WhenIncomeOverMax_ReturnHasErrorsTrue()
		{
			//Arrange
			int income = 10000;
			int id = It.IsAny<int>();

			Mock<IAccountRepository> mockAccountRepository = new();
			mockAccountRepository.Setup(x => x.GetAccountInfo(id)).Returns(new Account());

			Mock<IMovementRepository> mockMovementRepository = new();
			mockMovementRepository.Setup(x => x.GetMovements(id)).Returns(new List<Movement>());

			AccountService sut = new(
				mockAccountRepository.Object,
				mockMovementRepository.Object
				);

			//Act
			IncomeResultDTO result = sut.AddMoney(income, id);

			//Assert
			Assert.True(result.ResultHasErrors);
			Assert.True(result.Error == IncomeErrorEnum.OverMaxIncome);
		}

		[Fact]
		public void DepositMoney_WhenAccountRepositoryNull_ReturnHasErrorsTrue()
		{
			//Arrange
			int income = 500;
			int id = It.IsAny<int>();

			Mock<IMovementRepository> mockMovementRepository = new();
			mockMovementRepository.Setup(x => x.GetMovements(id)).Returns(new List<Movement>());

			AccountService sut = new(
				null,
				mockMovementRepository.Object
				);

			//Act
			IncomeResultDTO result = sut.AddMoney(income, id);

			//Assert
			Assert.True(result.ResultHasErrors);
			Assert.True(result.Error == IncomeErrorEnum.AccountRepositoryError);
		}

		[Fact]
		public void DepositMoney_WhenMovementRepositoryNull_ReturnHasErrorsTrue()
		{
			//Arrange
			int income = 500;
			int id = It.IsAny<int>();

			Mock<IAccountRepository> mockAccountRepository = new();
			mockAccountRepository.Setup(x => x.GetAccountInfo(id)).Returns(new Account());

			AccountService sut = new(
				mockAccountRepository.Object,
				null
				);

			//Act
			IncomeResultDTO result = sut.AddMoney(income, id);

			//Assert
			Assert.True(result.ResultHasErrors);
			Assert.True(result.Error == IncomeErrorEnum.MovementRepositoryError);
		}

		[Fact]
		public void DepositMoney_WhenEntityNull_ReturnHasErrorsTrue()
		{
			//Arrange
			int income = 500;
			int id = It.IsAny<int>();

			Mock<IAccountRepository> mockAccountRepository = new();

			Mock<IMovementRepository> mockMovementRepository = new();
			mockMovementRepository.Setup(x => x.GetMovements(id)).Returns(new List<Movement>());

			AccountService sut = new(
				mockAccountRepository.Object,
				mockMovementRepository.Object
				);

			//Act
			IncomeResultDTO result = sut.AddMoney(income, id);

			//Assert
			Assert.True(result.ResultHasErrors);
			Assert.True(result.Error == IncomeErrorEnum.AccountNotFound);
		}

		[Fact]
		public void DepositMoney_WhenMovemetListNull_ReturnHasErrorsTrue()
		{
			//Arrange
			int income = 500;
			int id = It.IsAny<int>();

			Mock<IAccountRepository> mockAccountRepository = new();
			mockAccountRepository.Setup(x => x.GetAccountInfo(id)).Returns(new Account());

			Mock<IMovementRepository> mockMovementRepository = new();

			AccountService sut = new(
				mockAccountRepository.Object,
				mockMovementRepository.Object
				);

			//Act
			IncomeResultDTO result = sut.AddMoney(income, id);

			//Assert
			Assert.True(result.ResultHasErrors);
			Assert.True(result.Error == IncomeErrorEnum.MovementsNotFound);
		}

		#endregion
	}
}
