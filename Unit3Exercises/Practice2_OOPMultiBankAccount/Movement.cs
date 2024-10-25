using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice2_OOPMultiBankAccount
{
    internal class Movement
    {
        int movementId;
        DateTime date;
        string type;
        string content;


        public Movement(int id, string type, string content) 
        {
            movementId = id;
            date = DateTime.Now;
            this.type = type;
            this.content = content;
        }
    }
}
