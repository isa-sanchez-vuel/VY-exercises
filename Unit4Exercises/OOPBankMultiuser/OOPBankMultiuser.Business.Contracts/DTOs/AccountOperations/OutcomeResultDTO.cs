using OOPBankMultiuser.XCutting.Enums;
namespace OOPBankMultiuser.Application.Contracts.DTOs.AccountOperations
{
    public class OutcomeResultDTO
    {

        public bool ResultHasErrors;
        public OutcomeErrorEnum? Error;
        public decimal MaxOutcomeAllowed;
        public decimal TotalBalance;
    }
}
