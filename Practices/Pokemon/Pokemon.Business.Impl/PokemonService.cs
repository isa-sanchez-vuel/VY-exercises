using Pokemon.Business.Contracts;
using Pokemon.Business.Contracts.DTOs;
using Pokemon.Domain;
using Pokemon.Infrastructure.Contracts;
using Pokemon.Infrastructure.Contracts.FromJson;
using Pokemon.XCutting.Enums;
using System.Text.Json;

namespace Pokemon.Business.Impl
{
    public class PokemonService : IPokemonService
	{
		private readonly IPokeApiImporter _importer;
		private readonly IPokemonRepository _repository;
		public PokemonService(IPokeApiImporter importer, IPokemonRepository pokemonRepository) 
		{
			_importer = importer;
			_repository = pokemonRepository;
		}

		public async Task<CountByInitialResultDTO> CountByInitial(string firstStringCharacter)
		{
			CountByInitialResultDTO result = new()
			{
				HasErrors = true,
				Error = null,
			};

			if (firstStringCharacter.Length != 1) result.Error = CountByInitialErrorEnum.NotOneCharacter;
			char firstCharacter = firstStringCharacter[0];

			if (_repository == null) result.Error = CountByInitialErrorEnum.RepositoryNull;
			else if(_importer == null) result.Error = CountByInitialErrorEnum.ImporterNull;
			else
			{
				await _importer.ImportApiJson();
				string? json = _importer.GetJson();
				if (json == null) result.Error = CountByInitialErrorEnum.JsonNull;
				else
				{
					PokemonPageFromJson? pokemonDeserialized = JsonSerializer.Deserialize<PokemonPageFromJson>(json);

					if (pokemonDeserialized == null) result.Error = CountByInitialErrorEnum.DeserializedNull;
					else
					{
						PokemonListModel domainModel = new()
						{
							Names = pokemonDeserialized?.PokemonList?.Select(x => x.Name).ToList(),
						};

						//var results = new List<ValidationResult>();
						//var context = new ValidationContext(domainModel, null, null);
						//if (!Validator.TryValidateObject(domainModel, context, results))
						//{
						//	result.Error = CountByInitialErrorEnum.ObjectInvalid;
						//}

						result.Count = domainModel.CountNumberOfPokemonsByLetter(firstCharacter);
						result.HasErrors = false;
					}
					
				}
			}
			return result;
		}
	}
}
