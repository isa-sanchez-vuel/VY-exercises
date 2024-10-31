using System.Text;
using OOPBankMultiuser.Application.Contracts.DTOs;
using OOPBankMultiuser.Business.Contracts;
using OOPBankMultiuser.Business.Contracts.DTOs;
using OOPBankMultiuser.Domain.Models;
using OOPBankMultiuser.XCutting.Enums;


namespace OOPBankMultiuser.Presentation.ConsoleUI
{
	internal class MainMenu
	{

		private readonly IAccountService _account;

		const int EXIT_OPTION = 0;
		const int MAX_LOGIN_ATTEMPTS = 5;

		int LoginAttempts;

		bool Exit;
		bool Logged;

		string UserAccount;
		string UserPin;

		int Option;

		AccountModel? Account = new();

		//BankModel Bank;

		public MainMenu(IAccountService accountService)
		{
			_account = accountService;
		}

		public void StartApplication()
		{
			LoginAttempts = 0;
			Exit = false;
			Logged = false;

			OpenLogin();
			if (Logged && Account != null)
			{
				OpenMenu();

				if (!Exit) StartApplication();
				else if (Exit) ExitApplication();
			}
		}

		void OpenLogin()
		{
			Console.Clear();
			while (!Logged)
			{
				Logged = false;

				if (LoginAttempts > 0 && LoginAttempts < MAX_LOGIN_ATTEMPTS - 1) MenuOutput.Print($"ERROR: Credentials not valid. Try again.\nAttempts left: {MAX_LOGIN_ATTEMPTS - LoginAttempts}.");
				else if (MAX_LOGIN_ATTEMPTS - LoginAttempts == 1) MenuOutput.Print("ERROR: Credentials not valid. This is your last attempt.");
				else if (LoginAttempts >= MAX_LOGIN_ATTEMPTS)
				{
					MenuOutput.Print("ERROR: You cannot try to login again. Go to the bank's office to recover your credentials.");
					return;
				}

				if (LoginAttempts < MAX_LOGIN_ATTEMPTS)
				{
					Console.WriteLine($"Welcome to our application. Please login with your account number and pin.");

					UserAccount = MenuInput.GetValidStringInputClear("\nAccount number:");

					UserPin = MenuInput.GetValidStringInputClear("\nPin:");

					//Account = Bank.CheckAccountLogin(UserAccount, UserPin);

					//if (_account.AccountIsLogged() != null) Logged = true;

					if (!Logged)
					{
						LoginAttempts++;
					}
				}
			}
		}

		void OpenMenu()
		{
			Console.Clear();
			MenuOutput.PrintMenu($"Welcome to your account, {_account.GetUserData()}! What do you wish to do?\n\n" +
				$"1 - Money Income\n" +
				$"2 - Money Outcome\n" +
				$"3 - See movement history\n" +
				$"4 - See Income history\n" +
				$"5 - See outcome history\n" +
				$"6 - See account money\n" +
				$"7 - See account data\n" +
				$"{EXIT_OPTION} - Logout\n\n" +
				$"Please choose an option:");
			Option = MenuInput.GetInputParsedInt();

			if (Option > 0 && Option < EXIT_OPTION) ManageOptions();
			else if (Option == EXIT_OPTION) Logged = false;
			else MenuOutput.PrintError("Invalid option. Try again.");

			if (!Exit)
			{
				if (Logged) OpenMenu();
				else if (!Logged) AskCloseApplication();
			}
		}

		void ManageOptions()
		{
			switch (Option)
			{
				case 1:
					HandleIncome();
					break;

				case 2:
					HandleOutcome();
					break;

				case 3:
					PrintAllMovements();
					break;

				case 4:
					PrintAllIncomes();
					break;

				case 5:
					PrintAllOutcomes();
					break;

				case 6:
					PrintAccountMoney();
					break;

				case 7:
					PrintAccountData();
					break;
			}
			AskCloseSession();
		}

		void AskCloseSession()
		{
			MenuOutput.PrintMenu($"Press {EXIT_OPTION} to close your session, press any other key if you wish to execute another action:");
			Option = MenuInput.GetInputParsedInt();
			if (Option == EXIT_OPTION) Logged = false;
		}

		void AskCloseApplication()
		{
			Logged = false;
			Console.Clear();
			Console.WriteLine("\nClosing session...\n");
			MenuOutput.PrintMenu($"Press {EXIT_OPTION} to close the application, press any other key if you wish to access other account:");
			Option = MenuInput.GetInputParsedInt();
			if (Option == EXIT_OPTION) Exit = true;
		}

		void ExitApplication()
		{
			Console.WriteLine("======================================" +
							"|| Closing application...            ||\n" +
							"|| Thank you for using our services! ||\n" +
							"======================================");
		}

		void Initialize()
		{
			Console.OutputEncoding = Encoding.UTF8;

			Bank = new("PoatatoBank S.L", "ES", "3000", "54", "4790");

			Bank.CreateAccount("1122334455", "1111", "Francisco Bezerra");
			Bank.CreateAccount("2233445566", "2222", "Manel Joaquin Terez");
			Bank.CreateAccount("3344556677", "3333", "Ithiar Lobillo");
			Bank.CreateAccount("4455667788", "4444", "Laura Bastión");
			Bank.CreateAccount("5566778899", "5555", "Pol Lorenzo Gutierrez");
			Bank.CreateAccount("6677889900", "6666", "Marina Vilanova");

			Bank.Initialize();
		}



		public void HandleIncome()
		{
			string message = "";
			decimal income = MenuInput.GetValidDecimalInput("Please write how much you want to deposit:");

			if(income != MenuInput.ERROR_VALUE)
			{
				IncomeResultDTO result = _account.DepositMoney(income);

				if (result.ResultHasErrors)
				{
					ManageIncomeErrors(result);
				}
				else message = $"{income}€ were added to your account.\nCurrent money: {result.TotalBalance:0.00}€";
			}
			else message = "ERROR: Input is not a valid decimal.";

			MenuOutput.Print(message);
		}

		private static string ManageIncomeErrors(IncomeResultDTO result)
		{
			return result.Error switch
			{
				IncomeErrorEnum.NegativeValue => "ERROR: Input can't be a negative value.",
				IncomeErrorEnum.OverMaxIncome => $"ERROR: Income can't be higher than {result.MaxIncomeAllowed:0.00}€.",
				_ => "",
			};
		}

		private static string ManageOutcomeErrors(IncomeResultDTO result)
		{
			return result.Error switch
			{
				IncomeErrorEnum.NegativeValue => "ERROR: Input can't be a negative value.",
				IncomeErrorEnum.OverMaxIncome => $"ERROR: Income can't be higher than {result.MaxIncomeAllowed:0.00}€.",
				_ => "",
			};
		}

		public void HandleOutcome()
		{
			string message = "";
			decimal outcome = MenuInput.GetValidDecimalInput("Please write how much you want to withdraw:");

			if (outcome != MenuInput.ERROR_VALUE)
			{
				OutcomeResultDTO result = _account.WithdrawMoney(outcome);

				if (result.ResultHasErrors)
				{
					ManageOutcomeErrors(result);
				}
				else message = $"{outcome}€ were withdrawn from your account.\nCurrent money: {result.TotalBalance:0.00}€";
			}
			else message = "ERROR: Input is not a valid decimal.";

			MenuOutput.Print(message);
		}

		private static string ManageOutcomeErrors(OutcomeResultDTO result)
		{
			return result.Error switch
			{
				OutcomeErrorEnum.NegativeValue => "ERROR: Input can't be a negative value.",
				OutcomeErrorEnum.OverMaxOutcome => $"ERROR: Outcome can't be higher than {result.MaxOutcomeAllowed:0.00}€.",
				OutcomeErrorEnum.OverTotalBalance => $"ERROR: Outcome can't be higher than total balance. Money available: {result.TotalBalance:0.00}€.",
				_ => "",
			};
		}

		void PrintAllMovements()
		{
			string message;

			MovementListDTO movementData = _account.GetAllMovements();
			if (movementData.Movements.Count == 0) message = "No movements registered.";
			else
			{
				message = "====== All Movements ======";

				foreach (MovementDTO movement in movementData.Movements)
				{
					message+=$"\n|| {movement.Timestamp:dd/MM/yyyy-hh:mm:ss} || {movement.Content:0.00}€";
				}
				message += $"================================\n              TOTAL | {movementData.TotalBalance:0.00}€";
			}

			MenuOutput.Print(message);
		}

		void PrintAllIncomes()
		{
			string message;

			MovementListDTO incomeData = _account.GetIncomes();
			if (incomeData.Movements.Count == 0) message = "No incomes registered.";
			else
			{
				message = "====== All Incomes ======";

				foreach (MovementDTO movement in incomeData.Movements)
				{
					message += $"\n|| {movement.Timestamp:dd/MM/yyyy-hh:mm:ss} || {movement.Content:0.00}€";
				}
				message += $"================================\n              TOTAL | {incomeData.TotalIncome:0.00}€";
			}

			MenuOutput.Print(message);
		}

		void PrintAllOutcomes()
		{
			string message;

			MovementListDTO incomeData = _account.GetIncomes();
			if (incomeData.Movements.Count == 0) message = "No outcomes registered.";
			else
			{
				message = "====== All Outcomes ======";

				foreach (MovementDTO movement in incomeData.Movements)
				{
					message += $"\n|| {movement.Timestamp:dd/MM/yyyy-hh:mm:ss} || {movement.Content:0.00}€";
				}
				message += $"================================\n              TOTAL | {incomeData.TotalOutcome:0.00}€";
			}

			MenuOutput.Print(message);
		}

		void PrintAccountMoney()
		{
			MenuOutput.Print($"Current money available {_account.GetBalance():0.00}€.");
		}

		void PrintAccountData()
		{
			string message = "";
			AccountResultDTO? result = _account?.GetAccountInfo();

			if (result != null)
			{
				message += "==================================================================\n";
				message += "||                                                              ||\n";
				message += $"||\tUser: {result.OwnerName}                                 ||\n";
				message += $"||\tIBAN: {result.Iban}                     ||\n";
				message += $"||\tAccount number: {result.AccountNumber}                              ||\n";
				message += $"||\tAccount pin: {result.Pin}                                       ||\n";
				message += $"||\tCurrent money available {result.TotalBalance:0.00}€.                         ||\n";
				message += "||                                                              ||\n";
				message += "==================================================================";
			}
			else message = "Account not found.";

		}


	} 
}
}