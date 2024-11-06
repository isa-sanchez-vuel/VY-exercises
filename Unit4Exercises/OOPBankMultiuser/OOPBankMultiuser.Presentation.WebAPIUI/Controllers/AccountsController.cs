using Microsoft.AspNetCore.Mvc;
using OOPBankMultiuser.Application.Contracts;
using OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.BankOperations;
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
		
		public IActionResult AddAccount([FromForm] CreateAccountDTO newAccount)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			CreateAccountResultDTO result = _accountService.CreateAccount(newAccount);

			if (!result.HasErrors) return Ok(result);
			else return StatusCode(StatusCodes.Status500InternalServerError, "Error during transaction.");
		}

		//Option 1: deposit money (outcomeValue)

		// POST: api/Deposit/5/5.0
		[HttpPost("deposit/{userId}/{incomeValue}")]
		
		public IActionResult DepositMoney(int userId, decimal incomeValue)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			IncomeResultDTO result = _accountService.DepositMoney(incomeValue, userId);

			if (!result.ResultHasErrors) return Ok(result);
			else return StatusCode(StatusCodes.Status500InternalServerError, "Error during transaction.");
		}


		//Option 2: withdraw money (outcome)

		// POST: api/Withdraw/5/5.0
		[HttpPost("withdraw/{userId}/{outcomeValue}")]
		public IActionResult WithdrawMoney(int userId, decimal outcomeValue)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			OutcomeResultDTO result = _accountService.WithdrawMoney(outcomeValue, userId);

			if (!result.ResultHasErrors) return Ok(result); 
			else return StatusCode(StatusCodes.Status500InternalServerError, "Error during transaction.");
		}


		//Option 3: get movement list

		// GET: api/Movements/5
		[HttpGet("movements/{userId}")]
		public IActionResult GetMovementList(int userId)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			MovementListDTO movementList = _accountService.GetAllMovements(userId);

			if (movementList == null || movementList.Movements.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, "No movements registered.");

			return Ok(movementList);
		}


		//Option 4: get incomes list

		// GET: api/Incomes/5
		[HttpGet("incomes/{userId}")]
		public IActionResult GetIncomeList(int userId)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			MovementListDTO incomeList = _accountService.GetIncomes(userId);

			if (incomeList == null || incomeList.Movements.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, "No incomes registered.");

			return Ok(incomeList);
		}


		//Option 5: get outcomes list

		// GET: api/Outcomes/5
		[HttpGet("outcomes/{userId}")]
		public IActionResult GetOutcomeList(int userId)
		{
			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

			MovementListDTO outcomeList = _accountService.GetOutcomes(userId);

			if (outcomeList == null || outcomeList.Movements.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, "No outcomes registered.");

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
		[HttpPut("{userId}")]
		public async Task<IActionResult> PutAccount(int userId, Account balanceDto)
		{
			if (userId != balanceDto.IdNumber)
			{
				return BadRequest();
			}

			_context.Entry(balanceDto).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AccountExists(userId))
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
		public async Task<ActionResult<Account>> PostAccount(Account balanceDto)
		{
		  if (_context.Accounts == null)
		  {
			  return Problem("Entity set 'OOPBankMultiuserContext.Accounts'  is null.");
		  }
			_context.Accounts.Add(balanceDto);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetAccount", new { userId = balanceDto.IdNumber }, balanceDto);
		}

		//Verify if balanceDto exists
		// DELETE: api/Accounts/5
		[HttpDelete("{userId}")]
		public async Task<IActionResult> DeleteAccount(int userId)
		{
			if (_context.Accounts == null)
			{
				return NotFound();
			}
			var balanceDto = await _context.Accounts.FindAsync(userId);
			if (balanceDto == null)
			{
				return NotFound();
			}

			_context.Accounts.Remove(balanceDto);
			await _context.SaveChangesAsync();

			return NoContent();
		}*/