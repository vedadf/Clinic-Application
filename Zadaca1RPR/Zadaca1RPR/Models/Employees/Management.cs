using SharedView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Abstracts;

namespace Zadaca1RPR.Models.Employees
{
    class Management : Staff
    {

        static int ID = 0;

        public Management(string name, string surname, string userName, string password)
        {
            Name = name;
            Surname = surname;
            UserName = userName;
            PasswordMD5 = MD5.Create();
            Password = SView.GetHash(PasswordMD5, password);
            IDnumber = ID; ID++;
        }

    }
}
