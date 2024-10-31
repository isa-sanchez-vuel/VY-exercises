using System.Security.Principal;
using System.Text;

namespace OOPBankMultiuser.Domain.Models
{
	public class BankModel
	{
		public string EntityName;
		public string BankId;
		public string ControlNum;
		public string OfficeId;
		public string CountryCode;
		public List<AccountModel> Accounts = new();


		public BankModel(string name, string country, string id, string control, string officeNumber)
		{
			EntityName = name;
			BankId = id;
			ControlNum = control;
			OfficeId = officeNumber;
			CountryCode = country;
		}

		public bool CreateAccount(string acId, string pin, string ownerName)
		{
			if (!AccountModel.CheckNumberFormat(acId)) return false;
			if (!CheckPinFormat(pin)) return false;
			if (!CheckNumberLength(acId)) return false;
			if (!CheckPinLength(pin)) return false;


			Accounts?.Add(new AccountModel
			{
				OwnerName = ownerName,
				AccountNumber = acId,
				Pin = pin,
				Iban = CreateIban(acId)
			});
			return true;
		}

		string CreateIban(string acId)
		{
			string iban = CountryCode + ControlNum + BankId + OfficeId + ControlNum + acId;

			iban = iban.Replace(" ", "").ToUpper();

			StringBuilder formattedIban = new StringBuilder();
			for (int i = 0; i < iban.Length; i += 4)
			{
				if (i > 0) formattedIban.Append(" ");
				formattedIban.Append(iban.Substring(i, Math.Min(4, iban.Length - i)));
			}
			iban = formattedIban.ToString();

			return iban;
		}

		public Account? CheckAccountLogin(string accountId, string pin)
		{
			if (!CheckNumberFormat(accountId)) return null;
			if (!CheckPinFormat(pin)) return null;
			if (!CheckNumberLength(accountId)) return null;
			if (!CheckPinLength(pin)) return null;

			foreach (Account? account in Accounts)
			{
				if (accountId.Equals(account.GetId()) && pin.Equals(account.GetPin())) return account;
			}
			Console.Clear();
			Menu.PrintError("Account not found.");
			return null;
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
