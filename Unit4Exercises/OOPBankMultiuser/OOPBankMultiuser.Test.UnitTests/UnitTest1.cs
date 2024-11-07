using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations;
using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.Infrastructure.Contracts.Entities;
using Stripe;
using Moq;

namespace OOPBankMultiuser.Test.UnitTests
{
	public class UnitTest1
	{
		int accountId = 1;
		/*
		[Fact]
		public void Test1()
		{
			//Arrange
			Mock<IAccountRepository> mockAccountRepository = new();
			mockAccountRepository.Setup(x => x.GetAccountInfo(accountId)).Returns(new Account{
			Balance = 0
			});
			mockAccountRepository<IMovementRepository> mockMovementRepository = new();
			mockMovementRepository.Setup(XmlReaderUtilities => XmlReaderUtilities.GetMovements()).Returns(new List<MovementEntity>);

			AccountService sut = new(
				mockAccountRepository.Object,
				mockMovementRepository.Object
				);

			//Act
			IncomeResultDTO result = StringUtilities.AddMoney(1);

			//Assert
			Assert.False(IAsyncResult.ResultHasErrors);

			
		}*/
	}
}