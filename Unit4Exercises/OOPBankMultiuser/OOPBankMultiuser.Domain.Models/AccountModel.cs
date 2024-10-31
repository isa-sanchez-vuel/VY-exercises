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

		public bool ValidateCredentials(string accountId)
		{
			if (accountId.Length != ACCOUNT_LENGTH)
			{
				//Menu.PrintError($"Account number length is incorrect. It must have {ACCOUNT_LENGTH} digits.");
				return false;
			}
			return true;
		}

		public bool ValidatePinLength(string pin)
		{
			if (pin.Length != PIN_LENGTH)
			{
				//Menu.PrintError($"Pin number length is incorrect. It must have {PIN_LENGTH} digits.");
				return false;
			}
			return true;
		}

		public bool ValidateNumberFormat(string accountId)
		{
			if (!long.TryParse(accountId, out _))
			{
				//Menu.PrintError("Account number has wrong format. It must contain only numerical values.");
				return false;
			}
			return true;
		}

		public bool CheckPinFormat(string pin)
		{
			if (!int.TryParse(pin, out _))
			{
				//Menu.PrintError("Pin has wrong format. It must contain only numerical values.");
				return false;
			}
			return true;
		}
	}
}
