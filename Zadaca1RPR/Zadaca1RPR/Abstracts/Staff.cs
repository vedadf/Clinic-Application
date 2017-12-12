using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Zadaca1RPR.Abstracts
{
    public abstract class Staff
    {

        public string Name { get; set; }

        public string Surname { get; set; }

        public int IDnumber { get; set; }

        public double BaseSalary { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        static int ID = 0;

    }
}
