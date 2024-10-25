using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice3_WorkersManagement
{
    internal class ITWorker : Worker
    {
        int id;
        string name;
        string surname;
        DateTime birthDate;
        DateTime leavingDate;

        int yearsOfExperience;
        List<string> techKnowledges = new();
        string level;


    }
}
