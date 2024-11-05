using Microsoft.AspNetCore.Mvc;
using OOPBankMultiuser.Application.Contracts;
using OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations;
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


		//Option 1: deposit money (outcomeValue)
		// GET: api/Deposit/5
		public ActionResult DepositMoney(decimal incomeValue, int id)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			IncomeResultDTO result = _accountService.DepositMoney(incomeValue, id);
			string message = $"{incomeValue:0.00}€ were added to your account.";

			if (!result.ResultHasErrors) return Ok(message);
			else return StatusCode(StatusCodes.Status500InternalServerError, "Error during transaction.");
		}


		//Option 2: withdraw money (outcome)
		// GET: api/Withdraw/5
		public ActionResult WithdrawMoney(decimal outcomeValue, int id)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			IncomeResultDTO result = _accountService.DepositMoney(outcomeValue, id);
			string message = $"{outcomeValue:0.00}€ were withdrawed from your account.";

			if (!result.ResultHasErrors) return Ok(message); 
			else return StatusCode(StatusCodes.Status500InternalServerError, "Error during transaction.");

			
		}

		//Option 3: get movement list
		// GET: api/Movements/5
		[HttpGet("{userId}")]
		public ActionResult GetMovementList(int id)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			MovementListDTO movementList = _accountService.GetAllMovements(id);

			if (movementList == null || movementList.Movements.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, "No movements registered.");

			string message = "====== All movements ======";

			foreach (MovementDTO movement in movementList.Movements)
			{
				message += $"\n|| {movement.Timestamp:dd/MM/yyyy-hh:mm:ss} || {movement.Content:0.00}€";
			}
			message += $"\n================================\n              TOTAL | {movementList.TotalIncome:0.00}€";

			return Ok(message);
		}

		//Option 4: get incomes list

		// GET: api/Incomes/5
		[HttpGet("{userId}")]
		public ActionResult GetIncomeList(int id)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			MovementListDTO incomeList = _accountService.GetIncomes(id);

			if (incomeList == null || incomeList.Movements.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, "No incomes registered.");

			string message = "====== All Incomes ======";

			foreach (MovementDTO movement in incomeList.Movements)
			{
				message += $"\n|| {movement.Timestamp:dd/MM/yyyy-hh:mm:ss} || {movement.Content:0.00}€";
			}
			message += $"\n================================\n              TOTAL | {incomeList.TotalIncome:0.00}€";

			return Ok(message);
		}


		//Option 5: get outcomes list

		// GET: api/Outcomes/5
		[HttpGet("{userId}")]
		public ActionResult GetOutcomeList(int id)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			MovementListDTO outcomeList = _accountService.GetOutcomes(id);

			if (outcomeList == null || outcomeList.Movements.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, "No outcomes registered.");

			string message = "====== All Outcomes ======";

			foreach (MovementDTO movement in outcomeList.Movements)
			{
				message += $"\n|| {movement.Timestamp:dd/MM/yyyy-hh:mm:ss} || {movement.Content:0.00}€";
			}
			message += $"\n================================\n              TOTAL | {outcomeList.TotalIncome:0.00}€";

			return Ok(message);
		}



		//Option 6: get accountDto balance

		// GET: api/TotalBalance/5
		[HttpGet("{userId}")]
		public ActionResult GetAccountBalance(int id)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			AccountDTO accountDto = _accountService.GetAccountInfo(id);

			if (accountDto == null) return StatusCode(StatusCodes.Status500InternalServerError, "Account doesn't exist.");

			string message = $"{accountDto.TotalBalance:0.00} €";

			return Ok(message);
		}


		//Option 7: get accountDto info

		// GET: api/AccountInfo/5
		[HttpGet("{id}")]
		public ActionResult GetAccountInfo(int id)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			AccountDTO accountDto = _accountService.GetAccountInfo(id);

			if (accountDto == null) return StatusCode(StatusCodes.Status500InternalServerError, "Account doesn't exist.");

			string message = $"{accountDto.AccountNumber} | {accountDto.OwnerName}";

			return Ok(message);
		}

	}
}

/*
  
  
 
//GET: api/Accounts
		[HttpGet]
 public ActionResult GetAccounts()
		{
			if (_accountService == null) return NotFound();
			if (_context.Accounts == null) return NotFound();


			return Ok();
		}


		// PUT: api/Accounts/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutAccount(int id, Account accountDto)
		{
			if (id != accountDto.IdNumber)
			{
				return BadRequest();
			}

			_context.Entry(accountDto).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AccountExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Accounts
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Account>> PostAccount(Account accountDto)
		{
		  if (_context.Accounts == null)
		  {
			  return Problem("Entity set 'OOPBankMultiuserContext.Accounts'  is null.");
		  }
			_context.Accounts.Add(accountDto);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetAccount", new { id = accountDto.IdNumber }, accountDto);
		}

		//Verify if accountDto exists
		// DELETE: api/Accounts/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAccount(int id)
		{
			if (_context.Accounts == null)
			{
				return NotFound();
			}
			var accountDto = await _context.Accounts.FindAsync(id);
			if (accountDto == null)
			{
				return NotFound();
			}

			_context.Accounts.Remove(accountDto);
			await _context.SaveChangesAsync();

			return NoContent();
		}*/