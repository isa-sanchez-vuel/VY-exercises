using OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs;
using OOPBankMultiuser.XCutting.Enums;

namespace OOPBankMultiuser.Application.Contracts.DTOs.DatabaseOperations
{
    public class UpdateAccountResultDTO
    {
		public bool HasErrors { get; set; }
		public UpdateAccountErrorEnum? Error { get; set; }
		public int PinLength { get; set; }
		public AccountDTO OldAccount { get; set; }
		public AccountDTO Account { get; set; }
	}
}
