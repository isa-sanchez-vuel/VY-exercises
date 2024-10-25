using Practice2_OOPMultiBankAccount;
using ConsoleMenu;

const int EXIT_OPTION = 7;

int loginAttempts;

bool exit;
bool logged;
string userAccount;
string userPin;

Account? account;

Bank bank = new("ES", "PoatatoBank S.L", "3000", "54", "4790");

bank.CreateAccount("1122334455", "1111", "Francisco Bezerra");
bank.CreateAccount("2233445566", "2222", "Manel Joaquin Terez");
bank.CreateAccount("3344556677", "3333", "Ithiar Lobillo");
bank.CreateAccount("4455667788", "4444", "Laura Bastión");
bank.CreateAccount("5566778899", "5555", "Pol Lorenzo Gutierrez");
bank.CreateAccount("6677889900", "6666", "Marina Vilanova");


OpenMenu();

void OpenMenu()
{
    loginAttempts = 0;
    exit = false;

    Login();
    if (logged)
    {
        Console.WriteLine($"Welcome to your account, {account.GetName()}!");
        OptionsMenu();
    }

    if (!exit) OpenMenu();
}

void Login()
{
    logged = false;

    if (loginAttempts > 0 && loginAttempts < 5) Menu.PrintError($"Credentials not valid. Try again.\nAttempts left: {5-loginAttempts}.");
    else if (loginAttempts == 5) Menu.PrintError("Credentials not valid. This is your last attempt.");
    else if (loginAttempts > 5) Menu.PrintError("You cannot try to login again. Go to the bank's office to recover your credentials.");
    Console.Clear();
    Console.WriteLine($"Welcome to our application. Please login with your account number and pin.");

    Menu.PrintMenu("\nAccount number:");
    userAccount = Menu.GetInputString();
    Menu.PrintMenu("\nPin:");
    userPin = Menu.GetInputString();

    account = bank.CheckAccount(userAccount, userPin);

    if (account != null) logged = true;

    if (!logged) Login();
}

void OptionsMenu()
{
    int option;

    Menu.PrintMenu(@$"What do you wish to do?

    1 - Money Income
    2 - Money Outcome
    3 - See movement history
    4 - See Income history
    5 - See outcome history
    6 - See account money
    {EXIT_OPTION} - Logout

Please choose an option:");

    option = Menu.GetInputParsedInt();

    if(option > 0 && option < EXIT_OPTION)
    {
        ManageOptions(option);
    }
    else Menu.PrintError("Invalid option. Try again.");
}

void ManageOptions(int option)
{
    /*switch (option)
    {

        case 1:
            HandleIncome();
            break;

        case 2:
            HandleOutcome();
            break;

        case 3:
            PrintAllMovements();
            break;

        case 4:
            PrintAllIncomes();
            break;

        case 5:
            PrintAllOutcomes();
            break;

        case 6:
            PrintAccountMoney();
            break;

        case 7:
            exit = true;
            Console.WriteLine("Closing your session...");
            break;
    }*/
}