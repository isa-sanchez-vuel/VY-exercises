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

        public bool PopulationNull { get; set; }


        public int GetPopulationFromYear(string year)
        {
            PopulationCountModel? populationFiltered;

            populationFiltered = PopulationCounts.Find(x => x.Year.Year.ToString().Equals(year));

            if (populationFiltered == null)
            {
                PopulationNull = true;
                return 0;
            }

            PopulationNull = false;
            return populationFiltered.Counter;
        }

    }
}
