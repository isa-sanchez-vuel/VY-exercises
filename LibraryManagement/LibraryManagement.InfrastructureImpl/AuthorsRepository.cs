

using LibraryManagement.Infrastructure.Contracts;
using LibraryManagement.Infrastructure.Contracts.Context;

namespace LibraryManagement.InfrastructureImpl
{
	public class AuthorsRepository : IAuthorsRepository
	{
		private readonly LibraryManagementContext _context;

		public AuthorsRepository(LibraryManagementContext context)
		{
			_context = context;
		}
	}
}
