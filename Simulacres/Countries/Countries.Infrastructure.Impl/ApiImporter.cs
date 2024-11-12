
using Countries.Infrastructure.Contracts;
using Countries.XCutting.GlobalVariables;

namespace Countries.Infrastructure.Impl
{
	public class ApiImporter : IApiImporter
	{

		public async Task<string> ImportData()
		{
			using HttpClient client = new();
			HttpResponseMessage data = await client.GetAsync(GlobalVariables.API_URL);
			string dataJsonString = await data.Content.ReadAsStringAsync();

			return dataJsonString;
		}
	}
}
