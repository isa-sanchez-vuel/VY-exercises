namespace SWApiManagement.Domain
{
	public class DataValidator
	{
		public static bool StringIsValid(string? value)
		{
			return !string.IsNullOrEmpty(value);
		}
	}
}
