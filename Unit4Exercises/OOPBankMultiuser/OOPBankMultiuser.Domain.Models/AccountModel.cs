using System.Text;

namespace OOPBankMultiuser.Domain.Models
{
	public class AccountModel
	{
		public const int ACCOUNT_LENGTH = 10;
		public const int PIN_LENGTH = 4;
		public const decimal MAX_INCOME = 5000;
		public const decimal MAX_OUTCOME = 3000;
		
		private static readonly string BankName = "PotatoBank S.L";
		private static readonly string BankId = "3000";
		private static readonly string ControlNum = "54";
		private static readonly string OfficeId = "4790";
		private static readonly string CountryCode = "ES";

		public string OwnerName { get; set; }
		public string Iban { get; set; }
		public int IdNumber { get; set; }
		public string AccountNumber { get; set; }
		public string Pin {  get; set; }
		public decimal TotalBalance { get; set; }
		public List<MovementModel> Movements {  get; set; }

		// movements bool checks
		public bool incomeNegative;
		public bool incomeOverMaxValue;

		public bool outcomeNegative;
		public bool outcomeOverMaxValue;
		public bool outcomeOverTotalBalance;

		//credentials bool checks
		public bool numberSizeWrong;
		public bool numberFormatWrong;

		public bool pinSizeWrong;
		public bool pinFormatWrong;


		public static string GenerateAccountNumber(int id)
		{
			return id.ToString().PadLeft(10, '0');
		}

		public static string CreateIban(string accountNumber)
		{
			string iban = CountryCode + ControlNum + BankId + OfficeId + ControlNum + accountNumber;

			iban = iban.Replace(" ", "").ToUpper();

			StringBuilder formattedIban = new();
			for (int i = 0; i < iban.Length; i += 4)
			{
				if (i > 0) formattedIban.Append(' ');
				formattedIban.Append(iban.AsSpan(i, Math.Min(4, iban.Length - i)));
			}
			iban = formattedIban.ToString();

			return iban;
		}


		public void AddIncome(decimal income)
		{
			TotalBalance += income;
			Movements?.Add(new MovementModel
			{
				Content = +income,
				Date = DateTime.Now,
			}); 
		}

		public void SubtractOutcome(decimal outcome)
		{
			TotalBalance -= outcome;
			Movements?.Add(new MovementModel
			{
				Content = -outcome,
				Date = DateTime.Now,
			});
		}

		public bool ValidateIncome(decimal income)
		{
			incomeNegative = income < 0;
			incomeOverMaxValue = income > MAX_INCOME;

			return !incomeNegative && !incomeOverMaxValue;
		}

		public bool ValidateOutcome(decimal outcome)
		{
			outcomeNegative = outcome < 0;
			outcomeOverMaxValue = outcome > MAX_OUTCOME;
			outcomeOverTotalBalance = outcome > TotalBalance;

			return !outcomeNegative && !outcomeOverMaxValue && !outcomeOverTotalBalance;
		}

		public bool ValidateNumber(string acNumber)
		{
			numberSizeWrong = acNumber.Length != ACCOUNT_LENGTH;
			if (!int.TryParse(acNumber, out _)) numberFormatWrong = true;

			return !numberSizeWrong && !numberFormatWrong;
		}
		public bool ValidatePin(string pin)
		{
			pinSizeWrong = pin.Length != PIN_LENGTH;
			if(!int.TryParse(pin, out _)) pinFormatWrong = true;

			return !pinSizeWrong && !pinFormatWrong;
		}

	}
}
