using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice2_OOPMultiBankAccount
{
    internal class Movement
    {
        DateTime date;
        string type;
        string content;


        public Movement(string type, string content) 
        {
            date = DateTime.Now;
            this.type = type;
            this.content = content;
        }


        public DateTime GetDate()
        {
            return date;
        }

        public string GetContent()
        {
            return content;
        }

        public string GetType()
        {
            return type;
        }
    }
}
