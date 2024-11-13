
using System.Text.Json.Serialization;

namespace Countries.Infrastructure.Contracts.JsonImport
{
	public class CountryListImported
	{
		[JsonPropertyName("error")]
		public bool Error { get; set; }

		[JsonPropertyName("msg")]
		public string Message { get; set; }

		[JsonPropertyName("data")]
		public List<CountryImported> Countries { get; set; }
	}
}
