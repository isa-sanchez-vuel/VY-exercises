
namespace Pokemon.Infrastructure.Contracts
{
	public interface IPokeApiImporter
	{
		Task ImportApiJson();

		string? GetJson();
	}
}
