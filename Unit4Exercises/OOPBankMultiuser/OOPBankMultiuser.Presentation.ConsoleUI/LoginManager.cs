
using OOPBankMultiuser.Application.Contracts;
using OOPBankMultiuser.Application.Contracts.DTOs.BankOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;
using OOPBankMultiuser.XCutting.Enums;
using System.Reflection.Metadata.Ecma335;

namespace OOPBankMultiuser.Presentation.ConsoleUI
{
	internal class LoginManager
	{
		const int MAX_LOGIN_ATTEMPTS = 5;

		public bool Logged;

		string? UserAccount;
		string? UserPin;

		int LoginAttempts;


		public AccountDTO? StartLogin(IBankService bank)
		{
			LoginResultDTO? loginResult = null;
			LoginAttempts = 0;
			MenuOutput.ClearConsole();
			while (!Logged)
			{
				Logged = false;
				CheckAttempts();

				if (LoginAttempts < MAX_LOGIN_ATTEMPTS)
				{
					MenuOutput.Print($"Welcome to our application. Please login with your account number and pin.");

					UserAccount = MenuInput.GetValidStringInput("\nAccount number:");
					if (UserAccount == null)
					{
						MenuOutput.ClearConsole();
						MenuOutput.PrintError("Invalid input.");
						continue;
					}

					UserPin = MenuInput.GetValidStringInput("\nPin:");
					if (UserPin == null)
					{
						MenuOutput.ClearConsole();
						MenuOutput.PrintError("Invalid input.");
						continue;
					}
					
					loginResult = bank?.LoginAccount(UserAccount, UserPin);

					if (loginResult != null) 
					{
						if (loginResult.HasErrors) ManageLoginErrors(loginResult);
						else Logged = true;
					}
					else MenuOutput.PrintError("Something went wrong while attempting login.");

					if (!Logged)
					{
						LoginAttempts++;
					}
				}
			}
			return loginResult?.Account;
		}


		private void CheckAttempts()
		{
			if (LoginAttempts > 0 && LoginAttempts < MAX_LOGIN_ATTEMPTS - 1) MenuOutput.PrintError($"Credentials not valid. Try again.\nAttempts left: {MAX_LOGIN_ATTEMPTS - LoginAttempts}.");
			else if (MAX_LOGIN_ATTEMPTS - LoginAttempts == 1) MenuOutput.PrintError("Credentials not valid. This is your last attempt.");
			else if (LoginAttempts >= MAX_LOGIN_ATTEMPTS) MenuOutput.PrintError("You cannot try to login again. Go to the bank's office to recover your credentials.");
		}


		static void ManageLoginErrors(LoginResultDTO result)
		{
			string message = "";
			switch (result.Error)
			{
				case LoginErrorEnum.NumberFormat:
					message = "Account number has wrong format. It must contain only numerical values.";
					break;
				case LoginErrorEnum.PinFormat:
					message = "Pin has wrong format. It must contain only numerical values.";
					break;
				case LoginErrorEnum.NumberLength:
					message = $"Account number length is incorrect. It must have {result.AccountNumberLength} digits.";
					break;
				case LoginErrorEnum.PinLenght:
					message = $"Pin number length is incorrect. It must have {result.PinLength} digits.";
					break;
			}
			MenuOutput.ClearConsole();
			MenuOutput.PrintError(message);
		}
	}
}
