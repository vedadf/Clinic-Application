using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Abstracts;
using Zadaca1RPR.Interfaces;
using Zadaca1RPR.Models.Patients;

namespace Zadaca1RPR.Models
{
    sealed class Clinic
    {

        public List<HealthCard> HealthCards { get; set; }
        public List<Patient> Patients { get; set; }
        public List<IOrdination> Ordinations { get; set; }
        public List<Staff> Employees { get; set; }

        public Clinic(List<Staff> employees,
            List<IOrdination> ordinations,
            List<HealthCard> healthCards = default(List<HealthCard>),
            List<Patient> patients = default(List<Patient>))
        {
            Employees = employees;
            Ordinations = ordinations;
            HealthCards = healthCards;
            Patients = patients;

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

    }
}
