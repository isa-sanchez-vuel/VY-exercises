using Microsoft.AspNetCore.Mvc;
using UniversitiesManagement.Application.Contracts;
using UniversitiesManagement.Application.Contracts.DTOs.Requests;
using UniversitiesManagement.Application.Contracts.DTOs.Results;
using UniversitiesManagement.XCutting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniversitiesManagement.Presentation.WebApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class UniversitiesController : ControllerBase
	{
		readonly IUniversityService _service;
		readonly IConfiguration _config;

		public UniversitiesController(IUniversityService service, IConfiguration config)
		{
			_service = service;
			_config = config;
		}

		// GET: Controller
		[HttpGet("UpdateDatabase")]
		public async Task<IActionResult> UpdateDatabase()
		{
			GetDatabaseResultDTO result = await _service.UpdateDatabase();

			if (result.HasErrors) 
			{ 
				return BadRequest(GetErrorMessages(result.ErrorMessages));
			}

			return Ok(result);
		}

		// GET: Controller
		[HttpGet("ListUniversities")]
		public IActionResult ListUniversities()
		{
			GetDatabaseResultDTO result = _service.ListDatabaseUniversities();

			if (result.HasErrors) 
			{ 
				return BadRequest(GetErrorMessages(result.ErrorMessages));
			}

			return Ok(result);
		}

		// DELETE api/<UniversitiesController>/5
		[HttpGet("GetUniversity")]
		public IActionResult Getuniversity([FromQuery] int idRequest)
		{
			GetUniversityResultDTO result = _service.FindUniversity(idRequest);

			if (result.HasErrors)
			{
				return base.BadRequest(GetErrorMessages(result.ErrorMessages));
			}

			return Ok(result);

		}

		// POST api/<UniversitiesController>
		[HttpPost("CreateUniversity")]
		public IActionResult CreateUniversity([FromForm] CreateUniversityRequestDTO request)
		{
			InsertUniversityResultDTO result = _service.CreateNewUniversity(request);

			if (result.HasErrors)
			{
				return BadRequest(GetErrorMessages(result.ErrorMessages));
			}

			return Ok(result);
		}

		// PUT api/<UniversitiesController>/5
		[HttpPut("UpdateUniversity")]
		public IActionResult UpdateUniversityData([FromForm] UpdateUniversityRequestDTO request)
		{
			InsertUniversityResultDTO result = _service.UpdateOldUniversity(request);

			if (result.HasErrors)
			{
				return BadRequest(GetErrorMessages(result.ErrorMessages));
			}

			return Ok(result); 
		}

		// DELETE api/<UniversitiesController>/5
		[HttpDelete("DeleteUniversity")]
		public IActionResult DeleteUniversity([FromQuery] int idRequest)
		{
			DeleteUniversityResultDTO result = _service.DeleteExistentUniversity(idRequest);

			if (result.HasErrors)
			{
				return BadRequest(GetErrorMessages(result.ErrorMessages));
			}

			return Ok(result);

		}

		private List<string?> GetErrorMessages(List<ErrorEnum> errors)
		{

			List<string?> errorStrings = errors.Select(x => Enum.GetName(typeof(ErrorEnum), x)).ToList();

			return errorStrings;
		}
	}
}
