using Practice1;
using ConsoleMenu;
using System.Text;

const int EXIT_NUMBER = 3;

Son Son;
bool Exit;
int Option;

Initialize();
OpenMenu();

void Initialize()
{
	Son = new();
	Console.OutputEncoding = Encoding.UTF8;
	Console.InputEncoding = Encoding.UTF8;
}

void OpenMenu()
{
	Exit = false;

	Console.Clear();
	Menu.PrintMenu($"1 - Show values\n" +
		$"2 - Modify Values\n" +
		$"{EXIT_NUMBER} - Exit\n" +
		$"Please select an option:");
	Option = Menu.GetInputParsedInt();

	if (Option != EXIT_NUMBER)
	{
		ManageOptions(Option);
	}

	if (Option == EXIT_NUMBER) Exit = true;

	if (!Exit) OpenMenu();
}

void ManageOptions(int opt)
{
	switch (opt)
	{
		case 1:
			Son.PrintAllValues();
			CheckRepeat();
			break;

		case 2:
			Son.ChangeValue();
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
	Option = Menu.GetInputParsedInt();
}