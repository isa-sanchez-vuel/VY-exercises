using System.Security.Principal;
using System.Text;

namespace OOPBankMultiuser.Domain.Models
{
	public class AccountModel
	{
		public const int ACCOUNT_LENGTH = 10;
		public const int PIN_LENGTH = 4;
		public const decimal MAX_INCOME = 5000;
		public const decimal MAX_OUTCOME = 3000;

		public string OwnerName { get; set; }
		public string Iban { get; set; }
		public string AccountNumber { get; set; }
		public string Pin {  get; set; }
		public decimal TotalBalance { get; set; }
		public List<MovementModel> Movements {  get; set; }

		public bool incomeNegative;
		public bool incomeOverMaxValue;

		public bool outcomeNegative;
		public bool outcomeOverMaxValue;
		public bool outcomeOverTotalBalance;

		//login bool checks
		public bool numberSizeWrong;
		public bool numberFormatWrong;
		public bool pinSizeWrong;
		public bool pinFormatWrong;

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

		public bool ValidateCredentials(string acNumber, string pin)
		{
			numberSizeWrong = acNumber.Length != ACCOUNT_LENGTH;
			pinSizeWrong = pin.Length != PIN_LENGTH;
			if (!int.TryParse(acNumber, out _)) numberFormatWrong = true;
			if(!int.TryParse(pin, out _)) pinFormatWrong = true;

			return !numberSizeWrong && !numberFormatWrong && !pinSizeWrong && !pinFormatWrong;
		}

	}
}
