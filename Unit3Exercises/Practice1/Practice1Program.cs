using Practice1;
using ConsoleMenu;
using System.Text;

const int EXIT_NUMBER = 3;

Son son;
bool exit;
int option;

Initialize();
OpenMenu();

void Initialize()
{
    son = new();
    Console.OutputEncoding = Encoding.UTF8;
    Console.InputEncoding = Encoding.UTF8;
}

void OpenMenu()
{
    exit = false;

    Console.Clear();
    Menu.PrintMenu($"1 - Show values\n" +
        $"2 - Modify Values\n" +
        $"{EXIT_NUMBER} - Exit\n" +
        $"Please select an option:");
    option = Menu.GetInputParsedInt();

    if (option != EXIT_NUMBER)
    {
        ManageOptions(option);
    }

    if (option == EXIT_NUMBER) exit = true;

    if (!exit) OpenMenu();
}

void ManageOptions(int opt)
{
    switch (opt)
    {
        case 1:
            son.PrintAllValues();
            CheckRepeat();
            break;

        case 2:
            son.ChangeValue();
            CheckRepeat();
            break;

        default:
            Console.WriteLine("Wrong option, try again.");
            break;
    }
}

void CheckRepeat()
{
    Menu.PrintMenu("Do you want to execute another action?\n" +
            $"Press {EXIT_NUMBER} to exit if not, press any other key if yes:");
    option = Menu.GetInputParsedInt();
}