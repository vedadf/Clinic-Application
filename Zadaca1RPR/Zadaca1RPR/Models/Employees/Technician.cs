using SharedView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Abstracts;
using Zadaca1RPR.Interfaces;

namespace Zadaca1RPR.Models.Employees
{
    public class Technician : Staff
    {

        static int ID = 0;
        
        public Technician(string name, string surname, double salary, string userName, string password)
        {
            Name = name;
            Surname = surname;
            ID++; IDnumber = ID;
            BaseSalary = salary;
            UserName = userName;
            MD5 PasswordMD5 = MD5.Create();
            Password = SView.GetHash(PasswordMD5, password);
        }
        
    }
}
