namespace OOPBankMultiuser.Business.Contracts.DTOs
{
	public class MovementListDTO
	{
		public List<MovementDTO> Movements { get; set; }
		public decimal TotalBalance { get; set; }
		public decimal TotalIncome { get; set; }
		public decimal TotalOutcome { get; set; }
	}
}
