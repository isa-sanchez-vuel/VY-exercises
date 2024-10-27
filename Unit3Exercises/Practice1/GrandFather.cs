using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice1
{
	internal class GrandFather
	{
		public string Field1G;
		protected string Field2G;
		private string Field3G;

		public string GetField3G()
		{
			return Field3G;
		}

		public void SetField3G(string value)
		{
			Field3G = value;
		}
	}
}
