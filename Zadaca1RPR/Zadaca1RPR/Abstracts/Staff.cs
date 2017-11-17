using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadaca1RPR.Abstracts
{
    abstract class Staff
    {

        string Name { get; set; }

        string Surname { get; set; }

        int IDnumber { get; set; }

        double BaseSalary { get; set; }

        static int ID = 0;

    }
}
