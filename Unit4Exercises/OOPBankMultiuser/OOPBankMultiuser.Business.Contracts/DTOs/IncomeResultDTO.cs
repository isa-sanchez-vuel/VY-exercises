using OOPBankMultiuser.XCutting.Enums;

namespace OOPBankMultiuser.Business.Contracts.DTOs
{
	public class IncomeResultDTO
	{
		public bool ResultHasErrors;
		public IncomeErrorEnum? Error;
		public decimal MaxIncomeAllowed;
		public decimal TotalBalance;
	}
}
