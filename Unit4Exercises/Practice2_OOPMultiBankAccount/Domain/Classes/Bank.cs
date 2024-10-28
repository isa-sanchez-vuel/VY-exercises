using ConsoleMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPMultiBankAccount.Domain.Classes
{
    internal class Bank
    {
        private const int ACCOUNT_LENGTH = 10;
        private const int PIN_LENGTH = 4;
        private const decimal MAX_INCOME = 5000;
        private const decimal MAX_OUTCOME = 3000;
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
            if (!CheckAccountNumberParse(acId)) return false;
            if (!CheckPinNumberParse(pin)) return false;
            if (!CheckAccountNumberLength(acId)) return false;
            if (!CheckPinNumberLength(pin)) return false;


            Account account = new(acId, pin, ownerName);
            account.CreateIban(CountryCode, BankId, ControlNum, OfficeId);
            Accounts.Add(account);

            return true;
        }

        public Account? CheckAccountLogin(string accountId, string pin)
        {
            if (!CheckAccountNumberParse(accountId)) return null;
            if (!CheckPinNumberParse(pin)) return null;
            if (!CheckAccountNumberLength(accountId)) return null;
            if (!CheckPinNumberLength(pin)) return null;

            foreach (Account? account in Accounts)
            {
                if (accountId.Equals(account.GetId()) && pin.Equals(account.GetPin())) return account;
            }
            Console.Clear();
            Menu.PrintError("Account not found.");
            return null;
        }

        public bool CheckAccountNumberLength(string accountId)
        {
            if (accountId.Length != ACCOUNT_LENGTH)
            {
                Menu.PrintError($"Account number length is incorrect. It must have {ACCOUNT_LENGTH} digits.");
                return false;
            }
            return true;
        }

        public bool CheckPinNumberLength(string pin)
        {
            if (pin.Length != PIN_LENGTH)
            {
                Menu.PrintError($"Pin number length is incorrect. It must have {PIN_LENGTH} digits.");
                return false;
            }
            return true;
        }

        public bool CheckAccountNumberParse(string accountId)
        {
            if (!long.TryParse(accountId, out _))
            {
                Menu.PrintError("Account number has wrong format. It must contain only numerical values.");
                return false;
            }
            return true;
        }

        public bool CheckPinNumberParse(string pin)
        {
            if (!int.TryParse(pin, out _))
            {
                Menu.PrintError("Pin has wrong format. It must contain only numerical values.");
                return false;
            }
            return true;
        }

        public void HandleIncome(Account account)
        {
            decimal income = Menu.GetValidDecimalInput("Please write how much you want to deposit:");

            if (income == Menu.ERROR_VALUE)
            {
                Menu.PrintError("Error: Invalid input, income operation cancelled.");
                return;
            }

            if (income > MAX_INCOME)
            {
                Menu.PrintError($"Income can't be higher than {MAX_INCOME:0.00}€.");
                return;
            }

            account.AddIncome(income);
            account.AddMovement($"{income:0.00}€", "+");
            Menu.Print($"{income}€ were added to your account.\nCurrent money: {account.GetTotalMoney():0.00}€");
        }


        public void HandleOutcome(Account account)
        {
            decimal outcome = Menu.GetValidDecimalInput("Please write how much you want to withdraw:");

            if (outcome == Menu.ERROR_VALUE)
            {
                Menu.PrintError("Error: Invalid input, outcome operation cancelled.");
                return;
            }

            if (outcome > account.GetTotalMoney())
            {
                Menu.PrintError($"Outcome can't be higher than your total money. Money available: {account.GetTotalMoney():0.00}€.");
                return;
            }
            if (outcome > MAX_OUTCOME)
            {
                Menu.PrintError($"Outcome can't be higher than {MAX_OUTCOME:0.00}€.");
                return;
            }

            account.SubtractOutcome(outcome);
            account.AddMovement($"{outcome:0.00}€", "-");
            Menu.Print($"{outcome:0.00}€ were withdrawn from your account.\nCurrent money: {account.GetTotalMoney():0.00}€");
        }


        public void PrintAllMovements(Account account)
        {
            foreach (Movement movement in account.GetAllMovements())
            {
                Menu.Print($"|| {movement.GetDate()} || {movement.GetType()} {movement.GetContent()}");
            }
        }

        public void PrintAllIncomes(Account account)
        {
            Menu.Print("These are your incomes:\n");
            foreach (Movement movement in account.GetAllMovements())
            {
                if (movement.GetType().Equals("+"))
                    Menu.Print($"|| {movement.GetDate()} || {movement.GetType()} {movement.GetContent()}");
            }
        }

        public void PrintAllOutcomes(Account account)
        {
            Menu.Print("These are your outcomes:\n");
            foreach (Movement movement in account.GetAllMovements())
            {
                if (movement.GetType().Equals("-"))
                    Menu.Print($"|| {movement.GetDate()} || {movement.GetType()} {movement.GetContent()}");
            }
        }

        public void PrintAccountMoney(Account account)
        {
            Menu.Print($"Current money available {account.GetTotalMoney():0.00}€.");
        }

        public void PrintAccountData(Account account)
        {
            Menu.Print("==================================================================");
            Menu.Print("||                                                              ||");
            Menu.Print($"||\tUser: {account.GetName()}                                 ||");
            Menu.Print($"||\tIBAN: {account.GetIban()}                     ||");
            Menu.Print($"||\tAccount number: {account.GetAccountNumber()}                              ||");
            Menu.Print($"||\tAccount pin: {account.GetPin()}                                       ||");
            Menu.Print($"||\tCurrent money available {account.GetTotalMoney():0.00}€.                         ||");
            Menu.Print("||                                                              ||");
            Menu.Print("==================================================================");
        }


        public void Initialize()
        {

            Accounts[0].AddMovement($"{350:0.00}€", "+");
            Accounts[0].AddMovement($"{60:0.00}€", "-");
            Accounts[0].AddMovement($"{80:0.00}€", "+");
            Accounts[0].AddMovement($"{300:0.00}€", "-");
            Accounts[0].AddIncome(70);

            Accounts[1].AddMovement($"{600:0.00}€", "+");
            Accounts[1].AddMovement($"{140:0.00}€", "+");
            Accounts[1].AddMovement($"{200:0.00}€", "-");
            Accounts[1].AddMovement($"{400:0.00}€", "-");
            Accounts[1].AddMovement($"{1000:0.00}€", "+");
            Accounts[1].AddIncome(640);

            Accounts[2].AddMovement($"{30:0.00}€", "+");
            Accounts[2].AddMovement($"{25:0.00}€", "-");
            Accounts[2].AddMovement($"{15:0.00}€", "+");
            Accounts[2].AddIncome(20);

            Accounts[3].AddMovement($"{350:0.00}€", "+");
            Accounts[3].AddMovement($"{60:0.00}€", "-");
            Accounts[3].AddMovement($"{80:0.00}€", "+");
            Accounts[3].AddMovement($"{300:0.00}€", "-");
            Accounts[3].AddIncome(70);

            Accounts[4].AddMovement($"{600:0.00}€", "+");
            Accounts[4].AddMovement($"{140:0.00}€", "+");
            Accounts[4].AddMovement($"{500:0.00}€", "-");
            Accounts[4].AddIncome(65);

            Accounts[5].AddMovement($"{1000:0.00}€", "+");
            Accounts[5].AddMovement($"{200:0.00}€", "-");
            Accounts[5].AddMovement($"{150:0.00}€", "-");
            Accounts[5].AddIncome(650);
        }
    }

}
