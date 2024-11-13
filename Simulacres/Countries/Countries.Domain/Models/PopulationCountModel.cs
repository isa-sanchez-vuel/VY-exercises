using System.ComponentModel.DataAnnotations.Schema;

namespace Countries.Domain.Models
{
    public class PopulationCountModel
    {
        public int Id { get; set; }
        public int Counter { get; set; }
        public DateTime Year { get; set; }
    }
}
