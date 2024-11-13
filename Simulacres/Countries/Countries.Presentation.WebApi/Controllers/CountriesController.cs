using Microsoft.AspNetCore.Mvc;
using Countries.Application.Contracts.DTOs;
using Countries.Application.Contracts;
using Countries.XCutting.Enums;

namespace Countries.Presentation.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountriesController : ControllerBase
	{
		private readonly ICountryService _service;

		public CountriesController(ICountryService service)
		{
			_service = service;
		}
		

		// GET: api/CountryList/5
		[HttpGet("GetCountriesPopulationByYear")]
		public async Task<IActionResult> GetCountriesPopulationByYear([FromQuery] CountryInitialYearRqtDTO request)
		{

			if (_service == null) return BadRequest();

			CountryInitialYearResultDTO result = await _service.GetCountriesByInitialAndYearPopulation(request);

			if(result == null) return StatusCode(StatusCodes.Status500InternalServerError);

			if (result.HasErrors)
			{ 
				if(result.Error != null) result.ErrorMessage = Enum.GetName(typeof(CountryInitialYearErrorEnum), result.Error!.Value);

				return result.Error switch
				{
					CountryInitialYearErrorEnum.RequestNull => StatusCode(StatusCodes.Status400BadRequest, result.ErrorMessage),
					CountryInitialYearErrorEnum.ImporterNull => StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage),
					CountryInitialYearErrorEnum.RepositoryNull => StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage),
					CountryInitialYearErrorEnum.FirstLetterNotAChar => StatusCode(StatusCodes.Status400BadRequest, result.ErrorMessage),
					CountryInitialYearErrorEnum.InvalidYear => StatusCode(StatusCodes.Status400BadRequest, result.ErrorMessage),
					CountryInitialYearErrorEnum.ListimportFailed => StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage),
					CountryInitialYearErrorEnum.ModelMapFailed => StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage),
					_ => StatusCode(StatusCodes.Status500InternalServerError, "Error unknown."),
				};
			}
				return Ok(result.Countries);
		}

	}
}
