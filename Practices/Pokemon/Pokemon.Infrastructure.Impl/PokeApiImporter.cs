using Pokemon.XCutting.GlobalVariables;
using Pokemon.Infrastructure.Contracts;

namespace Pokemon.Infrastructure.Impl
{
	public class PokeApiImporter : IPokeApiImporter
	{
		private string? DataString;

		public async Task ImportApiJson()
		{
			using HttpClient client = new();
			HttpResponseMessage data = await client.GetAsync(GlobalVariables.API_URL);
			string dataStr = await data.Content.ReadAsStringAsync();
			DataString = dataStr;
		}

		public string? GetJson()
		{
			return DataString;
		}
	}
}
