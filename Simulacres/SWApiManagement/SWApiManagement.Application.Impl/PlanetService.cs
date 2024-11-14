using SWApiManagement.Application.Contracts;
using SWApiManagement.Application.Contracts.DTOs;
using SWApiManagement.Domain;
using SWApiManagement.Infrastructure.Contracts;
using SWApiManagement.Infrastructure.Contracts.APIEntities;
using SWApiManagement.Infrastructure.Contracts.DBEntities;
using SWApiManagement.XCutting;
using SWApiManagement.XCutting.Enums;

namespace SWApiManagement.Application.Impl
{
	public class PlanetService : IPlanetService
	{
		private readonly IPlanetsRepository? _repository;
		private readonly IApiImporter? _importer;

		public PlanetService(IPlanetsRepository repository, IApiImporter importer)
		{
			_repository = repository;
			_importer = importer;
		}


		public async Task<UpdateResultDTO> UpdateDatabaseWithApi()
		{
			UpdateResultDTO result = new()
			{
				HasErrors = true,
				Error = null,
				Message = GlobalVariables.ERROR_MESSAGE,
			};


			if (_importer == null) result.Error = ErrorEnum.ImporterNull;
			else if (_repository == null) result.Error = ErrorEnum.RepositoryNull;
			else
			{
				PlanetListJson? jsonList = await _importer.ImportApiData();

				if(jsonList == null) result.Error = ErrorEnum.PlanetListIsNull;
				else
				{
					List<Planet>? planetEntityList =  _repository.UpdateDatabase(jsonList.Planets);

					if(planetEntityList == null) result.Error = ErrorEnum.RetrieveFromDatabaseFailed;
					else
					{
						result.PlanetNames = planetEntityList.Select(x => x.Name).ToList();
						result.Message = GlobalVariables.DB_UPDATE_SUCCESS;
						result.HasErrors = false;
					}
				}
			}

			return result;
		}

		public async Task<ResidentResultDTO> GetResidentsOfPlanet(string? requestPlanetName)
		{
			ResidentResultDTO result = new()
			{
				HasErrors = true,
				Error = null,
				Message = GlobalVariables.ERROR_MESSAGE,
			};

			if (!DataValidator.StringIsValid(requestPlanetName)) result.Error = ErrorEnum.StringIsNull;
			else if (_importer == null) result.Error = ErrorEnum.ImporterNull;
			else if (_repository == null) result.Error = ErrorEnum.RepositoryNull;
			else
			{
				Planet? planetEntity = _repository.GetPlanet(requestPlanetName!);

				if (planetEntity == null)
				{
					result.Error = ErrorEnum.PlanetNotFound;
					result.Message = GlobalVariables.PLANET_NOT_FOUND;
				}
				else
				{
					PlanetJson? planetJson = await _importer.FindPlanetInApi(planetEntity.Url);
					if (planetJson == null)
					{
						result.Error = ErrorEnum.PlanetNotFound;
						result.Message = GlobalVariables.PLANET_NOT_FOUND;
					}
					else
					{
						List<string>? residentNameList = await _importer.FindResidents(planetJson);

						if (residentNameList != null)
						{
							result.ResidentNames = residentNameList;
							result.Message = GlobalVariables.RESIDENTS_GET_SUCCESS;
							result.HasErrors = false;
						}
					}
				}
			}
			return result;
		}
	}
}
