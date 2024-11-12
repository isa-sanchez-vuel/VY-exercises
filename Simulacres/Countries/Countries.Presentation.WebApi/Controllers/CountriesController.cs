using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Countries.Infrastructure.Contracts.Entities;
using Countries.Presentation.WebApi.Data;
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

			if (result.HasErrors) return result.Error switch
				 {
					 CountryInitialYearErrorEnum.RequestNull => StatusCode(StatusCodes.Status400BadRequest),
					 CountryInitialYearErrorEnum.ImporterNull => StatusCode(StatusCodes.Status500InternalServerError),
					 CountryInitialYearErrorEnum.RepositoryNull => StatusCode(StatusCodes.Status500InternalServerError),
					 CountryInitialYearErrorEnum.FirstLetterNotOneChar => StatusCode(StatusCodes.Status400BadRequest),
					 CountryInitialYearErrorEnum.YearWrongFormat => StatusCode(StatusCodes.Status400BadRequest),
					 CountryInitialYearErrorEnum.ListimportFailed => StatusCode(StatusCodes.Status500InternalServerError),
					 CountryInitialYearErrorEnum.ModelMapFailed => StatusCode(StatusCodes.Status500InternalServerError),
					 _ => StatusCode(StatusCodes.Status500InternalServerError, "Error unknown."),
				 };

				return Ok(result.Countries);
		}

	}
}
