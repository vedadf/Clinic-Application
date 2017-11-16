using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Abstracts;
using Zadaca1RPR.Interfaces;
using Zadaca1RPR.Models.Employees;
using Zadaca1RPR.Models.Patients;

namespace Zadaca1RPR.Models.Ordinations
{
    class Laboratory : IOrdination
    {
        public bool DeviceBusy { get; set; }
        public bool DeviceBroken { get; set; }
        public string DeviceName { get; set; }

        public bool DoctorBusy { get; set; }
        public bool DoctorAbsent { get; set; }

        public Doctor Doctor { get; set; }

        public List<Patient> Patients { get; set; }

        public double Price { get; set; }

        public Laboratory(Doctor doctor,
            string deviceName)
        {
            Doctor = doctor;
            DeviceBusy = false;
            DeviceBroken = false;
            DoctorBusy = false;
            DoctorAbsent = false;
            DeviceName = deviceName;
            Patients = new List<Patient>();
            Price = 10.6;
        }
        
        public void NewPatient(Patient patient)
        {
            if (patient is NormalPatient) Patients.Add(patient);
            else if (patient is UrgentPatient) Patients.Add(patient);
        }

    }
}
