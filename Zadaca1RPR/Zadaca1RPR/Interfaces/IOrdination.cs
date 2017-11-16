using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Abstracts;
using Zadaca1RPR.Models.Employees;

namespace Zadaca1RPR.Interfaces
{
    interface IOrdination
    {
        bool DoctorBusy { get; set; }
        bool DoctorAbsent { get; set; }
        Doctor Doctor { get; set; }

        List<Patient> Patients { get; set; }

        double Price { get; set; }

    }
}
