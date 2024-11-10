using Microsoft.AspNetCore.Mvc;
using OOPBankMultiuser.Application.Contracts;
using OOPBankMultiuser.Application.Contracts.DTOs.DatabaseOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;

namespace OOPBankMultiuser.Presentation.WebAPIUI.Controllers.V1
{
	[Route("v{version:apiVersion}/[controller]")]
	[ApiVersion("1.0")]
	[ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #region Create Account

        // POST: api/CreateAccount/John/0000/5.0
        [HttpPost("Create")]

        public IActionResult CreateAccount([FromForm] CreateAccountDTO newAccount)
        {
            if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

            CreateAccountResultDTO result = _accountService.CreateAccount(newAccount);

            if (!result.HasErrors) return Ok(result);
            else return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
        #endregion

        #region Update Account

        // POST: api/UpdateAccount/John/0000
        [HttpPut("Update")]

        public IActionResult UpdateAccount([FromForm] UpdateAccountDTO modifiedAccount)
        {
            if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

            UpdateAccountResultDTO result = _accountService.UpdateAccount(modifiedAccount);

            if (!result.HasErrors) return Ok(result);
            else return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
        #endregion

        #region Delete Account

        // POST: api/DeleteAccount/5
        [HttpDelete("Delete")]

        public IActionResult DeleteAccount([FromQuery] int accountId)
        {
            if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

            DeleteAccountResultDTO result = _accountService.DeleteAccount(accountId);

            if (!result.HasErrors) return Ok(result);
            else return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
        #endregion

        #region Get account info by Id

        // GET: api/GetAccount/5
        [HttpGet("GetById")]
        public IActionResult GetAccountInfo([FromQuery] int accountId)
        {
            if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't connect with account service.");

            AccountDTO? accountDto = _accountService.GetAccountInfo(accountId);

            if (accountDto == null) return StatusCode(StatusCodes.Status500InternalServerError, "Account doesn't exist.");

            return Ok(accountDto);
        }
        #endregion

    }
}

