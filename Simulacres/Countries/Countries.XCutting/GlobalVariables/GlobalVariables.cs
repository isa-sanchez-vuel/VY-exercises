namespace Countries.XCutting.GlobalVariables
{
	public class GlobalVariables
	{
		//Api
		public const string CONFIG_CONNECTION_STRING = "ApiUrlConnection";

		public const int MIN_YEAR = 1960;
		public const int MAX_YEAR = 2018;



		//Unit testing

		public const string TEST_JSON_SUCCESS = "{\"error\":false,\"msg\":\"all countries and population 1961 - 2018\",\"data\":[]}";
		public const string TEST_JSON_ERROR = "{\"error\":true,\"msg\":\"all countries and population 1961 - 2018\",\"data\":[]}";
		
		public const string COUNTRY_NAME = "Test";
		public const string COUNTRY_CODE = "T";

		public const int COUNT_RESULT = 100;

		public const string CORRECT_COUNTRY_CHAR = "t";
		public const string CORRECT_POPULATION_YEAR = "2000";
		public const string WRONG_COUNTRY_CHAR = "a";
		public const string WRONG_POPULATION_YEAR = "1000";





	}
}
