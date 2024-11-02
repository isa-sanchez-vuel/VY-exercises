using OOPBankMultiuser.XCutting.Enums;

namespace OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs
{
    public class MovementListDTO
	{
		public bool HasErrors { get; set; }
		public GetMovementsErrorEnum? Error { get; set; }
		public List<MovementDTO>? Movements { get; set; }
        public decimal TotalBalance { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalOutcome { get; set; }
	}
}
