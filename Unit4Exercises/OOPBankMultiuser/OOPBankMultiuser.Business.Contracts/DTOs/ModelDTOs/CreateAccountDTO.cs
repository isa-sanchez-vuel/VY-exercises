namespace OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs
{
	public class CreateAccountDTO
	{
		public string OwnerName { get; set; }
		public string Pin { get; set; }
		public decimal InitialBalance { get; set; }
	}
}
