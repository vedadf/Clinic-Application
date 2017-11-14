using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Models;

namespace Zadaca1RPR.Interfaces.Patient
{

    interface IPatient
    {

        string Name { get; set; }

        string Surname { get; set; }

        DateTime BirthDate { get; set; }

        int IDnumber { get; set; }

        string Address { get; set; }

        bool Married { get; set; }

        DateTime RegisterDate { get; set; }

        EnumGender Gender { get; set; }

        HealthBook HealthBook { get; set; }

    }
}
