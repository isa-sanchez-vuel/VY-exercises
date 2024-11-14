using System.ComponentModel.DataAnnotations;

namespace Countries.Domain.Models
{
    public class CountryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Iso3 { get; set; }

        public List<PopulationCountModel> PopulationCounts { get; set; }

        public int GetPopulationFromYear(string year)
        {
            PopulationCountModel? populationFiltered;

            populationFiltered = PopulationCounts.Find(x => x.Year.Equals(year));

            if (populationFiltered == null) return -1;

            return populationFiltered.Counter;
        }

    }
}
