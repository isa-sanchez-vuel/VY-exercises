namespace ConsoleMenu
{
    using System;

    public class Menu
    {

        public static void PrintMenu(string message)
        {
            Console.WriteLine(message);
            Console.Write("=>");
        }

        public static void PrintError(string message)
        {
            Console.Error.WriteLine("ERROR:" + message);
        }

        public static int GetInputParsedInt()
        {
            string input = Console.ReadLine()?.Trim();

            if (int.TryParse(input, out _)) return int.Parse(input);
            else return 0;
        }

        public static decimal GetInputParsedDecimal()
        {
            string input = Console.ReadLine()?.Trim().Replace(".", ",");

            if (decimal.TryParse(input, out _)) return decimal.Parse(input);
            else return 0;
        }

        public static string GetInputString()
        {
            return Console.ReadLine()?.Trim();
        }
    }

}
