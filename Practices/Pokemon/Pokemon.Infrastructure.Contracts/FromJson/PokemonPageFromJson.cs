
using System.Text.Json.Serialization;
using Pokemon.Infrastructure.Contracts.Entities;

namespace Pokemon.Infrastructure.Contracts.FromJson
{
    public class PokemonPageFromJson
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("next")]
        public string? Next { get; set; }

        [JsonPropertyName("previous")]
        public string? Previous { get; set; }

        [JsonPropertyName("results")]
        public List<PokemonInfoFromJson>? PokemonList { get; set; }
    }
}
