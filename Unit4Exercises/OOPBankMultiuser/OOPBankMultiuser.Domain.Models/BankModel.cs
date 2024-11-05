using System.Text;

namespace OOPBankMultiuser.Domain.Models
{
	public class BankModel
	{
		public string EntityName { get; set; }
		public string BankId { get; set; }
		public string ControlNum { get; set; }
		public string OfficeId { get; set; }
		public string CountryCode { get; set; }
		public List<AccountModel> Accounts { get; set; }
		public AccountModel CurrentAccount { get; set; }


		public void CreateAccount(int acId, string pin, string ownerName)
		{
			string accountNumber = AccountModel.GenerateAccountNumber(acId);

			Accounts?.Add(new AccountModel
			{
				OwnerName = ownerName,
				IdNumber = acId,
				AccountNumber = accountNumber,
				Pin = pin,
				Iban = CreateIban(accountNumber)
			});
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

		public AccountModel? FindAccount(int acNumber, string pin)
		{
			foreach (AccountModel? account in Accounts)
			{
				if (acNumber.Equals(account.IdNumber) && pin.Equals(account.Pin))
				{
					return account;
				}
			}
			return null;
		}


/*
		public void Initialize()
		{

			Accounts[0].Movements.Add($"{350:0.00}€", "+");
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
		}*/
	}
}
