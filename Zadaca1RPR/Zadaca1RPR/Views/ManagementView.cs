﻿using SharedView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Abstracts;
using Zadaca1RPR.Models;
using Zadaca1RPR.Models.Employees;
using Zadaca1RPR.Models.Patients;

namespace Zadaca1RPR.Views
{
    partial class ManagementView
    {

        public void Main(ref Clinic clinic)
        {
            SView.WaitInput();
            Console.Clear();
            string input;
            Console.WriteLine("UPRAVA: ");
            Console.WriteLine("Dobro došli! Odaberite neku od opcija: ");
            Console.WriteLine("1. Prikazi raspored pregleda pacijenta");
            Console.WriteLine("2. Pretraga kartona pacijenta");
            Console.WriteLine("3. Analiza sadrzaja");
            Console.WriteLine("4. Izlaz");
            input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    string ii;
                    Console.WriteLine("1. Pretraga po identifikacijskom broju pacijenta");
                    Console.WriteLine("2. Pretraga po ordinaciji");
                    ii = Console.ReadLine();
                    ScheduleSeach(ref clinic, ii);
                    Main(ref clinic);
                    break;
                case "2":
                    string i4;
                    Console.WriteLine("1. Pretraga po identifikacijskom broju");
                    Console.WriteLine("2. Pretraga po prezimenu");
                    Console.WriteLine("3. Pretraga po JMBG");
                    i4 = Console.ReadLine();
                    if (i4 != "1" || i4 != "2" || i4 != "3") ProcessCardSearch(ref clinic, i4);
                    else { SView.NoCommand(); Main(ref clinic); }
                    break;
                case "3":
                    int choice;                    
                   
                    while (true)
                    {
                        Console.WriteLine("1. Najposjeceniji doktor ikada na klinici.");
                        Console.WriteLine("2. Broj hitnih slucajeva ikada na klinici.");
                        Console.WriteLine("3. Pacijent sa najvecim brojem trenutnih zdravstvenih problema (pri registraciji u kliniku).");
                        Console.WriteLine("4. Trenutna plata doktora.");
                        if (Int32.TryParse(Console.ReadLine(), out choice)) break;
                        else Console.WriteLine("Uneseni podatak nije cijeli broj.");
                    }

                    if (choice == 1)
                        Console.WriteLine("Doktor {0} {1}. Broj pacijenata {2}.", clinic.GetDoctorMostVisited().Name, clinic.GetDoctorMostVisited().Surname, clinic.GetDoctorMostVisited().NumOfPatientsProcessed);
                    else if (choice == 2)
                        Console.WriteLine("Broj hitnih slucajeva {0}.", clinic.GetNumOfUrgentCases());
                    else if (choice == 3)
                        Console.WriteLine("Pacijent je {0} {1}. Broj zdravstvenih problema {2}.", clinic.GetPatientMostHealthIssues().Name, clinic.GetPatientMostHealthIssues().Surname, clinic.GetPatientMostHealthIssues().HealthBook.CurrentHealthIssues.Count);
                    else if (choice == 4)
                    {
                        int idd;

                        while (true)
                        {
                            Console.WriteLine("Unesite identifikacijski broj doktora.");
                            if (Int32.TryParse(Console.ReadLine(), out idd)) break;
                            else Console.WriteLine("Uneseni podatak nije cijeli broj.");
                        }
                        Doctor doc = clinic.Doctors.Find(d => d.IDnumber == idd);
                        if (doc == null) Console.WriteLine("Doktor ne postoji.");
                        else
                        {
                            Console.WriteLine("Doktor {0} {1}", doc.Name, doc.Surname);
                            Console.WriteLine("Broj procesuiranih pacijenata {0}", doc.NumOfPatientsProcessed);
                            Console.WriteLine("Pocetna plata {0}", doc.BaseSalary);
                            Console.WriteLine("Trenutna plata {0}", doc.CurrentSalary);
                        }
                    }
                    else { SView.NoCommand(); Main(ref clinic); }
                    Main(ref clinic);
                    break;
                case "4":
                    Program.ChooseRole(ref clinic);
                    break;
                default:
                    SView.NoCommand();
                    break;
            }
        }

        void ScheduleSeach(ref Clinic clinic, string i)
        {
            if (i == "1")
            {
                int id;
                List<string> schedule = null;
                while (true)
                {
                    Console.WriteLine("Upisite identifikacijski broj pacijenta: ");
                    if (!Int32.TryParse(Console.ReadLine(), out id)) Console.WriteLine("Uneseni podatak nije cijeli broj");
                    else break;
                }
                if (!clinic.CardExists(id)) Console.WriteLine("Pacijent nema kreiran karton ili pacijent ne postoji.");
                else
                {
                    schedule = clinic.GetPatientSchedule(id);
                    if (schedule == null || schedule.Count == 0) Console.WriteLine("Nema rasporeda ili pacijent ne postoji.");
                    else
                    {
                        Console.WriteLine("Raspored je sljedeci: ");
                        for (int it = 0; it < schedule.Count; it++)
                        {
                            if (it == schedule.Count - 1) Console.WriteLine("{0}", schedule[it]);
                            else Console.Write("{0}, ", schedule[it]);
                        }
                    }
                }
            }
            else if (i == "2")
            {
                string ord;
                Console.WriteLine("Unesite ime ordinacije");
                Console.WriteLine("Laboratorija - L");
                Console.WriteLine("Kardioloska ordinacija - K");
                Console.WriteLine("Radioloska ordinacija - R");
                Console.WriteLine("Hirurska ordinacija - H");
                Console.WriteLine("Dermatoloska ordinacija - D");
                ord = Console.ReadLine();
                if (!clinic.Ordinations.Exists(target => target.Name == ord)) Console.WriteLine("Ordinacija ne postoji");
                else
                {
                    List<Patient> patients = clinic.GetPatientsFromOrdination(ord);
                    if (patients != null)
                        foreach (Patient pat in patients)
                            Console.WriteLine("{0} - {1} {2} {3}", pat.IDnum, pat.Name, pat.Surname, pat.CitizenID);
                    else Console.WriteLine("Nema pacijenata zakazanih za ovu ordinaciju.");
                }
            }
            Main(ref clinic);
        }

        void ProcessCardSearch(ref Clinic clinic, string i)
        {
            if (i == "1")
            {
                int id;
                
                while (true)
                {
                    Console.WriteLine("Unesite identifikacijski broj kartona");
                    if (Int32.TryParse(Console.ReadLine(), out id)) break;
                    else Console.WriteLine("Uneseni podatak nije cijeli broj.");
                }

                HealthCard card = clinic.GetCardFromID(id);
                if (card == null)
                {
                    Console.WriteLine("Karton ne postoji ili je identifikacijski broj van opsega.");
                    Main(ref clinic);
                }

                PrintPatientInfoFromCard(card);

            }
            else if (i == "2")
            {
                string surname;
                Console.WriteLine("Unesite prezime pacijenta");
                surname = Console.ReadLine();

                List<HealthCard> cards = clinic.GetCardsFromSurname(surname);
                if (cards.Count == 0)
                {
                    Console.WriteLine("Karton ne postoji.");
                    Main(ref clinic);
                }
                Console.WriteLine();
                Console.WriteLine("Pronadjeno {0} pacijenata: ", cards.Count);
                foreach (HealthCard card in cards) PrintPatientInfoFromCard(card);
            }
            else if(i == "3")
            {
                string citizenID;
                Console.WriteLine("Unesite JMBG pacijenta");
                citizenID = Console.ReadLine();

                HealthCard card = clinic.GetCardFromCitizenID(citizenID);
                if(card == null)
                {
                    Console.WriteLine("Karton ne postoji ili JMBG nije ispravan.");
                    Main(ref clinic);
                }
                PrintPatientInfoFromCard(card);
            }
            Main(ref clinic);
        }

    }

    partial class ManagementView
    {
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
            Console.WriteLine("Datum rodjenja: {0}", card.Patient.BirthDate);
            Console.WriteLine("Datum registracije: {0}", card.Patient.RegisterDate);
            Console.WriteLine("Bracno stanje: {0}", married);

            if (card.Ordinations != null)
            {
                Console.WriteLine("Ordinacije koje mora posjetiti: ");
                foreach (string ord in card.Ordinations) Console.Write("{0}, ", ord);
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
                    Console.WriteLine("Datum smrti: {0}", card.DateOfDeath.ToString("MM/dd/yyyy"));
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
