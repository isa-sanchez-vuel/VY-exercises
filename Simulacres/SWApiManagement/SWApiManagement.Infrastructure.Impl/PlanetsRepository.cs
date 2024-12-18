﻿
using SWApiManagement.Infrastructure.Context;
using SWApiManagement.Infrastructure.Contracts;
using SWApiManagement.Infrastructure.Contracts.APIEntities;
using SWApiManagement.Infrastructure.Contracts.DBEntities;

namespace SWApiManagement.Infrastructure.Impl
{
	public class PlanetsRepository : IPlanetsRepository
	{
		private readonly SWDBPlanetsContext _context;

		public PlanetsRepository(SWDBPlanetsContext context)
		{
			_context = context;
		}

		public List<Planet>? UpdateDatabase(List<PlanetJson> planetsImported)
		{
			foreach (var x in planetsImported)
			{
				Planet newPlanet = new()
				{
					Name = x.Name,
					Url = x.Url,
					Climate = x.Climate,
					Population = int.Parse(x.Population),
					OrbitalPeriod = int.Parse(x.Orbital_period),
					RotationPeriod = int.Parse(x.Rotation_period),
					Residents = x.Residents.Select(y => new Resident()
					{
						Url = y
					}).ToList(),

				};

				Planet? existentPlanet = _context.Planets.FirstOrDefault(x => x.Url.ToLower().Equals(newPlanet.Url));

				if (existentPlanet == null)
				{
					_context.Planets.Add(newPlanet);
				}
				else
				{
					existentPlanet.Name = newPlanet.Name;
					existentPlanet.Population = newPlanet.Population;
					existentPlanet.Climate = newPlanet.Climate;
					existentPlanet.OrbitalPeriod = newPlanet.OrbitalPeriod;
					existentPlanet.RotationPeriod = newPlanet.RotationPeriod;
					existentPlanet.Residents = newPlanet.Residents;
				}
			}
			_context.SaveChanges();

			return _context.Planets.ToList();
		}

		public Planet? GetPlanet(string name)
		{
			return _context.Planets.FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower()));
		}

		public List<Planet> GetAllPlanets()
		{
			return _context.Planets.ToList();
		}
	}
}
