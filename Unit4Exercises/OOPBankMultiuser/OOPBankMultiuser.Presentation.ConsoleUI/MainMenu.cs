using System.Text;
using OOPBankMultiuser.Application.Contracts;
using OOPBankMultiuser.Domain.Models;
using OOPBankMultiuser.Infrastructure.Contracts;
using OOPBankMultiuser.XCutting.Enums;
using OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations;
using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;
using OOPBankMultiuser.Application.Contracts.DTOs.BankOperations;


namespace OOPBankMultiuser.Presentation.ConsoleUI
{
    internal class MainMenu
	{

		private readonly IAccountService _account;
		private readonly IBankService _bank;

		private LoginManager Manager = new();

		const int EXIT_OPTION = 0;

		bool Exit;


		int Option;


		public MainMenu(IAccountService accountService, IBankService bankService)
		{
			_account = accountService;
			_bank = bankService;
			Initialize();
		}

		void Initialize()
		{
			Console.OutputEncoding = Encoding.UTF8;
		}

		#region MENU NAVIGATION
		public void StartApplication()
		{
			Exit = false;

			AccountDTO? account = Manager.StartLogin(_bank);
			if (Manager.Logged && account != null)
			{
				_bank.SetCurrentAccount(account);
				if (_bank.GetCurrentAccount() != null)
				{
					MenuOutput.Print("Login successful.");
					ShowMenu();
				}
				else
				{
					Manager.Logged = false;
					MenuOutput.PrintError("Login unsuccessful.");
				}

			}
			if (!Exit) StartApplication();
			else if (Exit) ExitApplication();
		}

		void ShowMenu()
		{
			MenuOutput.ClearConsole();
			Option = MenuInput.GetValidIntInput($"Welcome to your account, {_bank.GetCurrentAccount().OwnerName}! What do you wish to do?\n\n" +
				$"1 - Money Income\n" +
				$"2 - Money Outcome\n" +
				$"3 - See movement history\n" +
				$"4 - See Income history\n" +
				$"5 - See outcome history\n" +
				$"6 - See account money\n" +
				$"7 - See account data\n" +
				$"{EXIT_OPTION} - Logout\n\n" +
				$"Please choose an option:");
			ManageMenuOption();
		}

		private void ManageMenuOption()
		{
			if (Option > 0) ManageOptions();
			else if (Option == EXIT_OPTION) Manager.Logged = false;
			else
			{
				MenuOutput.ClearConsole();
				MenuOutput.PrintError("Invalid option. Try again.");
			}

			if (!Exit)
			{
				if (Manager.Logged) ShowMenu();
				else if (!Manager.Logged) AskCloseApplication();
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

		#endregion

		#region EXIT
		void AskCloseSession()
		{
			Option = MenuInput.GetValidIntInput($"Press {EXIT_OPTION} to close your session, press any other key if you wish to execute another action:");
			if (Option == EXIT_OPTION) Manager.Logged = false;
		}

		void AskCloseApplication()
		{
			MenuOutput.ClearConsole();
			MenuOutput.Print("\nClosing session...\n");
			Option = MenuInput.GetValidIntInput($"Press {EXIT_OPTION} to close the application, press any other key if you wish to access other account:");
			if (Option == EXIT_OPTION) Exit = true;
		}

		void ExitApplication()
		{
			MenuOutput.ClearConsole();
			MenuOutput.Print("======================================\n" +
							"|| Closing application...            ||\n" +
							"|| Thank you for using our services! ||\n" +
							"======================================");
		}

		#endregion

		#region OPTIONS

		void HandleIncome()
		{
			string message = "";
			decimal income = MenuInput.GetValidDecimalInput("Please write how much you want to deposit:");

			if(income != MenuInput.ERROR_VALUE)
			{
				IncomeResultDTO result = _account.DepositMoney(income);

				if (result.ResultHasErrors)
				{
					message = ManageIncomeErrors(result);
				}
				else message = $"{income}€ were added to your account.\nCurrent money: {result.TotalBalance:0.00}€";
			}
			else message = "ERROR: Input is not a valid decimal.";

			MenuOutput.Print(message);
		}

		static string ManageIncomeErrors(IncomeResultDTO result)
		{
			return result.Error switch
			{
				IncomeErrorEnum.NegativeValue => "ERROR: Input can't be a negative value.",
				IncomeErrorEnum.OverMaxIncome => $"ERROR: Income can't be higher than {result.MaxIncomeAllowed:0.00}€.",
				_ => "",
			};
		}

		void HandleOutcome()
		{
			string message = "";
			decimal outcome = MenuInput.GetValidDecimalInput("Please write how much you want to withdraw:");

			if (outcome != MenuInput.ERROR_VALUE)
			{
				OutcomeResultDTO result = _account.WithdrawMoney(outcome);

				if (result.ResultHasErrors)
				{
					message = ManageOutcomeErrors(result);
				}
				else message = $"{outcome}€ were withdrawn from your account.\nCurrent money: {result.TotalBalance:0.00}€";
			}
			else message = "ERROR: Input is not a valid decimal.";

			MenuOutput.Print(message);
		}

		static string ManageOutcomeErrors(OutcomeResultDTO result)
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
			if (movementData.Movements.Count > 0 && movementData != null)
			{
				message = "====== All Movements ======";

				foreach (MovementDTO movement in movementData.Movements)
				{
					message += $"\n|| {movement.Timestamp:dd/MM/yyyy-hh:mm:ss} || {movement.Content:0.00}€";
				}
				message += $"\n================================\n              TOTAL | {movementData.TotalBalance:0.00}€";
			}
			else
				message = "No movements registered.";

			MenuOutput.Print(message);
		}

		void PrintAllIncomes()
		{
			string message;

			MovementListDTO incomeData = _account.GetIncomes();
			if (incomeData.Movements.Count != 0 && incomeData != null)
			{
				message = "====== All Incomes ======";

				foreach (MovementDTO movement in incomeData.Movements)
				{
					message += $"\n|| {movement.Timestamp:dd/MM/yyyy-hh:mm:ss} || {movement.Content:0.00}€";
				}
				message += $"\n================================\n              TOTAL | {incomeData.TotalIncome:0.00}€";
			}
			else
				message = "No incomes registered.";

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
				message += $"\n================================\n              TOTAL | {incomeData.TotalOutcome:0.00}€";
			}

			MenuOutput.Print(message);
		}

		void PrintAccountMoney()
		{
			try { 
				MenuOutput.Print($"Current money available {_account.GetBalance():0.00}€.");
			}
            catch (Exception)
			{
				MenuOutput.ClearConsole();
				MenuOutput.PrintError("Failed to access account data.");
			}
	}

		void PrintAccountData()
		{
			string message = "";

			try { 
				AccountDTO? result = _account?.GetAccountInfo();

				if (result != null)
				{
					message += "==================================================================\n";
					message += "||\n";
					message += $"||\tUser: {result.OwnerName}\n";
					message += $"||\tIBAN: {result.Iban}\n";
					message += $"||\tAccount number: {result.AccountNumber}\n";
					message += $"||\tAccount pin: {result.Pin}\n";
					message += $"||\tCurrent money available {result.TotalBalance:0.00}€.\n";
					message += "||\n";
					message += "==================================================================";
				}
				else message = "Account not found.";
			}
			catch (Exception)
			{
				MenuOutput.ClearConsole();
				MenuOutput.PrintError("Failed to access account data.");
			}

			MenuOutput.Print(message);
		}

		#endregion
	}
}
