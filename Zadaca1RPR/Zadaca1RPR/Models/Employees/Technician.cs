using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Abstracts;
using Zadaca1RPR.Interfaces;

namespace Zadaca1RPR.Models.Employees
{
    class Technician : Staff
    {

        static int ID;

        public string Name { get; set; }

        public string Surname { get; set; }

        public int IDnumber { get; set; }

        public int Salary { get; set; }

        public Technician(string name, string surname, int salary)
        {
            Name = name;
            Surname = surname;
            ID++; IDnumber = ID;
            Salary = salary;
        }



    }
}
