using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Abstracts;

namespace Zadaca1RPR.Models.Employees
{
    class Doctor : Staff
    {

        static int ID = 0;

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Salary { get; set; }

        public int IDnumber { get; set; }

        public int numOfPatientsProcessed { get; set; }

        public Doctor(string name, string surname, int salary)
        {
            Name = name;
            Surname = surname;
            ID++; IDnumber = ID;
            Salary = salary;
        }

    }
}
