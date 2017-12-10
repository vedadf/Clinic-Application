using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Abstracts;
using Zadaca1RPR.Interfaces;
using Zadaca1RPR.Models.Employees;
using Zadaca1RPR.Models.Patients;

namespace Zadaca1RPR.Models
{
    public sealed class Clinic
    {

        public List<HealthCard> HealthCards { get; set; }
        public List<Patient> Patients { get; set; }
        public List<IOrdination> Ordinations { get; set; }
        public List<Staff> Employees { get; set; }

        public List<Doctor> Doctors { get; set; }

        public Clinic(List<Staff> employees,
            List<IOrdination> ordinations,
            List<HealthCard> healthCards = default(List<HealthCard>),
            List<Patient> patients = default(List<Patient>))
        {
            Employees = employees;
            Ordinations = ordinations;
            HealthCards = healthCards;
            Patients = patients;

            Doctors = new List<Doctor>();
            foreach (IOrdination ord in Ordinations)
                Doctors.Add(ord.Doctor);

            foreach(Patient p in Patients)
                if(!p.HasHealthCard && p is UrgentPatient)
                    throw new ArgumentException("Nemaju svi pacijenti hitni slucajevi karton. Molimo kreirajte isti prilikom registracije.", "UrgentPatient");
        }

        public HealthCard GetCardFromID(int id)
        {
            if (id > HealthCard.ID || id < 0) return null;
            foreach (HealthCard hc in HealthCards)
                if (hc.IDnumber == id) return hc;
            return null;
        }

        public List<string> GetPatientSchedule(int id)
        {
            foreach(Patient p in Patients)
                if (p.IDnum == id) return p.Schedule;
            return null;
        }
           
        public List<HealthCard> GetCardsFromSurname(string surname)
        {
            List<HealthCard> cards = new List<HealthCard>();
            foreach (HealthCard card in HealthCards)
                if (card.Patient.Surname == surname) cards.Add(card);
            return cards;
        }

        public bool DeletePatientCardFromCardID(int id)
        {
            if(GetCardFromID(id) != null)
            {
                foreach(HealthCard hc in HealthCards)
                {
                    if (hc.IDnumber == id)
                    {
                        hc.Patient.HasHealthCard = false;
                        HealthCards.Remove(hc);
                        break;
                    }
                }
                return true;
            }
            return false;
        }
        
        public List<Patient> GetPatientsWithoutCard()
        {
            List<Patient> res = new List<Patient>();
            foreach(Patient p in Patients)
                if (!p.HasHealthCard) res.Add(p);
            return res;
        }

        public bool CardExists(int patientID)
        {
            if (HealthCards.Exists(i => i.Patient.IDnum == patientID)) return true;
            return false;
        }

        public bool PatientExists(int patientID)
        {
            if (Patients.Exists(p => p.IDnum == patientID)) return true;
            return false;
        }

        public Patient GetPatientFromID(int patientID)
        {
            return Patients.Find(p => p.IDnum == patientID);
        }

        public HealthCard GetCardFromPatientID(int patientID)
        {
            return HealthCards.Find(h => h.Patient.IDnum == patientID);
        }

        public Doctor GetDoctorMostVisited()
        {
            Doctor res = Doctors[0];
            int max = 0;
            foreach (Doctor doc in Doctors)
            {
                if (max < doc.NumOfPatientsProcessed)
                {
                    res = doc;
                    max = doc.NumOfPatientsProcessed;
                }
            }                    
            return res;
        }

        public int GetNumOfUrgentCases()
        {
            int cnt = 0;
            foreach (Patient p in Patients)
                if (p is UrgentPatient)
                    cnt++;
            return cnt;
        }

        public Patient GetPatientMostHealthIssues()
        {
            Patient pat = Patients[0];
            int max = 0;
            foreach (Patient p in Patients) {
                if (p.HealthBook.CurrentHealthIssues.Count > max)
                {
                    max = p.HealthBook.CurrentHealthIssues.Count;
                    pat = p;
                }
            }
            return pat;
        }

        public HealthCard GetCardFromCitizenID(string citID)
        {
            return HealthCards.Find(hc => hc.Patient.CitizenID == citID);
        }

        public List<Patient> GetPatientsFromOrdination(string ord)
        {
            return Ordinations.Find(target => target.Name == ord).PatientsQueue;
        }

    }
}
