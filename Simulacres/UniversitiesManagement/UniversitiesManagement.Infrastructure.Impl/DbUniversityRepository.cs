﻿using System.Security.Cryptography.X509Certificates;
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

			foreach (var uni in newUniversities)
			{
				/*
				 List<Domain> domains = new(); 
				if(uni.Domains != null) {
					domains = uni.Domains.Select(d => new Domain() { DomainUrl = d }).ToList();
				}
				List<WebPage> pages = new(); 
				if(uni.WebPages != null) {
					pages = uni.WebPages.Select(w => new WebPage() { WebUrl = w }).ToList();
				}
				 
				 */
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


				University? oldUni = _context.Universities.FirstOrDefault(x => x.AlphaTwoCode.ToLower().Equals(newUni.AlphaTwoCode.ToLower()));

				if (oldUni == null)
				{
					_context.Add(newUni);
				}
				else UpdateUniversity(newUni);
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
			University? oldUni = _context.Universities.FirstOrDefault(x => x.AlphaTwoCode.ToLower().Equals(newUni.AlphaTwoCode.ToLower()));

			if (oldUni == null)
			{
				_context.Universities.Add(newUni);

				_context.SaveChanges();

				return newUni;
			}

			return null;
		}

		public University? UpdateUniversity(University updatedUni)
		{
			University? oldUni = _context.Universities.FirstOrDefault(x => x.Id == updatedUni.Id);

			if (oldUni != null)
			{
				if (updatedUni.Name != null) oldUni.Name = updatedUni.Name;
				if(updatedUni.Country != null) oldUni.Country = updatedUni.Country;
				if (updatedUni.StateProvince != null) oldUni.StateProvince = updatedUni.StateProvince;
				//oldUni.Domains = updatedUni.Domains;
				//oldUni.WebPages = updatedUni.WebPages;

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
