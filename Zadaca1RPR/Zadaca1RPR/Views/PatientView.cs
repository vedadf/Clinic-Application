using SharedView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Abstracts;
using Zadaca1RPR.Interfaces;
using Zadaca1RPR.Models;
using Zadaca1RPR.Models.Patients;

namespace Zadaca1RPR.Views
{
    class PatientView
    {

        public void Main(ref Clinic clinic)
        {
            SView.WaitInput();
            Console.Clear();

            int id = GetId(clinic);
            if (id == -1)
            {
                Console.WriteLine("JMBG ne postoji.");
                Main(ref clinic);
            }
            else if(id == -2) Program.ChooseRole(ref clinic);

            string input;
            Console.WriteLine("PACIJENT {0} {1}: ", clinic.GetPatientFromID(id).Name, clinic.GetPatientFromID(id).Surname);
            Console.WriteLine("Dobro došli! Odaberite neku od opcija: ");
            Console.WriteLine("1. Pregled zauzetosti ordinacija");
            Console.WriteLine("2. Prikaz vaseg kartona");
            Console.WriteLine("3. Prikaz vaseg duga");
            Console.WriteLine("4. Izlaz");

            while (true)
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        List<IOrdination> ordinations = new List<IOrdination>();
                        ordinations = clinic.Ordinations;
                        foreach (IOrdination ord in clinic.Ordinations)
                            Console.WriteLine("Ordinacija: {0}, Broj pacijenata u redu: {1}", ord.Name, ord.PatientsQueue.Count);
                        Main(ref clinic);
                        break;
                    case "2":
                        HealthCard card = clinic.GetCardFromID(id);
                        PrintPatientInfoFromCard(card);
                        Main(ref clinic);
                        break;
                    case "3":
                        Patient pat = clinic.GetPatientFromID(id);
                        PrintPatientDebt(clinic, pat);
                        Main(ref clinic);
                        break;
                    case "4":
                        Program.ChooseRole(ref clinic);
                        break;
                    default:
                        SView.NoCommand();
                        Main(ref clinic);
                        break;
                }
            }
        }

        int GetId(Clinic clinic)
        {
            Console.WriteLine("Unesite vas JMBG ili 0 za izlaz");
            string c = Console.ReadLine();
            if (c == "0") return -2;
            List<Patient> patients = new List<Patient>();
            patients = clinic.Patients;
            foreach(Patient pat in patients)
                if (pat.CitizenID == c) return pat.IDnum;
            return -1;
        }

        void PrintPatientDebt(Clinic clinic, Patient pat)
        {
            bool regular = false;
            Console.WriteLine("{0} {1}", pat.Name, pat.Surname);
            Console.WriteLine("Posjetili ste kliniku {0} puta.", pat.numOfTimesVisited);
            if (pat.numOfTimesVisited > 3) regular = true;
            else regular = false;
            Console.WriteLine("Odradili ste {0} pregleda.", pat.HealthBook.ExaminationDates.Count);
            Console.WriteLine("Glavna cijena je: {0}KM", pat.Cost);

            foreach (string str in pat.HealthBook.CompletedOrdinations)
                Console.WriteLine("Ordinacija: {0}; Cijena: {1};", str, clinic.Ordinations.Find(o => o.Name == str).Price);
            
            double res = pat.Cost;
            if (!regular) res = pat.Cost + 0.15 * pat.Cost;
            else res = pat.Cost - 0.1 * pat.Cost;
            
            Console.WriteLine("Mozete platiti na 3 rate podijeljene na 3 jednaka dijela.");
            if (regular)
            {
                Console.WriteLine("Posto ste redovan pacijent, cijena za placanje na rate ostaje ista kao i glavna cijena.");
                Console.WriteLine("Prva rata koja mora biti placena odmah iznosi {0}KM", res / 3.0);
                Console.WriteLine("Vi ste redovan pacijent, dakle cijena za placanje gotovinom iznosi: {0}KM.", res);
            }
            else
            {
                Console.WriteLine("Posto ste novi pacijent, cijena za placanje na rate iznosi: {0}KM", res);
                Console.WriteLine("Prva rata koja mora biti placena odmah iznosi {0}KM", res / 3.0);
                Console.WriteLine("Vi ste novi pacijent, pa cijena za placanje gotovinom ostaje ista kao i glavna cijena.");
            }
                        
        }

        void PrintPatientInfoFromCard(HealthCard card)
        {
            string gender, married, active;
            if (card.Patient.Gender == EnumGender.Male) gender = "Musko";
            else gender = "Zensko";
            if (card.Patient.Married) married = "Ozenjen";
            else married = "Neozenjen";
            if (card.CardActive) active = "Aktivan";
            else active = "Neaktivan";

            Console.WriteLine();
            Console.WriteLine("Karton upjesno pronadjen: ");
            Console.WriteLine("Identifikacijski broj kartona: {0}", card.IDnumber);
            Console.WriteLine("Identifikacijski broj pacijenta: {0}", card.Patient.IDnum);
            Console.WriteLine("Karton je: {0}", active);
            Console.WriteLine("Ime: {0}", card.Patient.Name);
            Console.WriteLine("Prezime: {0}", card.Patient.Surname);
            Console.WriteLine("Adresa: {0}", card.Patient.Address);
            Console.WriteLine("JMBG: {0}", card.Patient.CitizenID);

            Console.WriteLine("Spol: {0}", gender);
            Console.WriteLine("Datum rodjenja: {0}", card.Patient.BirthDate.ToString("dd/MM/yyyy"));
            Console.WriteLine("Datum registracije: {0}", card.Patient.RegisterDate.ToString("dd/MM/yyyy"));
            Console.WriteLine("Bracno stanje: {0}", married);

            if (card.Ordinations != null)
            {
                Console.WriteLine("Ordinacije koje mora posjetiti: ");
                for (int i = 0; i < card.Ordinations.Count; i++)
                {
                    if (i != card.Ordinations.Count - 1) Console.Write("{0}, ", card.Ordinations[i]);
                    else Console.Write("{0} ", card.Ordinations[i]);
                }
                Console.WriteLine();
            }

            if (card.Patient is UrgentPatient)
            {
                Console.WriteLine("Pacijent je hitan slucaj.");

                UrgentPatient uPatient;
                uPatient = (UrgentPatient)card.Patient;
                if (uPatient.Deceased)
                {
                    Console.WriteLine("Pacijent je preminuo.");
                    Console.WriteLine("Vrijeme smrti: {0}", card.TimeOfDeath);
                    Console.WriteLine("Datum smrti: {0}", card.DateOfDeath.ToString("dd/MM/yyyy"));
                    Console.WriteLine("Razlog smrti: {0}", card.CauseOfDeath);
                }

                Console.WriteLine("Prva pomoc: ");
                Console.WriteLine(uPatient.FirstAid);
            }
            else if (card.Patient is NormalPatient) Console.WriteLine("Pacijent je normalan slucaj.");

            if (card.Patient.HealthBook != null)
            {
                Console.WriteLine("Informacije iz zdravstvene knjzice: ");
                Console.WriteLine("->Misljenje doktora: ");
                Console.WriteLine(card.Patient.HealthBook.DoctorNotes);
                Console.WriteLine("->Trenutni zdravstveni problemi: ");
                foreach (string m in card.Patient.HealthBook.CurrentHealthIssues) Console.WriteLine(m);
                Console.WriteLine("->Prosli zdravstveni problemi: ");
                foreach (string m in card.Patient.HealthBook.PastHealthIssues) Console.WriteLine(m);
                Console.WriteLine("->Zdravstveni problemi unutar porodice: ");
                Console.WriteLine(card.Patient.HealthBook.FamilyHealthIssue);
            }
            else Console.WriteLine("Zdravstvena knjizica nije kreirana za ovog pacijenta.");

            Console.WriteLine();
        }

    }
}
