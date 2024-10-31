using OOPBankMultiuser.XCutting.Enums;
namespace OOPBankMultiuser.Business.Contracts.DTOs
{
	public class OutcomeResultDTO
	{

		public bool ResultHasErrors;
		public OutcomeErrorEnum? Error;
		public decimal MaxOutcomeAllowed;
		public decimal TotalBalance;
	}
}
