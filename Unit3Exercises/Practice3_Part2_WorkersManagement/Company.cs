using ConsoleMenu;


namespace Practice3_Part2_WorkersManagement
{
	internal class Company
	{
		internal enum User
		{
			ADMIN,
			MANAGER,
			TECHNICIAN
		}

		User UserCredential;
		string CredentialStr;

		int CurrentUserID;

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

		public ITWorker GetUser()
		{
			ITWorker user = ITWorkers[CurrentUserID];
			return user;
		}

		public bool UserExists(int userId)
		{
			if (ITWorkers.Contains(ITWorkers[userId]))
			{
				SetUserCredential(userId);
				CurrentUserID = userId;
				return true;
			}
			return false;
		}

		public void SetUserCredential(int userId)
		{
			if (userId == 0)
			{
				UserCredential = User.ADMIN;
				CredentialStr = "ADMIN";
			}
			if (userId != 0)
			{
				foreach (Team team in Teams) 
				{
					if (team.GetManagerId() == userId) 
					{
						UserCredential = User.MANAGER;
						CredentialStr = "MANAGER";
						return;
					}
				}
				UserCredential = User.TECHNICIAN;
				CredentialStr = "TECHNICIAN";
			}
		}

		public User GetUserCredential()
		{
			return UserCredential;

		}

		public string GetCredentialTitle()
		{
			return CredentialStr;
		}

		public void RegisterITWorker()
		{
			bool isValid = false;
			while (!isValid)
			{
				//Ask and check the inputs and restart if some is wrong
				Menu.Print("----new IT worker register menu----\nIntroduce the data to create a new team.");

				string name = Menu.GetValidStringInputClear("Worker's name:");
				if (name == null) continue;

				string surname = Menu.GetValidStringInputClear("Worker's surname:");
				if (surname == null) continue;

				DateTime birth = Menu.GetValidDateInput("Birthdate <dd-MM-yyyy>:");
				if (birth == DateTime.MinValue) continue;
				int age = DateTime.Today.Year - birth.Year;
				if (age < 18)
				{
					Menu.PrintError("Worker must have at least 18 years old.");
					continue;
				}

				DateTime leaving = Menu.GetValidDateInput("Date worker will leave <dd-MM-yyyy>:");
				if (leaving == DateTime.MinValue) continue;

				int experience = Menu.GetValidIntInput("Years of experience:");
				if (experience == Menu.ERROR_VALUE) continue;

				List<string> knowledges = GetKnowledges();

				//If everything is valid, the worker is created and added to the list
				ITWorker worker = new ITWorker(name, surname, experience, knowledges, birth, leaving);
				ITWorkers.Add(worker);

				Menu.Print($"New Worker {worker.GetFullName()} with Id {worker.GetId()} registered.\n" +
					$"Experience: {experience} years | Knowledges: {knowledges} | Age: {age}");
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

				string name = Menu.GetValidStringInputClear("Team name:");
				if (name == null) continue;

				int managerId = GetValidManagerId();
				if (managerId == Menu.ERROR_VALUE) continue;

				ITWorker manager = ITWorkers[managerId];

				// Verify required level
				if (manager.GetLevel() == ITWorker.Level.Senior)
				{
					Team team = new Team(name, manager);
					Teams.Add(team);
					team.AddWorker(manager);
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
				string description = Menu.GetValidStringInputClear("Task description:");
				if (description == null) continue;

				string technology = Menu.GetValidStringInputClear("Technology that must be used:");
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
			Menu.Print("===========================================================");
			Menu.Print("These are all the teams registered in the company:\n" +
			"\n---------------------------------------------\n");
			foreach (Team team in Teams)
			{
				Menu.Print($"- {team.GetName()}");
			}
			Menu.Print("\n---------------------------------------------\n");
		}

		public void ListAllTeamMembersByTeam()
		{
			Console.Clear();
			Menu.Print("===========================================================");
			Menu.Print("These are all team members classified by team:\n" +
			"\n---------------------------------------------\n");

			List<int> workersInTeams = new();

			//Loop through teams and list workers
			foreach (Team team in Teams)
			{
				Menu.Print($"--- {team.GetName()} - manager: {team.GetManagerString()} ---\n");
				bool hasMembers = false;

				foreach (ITWorker worker in team.GetWorkers())
				{
					hasMembers = true;
					workersInTeams.Add(worker.GetId()); 
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

		public void ListTeamMembersOfTeam()
		{
			Console.Clear();
			Menu.Print("===========================================================");
			Menu.Print("These are all team members in your team:\n" +
			"\n---------------------------------------------\n");

			List<int> workersInTeams = new();

			//Loop through teams and list workers
			foreach (Team team in Teams)
			{
				if (team.GetManagerId() == CurrentUserID)
				{
					Menu.Print($"--- {team.GetName()} - manager: {team.GetManagerString()} ---\n");

					bool hasMembers = false;

					foreach (ITWorker worker in team.GetWorkers())
					{
						hasMembers = true;
						workersInTeams.Add(worker.GetId());
						Menu.Print($"* {worker.GetFullName()} (Id-{worker.GetId()})");
					}
					// If team has no members
					if (!hasMembers) Menu.Print("No members in this team.");
				}
				break;
			}
			Menu.Print("\n---------------------------------------------\n");
		}

		public void ListUnassignedTasks()
		{
			Console.Clear();
			Menu.Print("===========================================================");
			Menu.Print("These are all the unassigned tasks:\n" +
			"\n---------------------------------------------\n");

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
			Menu.Print("===========================================================");
			Menu.Print("These are all the assigned tasks:\n" +
			"\n---------------------------------------------\n");
			foreach (Team team in Teams)
			{
				Menu.Print($"--- {team.GetName()} - manager: {team.GetManagerString()} ---\n");
				bool hasTasks = false;
				foreach (Task task in Tasks)
				{
					//Check if the worker is in the team
					bool workerInTeam = false;

					foreach (ITWorker worker in team.GetWorkers())
					{
						if (worker.GetId() == task.GetWorkerId())
						{
							workerInTeam = true;
							break;
						}
					}
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

		public void ListTasksAssignedToTeam()
		{
			Console.Clear();
			Menu.Print("===========================================================");
			Menu.Print("These are all the tasks assigned to your team(s):\n" +
			"\n---------------------------------------------\n");
			foreach (Team team in Teams)
			{
				Menu.Print($"--- {team.GetName()} - manager: {team.GetManagerString()} ---\n");

				bool hasTasks = false;

				if (team.GetWorkers().Contains(ITWorkers[CurrentUserID]))
				{
					foreach (Task task in Tasks)
					{
						//Check if the worker is in the team
						bool workerInTeam = false;

						foreach (ITWorker worker in team.GetWorkers())
						{
							if (worker.GetId() == task.GetWorkerId())
							{
								workerInTeam = true;
								break;
							}
						}
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
				else Menu.PrintError("Worker is not in any team.");
			}
		}

		public void AssignTeamManager()
		{
			Console.Clear();
			bool isValid = false;
			while (!isValid)
			{
				string teamName = Menu.GetValidStringInputClear("\n---------------------------------------------\n" +
				"Write the team's name where you want to assign a manager:\n");
				if (teamName == null)
				{
					Menu.PrintError("Empty name not valid.");
					continue;
				}
				int managerId = Menu.GetValidIntInput("Introduce the worker's ID:");
				if (managerId == Menu.ERROR_VALUE || managerId > Worker.IdCount)
				{
					Console.Clear();
					Menu.PrintError("Worker ID unvalid.");
					continue;
				}
				try
				{
					ITWorker manager = ITWorkers[managerId];
					if (ITWorkers[managerId].GetLevel() == ITWorker.Level.Senior)
					{
						if (manager.IsManager())
						{
							Console.Clear();
							Menu.PrintError("This worker is already a manager in a team.");
							continue;
						}

						foreach (Team team in Teams)
						{
							if (team.GetName().ToLower().Equals(teamName.ToLower()))
							{
								team.SetManager(manager);
								Menu.Print($"Worker {manager.GetFullName()} Id-{managerId} assigned as team {team.GetName()} manager.");
								isValid = true;
								break;
							}
						}
						Menu.PrintError("Team doesn't exist.");
					}
					else
					{
						Console.Clear();
						Menu.PrintError("Worker must be senior to be a manager in a team.");
					}
				}
				catch (Exception)
				{
					Console.Clear();
					Menu.PrintError("Worker ID not found.");
					
				}
			}
		}

		public void AddTechnicianToTeam()
		{
			bool isValid = false;
			while (!isValid)
			{
				string teamName = Menu.GetValidStringInputClear("\n---------------------------------------------\n" +
					"Write the team's name where you want to add a technician:\n");
				if (teamName == null)
				{
					Menu.PrintError("Empty name not valid.");
					continue;
				}

				int workerId = Menu.GetValidIntInput("Introduce the worker's ID to add them to the team:");
				if (workerId == Menu.ERROR_VALUE || workerId > Worker.IdCount)
				{
					Menu.PrintError("Worker ID unvalid");
					continue;
				}
				foreach (Team team in Teams)
				{
					if (team.GetName().ToLower().Equals(teamName.ToLower()))
					{
						try
						{
							team.AddWorker(ITWorkers[workerId]);
							Menu.Print($"TWorker {ITWorkers[workerId].GetFullName()} ID-{ITWorkers[workerId].GetId()} assigned successfully to team {team.GetName()}");
							isValid = true;
							return;
						}
						catch (Exception)
						{
							Menu.PrintError("Worker not found.");
							AddTechnicianToTeam();
						}
					}
				}
				Menu.PrintError("Team doesn't exist.");
			}
		}

		public void AssignTask()
		{
			bool isValid = false;
			while (!isValid)
			{
				Menu.PrintMenu("Task ID to assign:\n");
				int taskId = Menu.GetInputParsedInt();
				if (taskId == Menu.ERROR_VALUE || taskId >= Task.IdCount)
				{
					Menu.PrintError("Task ID unvalid.");
					continue;
				}
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
							Menu.Print($"Task ID-{taskId} {Tasks[taskId].GetDescription()} assigned succesfully to worker {ITWorkers[workerId].GetFullName()} ID-{ITWorkers[workerId].GetId()}");
							return;
						}
						else 
						{
							Menu.PrintError("Worker has not the correct knowledge");
							continue;
						}
					}
					else
					{
						Menu.PrintError("Task already DONE");
						continue;
					}
				}
			}
		}
		
		public void AssignTaskToCurrentUser()
		{
			bool isValid = false;
			while (!isValid)
			{

				int taskId = Menu.GetValidIntInput("Task ID to assign to yourself:");

				if (taskId == Menu.ERROR_VALUE || taskId >= Task.IdCount)
				{
					Menu.PrintError("Task ID unvalid.");
					continue;
				}
				else
				{
					if (Tasks[taskId].GetStatus() != Task.Status.Done)
					{
						List<string> knowledges = ITWorkers[CurrentUserID].GetKnowledges();

						if (knowledges.Contains(Tasks[taskId].GetTechnology()))
						{
							Tasks[taskId].AssignWorker(CurrentUserID);
							Menu.Print($"Task ID-{taskId} {Tasks[taskId].GetDescription()} assigned succesfully to worker {ITWorkers[CurrentUserID].GetFullName()} ID-{CurrentUserID}");
							return;
						}
						else Menu.PrintError("Worker has not the correct knowledge");
					}
					else Menu.PrintError("Task already DONE");
				}
			}
		}

		public void UnregisterITWorker()
		{
			Menu.PrintMenu("Write the id of the worker you want to unregister:");
			int input = Menu.GetInputParsedInt();

			if (input > Menu.ERROR_VALUE && input < Worker.IdCount)
			{
				ITWorker worker = ITWorkers[input];
				foreach (Task task in Tasks)
				{
					if (task.GetWorkerId() == input) task.UnassignWorker();
				}
				foreach (Team team in Teams)
				{
					if (team.IsManager(worker))
					{
						Menu.PrintError($"You cannot remove this worker because they are a manager in {team.GetName()}.\n" +
						$"Please assign another manager first.");
						return;
					}
					team.DeleteWorker(worker);
					Menu.Print($"Worker {worker.GetFullName()} with ID-{worker.GetId()} removed from {team.GetName()} team.");
				}
				if (!ITWorkers.Contains(worker))
				{
					Menu.PrintError("Worker ID not found.");
					return;
				}
				ITWorkers.Remove(worker);
				Menu.Print($"Worker {worker.GetFullName()} with ID-{worker.GetId()} removed from database.");
			}
			else Menu.PrintError("Invalid ID.");
		}

		static List<string> GetKnowledges()
		{
			List<string> knowledges = new();
			while (true)
			{
				string knowledge = Menu.GetValidStringInputClear("Add a knowledge:");
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
			ITWorkers.Add(new("Felix", "Leigo", 10, new() { "python", "sql", "java" }, DateTime.ParseExact("20-02-1987", "dd-MM-yyyy", null), DateTime.ParseExact("15-02-2029", "dd-MM-yyyy", null)));
			ITWorkers.Add(new("Ramona", "Misiva", 7, new() { "sql", "java" }, DateTime.ParseExact("05-04-1998", "dd-MM-yyyy", null), DateTime.ParseExact("15-02-2029", "dd-MM-yyyy", null)));
			ITWorkers.Add(new("Leticia", "Olivia", 2, new() { "python", "java" }, DateTime.ParseExact("25-12-2003", "dd-MM-yyyy", null), DateTime.ParseExact("15-02-2029", "dd-MM-yyyy", null)));
			ITWorkers.Add(new("Nathan", "Carrera", 5, new() { "sql", "css" }, DateTime.ParseExact("12-08-1995", "dd-MM-yyyy", null), DateTime.ParseExact("15-02-2029", "dd-MM-yyyy", null)));
			ITWorkers.Add(new("Pol", "Rubio", 4, new() { "java", "c#" }, DateTime.ParseExact("29-06-2000", "dd-MM-yyyy", null), DateTime.ParseExact("15-02-2029", "dd-MM-yyyy", null)));

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