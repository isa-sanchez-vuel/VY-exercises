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

		public static int GetValidIntInput(string prompt)
		{
			MenuOutput.PrintMenu(prompt);
			string? input = Console.ReadLine()?.Trim().Replace(".", "").Replace(",", "");

			if (!string.IsNullOrEmpty(input))
			{
				if (int.TryParse(input, out int parsedInt))
				{
					return parsedInt;
				}
			}
			return ERROR_VALUE;
		}


		public static decimal GetValidDecimalInput(string prompt)
		{
			MenuOutput.PrintMenu(prompt);
			string? input = Console.ReadLine()?.Trim().Replace(".", ",");

			if (!string.IsNullOrEmpty(input))
			{
				if (decimal.TryParse(input, out decimal parsedDecimal)) {

					return parsedDecimal;
				}
			}
			return ERROR_VALUE;
		}

		public static string GetValidStringInput(string prompt)
		{
			MenuOutput.PrintMenu(prompt);
			string? input = Console.ReadLine()?.Trim();
			if (!string.IsNullOrEmpty(input))
			{
				return input;
			}
			return ERROR_VALUE_S;
		}

		public static DateTime GetValidDateInput(string prompt)
		{
			MenuOutput.PrintMenu(prompt);
			string? dateInput = Console.ReadLine()?.Trim();
			if (DateTime.TryParseExact(dateInput, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
			{
				return date;
			}
			return DateTime.MinValue;
		}
	}
}
