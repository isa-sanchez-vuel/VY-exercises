
using System.Text.Json.Serialization;

namespace SWApiManagement.Infrastructure.Contracts.APIEntities
{
	public class ResidentJson
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("url")]
		public string Url { get; set; }
	}
}

/*
		public string height { get; set; }
		public string mass { get; set; }
		public string hair_color { get; set; }
		public string skin_color { get; set; }
		public string eye_color { get; set; }
		public string birth_year { get; set; }
		public string gender { get; set; }
		public string homeworld { get; set; }
		public string[] films { get; set; }
		public object[] species { get; set; }
		public string[] vehicles { get; set; }
		public string[] starships { get; set; }
		public DateTime created { get; set; }
		public DateTime edited { get; set; }
*/