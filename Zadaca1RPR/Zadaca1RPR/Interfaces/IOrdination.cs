using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Abstracts;
using Zadaca1RPR.Models.Employees;

namespace Zadaca1RPR.Interfaces
{
    public interface IOrdination
    {
        bool OrdBusy { get; set; }
        bool DoctorAbsent { get; set; }
        Doctor Doctor { get; set; }

        Patient Patient { get; set; }
        List<Patient> PatientsQueue { get; set; }

        double Price { get; set; }

        string Name { get; set; }

        bool ProcessPatient();
        bool NewPatient(Patient patient);

    }
}
