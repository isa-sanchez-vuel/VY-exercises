using System.Text;

namespace ConsoleMenu
{
	public class Menu
	{
		public const int ERROR_VALUE = -1;
		public const string ERROR_VALUE_S = "-1";

		public Menu()
		{
			Console.OutputEncoding = Encoding.UTF8;
			Console.InputEncoding = Encoding.UTF8;
		}

		public static void PrintMenu(string message)
		{
			Console.WriteLine(message);
			Console.Write("=>");
		}

		public static void Print(string message)
		{
			Console.WriteLine(message);
		}

		public static void PrintError(string message)
		{
			Console.Error.WriteLine("ERROR: " + message);
		}

		public static int GetInputParsedInt()
		{
			string input = Console.ReadLine()?.Trim();

			if (!input.Equals(""))
			{
				if (int.TryParse(input, out _)) return int.Parse(input);
				else return -2;
			}
			return ERROR_VALUE;
		}

		public static decimal GetInputParsedDecimal()
		{
			string input = Console.ReadLine()?.Trim().Replace(".", ",");

			if (!input.Equals("")) {
				if (decimal.TryParse(input, out _)) return decimal.Parse(input);
			}
			return ERROR_VALUE;
		}

		public static string GetInputString()
		{
			string input = Console.ReadLine()?.Trim();
			if (!input.Equals(""))
			{
				return input;
			}
			else return ERROR_VALUE_S;
		}



		public static string GetValidStringInput(string prompt)
		{
			PrintMenu(prompt);
			string input = GetInputString();
			if (input.Equals(ERROR_VALUE_S))
			{
				Console.Clear();
				PrintError("Invalid input.");
				return null;
			}
			return input;
		}

		public static DateTime GetValidDateInput(string prompt)
		{
			PrintMenu(prompt);
			string dateInput = GetInputString();
			if (DateTime.TryParseExact(dateInput, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
			{
				return date;
			}
			Console.Clear();
			PrintError("Date format not valid.");
			return DateTime.MinValue;
		}

		public static int GetValidIntInput(string prompt)
		{
			PrintMenu(prompt);
			int value = GetInputParsedInt();
			if (value == ERROR_VALUE)
			{
				Console.Clear();
				PrintError("Invalid number.");
			}
			return value;
		}

		public static decimal GetValidDecimalInput(string prompt)
		{
			PrintMenu(prompt);
			decimal value = GetInputParsedDecimal();
			if (value == ERROR_VALUE)
			{
				Console.Clear();
				PrintError("Invalid number.");
			}
			return value;
		}
	}

}
