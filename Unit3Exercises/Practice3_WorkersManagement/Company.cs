using ConsoleMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Practice3_WorkersManagement
{
	internal class Company
	{

		List<ITWorker> ITWorkers;
		List<Team> Teams;
		List<Task> Tasks;

		public Company()
		{
			ITWorkers = new();
			Teams = new();
			Tasks = new();
			Initialize();
		}

		public void RegisterITWorker()
		{
			bool isValid = false;
			while (!isValid)
			{
				//Ask and check the inputs and restart if some is wrong
				Menu.Print("----new IT worker register menu----\nIntroduce the data to create a new team.");

				string name = Menu.GetValidStringInput("Worker's name:");
				if (name == null) continue;

				string surname = Menu.GetValidStringInput("Worker's surname:");
				if (surname == null) continue;

				DateTime birth = Menu.GetValidDateInput("Birthdate <dd-MM-yyyy>:");
				if (birth == DateTime.MinValue) continue;

				DateTime leaving = Menu.GetValidDateInput("Date worker will leave <dd-MM-yyyy>:");
				if (leaving == DateTime.MinValue) continue;

				int experience = Menu.GetValidIntInput("Years of experience:");
				if (experience == Menu.ERROR_VALUE) continue;

				List<string> knowledges = GetKnowledges();

				//If everything is valid, the worker is created and added to the list
				ITWorker worker = new ITWorker(name, surname, experience, knowledges, birth, leaving);
				ITWorkers.Add(worker);

				Menu.Print($"New Worker {worker.GetFullName()} with Id {worker.GetId()} registered.");
				isValid = true; // Break the loop
			}
		}

		public void RegisterNewTeam()
		{
			bool isValid = false;
			while (!isValid)
			{
				//Ask and check the inputs and restart if some is wrong
				Menu.Print("----new team creation menu----\nIntroduce the data to create a new team.");

				string name = Menu.GetValidStringInput("Team name:");
				if (name == null) continue;

				int managerId = GetValidManagerId();
				if (managerId == Menu.ERROR_VALUE) continue;

				ITWorker manager = ITWorkers[managerId];

				// Verify required level
				if (manager.GetLevel() == ITWorker.Level.Senior)
				{
					Teams.Add(new Team(name, manager));
					Menu.Print($"New team {name} registered with manager {manager.GetFullName()} Id-{manager.GetId()}.");
					isValid = true; //Break loop
				}
				else
				{
					Console.Clear();
					Menu.PrintError("A worker must be Senior to be assigned as Team Manager.\nPlease try again.");
				}
			}
		}

		public void RegisterNewTask() 
		{
			bool isValid = false;
			while (!isValid)
			{
				//Ask and check the inputs and restart if some is wrong
				string description = Menu.GetValidStringInput("Task description:");
				if (description == null) continue;

				string technology = Menu.GetValidStringInput("Technology that must be used:");
				if (technology == null) continue;
				
				Tasks.Add(new(description, technology)); //Add task

				Menu.Print("New task registered.");
				Menu.Print($"* Task-Id{Task.IdCount} | {description} >> with {technology}");

				isValid = true; // Break the loop
			}
		}

		public void ListAllTeamNames()
		{
			Console.Clear();
			Menu.Print("\n---------------------------------------------\n" +
				"These are all the teams registered in the company:\n");
			foreach (Team team in Teams)
			{
				Menu.Print($"- {team.GetName()}");
			}
			Menu.Print("\n---------------------------------------------\n");
		}

		public void ListAllTeamMembersByTeam()
		{
			Console.Clear();
			Menu.Print("\n---------------------------------------------\n" +
			"These are all team members by team:\n");

			HashSet<int> workersInTeams = new HashSet<int>();

			//Loop through teams and list workers
			foreach (Team team in Teams)
			{
				Menu.Print($"\n--- {team.GetName()} ---");
				bool hasMembers = false;

				foreach (ITWorker worker in team.GetWorkers())
				{
					hasMembers = true;
					workersInTeams.Add(worker.GetId()); //Checks if worker is part of a team
					Menu.Print($"* {worker.GetFullName()} (Id-{worker.GetId()})");
				}

				// If team has no members
				if (!hasMembers) Menu.Print("No members in this team.");
				Menu.Print("\n---------------------------------------------\n");
			}

			//"No-Team" category
			Menu.Print("\n--- Are not in a team ---");
			bool hasNoTeamWorkers = false;

			foreach (ITWorker worker in ITWorkers)
			{
				if (!workersInTeams.Contains(worker.GetId()))
				{
					hasNoTeamWorkers = true;
					Menu.Print($"* {worker.GetFullName()} (Id-{worker.GetId()})");
				}
			}

			if (!hasNoTeamWorkers) Menu.Print("No workers without a team.");
			Menu.Print("\n---------------------------------------------\n");
		}

		public void ListUnassignedTasks()
		{
			Console.Clear();
			Menu.Print("\n---------------------------------------------\n" +
					"These are all the unassigned tasks:\n");

			foreach (Task task in Tasks)
			{
				if (task.GetWorkerId() < 0)
				{
					Menu.Print($"* Task-Id{task.GetId()} | {task.GetDescription()} >> with {task.GetTechnology()}");
				}
			}

			Menu.Print("\n---------------------------------------------\n");
		}

		public void ListTasksAssignmentsByTeam()
		{
			Console.Clear();
			Menu.Print("\n---------------------------------------------\n" +
				"These are all the assigned tasks:\n");

			foreach (Team team in Teams)
			{
				Menu.Print($"\n--- {team.GetName()} - manager: {team.GetManagerString()} ---");

				bool hasTasks = false;

				foreach (Task task in Tasks)
				{
					//Check if the worker is in the team
					bool workerInTeam = team.GetWorkers().Any(worker => worker.GetId() == task.GetWorkerId());

					if (workerInTeam)
					{
						hasTasks = true;
						ITWorker worker = team.GetWorkers().First(w => w.GetId() == task.GetWorkerId());
						Menu.Print($"* Task Id-{task.GetId()} assigned to {worker.GetFullName()} (Id-{task.GetWorkerId()}) | {task.GetDescription()} >> with {task.GetTechnology()}");
					}
				}
				//Team has no tasks assigned
				if (!hasTasks)
				{
					Menu.Print("No tasks assigned to this team.");
				}
				Menu.Print("\n---------------------------------------------\n");
			}
		}

		public void AssignTeamManager()
		{
			Menu.PrintMenu("\n---------------------------------------------\n" +
				"Write the team's name where you want to assign a manager:\n");
			string input = Menu.GetInputString();
			if (input.Equals(Menu.ERROR_VALUE_S))
			{
				Menu.PrintError("Empty name not valid.");
				AssignTeamManager();
			}

				Menu.PrintMenu("Introduce the worker's ID:");
			int managerId = Menu.GetInputParsedInt();
			if (managerId == Menu.ERROR_VALUE || managerId > Worker.IdCount)
			{
				Menu.PrintError("Invalid ID");
				AssignTeamManager();
			}

			if (!input.Equals(Menu.ERROR_VALUE_S))
			{
				foreach (Team team in Teams)
				{
					if (team.GetName().ToLower().Equals(input.ToLower()))
					{
						try
						{
							ITWorker manager = ITWorkers[managerId];
							if (ITWorkers[managerId].GetLevel() == ITWorker.Level.Senior)
							{
								team.SetManager(manager);
								team.AddWorker(manager);
							}
						}
						catch (Exception)
						{
							Menu.PrintError("Worker ID not found.");
							throw;
						}
					}
				}
			}
		}

		public void AddTechnicianToTeam()
		{
			Menu.PrintMenu("\n---------------------------------------------\n" +
				"Write the team's name where you want to add a technician:\n");
			string input = Menu.GetInputString();
			if (input.Equals(Menu.ERROR_VALUE_S))
			{
				Menu.PrintError("Empty name not valid.");
				AssignTeamManager();
			}

			Menu.PrintMenu("Introduce the worker's ID to add them to the team:");
			int workerId = Menu.GetInputParsedInt();
			if (workerId == Menu.ERROR_VALUE || workerId > Worker.IdCount)
			{
				Menu.PrintError("Invalid ID");
				AssignTeamManager();
			}

			if (!input.Equals(Menu.ERROR_VALUE_S))
			{
				foreach (Team team in Teams)
				{
					if (team.GetName().ToLower().Equals(input.ToLower()))
					{
						try
						{
							team.AddWorker(ITWorkers[workerId]);
						}
						catch (Exception)
						{
							Menu.PrintError("Worker not found.");
							throw;
						}
					}
				}
			}
		}

		public void AssignTask()
		{
			Menu.PrintMenu("Task ID to assign:\n");
			int taskId = Menu.GetInputParsedInt();
			if (taskId == Menu.ERROR_VALUE || taskId >= Task.IdCount) Menu.PrintError("Task ID unvalid.");
			else
			{
				Menu.PrintMenu("Worker ID to assign the task:\n");
				int workerId = Menu.GetInputParsedInt();
				if (workerId == Menu.ERROR_VALUE || workerId >= Worker.IdCount) Menu.PrintError("Worker ID unvalid.");
				else if (Tasks[taskId].GetStatus() != Task.Status.Done)
				{
					List<string> knowledges = ITWorkers[workerId].GetKnowledges();

					if (knowledges.Contains(Tasks[taskId].GetTechnology()))
					{
						Tasks[taskId].AssignWorker(workerId);
						return;
					}
					else Menu.PrintError("Worker has not the correct knowledge");
				}
				else Menu.PrintError("Task already DONE");
			}
			AssignTask();
		}

		public void UnregisterITWorker()
		{
			Menu.PrintMenu("Write the id of the worker you want to unregister:");
			int input = Menu.GetInputParsedInt();

			if (input > Menu.ERROR_VALUE && input < Worker.IdCount)
			{
				foreach(Task task in Tasks)
				{
					if (task.GetWorkerId() == input) task.UnassignWorker();
				}
				foreach(Team team in Teams)
				{
					if(!team.DeleteWorker(ITWorkers[input]))
					{
						Menu.PrintError($"You cannot remove this worker because they are a manager in {team.GetName()}.\n" +
						$"Please assign another manager first.");
						return;
					}
				}
				if (!ITWorkers.Remove(ITWorkers[input])) Menu.PrintError("Worker ID not found.");
			}
			else Menu.PrintError("Invalid worker ID.");
		}

		static List<string> GetKnowledges()
		{
			List<string> knowledges = new();
			while (true)
			{
				string knowledge = Menu.GetValidStringInput("Add a knowledge:");
				if (knowledge == null) continue;

				knowledges.Add(knowledge);
				Menu.PrintMenu("Want to add another knowledge? Type 0 if you don't.");
				if (Menu.GetInputParsedInt() == 0) break;
			}
			return knowledges;
		}

		int GetValidManagerId()
		{
			Menu.PrintMenu("Manager ID:");
			int managerId = Menu.GetInputParsedInt();

			// Verify the ID and if the manager exists in the worker list
			if (managerId == Menu.ERROR_VALUE || managerId < 0 || managerId >= ITWorkers.Count)
			{
				Console.Clear();
				Menu.PrintError("Invalid Manager ID.");
				return Menu.ERROR_VALUE;
			}
			return managerId;
		}

		public void Initialize()
		{
			ITWorkers.Add(new("Felix", "Leigo", 10, new(){ "python", "sql", "java" }, DateTime.ParseExact("20-02-1987", "dd-MM-yyyy", null), DateTime.ParseExact("15-02-2029", "dd-MM-yyyy", null)));
			ITWorkers.Add(new("Ramona", "Misiva", 7, new(){ "sql", "java" }, DateTime.ParseExact("05-04-1998", "dd-MM-yyyy", null), DateTime.ParseExact("15-02-2029", "dd-MM-yyyy", null)));
			ITWorkers.Add(new("Leticia", "Olivia", 2, new(){ "python", "java" }, DateTime.ParseExact("25-12-2003", "dd-MM-yyyy", null), DateTime.ParseExact("15-02-2029", "dd-MM-yyyy", null)));
			ITWorkers.Add(new("Nathan", "Carrera", 5, new(){ "sql", "css" }, DateTime.ParseExact("12-08-1995", "dd-MM-yyyy", null), DateTime.ParseExact("15-02-2029", "dd-MM-yyyy", null)));
			ITWorkers.Add(new("Pol", "Rubio", 4, new(){ "java", "c#" }, DateTime.ParseExact("29-06-2000", "dd-MM-yyyy", null), DateTime.ParseExact("15-02-2029", "dd-MM-yyyy", null)));

			Teams.Add(new("Data Science", ITWorkers[3]));
			Teams.Add(new("Front End Web", ITWorkers[1]));
			Teams.Add(new("AI", ITWorkers[0]));

			Tasks.Add(new("Train AI for ChatGPT", "python"));
			Tasks.Add(new("Send eclipte to the moon", "java"));
			Tasks.Add(new("Clean BBDD", "sql"));
			Tasks.Add(new("Make API endpoint", "c#"));
			Tasks.Add(new("Finish webpage design", "css"));
			Tasks.Add(new("Fix BBDD tables", "sql"));

			Tasks[0].AssignWorker(2);
			Tasks[2].AssignWorker(1);
			Tasks[4].AssignWorker(3);

			Teams[0].AddWorker(ITWorkers[0]);
			Teams[0].AddWorker(ITWorkers[1]);
			Teams[0].AddWorker(ITWorkers[2]);
			Teams[0].AddWorker(ITWorkers[4]);

			Teams[1].AddWorker(ITWorkers[3]);
			Teams[1].AddWorker(ITWorkers[4]);

			Teams[2].AddWorker(ITWorkers[0]);
			Teams[2].AddWorker(ITWorkers[2]);
			Teams[2].AddWorker(ITWorkers[3]);
		}
	}
}
