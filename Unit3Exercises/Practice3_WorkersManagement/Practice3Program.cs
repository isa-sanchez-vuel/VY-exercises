using ConsoleMenu;
using Practice3_WorkersManagement;
using System.Text;

const int EXIT_OPTION = 12;

int LoginAttempts;

bool Exit;
bool Logged;

string UserId;

int Option;
Company Company;

Initialize();
StartApplication();

void Initialize()
{
	Company = new();
	Console.OutputEncoding = Encoding.UTF8;
}

void StartApplication()
{
	Exit = false;

	OpenMenu();

	if (!Exit) StartApplication();
	else if (Exit) ExitApplication();
}

void OpenMenu()
{
	Console.Clear();
	Menu.PrintMenu("Choose an action to execute:\n" +
		"	 1. Register new IT worker\n" +
		"	 2. Register new team\n" +
		"	 3. Register new task\n" +
		"	 4. List all team names\n" +
		"	 5. List team members by team name\n" +
		"	 6. List unassigned tasks\n" +
		"	 7. List task assignments by team name\n" +
		"	 8. Assign IT worker to a team as manager\n" +
		"	 9. Assign IT worker to a team as technician\n" +
		"	10. Assign task to IT worker\n" +
		"	11. Unregister IT worker\n" +
		$"	{EXIT_OPTION}. Exit" +
		"------------------------------------------------------");
	Option = Menu.GetInputParsedInt();

	if (Option > 0 && Option < EXIT_OPTION) ManageOptions();
	else if (Option == EXIT_OPTION) Exit = true;
	else Menu.PrintError("Invalid option. Try again.");

	if (!Exit) OpenMenu();
	
}

void ManageOptions()
{
	Console.Clear();

	switch (Option)
	{
		case 1:
			Company.RegisterITWorker();
			break;

		case 2:
			Company.RegisterNewTeam();
			break;

		case 3:
			Company.RegisterNewTask();
			break;

		case 4:
			Company.ListAllTeamNames();
			break;

		case 5:
			Company.ListAllTeamMembersByTeam();
			break;

		case 6:
			Company.ListUnassignedTasks();
			break;

		case 7:
			Company.ListTasksAssignmentsByTeam();
			break;

		case 8:
			Company.AssignTeamManager();
			break;

		case 9:
			Company.AddTechnicianToTeam();
			break;

		case 10:
			Company.AssignTask();
			break;

		case 11:
			Company.UnregisterITWorker();
			break;
	}

	AskCloseSession();
}

void AskCloseSession()
{
	Menu.PrintMenu($"Press {EXIT_OPTION} to close your session, press any other key if you wish to execute another action:");
	Option = Menu.GetInputParsedInt();
	if (Option == EXIT_OPTION) Exit = true;
}

void ExitApplication()
{
	Menu.Print("======================================\n" +
			"|| Closing application...            ||\n" +
			"======================================");
}
