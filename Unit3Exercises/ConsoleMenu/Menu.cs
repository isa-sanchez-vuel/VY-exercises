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
            Console.Error.WriteLine("ERROR: " + message);
        }

        public static void PrintEmptyStringError()
        {
            Console.WriteLine("ERROR: " );
        }

        public static int GetInputParsedInt()
        {
            string input = Console.ReadLine()?.Trim();

            if (!input.Equals(""))
            {
                if (int.TryParse(input, out _)) return int.Parse(input);
                else return -2;
            }
            return -1;
        }

        public static decimal GetInputParsedDecimal()
        {
            string input = Console.ReadLine()?.Trim().Replace(".", ",");

            if (!input.Equals("")) {
                if (decimal.TryParse(input, out _)) return decimal.Parse(input);
                else return -2;
            }
            return -1;
        }

        public static string GetInputString()
        {
            string input = Console.ReadLine()?.Trim();
            if (!input.Equals(""))
            {
                return input;
            }
            else return "-1";
        }

        bool CheckInputDecimal(decimal input) //TODO Falta hacer las comprobaciones de errores
        {


            return false;
        }
    }

}
