
using SWApiManagement.Infrastructure.Contracts.APIEntities;

namespace SWApiManagement.Infrastructure.Contracts
{
	public interface IApiImporter
	{
		Task<PlanetListJson?> ImportApiData();
		Task<PlanetJson?> FindPlanetInApi(string planetUrl);
		Task<List<string>> FindResidents(PlanetJson planet);
	}
}
