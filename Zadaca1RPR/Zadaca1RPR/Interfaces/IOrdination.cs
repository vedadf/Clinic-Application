using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Interfaces.Patient;
using Zadaca1RPR.Models.Staff;

namespace Zadaca1RPR.Interfaces
{
    interface IOrdination
    {

        string Name { get; set; }

        bool DeviceBusy { get; set; }
        bool DeviceBroken { get; set; }
        string DeviceName { get; set; }

        bool DoctorBusy { get; set; }
        bool DoctorAbsent { get; set; }
        Doctor Doctor { get; set; }

        List<IPatient> Patients { get; set; }

    }
}
