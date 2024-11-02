using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;
using OOPBankMultiuser.XCutting.Enums;

namespace OOPBankMultiuser.Application.Contracts.DTOs.BankOperations
{
    public class CreateAccountResultDTO
    {
        public bool HasErrors { get; set; }
        public CreateAccountErrorEnum? Error { get; set; }
		public int AccountNumberLength { get; set; }
		public int PinLength { get; set; }
		public AccountDTO account { get; set; }
    }
}
