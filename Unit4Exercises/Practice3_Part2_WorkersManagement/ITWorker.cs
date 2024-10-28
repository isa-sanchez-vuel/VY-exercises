using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Practice3_Part2_WorkersManagement
{

	internal class ITWorker : Worker
	{
		internal enum Level
		{
			Junior,
			Medium,
			Senior
		}

		protected int YearsOfExperience;
		protected List<string> TechKnowledges;

		Level WorkerLevel;

		bool isManager;

		public ITWorker()
		{

		}

		public ITWorker(string newName, string newSurname, int experience, List<string> knowledges, DateTime birth, DateTime leaving)
		{
			TechKnowledges = new();
			Id = IdCount;
			IdCount++;
			Name = newName;
			Surname = newSurname;
			BirthDate = birth;
			LeavingDate = leaving;
			YearsOfExperience = experience;
			foreach (string k in knowledges)
			{
				TechKnowledges.Add(k);
			}
			SetLevel();
			isManager = false;
		}

		public void SetManager(bool isManager)
		{
			this.isManager = isManager;
		}

		public bool IsManager()
		{
			return isManager;
		}

		public void SetLevel()
		{
			if (YearsOfExperience >= 5)
			{
				WorkerLevel = Level.Senior;
			}
			else if (YearsOfExperience > 2)
			{
				WorkerLevel = Level.Medium;
			}
			else WorkerLevel = Level.Junior;
		}

		public Level GetLevel()
		{
			return WorkerLevel;
		}

		public string GetFullName()
		{
			return $"{Name} {Surname}";
		}

		public int GetId()
		{
			return Id;
		}

		public List<string> GetKnowledges()
		{
			return TechKnowledges;
		}

	}
}