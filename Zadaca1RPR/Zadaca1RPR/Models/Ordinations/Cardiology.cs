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
    class Cardiology : IOrdination
    {
        
        public bool DeviceBroken { get; set; }
        public string DeviceName { get; set; }

        public bool OrdBusy { get; set; }
        public bool DoctorAbsent { get; set; }

        public Doctor Doctor { get; set; }

        public Patient Patient { get; set; }
        public List<Patient> PatientsQueue { get; set; }

        public double Price { get; set; }

        public string Name { get; set; }
        public string FullName { get; set; }

        public Cardiology(Doctor doctor)
        {
            Doctor = doctor;
            OrdBusy = false;
            DoctorAbsent = false;
            DeviceBroken = true;
            Patient = null;
            Price = 20.6;
            Name = "K";
            DeviceName = "EKG";
            FullName = "Kardiologija";
        }

        public bool NewPatient(Patient patient)
        {
            
            if (!patient.HasHealthCard) return false;

            if (Patient == null)
            {
                Patient = patient;
                OrdBusy = true;
            }
            else PatientsQueue.Add(patient);

            return true;
        }

        public bool ProcessPatient()
        {
            if (DeviceBroken)
            {
                throw new ArgumentException("Aparat je u kvaru. Nije moguce procesuirati pacijenta.");
            }
            if (Patient != null)
            {
                Doctor.NumOfPatientsProcessed++;
                if (Doctor.NumOfPatientsProcessed <= 20)
                    Doctor.CurrentSalary += (Doctor.BaseSalary * 0.01);
                Patient.Cost += Price;
                Patient.Schedule.Remove("K");
                if (PatientsQueue == null || PatientsQueue.Count == 0)
                {
                    Patient = null;
                    OrdBusy = false;
                }
                else
                {
                    Patient = PatientsQueue[0];
                    PatientsQueue.RemoveAt(0);
                    OrdBusy = true;
                }
                return true;
            }
            return false;
        }


    }
}
