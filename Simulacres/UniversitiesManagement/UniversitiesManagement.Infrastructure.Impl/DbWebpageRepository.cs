
using UniversitiesManagement.Infrastructure.Context;
using UniversitiesManagement.Infrastructure.Contracts;
using UniversitiesManagement.Infrastructure.Contracts.APIEntities;
using UniversitiesManagement.Infrastructure.Contracts.DBEntities;

namespace UniversitiesManagement.Infrastructure.Impl
{
	public class DbWebpageRepository : IDbWebpageRepository
	{
		readonly UniversitiesManagementContext _context;

		public DbWebpageRepository(UniversitiesManagementContext context)
		{
			_context = context;
		}

		public bool CreateWebpage(WebPage newWeb)
		{
			WebPage? oldWeb = _context.WebPages.FirstOrDefault(x => x.WebUrl.Equals(newWeb.WebUrl.ToLower()));

			if (oldWeb == null)
			{
				_context.WebPages.Add(newWeb);

				return true;
			}
			return false;
		}
	}
}
