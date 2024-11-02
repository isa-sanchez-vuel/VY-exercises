using OOPBankMultiuser.XCutting.Enums;

namespace OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations
{
    public class IncomeResultDTO
    {
        public bool ResultHasErrors;
        public IncomeErrorEnum? Error;
        public decimal MaxIncomeAllowed;
        public decimal TotalBalance;
    }
}
