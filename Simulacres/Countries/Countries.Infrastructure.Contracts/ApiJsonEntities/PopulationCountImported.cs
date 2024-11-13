
using System.Text.Json.Serialization;

namespace Countries.Infrastructure.Contracts.JsonImport
{
	public class PopulationCountImported
	{
		[JsonPropertyName("year")]
		public int Year { get; set; }

		[JsonPropertyName("value")]
		public long Count { get; set; }
	}
}
