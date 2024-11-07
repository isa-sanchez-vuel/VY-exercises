using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OOPBankMultiuser.Application.Contracts;
using OOPBankMultiuser.Application.Contracts.DTOs.BankOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.DatabaseOperations;
using OOPBankMultiuser.Application.Impl;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace OOPBankMultiuser.Presentation.WebAPIUI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private IConfiguration _config;
		private IAccountService _accountService;
		public LoginController(IConfiguration config, IAccountService accountService)
		{
			_config = config;
			_accountService = accountService;
		}

		[HttpPost]
		public IActionResult Post([FromForm] LoginDTO loginRequest)
		{
			//your logic for login process

			if (_accountService == null) return StatusCode(StatusCodes.Status500InternalServerError, _accountService);

			LoginResultDTO result = _accountService.LoginAccount(loginRequest);

			if (result.HasErrors) return StatusCode(StatusCodes.Status500InternalServerError, result);

			//If login usrename and password are correct then proceed to generate token

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
				var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

				var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
				  _config["Jwt:Issuer"],
				  null,
				  expires: DateTime.Now.AddMinutes(120),
				  signingCredentials: credentials);

				var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
			
			return Ok(token);
		}
	}
}

