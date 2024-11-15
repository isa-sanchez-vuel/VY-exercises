using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SWApiManagement.Application.Contracts;
using SWApiManagement.Application.Contracts.DTOs;
using SWApiManagement.XCutting.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWApiManagement.Presentation.WebApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class PlanetsController : ControllerBase
	{
		private readonly IPlanetService _service;
		private readonly IConfiguration config;

		public PlanetsController(IPlanetService planetService, IConfiguration config2)
		{
			_service = planetService;
			config = config2;
		}


		// GET: api/<PlanetsController>
		[HttpGet("UpdateDatabase")]
		public async Task<IActionResult> UpdateDatabase()
		{
			UpdateResultDTO result = await _service.UpdateDatabaseWithApi();

			if (result == null) return BadRequest();
			else if (result.HasErrors)
			{
				return BadRequest(result.Message);
			}
			else return Ok(result);
		}

		// GET api/<PlanetsController>/Name
		[HttpGet("GetResidentsOfPlanet")]
		public async Task<IActionResult> GetResidents([FromQuery] string? planetName)
		{
			if (planetName == null) return BadRequest();
			ResidentResultDTO result = await _service.GetResidentsOfPlanet(planetName);

			if (result == null) return BadRequest();
			else if (result.HasErrors)
				return result.Error switch
				{
					ErrorEnum.PlanetNotFound => NotFound(result.Message),
					_ => BadRequest(result.Message)
				};
			return Ok(result);
		}

	}
}
 
