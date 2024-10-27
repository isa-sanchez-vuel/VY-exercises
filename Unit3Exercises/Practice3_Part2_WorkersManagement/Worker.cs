using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice3_Part2_WorkersManagement;
{
	internal class Worker
	{
		internal static int IdCount = 0;
		protected int Id;
		protected string Name;
		protected string Surname;
		protected DateTime BirthDate;
		protected DateTime LeavingDate;

	}
}
