using System.ComponentModel.DataAnnotations;

namespace UniversitiesManagement.Domain
{
	public class UniversityModel
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? AlphaTwoCode { get; set; }
		public string? StateProvince { get; set; }
		public string? Country { get; set; }

		

		public static bool CheckAlphaCode(string? code) 
		{
			bool result;

			result = string.IsNullOrEmpty(code);
			if(!result) result = code.Length != 2;

			return !result;
		}

		public static bool CheckName(string? name) 
		{
			return !string.IsNullOrEmpty(name);
		}

		public static bool CheckCountry(string? country) 
		{
			return !string.IsNullOrEmpty(country);
		}

		public static bool CheckId(int id) 
		{
			bool result;

			result = id <= 0;
			if(!result) result = !int.TryParse(id.ToString(), out _);

			return !result;
		}


	}
}
