
using System.Text.Json.Serialization;

namespace SWApiManagement.Infrastructure.Contracts.APIEntities
{
	public class PlanetJson
	{

		[JsonPropertyName("url")]
		public string Url { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }


		[JsonPropertyName("rotation_period")]
		public string Rotation_period { get; set; }

		[JsonPropertyName("orbital_period")]
		public string Orbital_period { get; set; }

		[JsonPropertyName("climate")]
		public string Climate { get; set; }



		[JsonPropertyName("population")]
		public string Population { get; set; }

		[JsonPropertyName("residents")]
		public List<string> Residents { get; set; }


		
	}
}

/*		
 		[JsonPropertyName("diameter")]
		public string Diameter { get; set; }
		[JsonPropertyName("gravity")]
		public string Gravity { get; set; }

		[JsonPropertyName("terrain")]
		public string Terrain { get; set; }

		[JsonPropertyName("surface_water")]
		public string Surface_water { get; set; }

		[JsonPropertyName("films")]
		public List<string> Films { get; set; }

		[JsonPropertyName("created")]
		public DateTime Created { get; set; }

		[JsonPropertyName("edited")]
		public DateTime Edited { get; set; }
*/