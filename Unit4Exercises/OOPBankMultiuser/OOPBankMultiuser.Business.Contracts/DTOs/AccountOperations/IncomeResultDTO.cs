using OOPBankMultiuser.XCutting.Enums;

namespace OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations
{
    public class IncomeResultDTO
    {
        public bool ResultHasErrors { get; set; }
        public IncomeErrorEnum? Error { get; set; }
		public decimal MaxIncomeAllowed { get; set; }
		public decimal TotalBalance { get; set; }
		public decimal MoneyDeposited { get; set; }
	}
}
