using Microsoft.AspNetCore.Mvc;
using OOPBankMultiuser.Application.Contracts;
using OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.DatabaseOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;

namespace OOPBankMultiuser.Presentation.WebAPIUI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly IAccountService _accountService;

		public AccountsController(IAccountService accountService)
		{
			_accountService = accountService;
		}


		// Create Account

		// POST: api/CreateAccount/John/0000/5.0
		[HttpPost("create")]

		public IActionResult CreateAccount([FromForm] CreateAccountDTO newAccount)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			CreateAccountResultDTO result = _accountService.CreateAccount(newAccount);

			if (!result.HasErrors) return Ok(result);
			else return StatusCode(StatusCodes.Status500InternalServerError, result);
		}


		// Update Account

		// POST: api/UpdateAccount/John/0000
		[HttpPut("update")]

		public IActionResult UpdateAccount([FromForm] UpdateAccountDTO modifiedAccount)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			UpdateAccountResultDTO result = _accountService.UpdateAccount(modifiedAccount);

			if (!result.HasErrors) return Ok(result);
			else return StatusCode(StatusCodes.Status500InternalServerError, result);
		}




		//Option 1: deposit money (outcomeValue)

		// POST: api/Deposit/5/5.0
		[HttpPut("deposit/{userId}/{incomeValue}")]

		public IActionResult DepositMoney(int userId, decimal incomeValue)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			IncomeResultDTO result = _accountService.DepositMoney(incomeValue, userId);

			if (!result.ResultHasErrors) return Ok(result);
			else return StatusCode(StatusCodes.Status500InternalServerError, result);
		}


		//Option 2: withdraw money (outcome)

		// POST: api/Withdraw/5/5.0
		[HttpPut("withdraw/{userId}/{outcomeValue}")]
		public IActionResult WithdrawMoney(int userId, decimal outcomeValue)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			OutcomeResultDTO result = _accountService.WithdrawMoney(outcomeValue, userId);

			if (!result.ResultHasErrors) return Ok(result);
			else return StatusCode(StatusCodes.Status500InternalServerError, result);
		}


		//Option 3: get movement list

		// GET: api/Movements/5
		[HttpGet("movements/{userId}")]
		public IActionResult GetMovementList(int userId)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			MovementListDTO movementList = _accountService.GetAllMovements(userId);

			if (movementList == null || movementList.Movements.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, movementList);

			return Ok(movementList);
		}


		//Option 4: get incomes list

		// GET: api/Incomes/5
		[HttpGet("incomes/{userId}")]
		public IActionResult GetIncomeList(int userId)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			MovementListDTO incomeList = _accountService.GetIncomes(userId);

			if (incomeList == null || incomeList.Movements.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, incomeList);

			return Ok(incomeList);
		}


		//Option 5: get outcomes list

		// GET: api/Outcomes/5
		[HttpGet("outcomes/{userId}")]
		public IActionResult GetOutcomeList(int userId)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			MovementListDTO outcomeList = _accountService.GetOutcomes(userId);

			if (outcomeList == null || outcomeList.Movements.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, outcomeList);

			return Ok(outcomeList);
		}


		//Option 6: get balanceDto balance

		// GET: api/InitialBalance/5
		[HttpGet("totalBalance/{userId}")]
		public IActionResult GetAccountBalance(int userId)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			BalanceDTO? balanceDto = _accountService.GetBalance(userId);

			if (balanceDto == null) return StatusCode(StatusCodes.Status500InternalServerError, "Account doesn't exist.");

			return Ok(balanceDto);
		}


		//Option 7: get balanceDto info

		// GET: api/AccountInfo/5
		[HttpGet("accountInfo/{userId}")]
		public IActionResult GetAccountInfo(int userId)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			AccountDTO? accountDto = _accountService.GetAccountInfo(userId);

			if (accountDto == null) return StatusCode(StatusCodes.Status500InternalServerError, "Account doesn't exist.");

			return Ok(accountDto);
		}


	}
}

