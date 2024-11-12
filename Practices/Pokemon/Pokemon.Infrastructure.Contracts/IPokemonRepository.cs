
using Pokemon.Infrastructure.Contracts.Entities;

namespace Pokemon.Infrastructure.Contracts
{
	public interface IPokemonRepository
	{
		PokemonEntity GetPokemon(int id);
		void AddPokemon();
		List<PokemonEntity> ListAllPokemons();

	}
}
