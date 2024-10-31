using OOPBankMultiuser.XCutting.Enums;

namespace OOPBankMultiuser.Application.Contracts.DTOs
{
	public class AccountResultDTO
	{
		public string OwnerName { get; set; }
		public string Iban { get; set; }
		public string AccountNumber { get; set; }
		public string Pin { get; set; }
		public decimal TotalBalance { get; set; }
	}
}
