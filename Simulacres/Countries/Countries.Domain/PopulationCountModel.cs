using System.ComponentModel.DataAnnotations.Schema;

namespace Countries.Domain
{
	public class PopulationCountModel
	{
		public int Id { get; set; }
		public int Counter { get; set; }
		public DateTime Year { get; set; }
	}
}
