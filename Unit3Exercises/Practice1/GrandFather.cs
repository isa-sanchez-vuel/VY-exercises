using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice1
{
    internal class GrandFather
    {
        public string field1G;
        protected string field2G;
        private string field3G;

        public string GetField3G()
        {
            return field3G;
        }

        public void SetField3G(string value)
        {
            field3G = value;
        }
    }
}
