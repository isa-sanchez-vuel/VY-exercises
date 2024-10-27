using ConsoleMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice3_Part2_WorkersManagement
{
	internal class Team
	{
		string Name;
		ITWorker Manager;
		List<ITWorker> Technicians;

		public Team(string name, ITWorker manager)
		{
			Technicians = new();
			Name = name;
			Manager = manager;
		}

		public void SetManager(ITWorker newManager)
		{
			Manager.SetManager(false);
			Manager = newManager;
			Manager.SetManager(true);
			Technicians.Add(newManager);
		}

		public bool IsManager(ITWorker worker)
		{
			if (worker.GetId() == Manager.GetId()) return true;
			else return false;
		}

		public string GetName()
		{
			return Name;
		}

		public int GetManagerId()
		{
			return Manager.GetId();
		}

		public string GetManagerString()
		{
			return Manager.GetFullName() +" Id-" + Manager.GetId();
		}

		public void AddWorker(ITWorker worker)
		{
			Technicians.Add(worker);
		}

		public void DeleteWorker(ITWorker worker)
		{
			
			Technicians.Remove(worker);
		}

		public List<ITWorker> GetWorkers()
		{
			return Technicians;
		}
	}
}
