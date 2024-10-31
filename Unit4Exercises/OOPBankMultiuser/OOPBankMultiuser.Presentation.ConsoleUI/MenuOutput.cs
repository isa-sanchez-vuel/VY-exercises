using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPBankMultiuser.Presentation.ConsoleUI
{
	internal class MenuOutput
	{

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
			Console.Clear();
			Console.WriteLine("ERROR: " + message);
		}
	}
}
