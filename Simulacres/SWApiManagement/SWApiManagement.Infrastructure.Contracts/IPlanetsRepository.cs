
using SWApiManagement.Infrastructure.Contracts.APIEntities;
using SWApiManagement.Infrastructure.Contracts.DBEntities;

namespace SWApiManagement.Infrastructure.Contracts
{
	public interface IPlanetsRepository
	{

		List<Planet>? UpdateDatabase(List<PlanetJson> planetsImported);
		Planet? GetPlanet(string name);
		List<Planet> GetAllPlanets();
	}
}
