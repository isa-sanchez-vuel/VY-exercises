using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OOPBankMultiuser.Application.Contracts;
using OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.DatabaseOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;

namespace OOPBankMultiuser.Presentation.WebAPIUI.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly IAccountService _accountService;

		public AccountsController(IAccountService accountService)
		{
			_accountService = accountService;
		}


		/*/ Create Account

		// POST: api/CreateAccount/John/0000/5.0
		[HttpPost("create")]
		
		public IActionResult CreateAccount([FromForm] CreateAccountDTO newAccount)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			CreateAccountResultDTO result = _accountService.CreateAccount(newAccount);

			if (!result.HasErrors) return Ok(result);
			else return StatusCode(StatusCodes.Status500InternalServerError, "Error during transaction.");
		}*/


		// Update Account

		// POST: api/UpdateAccount/John/0000
		[HttpPut("update")]
		
		public IActionResult UpdateAccount([FromForm] UpdateAccountDTO modifiedAccount)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			UpdateAccountResultDTO result = _accountService.UpdateAccount(modifiedAccount);

			if (!result.HasErrors) return Ok(result);
			else return StatusCode(StatusCodes.Status500InternalServerError, "Error during transaction.");
		}


		//Option 1: deposit money (outcomeValue)

		[Authorize()]
		// POST: api/Deposit/5/5.0
		[HttpPut("deposit/{incomeValue}")]
		
		public IActionResult DepositMoney(decimal incomeValue)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			IncomeResultDTO result = _accountService.DepositMoney(incomeValue);

			if (!result.ResultHasErrors) return Ok(result);
			else return StatusCode(StatusCodes.Status500InternalServerError, "Error during transaction.");
		}


		//Option 2: withdraw money (outcome)

		[Authorize]
		// POST: api/Withdraw/5/5.0
		[HttpPut("withdraw/{outcomeValue}")]
		public IActionResult WithdrawMoney(decimal outcomeValue)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			OutcomeResultDTO result = _accountService.WithdrawMoney(outcomeValue);

			if (!result.ResultHasErrors) return Ok(result); 
			else return StatusCode(StatusCodes.Status500InternalServerError, "Error during transaction.");
		}


		//Option 3: get movement list

		[Authorize]
		// GET: api/Movements/5
		[HttpGet("movements")]
		public IActionResult GetMovementList()
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			MovementListDTO movementList = _accountService.GetAllMovements();

			if (movementList == null || movementList.Movements.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, "No movements registered.");

			return Ok(movementList);
		}


		//Option 4: get incomes list

		[Authorize]
		// GET: api/Incomes/5
		[HttpGet("incomes")]
		public IActionResult GetIncomeList()
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			MovementListDTO incomeList = _accountService.GetIncomes();

			if (incomeList == null || incomeList.Movements.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, "No incomes registered.");

			return Ok(incomeList);
		}


		//Option 5: get outcomes list

		[Authorize]
		// GET: api/Outcomes/5
		[HttpGet("outcomes")]
		public IActionResult GetOutcomeList()
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			MovementListDTO outcomeList = _accountService.GetOutcomes();

			if (outcomeList == null || outcomeList.Movements.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, "No outcomes registered.");

			return Ok(outcomeList);
		}


		//Option 6: get balanceDto balance

		[Authorize]
		// GET: api/InitialBalance/5
		[HttpGet("totalBalance")]
		public IActionResult GetAccountBalance()
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			BalanceDTO? balanceDto = _accountService.GetBalance();

			if (balanceDto == null) return StatusCode(StatusCodes.Status500InternalServerError, "Account doesn't exist.");
			 
			return Ok(balanceDto);
		}


		//Option 7: get balanceDto info

		[Authorize]
		// GET: api/AccountInfo/5
		[HttpGet("accountInfo")]
		public IActionResult GetAccountInfo()
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			AccountDTO? accountDto = _accountService.GetAccountInfo();

			if (accountDto == null) return StatusCode(StatusCodes.Status500InternalServerError, "Account doesn't exist.");

			return Ok(accountDto);
		}


	}
}

