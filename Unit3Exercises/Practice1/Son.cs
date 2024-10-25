using ConsoleMenu;

namespace Practice1
{
    internal class Son : Father
    {
        public string field1S;
        protected string field2S;
        private string field3S;

        public Son() 
        {
            field1S = "Field 1 Son";
            field2S = "Field 2 Son";
            field3S = "Field 3 Son";

            field1F = "Field 1 Father";
            field2F = "Field 2 Father";
            SetField3F("Field 3 Father");

            field1G = "Field 1 Grandfather";
            field2G = "Field 2 Grandfather";
            SetField3G("Field 3 Grandfather");

        }

        public void PrintAllValues()
        {
            Console.WriteLine($"- {field1S}\n" +
                $"- {field2S}\n" +
                $"- {field3S}\n" +
                $"- {field1F}\n" +
                $"- {field2F}\n" +
                $"- {GetField3F()}\n" +
                $"- {field1G}\n" +
                $"- {field2G}\n" +
                $"- {GetField3G()}\n");
        }

        public void ChangeValue()
        {
            Menu.PrintMenu("Change field 1 Son");
            field1S = Menu.GetInputString();
            Menu.PrintMenu("Change field 2 Son");
            field2S = Menu.GetInputString();
            Menu.PrintMenu("Change field 3 Son");
            field3S = Menu.GetInputString();

            Menu.PrintMenu("Change field 1 Father");
            field1F = Menu.GetInputString();
            Menu.PrintMenu("Change field 2 Father");
            field2F = Menu.GetInputString();
            Menu.PrintMenu("Change field 3 Father");
            SetField3F(Menu.GetInputString());

            Menu.PrintMenu("Change field 1 Grandfather");
            field1G = Menu.GetInputString();
            Menu.PrintMenu("Change field 2 Grandfather");
            field2G = Menu.GetInputString();
            Menu.PrintMenu("Change field 3 Grandfather");
            SetField3G(Menu.GetInputString());

            PrintAllValues();
        }

    }
}
