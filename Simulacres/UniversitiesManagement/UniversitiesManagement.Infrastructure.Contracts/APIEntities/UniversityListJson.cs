using System.Text.Json.Serialization;

namespace UniversitiesManagement.Infrastructure.Contracts.APIEntities
{
	public class UniversityListJson
	{
		[JsonPropertyName("Property1")]
		public List<UniversityJson> Universities { get; set; }
	}
}


//public class Rootobject
//{
//	public Class1[] Universities { get; set; }
//}

//public class Class1
//{
//	public string name { get; set; }
//	public string alpha_two_code { get; set; }
//	public string[] domains { get; set; }
//	public string stateprovince { get; set; }
//	public string country { get; set; }
//	public string[] web_pages { get; set; }
//}

