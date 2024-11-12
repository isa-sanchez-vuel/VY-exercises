
using LibraryManagement.Infrastructure.Contracts;
using LibraryManagement.Infrastructure.Contracts.Context;

namespace LibraryManagement.InfrastructureImpl
{
	public class BooksRepository : IBooksRepository
	{
		private readonly LibraryManagementContext _context;

		public BooksRepository(LibraryManagementContext context)
		{
			_context = context;
		}
	}
}
