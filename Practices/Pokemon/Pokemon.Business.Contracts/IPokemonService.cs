
using Pokemon.Business.Contracts.DTOs;

namespace Pokemon.Business.Contracts
{
	public interface IPokemonService
	{
		Task<CountByInitialResultDTO> CountByInitial(string firstStringCharacter);
	}
}
