

using LibraryManagement.Infrastructure.Contracts;
using LibraryManagement.Infrastructure.Contracts.Context;

namespace LibraryManagement.InfrastructureImpl
{
	public class PurchasesRepository : IPurchasesRepository
	{
		private readonly LibraryManagementContext _context;

		public PurchasesRepository(LibraryManagementContext context)
		{
			_context = context;
		}

	}
}
