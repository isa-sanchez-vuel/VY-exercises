
namespace Countries.Domain
{
	public class CountryListModel
	{
		public List<CountryModel> CountryList { get; set; }

		public List<CountryModel>? GetCountriesByInitial(char initial)
		{
			if(CountryList == null) return null;

			var result = CountryList.FindAll(x => x.Name[0].ToString().ToLower() == initial.ToString().ToLower());
			if (CountryList.Count > 0) return result;
			else return null;
		}

	}
}
