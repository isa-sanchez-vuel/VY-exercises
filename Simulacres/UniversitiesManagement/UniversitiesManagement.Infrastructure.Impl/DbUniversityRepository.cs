using UniversitiesManagement.Infrastructure.Context;
using UniversitiesManagement.Infrastructure.Contracts;
using UniversitiesManagement.Infrastructure.Contracts.APIEntities;
using UniversitiesManagement.Infrastructure.Contracts.DBEntities;

namespace UniversitiesManagement.Infrastructure.Impl
{
	public class DbUniversityRepository : IDbUniversityRepository
	{
		readonly UniversitiesManagementContext _context;

		public DbUniversityRepository(UniversitiesManagementContext context)
		{
			_context = context;
		}

		public List<University>? UpdateDatabaseWithApiData(List<UniversityJson> newUniversities)
		{

			List<University> unisToDelete = _context.Universities.Where(x => x.IsDeleted == false).ToList();
			_context.Universities.RemoveRange(unisToDelete);
			_context.SaveChanges();

			foreach (var uni in newUniversities)
			{
				University newUni = new()
				{
					Name = uni.Name,
					Country = uni.Country,
					AlphaTwoCode = uni.AlphaTwoCode,
					StateProvince = uni.StateProvince,
					Domains = uni.Domains.Select(y => new Domain()
					{
						DomainUrl = y
					}).ToList(),
					WebPages = uni.WebPages.Select(y => new WebPage()
					{
						WebUrl = y
					}).ToList(),
				};

				_context.Add(newUni);

				//University? oldUni = _context.Universities.FirstOrDefault(x => x.Name.ToLower().Equals(newUni.Name.ToLower()));

				//if (oldUni == null)
				//{ }
				//else
				//{
				//	newUni.Id = oldUni.Id;
				//	UpdateUniversity(newUni);
				//}
			}

			_context.SaveChanges();

			return _context.Universities.ToList();
		}

		public List<University>? GetAllUniversities()
		{
			return _context.Universities.ToList();
		}

		public University? GetUniversity(int code)
		{
			return _context.Universities.FirstOrDefault(x => x.Id.Equals(code) && !x.IsDeleted);
		}

		public University? CreateUniversity(University newUni)
		{
			University? oldUni = _context.Universities.FirstOrDefault(x => x.Name.ToLower().Equals(newUni.Name.ToLower()));

			if (oldUni == null)
			{
				_context.Universities.Add(newUni);

				_context.SaveChanges();

				return newUni;
			}

			return null;
		}

		public bool CreateWebPage(WebPage newWeb)
		{
			University? oldUni = _context.Universities.FirstOrDefault(x => x.Id == newWeb.UniversityId);

			if (oldUni != null)
			{
				oldUni.WebPages.Add(new() 
				{ 
					WebUrl = newWeb.WebUrl 
				});

				_context.SaveChanges();

				return true;
			}
			return false;
		}

		public University? UpdateUniversity(University updatedUni)
		{
			University? oldUni = _context.Universities.FirstOrDefault(x => x.Id == updatedUni.Id);

			if (oldUni != null)
			{
				if (updatedUni.Name != null) oldUni.Name = updatedUni.Name;
				if (updatedUni.Country != null) oldUni.Country = updatedUni.Country;
				if (updatedUni.StateProvince != null) oldUni.StateProvince = updatedUni.StateProvince;
				if (updatedUni.WebPages.Count > 0 && updatedUni.WebPages != null) oldUni.WebPages = updatedUni.WebPages;
				if (updatedUni.Domains.Count > 0 && updatedUni.Domains != null) oldUni.Domains = updatedUni.Domains;

				_context.SaveChanges();

				return oldUni;
			}

			return null;
		}

		public University? DeleteUniversity(int code)
		{
			University? uni = _context.Universities.FirstOrDefault(x => x.Id.Equals(code));

			if (uni != null)
			{
				uni.IsDeleted = true;

				_context.SaveChanges();
				return uni;
			}

			return null;
		}

	}
}
