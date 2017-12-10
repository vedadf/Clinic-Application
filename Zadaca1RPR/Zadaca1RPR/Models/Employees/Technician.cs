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

        static int ID;

        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        string Password { get; set; }

        MD5 PasswordMD5 { get; set; }

        public int IDnumber { get; set; }

        public double BaseSalary { get; set; }

        public Technician(string name, string surname, double salary, string userName, string password)
        {
            Name = name;
            Surname = surname;
            ID++; IDnumber = ID;
            BaseSalary = salary;
            UserName = userName;
            PasswordMD5 = MD5.Create();
            Password = SView.GetHash(PasswordMD5, password);
        }



    }
}
