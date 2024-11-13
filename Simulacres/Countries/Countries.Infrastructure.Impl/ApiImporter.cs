using Countries.Infrastructure.Contracts;
using Countries.Infrastructure.Contracts.JsonImport;
using Countries.XCutting.GlobalVariables;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Countries.Infrastructure.Impl
{
	public class ApiImporter : IApiImporter
	{

		private readonly IConfiguration _configuration;

		public ApiImporter(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<string> ImportData()
		{
			using HttpClient client = new();
			HttpResponseMessage data = await client.GetAsync(_configuration[GlobalVariables.CONFIG_CONNECTION_STRING]);
			string dataJsonString = await data.Content.ReadAsStringAsync();

			return dataJsonString;
		}
	}
}
