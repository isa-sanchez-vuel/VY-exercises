using Microsoft.AspNetCore.Mvc;
using Moq;
using OOPBankMultiuser.Application.Contracts;
using OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations;
using OOPBankMultiuser.XCutting.Enums;
using Microsoft.AspNetCore.Http;
using OOPBankMultiuser.Domain.Models;
using OOPBankMultiuser.Presentation.WebAPIUI.Controllers.V1;

namespace OOPBankMultiuser.Testing.UnitTesting.Presentation
{
    public class MovementsControllerTests
	{
		#region DepositMoney

		[Fact]
		public void DepositMoney_IfResultHasNoErrors_ReturnIsOk()
		{
			//Arrange
			decimal income = 500;
			int id = It.IsAny<int>();

			Mock<IAccountService> mockAccountService = new();
			mockAccountService.Setup(x => x.AddMoney(income, id)).Returns(new IncomeResultDTO()
			{
				ResultHasErrors = false,
			});

			MovementsController sut = new(
				mockAccountService.Object
				);

			//Act
			IActionResult result = sut.DepositMoney(id, income);

			//Assert
			Assert.IsType<OkObjectResult>(result);

		}


		[Fact]
		public void DepositMoney_IfServiceisNull_ReturnTrue()
		{
			//Arrange
			decimal income = 500;
			int id = It.IsAny<int>();

			MovementsController sut = new(
				null
				);

			//Act
			IActionResult result = sut.DepositMoney(id, income);

			//Assert
			Assert.IsType<ObjectResult>(result);
			Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
			Assert.Equal("Couldn't connect with account service.", ((ObjectResult)result).Value);
		}

		[Fact]
		public void DepositMoney_IfAccountRepositoryNull_ReturnTrue()
		{
			//Arrange
			decimal income = 500;
			int id = It.IsAny<int>();

			Mock<IAccountService> mockAccountService = new();
			mockAccountService.Setup(x => x.AddMoney(income, id)).Returns(new IncomeResultDTO()
			{
				ResultHasErrors = true,
				Error = IncomeErrorEnum.AccountRepositoryError,
			});

			MovementsController sut = new(
				mockAccountService.Object
				);

			//Act
			IActionResult result = sut.DepositMoney(id, income);

			//Assert
			Assert.IsType<ObjectResult>(result);
			Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
			Assert.Equal("Couldn't connect with account repository.", ((ObjectResult)result).Value);
		}


		[Fact]
		public void DepositMoney_IfMovementRepositoryNull_ReturnTrue()
		{
			//Arrange
			decimal income = 500;
			int id = It.IsAny<int>();

			Mock<IAccountService> mockAccountService = new();
			mockAccountService.Setup(x => x.AddMoney(income, id)).Returns(new IncomeResultDTO()
			{
				ResultHasErrors = true,
				Error = IncomeErrorEnum.MovementRepositoryError,
			});

			MovementsController sut = new(
				mockAccountService.Object
				);

			//Act
			IActionResult result = sut.DepositMoney(id, income);

			//Assert
			Assert.IsType<ObjectResult>(result);
			Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
			Assert.Equal("Couldn't connect with movement repository.", ((ObjectResult)result).Value);
		}


		[Fact]
		public void DepositMoney_IfAccountNull_ReturnTrue()
		{
			//Arrange
			decimal income = 500;
			int id = It.IsAny<int>();

			Mock<IAccountService> mockAccountService = new();
			mockAccountService.Setup(x => x.AddMoney(income, id)).Returns(new IncomeResultDTO()
			{
				ResultHasErrors = true,
				Error = IncomeErrorEnum.AccountNotFound,
			});

			MovementsController sut = new(
				mockAccountService.Object
				);

			//Act
			IActionResult result = sut.DepositMoney(id, income);

			//Assert
			Assert.IsType<ObjectResult>(result);
			Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
			Assert.Equal("Account not found.", ((ObjectResult)result).Value);
		}


		[Fact]
		public void DepositMoney_IfMovementsNull_ReturnTrue()
		{
			//Arrange
			decimal income = 500;
			int id = It.IsAny<int>();

			Mock<IAccountService> mockAccountService = new();
			mockAccountService.Setup(x => x.AddMoney(income, id)).Returns(new IncomeResultDTO()
			{
				ResultHasErrors = true,
				Error = IncomeErrorEnum.MovementsNotFound,
			});

			MovementsController sut = new(
				mockAccountService.Object
				);

			//Act
			IActionResult result = sut.DepositMoney(id, income);

			//Assert
			Assert.IsType<ObjectResult>(result);
			Assert.Equal(StatusCodes.Status404NotFound, ((ObjectResult)result).StatusCode);
			Assert.Equal("Movement list not found.", ((ObjectResult)result).Value);
		}


		[Fact]
		public void DepositMoney_IfInputNegative_ReturnTrue()
		{
			//Arrange
			decimal income = 500;
			int id = It.IsAny<int>();

			Mock<IAccountService> mockAccountService = new();
			mockAccountService.Setup(x => x.AddMoney(income, id)).Returns(new IncomeResultDTO()
			{
				ResultHasErrors = true,
				Error = IncomeErrorEnum.NegativeValue,
			});

			MovementsController sut = new(
				mockAccountService.Object
				);

			//Act
			IActionResult result = sut.DepositMoney(id, income);

			//Assert
			Assert.IsType<ObjectResult>(result);
			Assert.Equal(StatusCodes.Status406NotAcceptable, ((ObjectResult)result).StatusCode);
			Assert.Equal("Input can't be a negative value.", ((ObjectResult)result).Value);
		}


		[Fact]
		public void DepositMoney_IfInputOverMax_ReturnTrue()
		{
			//Arrange
			decimal income = 10000;
			int id = It.IsAny<int>();

			Mock<IAccountService> mockAccountService = new();
			mockAccountService.Setup(x => x.AddMoney(income, id)).Returns(new IncomeResultDTO()
			{
				ResultHasErrors = true,
				Error = IncomeErrorEnum.OverMaxIncome,
				MaxIncomeAllowed = AccountModel.MAX_INCOME,
			});

			MovementsController sut = new(
				mockAccountService.Object
				);

			//Act
			IActionResult result = sut.DepositMoney(id, income);

			//Assert
			Assert.IsType<ObjectResult>(result);
			Assert.Equal(StatusCodes.Status406NotAcceptable, ((ObjectResult)result).StatusCode);
			Assert.Equal($"Income can't be higher than {AccountModel.MAX_INCOME:0.00}€.", ((ObjectResult)result).Value);
		}

		#endregion
	}
}
