using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;
using OOPBankMultiuser.XCutting.Enums;

namespace OOPBankMultiuser.Application.Contracts.DTOs.BankOperations
{
    public class LoginResultDTO
    {
        public bool HasErrors { get; set; }
        public LoginErrorEnum? Error { get; set; }

        public int AccountNumberLength { get; set; }
        public int PinLength { get; set; }
        public AccountDTO Account { get; set; }
    }
}
