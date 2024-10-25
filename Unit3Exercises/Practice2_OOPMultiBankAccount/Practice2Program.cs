using Practice2_OOPMultiBankAccount;
using ConsoleMenu;
using System.Text;

const int EXIT_OPTION = 7;
const int MAX_LOGIN_ATTEMPTS = 5;

int loginAttempts;

bool exit;
bool logged;

string userAccount;
string userPin;

int option;

Account? account = null;

Bank bank = new("ES", "PoatatoBank S.L", "3000", "54", "4790");

bank.CreateAccount("1122334455", "1111", "Francisco Bezerra");
bank.CreateAccount("2233445566", "2222", "Manel Joaquin Terez");
bank.CreateAccount("3344556677", "3333", "Ithiar Lobillo");
bank.CreateAccount("4455667788", "4444", "Laura Bastión");
bank.CreateAccount("5566778899", "5555", "Pol Lorenzo Gutierrez");
bank.CreateAccount("6677889900", "6666", "Marina Vilanova");

Initialize();
StartApplication();

void StartApplication()
{

    loginAttempts = 0;
    exit = false;

    OpenLogin();
    if (logged && account != null)
    {
        OpenMenu();

        if (!exit) StartApplication();
        else if (exit) ExitApplication();
    }
}

void OpenLogin()
{
    Console.Clear();
    logged = false;

    if (loginAttempts > 0 && loginAttempts < MAX_LOGIN_ATTEMPTS-1) Menu.PrintError($"Credentials not valid. Try again.\nAttempts left: {MAX_LOGIN_ATTEMPTS - loginAttempts}.");
    else if (MAX_LOGIN_ATTEMPTS-loginAttempts == 1) Menu.PrintError("Credentials not valid. This is your last attempt.");
    else if (loginAttempts >= MAX_LOGIN_ATTEMPTS) Menu.PrintError("You cannot try to login again. Go to the bank's office to recover your credentials.");

    if (loginAttempts < MAX_LOGIN_ATTEMPTS)
    {
        Console.WriteLine($"Welcome to our application. Please login with your account number and pin.");

        Menu.PrintMenu("\nAccount number:");
        userAccount = Menu.GetInputString();
        Menu.PrintMenu("\nPin:");
        userPin = Menu.GetInputString();

        account = bank.CheckAccount(userAccount, userPin);

        if (account != null) logged = true;

        if (!logged)
        {
            loginAttempts++;
            OpenLogin();
        }
    }
}

void OpenMenu()
{
    Console.Clear();
    Menu.PrintMenu($"Welcome to your account, {account.GetName()}! What do you wish to do?\n\n1 - Money Income\n2 - Money Outcome\n3 - See movement history\n4 - See Income history\n" +
        $"5 - See outcome history\n6 - See account money\n{EXIT_OPTION} - Logout\n\nPlease choose an option:");
    option = Menu.GetInputParsedInt();

    if (option > 0 && option < EXIT_OPTION) ManageOptions();
    else if (option == EXIT_OPTION) logged = false;
    else Menu.PrintError("Invalid option. Try again.");

    if (!exit) { 
        if (logged) OpenMenu();
        else if (!logged) AskCloseApplication();
    }
}

void ManageOptions()
{
    switch (option)
    {
        case 1:
            bank.HandleIncome(account);
            break;

        case 2:
            bank.HandleOutcome(account);
            break;

        case 3:
            bank.PrintAllMovements(account);
            break;

        case 4:
            bank.PrintAllIncomes(account);
            break;

        case 5:
            bank.PrintAllOutcomes(account);
            break;

        case 6:
            bank.PrintAccountMoney(account);
            break;
    }
    AskCloseSession();
}

void AskCloseSession()
{
    Menu.PrintMenu($"Press {EXIT_OPTION} to close your session, press any other key if you wish to execute another action:");
    option = Menu.GetInputParsedInt();
    if (option == EXIT_OPTION) logged = false;
}

void AskCloseApplication()
{
    logged = false;
    Console.Clear();
    Console.WriteLine("\nClosing session...\n");
    Menu.PrintMenu($"Press {EXIT_OPTION} to close the application, press any other key if you wish to acces an account:");
    option = Menu.GetInputParsedInt();
    if(option == EXIT_OPTION) ExitApplication();
}

void ExitApplication()
{
    exit = true;
    Console.WriteLine("Thank you for using our services!\nClosing application...");
}

void Initialize() {
    Console.OutputEncoding = Encoding.UTF8;
    Console.InputEncoding = Encoding.UTF8;
}
