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
			Console.WriteLine(
				"\n=========================\n\n" +
				$"- {Field1S}\n" +
				$"- {Field2S}\n" +
				$"- {Field3S}\n" +
				$"- {Field1F}\n" +
				$"- {Field2F}\n" +
				$"- {GetField3F()}\n" +
				$"- {Field1G}\n" +
				$"- {Field2G}\n" +
				$"- {GetField3G()}\n" +
				"\n=========================\n");
		}

		public void ChangeValue()
		{
			string input = Menu.GetValidStringInput("Change field 1 Son");
			if (input != null) Field1S = input;
			input = Menu.GetValidStringInput("Change field 2 Son");
			if (input != null) Field2S = input;
			input = Menu.GetValidStringInput("Change field 3 Son");
			if(input!=null) Field3S = input;

			input = Menu.GetValidStringInput("Change field 1 Father");
			if (input != null) Field1F = input;
			input = Menu.GetValidStringInput("Change field 2 Father");
			if (input != null) Field2F = input;
			input = Menu.GetValidStringInput("Change field 3 Father");
			if (input != null) SetField3F(input);

			input = Menu.GetValidStringInput("Change field 1 Grandfather");
			if (input != null) Field1G = input;
			input = Menu.GetValidStringInput("Change field 2 Grandfather");
			if (input != null)  Field2G = input;
			input = Menu.GetValidStringInput("Change field 3 Grandfather");
			if (input != null) SetField3G(input);

			PrintAllValues();
		}

	}
}
