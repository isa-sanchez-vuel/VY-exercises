using Countries.XCutting.GlobalVariables;
using System.Globalization;

namespace Countries.Domain
{
	public class InputValidator
	{

		public static bool ValidateYear(string year)
		{
			bool result;

			result = int.TryParse(year, out int yearControl);

			if(result) result = yearControl >= GlobalVariables.MIN_YEAR && yearControl <= GlobalVariables.MAX_YEAR;

			if (result) result = DateTime.TryParseExact(year, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

			//returns true if data is valid, else it will return false
			return result;
		}

		public static bool ValidateChar(string firstCharString)
		{
			bool result;

			result = firstCharString.Length == 1;

			if (result) result = char.IsLetter(firstCharString[0]);

			//returns true if data is valid, else it will return false
			return result;
		}


	}
}
