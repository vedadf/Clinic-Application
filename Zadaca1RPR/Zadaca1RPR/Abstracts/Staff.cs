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

        string Name { get; set; }

        string Surname { get; set; }

        int IDnumber { get; set; }

        double BaseSalary { get; set; }
        
        string UserName { get; set; }
        
        string Password { get; set; }

        MD5 PasswordMD5 { get; set; }

        static int ID = 0;

    }
}
