using OOPBankMultiuser.XCutting.Enums;
namespace OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations
{
    public class OutcomeResultDTO
    {

        public bool ResultHasErrors { get; set; }
		public OutcomeErrorEnum? Error { get; set; }
		public decimal MaxOutcomeAllowed { get; set; }
		public decimal TotalBalance { get; set; }
		public decimal MoneyWithdrawed { get; set; }
	}
}
