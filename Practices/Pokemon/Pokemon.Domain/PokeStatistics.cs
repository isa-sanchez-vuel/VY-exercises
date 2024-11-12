using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pokemon.Domain
{
	public class PokeStatistics
	{
		[Required]
		[Column("initial")]
		[StringLength(1)]
		public string Initial { get; set; }

		[Column("counter")]
		public int Counter { get; set; }

		public int PokemonId { get; set; }
	}
}
