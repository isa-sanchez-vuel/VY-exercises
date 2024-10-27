using ConsoleMenu;

namespace Practice1
{
	internal class Son : Father
	{
		public string Field1S;
		protected string Field2S;
		private string Field3S;

		public Son() 
		{
			Field1S = "Field 1 Son";
			Field2S = "Field 2 Son";
			Field3S = "Field 3 Son";

			Field1F = "Field 1 Father";
			Field2F = "Field 2 Father";
			SetField3F("Field 3 Father");

			Field1G = "Field 1 Grandfather";
			Field2G = "Field 2 Grandfather";
			SetField3G("Field 3 Grandfather");

		}

		public void PrintAllValues()
		{
			Console.WriteLine($"- {Field1S}\n" +
				$"- {Field2S}\n" +
				$"- {Field3S}\n" +
				$"- {Field1F}\n" +
				$"- {Field2F}\n" +
				$"- {GetField3F()}\n" +
				$"- {Field1G}\n" +
				$"- {Field2G}\n" +
				$"- {GetField3G()}\n");
		}

		public void ChangeValue()
		{
			Menu.PrintMenu("Change field 1 Son");
			Field1S = Menu.GetInputString();
			Menu.PrintMenu("Change field 2 Son");
			Field2S = Menu.GetInputString();
			Menu.PrintMenu("Change field 3 Son");
			Field3S = Menu.GetInputString();

			Menu.PrintMenu("Change field 1 Father");
			Field1F = Menu.GetInputString();
			Menu.PrintMenu("Change field 2 Father");
			Field2F = Menu.GetInputString();
			Menu.PrintMenu("Change field 3 Father");
			SetField3F(Menu.GetInputString());

			Menu.PrintMenu("Change field 1 Grandfather");
			Field1G = Menu.GetInputString();
			Menu.PrintMenu("Change field 2 Grandfather");
			Field2G = Menu.GetInputString();
			Menu.PrintMenu("Change field 3 Grandfather");
			SetField3G(Menu.GetInputString());

			PrintAllValues();
		}

	}
}
