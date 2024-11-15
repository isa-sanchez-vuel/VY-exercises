using System.ComponentModel.Design;
using UniversitiesManagement.Application.Contracts;
using UniversitiesManagement.Application.Contracts.DTOs.ContentModels;
using UniversitiesManagement.Application.Contracts.DTOs.Requests;
using UniversitiesManagement.Application.Contracts.DTOs.Results;
using UniversitiesManagement.Domain;
using UniversitiesManagement.Infrastructure.Contracts;
using UniversitiesManagement.Infrastructure.Contracts.APIEntities;
using UniversitiesManagement.Infrastructure.Contracts.DBEntities;
using UniversitiesManagement.XCutting;

namespace UniversitiesManagement.Application.Impl
{
	public class UniversityService : IUniversityService
	{
		readonly IDbUniversityRepository _dbRepository;
		readonly IApiRepository _apiRepository;

		public UniversityService(IDbUniversityRepository dbRepository, IApiRepository apiRepository)
		{
			_dbRepository = dbRepository;
			_apiRepository = apiRepository;
		}

		public async Task<GetDatabaseResultDTO> UpdateDatabase()
		{
			GetDatabaseResultDTO result = new()
			{
				HasErrors = true,
				Error = null,
				ErrorMessages = new(),
			};

			if (_apiRepository == null || _dbRepository == null)
			{
				if(_apiRepository == null) result.ErrorMessages.Add(ErrorEnum.ApiRepositoryNull);
				else result.ErrorMessages.Add(ErrorEnum.DatabaseRepositoryNull);
			}
			else
			{
				UniversityListJson? jsonList = await _apiRepository.ImportApiData();

				if (jsonList == null) result.ErrorMessages.Add(ErrorEnum.JsonListimportedNull);
				else
				{
					List<University>? universities = _dbRepository.UpdateDatabaseWithApiData(jsonList.Universities);

					if (universities == null || universities.Count <= 0) result.ErrorMessages.Add(ErrorEnum.DatabaseUniversityListNull);
					else
					{
						result.HasErrors = false;
						result.Universities = universities.Select(x => new UniversityDTO()
						{
							Name = x.Name,
							Country = x.Country,
						}).ToList();
					}
				}
			}
			return result;
		}

		public GetDatabaseResultDTO ListDatabaseUniversities()
		{
			GetDatabaseResultDTO result = new()
			{
				HasErrors = true,
				Error = null,
				ErrorMessages = new(),
			};

			if (_dbRepository == null) result.ErrorMessages.Add(ErrorEnum.DatabaseRepositoryNull);
			else
			{
				List<University>? universities = _dbRepository.GetAllUniversities();

				if (universities == null || universities.Count <= 0) result.ErrorMessages.Add(ErrorEnum.DatabaseUniversityListNull);
				else
				{
					result.HasErrors = false;
					result.Universities = universities.Select(x => new UniversityDTO()
					{
						Name = x.Name,
						Country = x.Country,
					}).ToList();
				}
			}
			return result;
		}

		public InsertUniversityResultDTO CreateNewUniversity(CreateUniversityRequestDTO request)
		{
			InsertUniversityResultDTO result = new()
			{
				HasErrors = true,
				Error = null,
				ErrorMessages = new(),
			};

			if (!UniversityModel.CheckCountry(request.Country)) result.ErrorMessages.Add(ErrorEnum.CountryNameIsEmpty);
			if (!UniversityModel.CheckName(request.Name)) result.ErrorMessages.Add(ErrorEnum.UniversityNameIsEmpty);
			if (!UniversityModel.CheckAlphaCode(request.AlphaTwoCode)) result.ErrorMessages.Add(ErrorEnum.AlphaTwoCodeIsEmpty);
			if(result.ErrorMessages.Count > 0) result.ErrorMessages.Add(ErrorEnum.RequestNotValid);
			else if (_dbRepository == null) result.ErrorMessages.Add(ErrorEnum.DatabaseRepositoryNull);
			else
			{
				University? newUni = new()
				{
					Name = request.Name,
					Country = request.Country,
					AlphaTwoCode = request.AlphaTwoCode!.ToUpper(),
					StateProvince = request.StateProvince,
				};

				newUni = _dbRepository.CreateUniversity(newUni);

				if (newUni == null) result.ErrorMessages.Add(ErrorEnum.UniversityAlreadyExists);
				else
				{
					result.HasErrors = false;
					result.UpdatedUniversity = new()
					{
						Name = newUni.Name,
						Id = newUni.Id,
						Country = newUni.Country,
						AlphaTwoCode = newUni.AlphaTwoCode,
						StateProvince = newUni.StateProvince,
					};
				}
			}
			return result;
		}
	
		public InsertUniversityResultDTO UpdateOldUniversity(UpdateUniversityRequestDTO request)
		{
			InsertUniversityResultDTO result = new()
			{
				HasErrors = true,
				Error = null,
				ErrorMessages = new(),
			};

			if (!UniversityModel.CheckId(request.Id)) result.ErrorMessages.Add(ErrorEnum.IdNotValid);
			
			if(result.ErrorMessages.Count > 0) result.ErrorMessages.Add(ErrorEnum.RequestNotValid);
			else if (_dbRepository == null) result.ErrorMessages.Add(ErrorEnum.DatabaseRepositoryNull);
			else
			{
				University? oldUni = _dbRepository.GetUniversity(request.Id);
				if (oldUni == null) result.ErrorMessages.Add(ErrorEnum.UniversityNotFound);
				else
				{
					result.OldUniversity = new()
					{
						Id = oldUni.Id,
						Name = oldUni.Name,
						Country = oldUni.Country,
						AlphaTwoCode = oldUni.AlphaTwoCode,
						StateProvince = oldUni.StateProvince,
					};

					University updatedUni = new()
					{
						Id = request.Id,
						Name = request.Name,
					};

					updatedUni = _dbRepository.UpdateUniversity(updatedUni);

					if (updatedUni == null) result.ErrorMessages.Add(ErrorEnum.UniversityNotFound);
					else
					{
						result.HasErrors = false;
						result.UpdatedUniversity = new()
						{
							Id = updatedUni.Id,
							Name = updatedUni.Name,
							Country = updatedUni.Country,
							AlphaTwoCode = updatedUni.AlphaTwoCode,
							StateProvince = updatedUni.StateProvince,
						};
					}
				}
			}
			return result;
		}
		
		public GetUniversityResultDTO FindUniversity(int idRequest)
		{
			GetUniversityResultDTO result = new()
			{
				HasErrors = true,
				Error = null,
				ErrorMessages = new(),
				
			};

			if (!UniversityModel.CheckId(idRequest)) result.ErrorMessages.Add(ErrorEnum.IdNotValid);

			if (result.ErrorMessages.Count > 0) result.ErrorMessages.Add(ErrorEnum.RequestNotValid);
			else if (_dbRepository == null) result.ErrorMessages.Add(ErrorEnum.DatabaseRepositoryNull);
			else
			{
				University? deletedUni = _dbRepository.GetUniversity(idRequest);

				if (deletedUni == null) result.ErrorMessages.Add(ErrorEnum.UniversityNotFound);
				else
				{
					result.HasErrors = false;
					result.University = new()
					{
						Id = deletedUni.Id,
						Name = deletedUni.Name,
						Country = deletedUni.Country,
						AlphaTwoCode = deletedUni.AlphaTwoCode,
						StateProvince = deletedUni.StateProvince,
					};
				}
			}
			return result;
		}

		public DeleteUniversityResultDTO DeleteExistentUniversity(int idRequest)
		{
			DeleteUniversityResultDTO result = new()
			{
				HasErrors = true,
				Error = null,
				ErrorMessages = new(),
				
			};

			if (!UniversityModel.CheckId(idRequest)) result.ErrorMessages.Add(ErrorEnum.IdNotValid);

			if (result.ErrorMessages.Count > 0) result.ErrorMessages.Add(ErrorEnum.RequestNotValid);
			else if (_dbRepository == null) result.ErrorMessages.Add(ErrorEnum.DatabaseRepositoryNull);
			else
			{
				University? deletedUni = _dbRepository.DeleteUniversity(idRequest);

				if (deletedUni == null) result.ErrorMessages.Add(ErrorEnum.UniversityNotFound);
				else
				{
					result.HasErrors = false;
					result.DeletedUniversity = new()
					{
						IsDeleted = deletedUni.IsDeleted,
						Id = deletedUni.Id,
						Name = deletedUni.Name,
						Country = deletedUni.Country,
						AlphaTwoCode = deletedUni.AlphaTwoCode,
						StateProvince = deletedUni.StateProvince,
					};
				}
			}
			return result;
		}
	}
}
