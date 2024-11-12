using System.Text.Json.Serialization;

namespace Pokemon.Domain
{
	public class PokemonListModel
	{
		public List<string?>? Names { get; set; }

		public int CountNumberOfPokemonsByLetter(char firstLetter)
		{
			return Names?.Count(x => x != null && x.Length >= 1 && x[0] == firstLetter) ?? 0;
		}



	}

}
