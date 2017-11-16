﻿using SharedView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Abstracts;
using Zadaca1RPR.Models;
using Zadaca1RPR.Models.Patients;

namespace Zadaca1RPR.Views
{
    partial class DoctorView
    {

        public void Main(ref Clinic clinic)
        {
            SView.WaitInput();
            Console.Clear();
            string input;
            Console.WriteLine("DOKTOR: ");
            Console.WriteLine("Dobro došli! Odaberite neku od opcija: ");
            Console.WriteLine("1. Prikazi raspored pregleda pacijenta");
            Console.WriteLine("2. Pretraga kartona pacijenta");
            Console.WriteLine("3. Kreiranje kartona pacijenta");
            Console.WriteLine("4. Lista pacijenata bez kartona");
            Console.WriteLine("5. Registruj novi pregled");
            Console.WriteLine("6. Procesiraj sljedeceg pacijenta");
            Console.WriteLine("7. Izlaz");
            input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    int id;
                    List<string> schedule = null;
                    Console.WriteLine("Upisite identifikacijski broj pacijenta: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    schedule = clinic.GetPatientSchedule(id);
                    if (schedule == null) Console.WriteLine("Nema rasporeda ili pacijent ne postoji.");
                    else
                    {
                        Console.WriteLine("Raspored je sljedeci: ");
                        for (int it = 0; it < schedule.Count; it++)
                        {
                            if (it == schedule.Count - 1) Console.WriteLine("{0}", schedule[it]);
                            else Console.Write("{0}, ", schedule[it]);
                        }
                    }
                    Main(ref clinic);
                    break;
                case "2":
                    string i4;
                    Console.WriteLine("1. Pretraga po identifikacijskom broju");
                    Console.WriteLine("2. Pretraga po prezimenu");
                    i4 = Console.ReadLine();
                    if (i4 != "1" || i4 != "2") ProcessCardSearch(ref clinic, i4);
                    else { SView.NoCommand(); Main(ref clinic); }
                    break;
                case "3":
                    while (true)
                    {
                        int id2;
                        Console.WriteLine("Unesite identifikacijski broj pacijenta kojem zelite napraviti karton: ");
                        id2 = Convert.ToInt32(Console.ReadLine());
                        if (clinic.CardExists(id2)) Console.WriteLine("Odabrani pacijent vec ima karton.");
                        else if (!clinic.PatientExists(id2)) Console.WriteLine("Odabrani pacijent ne postoji.");
                        else
                        {
                            Patient patient = clinic.GetPatientFromID(id2);
                            clinic.HealthCards.Add(new HealthCard((NormalPatient)patient));
                            Console.WriteLine("Karton uspjesno kreiran.");
                            break;
                        }                        
                    }
                    Main(ref clinic);
                    break;
                case "4":
                    List<Patient> patients = clinic.GetPatientsWithoutCard();
                    if (patients.Count == 0) Console.WriteLine("Svi pacijenti imaju karton.");
                    else
                    {
                        Console.WriteLine("Lista pacijenata bez kartona: ");
                        foreach (Patient pat in patients)
                        {
                            Console.WriteLine("Ime: {0}", pat.Name);
                            Console.WriteLine("Prezime: {0}", pat.Surname);
                            Console.WriteLine("Adresa: {0}", pat.Address);
                            Console.WriteLine("Identifikacijski broj pacijenta: {0}", pat.IDnum);
                        }
                    }
                    Main(ref clinic);
                    break;
                case "5":
                    int id3;
                    Console.WriteLine("Upisite identifikacijski broj pacijenta: (mora biti broj)");
                    id3 = Convert.ToInt32(Console.ReadLine());
                    string choice;
                    Console.WriteLine("Pacijent: {0} {1}", clinic.GetPatientFromID(id3).Name, clinic.GetPatientFromID(id3).Surname);
                    Console.WriteLine("Koje ordinacije pacijent treba posjetiti? (Odvojite u novi red, ne smiju se ordinacije ponavljati)");
                    Console.WriteLine("Laboratoriju - L");
                    Console.WriteLine("Kardiolosku ordinaciju - K");
                    Console.WriteLine("Radiolosku ordinaciju - R");
                    Console.WriteLine("Hirursku ordinaciju - H");
                    Console.WriteLine("Ortopedsku ordinaciju - O");
                    Console.WriteLine("Dermatolosku ordinaciju - D");
                    Console.WriteLine("Stomatolosku ordinaciju - S");
                    Console.WriteLine("--Upisite . za prekid--");
                    List<string> schedule1 = clinic.GetPatientSchedule(id3);
                    do
                    {
                        choice = Console.ReadLine();
                        if (choice != ".")
                        {
                            if (choice != "L" || choice != "K" || choice != "R" || choice != "H" || choice != "O" || choice != "D" || choice != "S")
                            {
                                bool exists = schedule1.Exists(i => i == choice);
                                if (exists) Console.WriteLine("Ordinacija vec postoji u rasporedu.");
                                else schedule1.Add(choice);
                            }
                            else SView.NoCommand();
                        }
                    }
                    while (choice != ".");
                    clinic.GetPatientFromID(id3).Schedule = schedule1;
                    Main(ref clinic);
                    break;
                case "6":
                    
                    break;
                case "7":
                    Program.ChooseRole(ref clinic);
                    break;
                default:
                    SView.NoCommand();
                    Main(ref clinic);
                    break;
            }
        }

        void ProcessCardSearch(ref Clinic clinic, string i)
        {
            if (i == "1")
            {
                int id;
                Console.WriteLine("Unesite identifikacijski broj kartona");
                id = Convert.ToInt32(Console.ReadLine());

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
            Main(ref clinic);
        }

    }

    partial class DoctorView
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