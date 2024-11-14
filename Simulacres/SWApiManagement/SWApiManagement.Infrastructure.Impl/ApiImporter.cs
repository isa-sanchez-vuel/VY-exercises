using Microsoft.Extensions.Configuration;
using SWApiManagement.Infrastructure.Contracts;
using SWApiManagement.Infrastructure.Contracts.APIEntities;
using SWApiManagement.XCutting;
using System.Net.Http;
using System.Text.Json;

namespace SWApiManagement.Infrastructure.Impl
{
	public class ApiImporter : IApiImporter
	{
		private readonly IConfiguration _configuration;

		public ApiImporter(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<PlanetListJson?> ImportApiData()
		{
			using HttpClient client = new();
			HttpResponseMessage data = await client.GetAsync(_configuration[GlobalVariables.API_URL_STRING]);
			string dataString = await data.Content.ReadAsStringAsync();

			PlanetListJson? planetList = JsonSerializer.Deserialize<PlanetListJson>(dataString);

			return planetList;
		}

		public async Task<PlanetJson?> FindPlanetInApi(string planetUrl)
		{

			using HttpClient client = new();
			HttpResponseMessage data = await client.GetAsync(planetUrl);
			string dataString = await data.Content.ReadAsStringAsync();

			return JsonSerializer.Deserialize<PlanetJson>(dataString);
		}

		public async Task<List<string>> FindResidents(PlanetJson planet)
		{

			using HttpClient client = new();
	
			List<string> residents = new();

			foreach (string residentUrl in planet.Residents)
			{
				HttpResponseMessage data = await client.GetAsync(residentUrl);
				string residentStringData = await data.Content.ReadAsStringAsync();
				ResidentJson residentJson = JsonSerializer.Deserialize<ResidentJson>(residentStringData);
				if (residentJson != null)
				{
					string? residentName = residentJson.Name;
					if (residentName != null) residents.Add(residentName);
				}
			}

			return residents;
		}
	}
}
