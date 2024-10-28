using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPMultiBankAccount.Domain.Classes
{
    internal class Movement
    {
        DateTime Date;
        string Type;
        string Content;


        public Movement(string type, string content)
        {
            Date = DateTime.Now;
            Type = type;
            Content = content;
        }


        public DateTime GetDate()
        {
            return Date;
        }

        public string GetContent()
        {
            return Content;
        }

        public string GetType()
        {
            return Type;
        }
    }
}
