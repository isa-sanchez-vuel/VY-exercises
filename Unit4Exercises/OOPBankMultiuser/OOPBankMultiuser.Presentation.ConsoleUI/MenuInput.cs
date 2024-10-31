using OOPBankMultiuser.XCutting.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPBankMultiuser.Presentation.ConsoleUI
{
	internal class MenuInput
	{
		public const int ERROR_VALUE = -1;
		public const string ERROR_VALUE_S = "-1";

		public static bool HasErrors = false;
		public static InputErrorEnum Error { get; set; }


		public static int GetInputParsedInt()
		{
			string? input = Console.ReadLine()?.Trim();

			if (!input.Equals(""))
			{
				if (int.TryParse(input, out _)) return int.Parse(input);
				else return -2;
			}
			return ERROR_VALUE;
		}


		public static string GetInputString()
		{
			string? input = Console.ReadLine()?.Trim();
			if (!string.IsNullOrEmpty(input))
			{
				return input;
			}
			else return ERROR_VALUE_S;
		}

		public static int GetValidIntInput(string prompt)
		{
			MenuOutput.PrintMenu(prompt);
			int value = GetInputParsedInt() * 1;
			if (value == ERROR_VALUE)
			{
				//MenuOutput.PrintError("Invalid number.");
			}
			return value;
		}


		public static decimal GetValidDecimalInput(string prompt)
		{
			HasErrors = true;

			MenuOutput.PrintMenu(prompt);
			string? input = Console.ReadLine()?.Trim().Replace(".", ",");

			if (!string.IsNullOrEmpty(input))
			{
				if (decimal.TryParse(input, out decimal parsedInput)) {

					HasErrors = false;
					return parsedInput;
				}
				else Error = InputErrorEnum.WrongFormat;
			}
			else Error = InputErrorEnum.EmptyInput;
			return ERROR_VALUE;
		}

		public static string GetValidStringInputClear(string prompt)
		{
			MenuOutput.PrintMenu(prompt);
			string input = GetInputString();
			if (input.Equals(ERROR_VALUE_S))
			{
				//MenuOutput.PrintError("Invalid input.");
				return null;
			}
			return input;
		}

		public static string GetValidStringInput(string prompt)
		{
			MenuOutput.PrintMenu(prompt);
			string input = GetInputString();
			if (input.Equals(ERROR_VALUE_S))
			{
				//MenuOutput.Print("Invalid input.");
				return null;
			}
			return input;
		}

		public static DateTime GetValidDateInput(string prompt)
		{
			MenuOutput.PrintMenu(prompt);
			string dateInput = GetInputString();
			if (DateTime.TryParseExact(dateInput, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
			{
				return date;
			}
			//MenuOutput.PrintError("Date format not valid.");
			return DateTime.MinValue;
		}
	}
}
