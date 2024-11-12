using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Business.Contracts;
using Pokemon.Business.Contracts.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Pokemon.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PokemonController : ControllerBase
	{
		private readonly IPokemonService _service;


		public PokemonController(IPokemonService service) 
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> ListPokemon(string firstLetter)
		{
			if (firstLetter.Length != 1) return NotFound("Entry must be only one character.");

			Task<CountByInitialResultDTO> result = _service.CountByInitial(firstLetter);

			CountByInitialResultDTO print = result.Result;

			return Ok(print.Count);
		}
	}
}
