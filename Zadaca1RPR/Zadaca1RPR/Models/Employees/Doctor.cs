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
    public class Doctor : Staff
    {

        static int ID = 0;

        public string Name { get; set; }

        public string Surname { get; set; }

        public double BaseSalary { get; set; }

        public double CurrentSalary { get; set; }

        public string UserName { get; set; }

        MD5 PasswordMD5 { get; set; }

        string Password { get; set; }

        public int IDnumber { get; set; }

        public int NumOfPatientsProcessed { get; set; }

        public Doctor(string name, string surname, double baseSalary, string userName, string password)
        {
            Name = name;
            Surname = surname;
            IDnumber = ID; ID++;
            BaseSalary = baseSalary;
            CurrentSalary = baseSalary;
            UserName = userName;
            PasswordMD5 = MD5.Create();
            Password = SView.GetHash(PasswordMD5, password);
            
        }

    }
}
