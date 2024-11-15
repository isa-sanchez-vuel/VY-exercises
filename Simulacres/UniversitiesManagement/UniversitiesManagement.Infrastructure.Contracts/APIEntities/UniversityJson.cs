using System.Text.Json.Serialization;
using UniversitiesManagement.Infrastructure.Contracts.APIEntities;

namespace UniversitiesManagement.Infrastructure.Contracts.APIEntities
{
	public class UniversityJson
	{
		[JsonPropertyName("name")]
		public string? Name { get; set; }

		[JsonPropertyName("alpha_two_code")]
		public string? AlphaTwoCode { get; set; }

		[JsonPropertyName("stateprovince")]
		public string? StateProvince { get; set; }

		[JsonPropertyName("country")]
		public string? Country { get; set; }

		[JsonPropertyName("domains")]
		public List<string>? Domains { get; set; }

		[JsonPropertyName("web_pages")]
		public List<string>? WebPages { get; set; }
	}
}
