namespace OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs
{
	public class UpdateAccountDTO
	{
		public int AccountId { get; set; }
		public string NewName { get; set; }
		public string NewPin { get; set; }
	}
}
