
using System.Text.Json.Serialization;

namespace Pokemon.Infrastructure.Contracts.FromJson
{
    public class PokemonInfoFromJson
    {
		[JsonPropertyName("name")]
		public string? Name { get; set; }

		[JsonPropertyName("url")]
		public string? Url { get; set; }
	}
}
