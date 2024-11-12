
namespace LibraryManagement.Domain.Models
{
	public class PurchaseModel
	{
		public int PurchaseId { get; set; }
		public int BookId { get; set; }
		public DateTime Date { get; set; }
		public int Quantity { get; set; }
		public decimal TotalMoney { get; set; }

	}
}
