﻿using System;
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
            if (Patient != null)
            {
                Doctor.numOfPatientsProcessed++;
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