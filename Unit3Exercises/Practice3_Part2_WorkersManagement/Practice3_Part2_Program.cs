using ConsoleMenu;
using Practice3_Part2_WorkersManagement;
using System.Text;

const int EXIT_OPTION = 12;
const int MAX_LOGIN_ATTEMPTS = 5;

int LoginAttempts;

bool Exit;
bool Logged;

int Option;
int UserId;

Company Company;
ITWorker User;

Initialize();
StartApplication();


void Initialize()
{
	Company = new();
	User = new();
	UserId = -1;
	Console.OutputEncoding = Encoding.UTF8;
}


void StartApplication()
{
	LoginAttempts = 0;
	Exit = false;

	OpenLogin();
	if (Logged)
	{
		OpenMenu();

		if (!Exit) StartApplication();
		else if (Exit) ExitApplication();
	}
}


void OpenLogin()
{
	Console.Clear();
	Logged = false;

	if (LoginAttempts > 0 && LoginAttempts < MAX_LOGIN_ATTEMPTS - 1) Menu.PrintError($"Credentials not valid. Try again.\nAttempts left: {MAX_LOGIN_ATTEMPTS - LoginAttempts}.");
	else if (MAX_LOGIN_ATTEMPTS - LoginAttempts == 1) Menu.PrintError("Credentials not valid. This is your last attempt.");
	else if (LoginAttempts >= MAX_LOGIN_ATTEMPTS) Menu.PrintError("You cannot try to login again. Call the IT service to recover your credentials.");

	if (LoginAttempts < MAX_LOGIN_ATTEMPTS)
	{
		Console.WriteLine("Welcome to our application. Please login with your user ID.");

		UserId = Menu.GetValidIntInput("\nUser ID:");

		if (Company.UserExists(UserId))
		{
			User = Company.GetUser();
			Logged = true;
		}

		if (!Logged)
		{
			LoginAttempts++;
			OpenLogin();
		}
	}
}


void OpenMenu()
{
	Console.Clear();
	Menu.Print($"Welcome {Company.GetCredentialTitle()} {User.GetFullName()} ID-{UserId}! Choose an action to execute:\n");

	FilterOptionsByUser();

	Menu.Print($"\t{EXIT_OPTION}. Exit");
	Menu.PrintMenu("\n------------------------------------------------------");
	Option = Menu.GetInputParsedInt();

	if (Option > 0 && Option < EXIT_OPTION) 
	{
		if (Company.GetUserCredential() == Company.User.ADMIN) ManageOptionsAdmin();
		else if (Company.GetUserCredential() == Company.User.MANAGER) ManageOptionsManager();
		else if (Company.GetUserCredential() == Company.User.TECHNICIAN) ManageOptionsTechnician();
	}
	else if (Option == EXIT_OPTION) Logged = false;
	else Menu.PrintError("Invalid option. Try again.");

	if (!Exit)
	{
		if (Logged) OpenMenu();
		else if (!Logged) AskCloseApplication();
	}

}

void ManageOptionsAdmin()
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

void ManageOptionsManager()
{
	Console.Clear();

	switch (Option)
	{
		

		case 5:
			Company.ListTeamMembersOfTeam();
			break;

		case 6:
			Company.ListUnassignedTasks();
			break;

		case 7:
			Company.ListTasksAssignedToTeam();
			break;

		case 9:
			Company.AddTechnicianToTeam();
			break;

		case 10:
			Company.AssignTask();
			break;
	}

	AskCloseSession();
}

void ManageOptionsTechnician()
{
	Console.Clear();

	switch (Option)
	{
		case 6:
			Company.ListUnassignedTasks();
			break;

		case 7:
			Company.ListTasksAssignedToTeam();
			break;

		case 10:
			Company.AssignTaskToCurrentUser();
			break;
	}

	AskCloseSession();
}
void AskCloseSession()
{
	Menu.PrintMenu($"Press {EXIT_OPTION} to close your session, press any other key if you wish to execute another action:");
	Option = Menu.GetInputParsedInt();
	if (Option == EXIT_OPTION) Logged = false;
}

void AskCloseApplication()
{
	Logged = false;
	Console.Clear();
	Console.WriteLine("\nClosing session...\n");
	Menu.PrintMenu($"Press {EXIT_OPTION} to close the application, press any other key if you wish to access an account:");
	Option = Menu.GetInputParsedInt();
	if (Option == EXIT_OPTION) Exit = true;
}


void ExitApplication()
{
	Menu.Print("======================================\n" +
			"|| Closing application...            ||\n" +
			"======================================");
}

void FilterOptionsByUser()
{
	switch (Company.GetUserCredential())
	{

		case Company.User.ADMIN:
			Menu.Print("\t 1. Register new IT worker");
			Menu.Print("\t 2. Register new team");
			Menu.Print("\t 3. Register new task");
			Menu.Print("\t 4. List all team names");
			Menu.Print("\t 5. List team members by team name");
			Menu.Print("\t 6. List unassigned tasks");
			Menu.Print("\t 7. List task assignments by team name");
			Menu.Print("\t 8. Assign IT worker to a team as manager");
			Menu.Print("\t 9. Assign IT worker to a team as technician");
			Menu.Print("\t10. Assign task to IT worker");
			Menu.Print("\t11. Unregister IT worker");
			break;

		case Company.User.MANAGER:
			Menu.Print("\t 5. List team members by team name");
			Menu.Print("\t 6. List unassigned tasks");
			Menu.Print("\t 7. List task assignments by team name");
			Menu.Print("\t 9. Assign IT worker to a team as technician");
			Menu.Print("\t10. Assign task to IT worker");
			break;

		case Company.User.TECHNICIAN:
			Menu.Print("\t 6. List unassigned tasks");
			Menu.Print("\t 7. List task assignments by team name");
			Menu.Print("\t10. Assign task to IT worker");
			break;

	}
}