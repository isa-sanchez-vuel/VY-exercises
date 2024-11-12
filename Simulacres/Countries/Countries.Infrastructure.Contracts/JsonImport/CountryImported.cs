using System.Text.Json.Serialization;

namespace Countries.Infrastructure.Contracts.JsonImport
{
	public class CountryImported
	{
		[JsonPropertyName("country")]
		public string Name { get; set; }

		[JsonPropertyName("code")]
		public string Code { get; set; }

		[JsonPropertyName("iso3")]
		public string Iso3 { get; set; }

		[JsonPropertyName("populationCounts")]
		public List<PopulationCountImported> PopulationCounts { get; set; }
	}
}

