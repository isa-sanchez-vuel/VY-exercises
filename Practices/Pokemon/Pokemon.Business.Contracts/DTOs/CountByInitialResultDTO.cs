using Pokemon.XCutting.Enums;

namespace Pokemon.Business.Contracts.DTOs
{
	public class CountByInitialResultDTO
	{
		public bool HasErrors { get; set; }
		public CountByInitialErrorEnum? Error { get; set; }
		public string? Message { get; set; }
		public List<string>? MessageList { get; set; }
		public int Count { get; set; }
	}
}
