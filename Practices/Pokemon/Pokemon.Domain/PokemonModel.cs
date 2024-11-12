
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pokemon.Domain
{
	public class PokemonModel
	{
		public string? Name { get; set; }

		public string? Url { get; set; }
	}
}
