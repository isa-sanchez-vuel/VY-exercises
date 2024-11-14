namespace SWApiManagement.Domain.Models
{
	public class PlanetModel
	{
		public string Url { get; set; }
		public string Name { get; set; }

		public int Rotation_period { get; set; }
		public int Orbital_period { get; set; }
		public string Climate { get; set; }


		public int Population { get; set; }
		public List<string> Residents { get; set; }
	}
}
