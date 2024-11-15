
namespace SWApiManagement.Domain.Models
{
	public class PlanetListModel
	{
		public int Count { get; set; }
		public string? Next { get; set; }
		public string? Previous { get; set; }
		public List<PlanetModel>? Planets { get; set; }
	}
}
