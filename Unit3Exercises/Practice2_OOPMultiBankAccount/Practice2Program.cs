using Practice2_OOPMultiBankAccount;
using ConsoleMenu;
using System.Text;

const int EXIT_OPTION = 8;
const int MAX_LOGIN_ATTEMPTS = 5;

int LoginAttempts;

bool Exit;
bool Logged;

string UserAccount;
string UserPin;

int Option;

Account? Account = new();

Bank Bank;

Initialize();
StartApplication();

void StartApplication()
{
	LoginAttempts = 0;
	Exit = false;
	Logged = false;

	OpenLogin();
	if (Logged && Account != null)
	{
		OpenMenu();

		if (!Exit) StartApplication();
		else if (Exit) ExitApplication();
	}
}

void OpenLogin()
{
	Console.Clear();
	while (!Logged)
	{
		Logged = false;

		if (LoginAttempts > 0 && LoginAttempts < MAX_LOGIN_ATTEMPTS - 1) Menu.Print($"ERROR: Credentials not valid. Try again.\nAttempts left: {MAX_LOGIN_ATTEMPTS - LoginAttempts}.");
		else if (MAX_LOGIN_ATTEMPTS - LoginAttempts == 1) Menu.Print("ERROR: Credentials not valid. This is your last attempt.");
		else if (LoginAttempts >= MAX_LOGIN_ATTEMPTS)
		{
			Menu.Print("ERROR: You cannot try to login again. Go to the bank's office to recover your credentials.");
			return;
		}

		if (LoginAttempts < MAX_LOGIN_ATTEMPTS)
		{
			Console.WriteLine($"Welcome to our application. Please login with your account number and pin.");

			UserAccount = Menu.GetValidStringInputClear("\nAccount number:");

			UserPin = Menu.GetValidStringInputClear("\nPin:");

			Account = Bank.CheckAccountLogin(UserAccount, UserPin);

			if (Account != null) Logged = true;

			if (!Logged)
			{
				LoginAttempts++;
			}
		}
	}
}

void OpenMenu()
{
	Console.Clear();
	Menu.PrintMenu($"Welcome to your account, {Account.GetName()}! What do you wish to do?\n\n" +
		$"1 - Money Income\n" +
		$"2 - Money Outcome\n" +
		$"3 - See movement history\n" +
		$"4 - See Income history\n" +
		$"5 - See outcome history\n" +
		$"6 - See account money\n" +
		$"7 - See account data\n" +
		$"{EXIT_OPTION} - Logout\n\n" +
		$"Please choose an option:");
	Option = Menu.GetInputParsedInt();

	if (Option > 0 && Option < EXIT_OPTION) ManageOptions();
	else if (Option == EXIT_OPTION) Logged = false;
	else Menu.PrintError("Invalid option. Try again.");

	if (!Exit)
	{
		if (Logged) OpenMenu();
		else if (!Logged) AskCloseApplication();
	}
}

void ManageOptions()
{
	switch (Option)
	{
		case 1:
			Bank.HandleIncome(Account);
			break;

		case 2:
			Bank.HandleOutcome(Account);
			break;

		case 3:
			Bank.PrintAllMovements(Account);
			break;

		case 4:
			Bank.PrintAllIncomes(Account);
			break;

		case 5:
			Bank.PrintAllOutcomes(Account);
			break;

		case 6:
			Bank.PrintAccountMoney(Account);
			break;

		case 7:
			Bank.PrintAccountData(Account);
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
	Menu.PrintMenu($"Press {EXIT_OPTION} to close the application, press any other key if you wish to access other account:");
	Option = Menu.GetInputParsedInt();
	if (Option == EXIT_OPTION) Exit = true;
}

void ExitApplication()
{
	Console.WriteLine("======================================" +
					"|| Closing application...            ||\n" +
					"|| Thank you for using our services! ||\n" +
					"======================================");
}

void Initialize()
{
	Console.OutputEncoding = Encoding.UTF8;

	Bank = new("PoatatoBank S.L", "ES", "3000", "54", "4790");

	Bank.CreateAccount("1122334455", "1111", "Francisco Bezerra");
	Bank.CreateAccount("2233445566", "2222", "Manel Joaquin Terez");
	Bank.CreateAccount("3344556677", "3333", "Ithiar Lobillo");
	Bank.CreateAccount("4455667788", "4444", "Laura Bastión");
	Bank.CreateAccount("5566778899", "5555", "Pol Lorenzo Gutierrez");
	Bank.CreateAccount("6677889900", "6666", "Marina Vilanova");

	Bank.Initialize();
}
