namespace Countries.Domain.Models
{
    public class CountryListModel
    {
        public List<CountryModel>? CountryList { get; set; }

        public List<CountryModel>? GetCountriesByInitial(string initial)
        {
            if (CountryList == null) return null;

            var result = CountryList.FindAll(x => x.Name.ToLower().StartsWith(initial.ToLower()));
			if (CountryList.Count > 0) return result;
            else return null;
        }

    }
}
