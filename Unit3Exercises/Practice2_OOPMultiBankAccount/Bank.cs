using ConsoleMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice2_OOPMultiBankAccount
{
	internal class Bank
	{
		string EntityName;
		string BankId;
		string ControlNum;
		string OfficeId;
		string CountryCode;
		List<Account> Accounts = new();


		public Bank(string name, string country, string id, string control, string officeNumber) 
		{
			EntityName = name;
			BankId = id;
			ControlNum = control;
			OfficeId = officeNumber;
			CountryCode = country;
		}

		public bool CreateAccount(string acId, string pin, string ownerName)
		{
			Account account = new(acId, pin, ownerName);
			account.CreateIban(CountryCode, BankId, ControlNum, OfficeId);
			Accounts.Add(account);

			return true;
		}

		public Account CheckAccount(string accountId, string pin)
		{
			if (accountId.Length == 10 && pin.Length == 4)
			{
				foreach (Account account in Accounts)
				{
					if (accountId.Equals(account.GetId()) && pin.Equals(account.GetPin())) return account;
				}
			}
			return null;
		}

		public void HandleIncome(Account account)
		{
			decimal income = 0;

			Menu.PrintMenu("Please write how much do you want to deposit:");
			income = Menu.GetInputParsedDecimal();

			//if (Menu.CheckInputDecimal(income))
			//{
				account.AddIncome(income);
				account.AddMovement($"{income:0.00}€", "+");
				Console.WriteLine($"{income:0.00}€ were added to your account.\n");
			//}
		}

		public void HandleOutcome(Account account)
		{
			decimal outcome = 0;

			Menu.PrintMenu("Please write how much do you want to withdraw:");
			outcome = Menu.GetInputParsedDecimal();

			//if (Menu.CheckInputDecimal(outcome))
			//{
				account.SubtractOutcome(outcome);
				account.AddMovement($"{outcome:0.00}€", "-");
				Console.WriteLine($"{outcome:0.00}€ were withdrawed from your account.\n");
			//}
		}

		public void PrintAllMovements(Account account)
		{
			foreach (Movement movement in account.GetAllMovements())
			{
				Console.WriteLine($"|| {movement.GetDate()} || {movement.GetType()} {movement.GetContent()}");
			}
		}

		public void PrintAllIncomes(Account account)
		{
			Console.WriteLine("These are your incomes:\n");
			foreach (Movement movement in account.GetAllMovements())
			{
				if(movement.GetType().Equals("+"))
				Console.WriteLine($"|| {movement.GetDate()} || {movement.GetType()} {movement.GetContent()}");
			}
		}

		public void PrintAllOutcomes(Account account)
		{
			Console.WriteLine("These are your outcomes:\n");
			foreach (Movement movement in account.GetAllMovements())
			{
				if (movement.GetType().Equals("-"))
					Console.WriteLine($"|| {movement.GetDate()} || {movement.GetType()} {movement.GetContent()}");
			}
		}

		public void PrintAccountMoney(Account account)
		{
			Console.WriteLine($"Current money available {account.GetTotalMoney():0.00}€.");
		}
	}

}
