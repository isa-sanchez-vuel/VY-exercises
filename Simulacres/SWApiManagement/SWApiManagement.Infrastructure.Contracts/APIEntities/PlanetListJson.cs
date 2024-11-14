using System.Text.Json.Serialization;

namespace SWApiManagement.Infrastructure.Contracts.APIEntities
{
	public class PlanetListJson
	{
		[JsonPropertyName("count")]
		public int Count { get; set; }

		[JsonPropertyName("next")]
		public string Next { get; set; }

		[JsonPropertyName("previous")]
		public string Previous { get; set; }

		[JsonPropertyName("results")]
		public List<PlanetJson> Planets { get; set; }
	}
}
