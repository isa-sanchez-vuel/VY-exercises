
using SWApiManagement.Application.Contracts.DTOs;

namespace SWApiManagement.Application.Contracts
{
	public interface IPlanetService
	{
		Task<UpdateResultDTO> UpdateDatabaseWithApi();

		Task<ResidentResultDTO> GetResidentsOfPlanet(string requestPlanetName);
	}
}
