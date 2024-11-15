using Microsoft.Extensions.Configuration;
using System.Text.Json;
using UniversitiesManagement.Infrastructure.Contracts;
using UniversitiesManagement.Infrastructure.Contracts.APIEntities;

namespace UniversitiesManagement.Infrastructure.Impl
{
	public class ApiRepository : IApiRepository
	{
		const string API_STRING_CONFIG = "ConnectionStrings:ApiConnection";
		readonly IConfiguration _config;

		public ApiRepository(IConfiguration config)
		{
			_config = config;
		}

		public async Task<UniversityListJson?> ImportApiData()
		{
			using HttpClient client = new();
			HttpResponseMessage response = await client.GetAsync(_config[API_STRING_CONFIG]);
			string? dataAsString = await response.Content.ReadAsStringAsync();
			List<UniversityJson>? jsonObject = JsonSerializer.Deserialize<List<UniversityJson>>(dataAsString);

			if (jsonObject != null)
			{
				UniversityListJson list = new()
				{
					Universities = jsonObject
				};
				return list;
			}
			return null;
		}

		public async Task<T?> ImportApiData<T>(string url)
		{
			using HttpClient client = new();

			HttpResponseMessage response = await client.GetAsync(url);
			string? dataAsString = await response.Content.ReadAsStringAsync();

			return JsonSerializer.Deserialize<T>(dataAsString);
		}
	}
}
