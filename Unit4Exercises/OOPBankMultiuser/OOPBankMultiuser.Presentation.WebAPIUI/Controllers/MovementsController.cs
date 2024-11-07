using Microsoft.AspNetCore.Mvc;
using OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;
using OOPBankMultiuser.Application.Contracts;

namespace OOPBankMultiuser.Presentation.WebAPIUI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MovementsController : ControllerBase
	{
		private readonly IAccountService _accountService;

		public MovementsController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		#region Option 1: deposit money (outcomeValue)

		// POST: api/Deposit/5/5.0
		[HttpPatch("DepositMoney")]

		public IActionResult DepositMoney([FromForm] int accountId, [FromForm] decimal incomeValue)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			IncomeResultDTO result = _accountService.DepositMoney(incomeValue, accountId);

			if (!result.ResultHasErrors) return Ok(result);
			else return StatusCode(StatusCodes.Status500InternalServerError, result);
		}
		#endregion

		#region Option 2: withdraw money (outcome)

		// POST: api/Withdraw/5/5.0
		[HttpPatch("WithdrawMoney")]
		public IActionResult WithdrawMoney([FromForm] int accountId, [FromForm] decimal outcomeValue)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			OutcomeResultDTO result = _accountService.WithdrawMoney(outcomeValue, accountId);

			if (!result.ResultHasErrors) return Ok(result);
			else return StatusCode(StatusCodes.Status500InternalServerError, result);
		}
		#endregion

		#region Option 3: get movement list

		// GET: api/Movements/5
		[HttpGet("GetAllMovements")]
		public IActionResult GetMovementList([FromForm] int accountId)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			MovementListDTO movementList = _accountService.GetAllMovements(accountId);

			if (movementList == null || movementList.Movements.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, movementList);

			return Ok(movementList);
		}
		#endregion

		#region Option 4: get incomes list

		// GET: api/Incomes/5
		[HttpGet("GetAllIncomes")]
		public IActionResult GetIncomeList([FromForm] int accountId)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			MovementListDTO incomeList = _accountService.GetIncomes(accountId);

			if (incomeList == null || incomeList.Movements.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, incomeList);

			return Ok(incomeList);
		}
		#endregion

		#region Option 5: get outcomes list

		// GET: api/Outcomes/5
		[HttpGet("GetAllOutcomes")]
		public IActionResult GetOutcomeList([FromForm] int accountId)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			MovementListDTO outcomeList = _accountService.GetOutcomes(accountId);

			if (outcomeList == null || outcomeList.Movements.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, outcomeList);

			return Ok(outcomeList);
		}
		#endregion

		#region Option 6: get account balance

		// GET: api/TotalBalance/5
		[HttpGet("GetTotalBalance")]
		public IActionResult GetAccountBalance([FromForm] int accountId)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			BalanceDTO? balanceDto = _accountService.GetBalance(accountId);

			if (balanceDto == null) return StatusCode(StatusCodes.Status500InternalServerError, "Account doesn't exist.");

			return Ok(balanceDto);
		}
		#endregion
	}
}
