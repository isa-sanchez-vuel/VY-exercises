using Microsoft.AspNetCore.Mvc;
using OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;
using OOPBankMultiuser.Application.Contracts;
using OOPBankMultiuser.XCutting.Enums;

namespace OOPBankMultiuser.Presentation.WebAPIUI.Controllers.V1
{
    [Route("v{version:apiVersion}/[controller]")]
	[ApiVersion("1.0")]
	[ApiController]
    public class MovementsController : ControllerBase
    {
        private readonly IAccountService? _accountService;

        public MovementsController(IAccountService? accountService)
        {
            _accountService = accountService;
        }

        #region Option 1: deposit money (outcomeValue)

        // POST: api/Deposit/5/5.0
        [HttpPatch("DepositMoney")]

        public IActionResult DepositMoney([FromQuery] int accountId, [FromQuery] decimal incomeValue)
        {
            if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

            IncomeResultDTO result = _accountService.AddMoney(incomeValue, accountId);

            if (!result.ResultHasErrors) return Ok(result);
            else return result.Error switch
            {
                IncomeErrorEnum.AccountRepositoryError => StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account repository."),
                IncomeErrorEnum.MovementRepositoryError => StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with movement repository."),
                IncomeErrorEnum.AccountNotFound => StatusCode(StatusCodes.Status404NotFound, "Account not found."),
                IncomeErrorEnum.MovementsNotFound => StatusCode(StatusCodes.Status404NotFound, "Movement list not found."),
                IncomeErrorEnum.NegativeValue => StatusCode(StatusCodes.Status406NotAcceptable, "Input can't be a negative value."),
                IncomeErrorEnum.OverMaxIncome => StatusCode(StatusCodes.Status406NotAcceptable, $"Income can't be higher than {result.MaxIncomeAllowed:0.00}€."),
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Error unknown."),
            };

        }
        #endregion

        #region Option 2: withdraw money (outcome)

        // POST: api/Withdraw/5/5.0
        [HttpPatch("WithdrawMoney")]
        public IActionResult WithdrawMoney([FromQuery] int accountId, [FromQuery] decimal outcomeValue)
        {
            if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

            OutcomeResultDTO result = _accountService.SubtractMoney(outcomeValue, accountId);

            if (!result.ResultHasErrors) return Ok(result);
            else return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
        #endregion

        #region Option 3: get movement list

        // GET: api/Movements/5
        [HttpGet("GetAllMovements")]
        public IActionResult GetMovementList([FromQuery] int accountId)
        {
            if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

            MovementListDTO movementList = _accountService.GetAllMovements(accountId);

            if (movementList == null || movementList.Movements.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, movementList);

            return Ok(movementList.Movements);
        }
        #endregion

        #region Option 4: get incomes list

        // GET: api/Incomes/5
        [HttpGet("GetAllIncomes")]
        public IActionResult GetIncomeList([FromQuery] int accountId)
        {
            if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

            MovementListDTO incomeList = _accountService.GetIncomes(accountId);

            if (incomeList == null || incomeList.Movements.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, incomeList);

            return Ok(incomeList.Movements);
        }
        #endregion

        #region Option 5: get outcomes list

        // GET: api/Outcomes/5
        [HttpGet("GetAllOutcomes")]
        public IActionResult GetOutcomeList([FromQuery] int accountId)
        {
            if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

            MovementListDTO outcomeList = _accountService.GetOutcomes(accountId);

            if (outcomeList == null || outcomeList.Movements.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, outcomeList);

            return Ok(outcomeList.Movements);
        }
        #endregion

        #region Option 6: get account balance

        // GET: api/TotalBalance/5
        [HttpGet("GetTotalBalance")]
        public IActionResult GetAccountBalance([FromQuery] int accountId)
        {
            if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

            BalanceDTO? balanceDto = _accountService.GetBalance(accountId);

            if (balanceDto == null) return StatusCode(StatusCodes.Status500InternalServerError, "Account doesn't exist.");

            return Ok(balanceDto);
        }
        #endregion
    }
}
