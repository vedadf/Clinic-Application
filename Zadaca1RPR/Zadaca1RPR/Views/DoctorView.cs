using SharedView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Abstracts;
using Zadaca1RPR.Interfaces;
using Zadaca1RPR.Models;
using Zadaca1RPR.Models.Ordinations;
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

                    while (true)
                    {
                        Console.WriteLine("Upisite identifikacijski broj pacijenta: ");
                        if (Int32.TryParse(Console.ReadLine(), out id)) break;
                        else Console.WriteLine("Uneseni podatak nije cijeli broj.");
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
                    int id2;
                        
                    while (true)
                    {
                        Console.WriteLine("Unesite identifikacijski broj pacijenta kojem zelite napraviti karton: ");
                        if (Int32.TryParse(Console.ReadLine(), out id2)) break;
                        else Console.WriteLine("Uneseni podatak nije cijeli broj.");
                    }
                    if (clinic.CardExists(id2)) Console.WriteLine("Odabrani pacijent vec ima karton.");
                    else if (!clinic.PatientExists(id2)) Console.WriteLine("Odabrani pacijent ne postoji.");
                    else
                    {
                        Patient patient = clinic.GetPatientFromID(id2);
                        clinic.HealthCards.Add(new HealthCard((NormalPatient)patient));
                        if (patient.Schedule == null || patient.Schedule.Count == 0)
                            Console.WriteLine("Pacijent nema kreiran raspored.");
                        else
                        {
                            clinic.Ordinations.Find(o => o.Name == patient.Schedule[0]).NewPatient(patient);
                            Console.WriteLine("Pacijent je prosljedjen u ordinaciju {0}.", patient.Schedule[0]);
                        }
                        Console.WriteLine("Karton uspjesno kreiran.");
                        break;
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
                            Console.WriteLine();
                        }
                    }
                    Main(ref clinic);
                    break;
                case "5":
                    int id3;
                    
                    while (true)
                    {
                        Console.WriteLine("Upisite identifikacijski broj pacijenta: ");
                        if (Int32.TryParse(Console.ReadLine(), out id3)) break;
                        else Console.WriteLine("Uneseni podatak nije cijeli broj.");
                    }
                    string choice;
                    if (clinic.GetPatientFromID(id3) == null || clinic.GetCardFromPatientID(id3) == null) Console.WriteLine("Karton za pacijenta ne postoji.");
                    else if (clinic.GetPatientFromID(id3).Schedule == null) Console.WriteLine("Pacijent od pocetka nema kreiran raspored. Mozda je preminuo.");
                    else
                    {
                        Console.WriteLine("Pacijent: {0} {1}", clinic.GetPatientFromID(id3).Name, clinic.GetPatientFromID(id3).Surname);
                        Console.WriteLine("Koje ordinacije pacijent treba posjetiti? (Odvojite u novi red, ne smiju se ordinacije ponavljati)");
                        Console.WriteLine("Laboratoriju - L");
                        Console.WriteLine("Kardiolosku ordinaciju - K");
                        Console.WriteLine("Radiolosku ordinaciju - R");
                        Console.WriteLine("Hirursku ordinaciju - H");
                        Console.WriteLine("Dermatolosku ordinaciju - D");
                        Console.WriteLine("--Upisite . za prekid--");
                        List<string> schedule1 = clinic.GetPatientSchedule(id3);
                        do
                        {
                            choice = Console.ReadLine();
                            if (choice != ".")
                            {
                                if (choice == "L" || choice == "K" || choice == "R" || choice == "H" || choice == "D")
                                {
                                    bool exists = schedule1.Exists(i => i == choice);
                                    if (exists) Console.WriteLine("Vec je unesena ordinacija.");
                                    else schedule1.Add(choice);
                                }
                                else SView.NoCommand();
                            }
                        }
                        while (choice != ".");

                        List<string> dynamic = new List<string>();

                        List<string> ordinationsDoctorAbsent = new List<string>();
                        List<IOrdination> ordinationsQueueExists = new List<IOrdination>();
                        List<string> ordinationsDeviceBroken = new List<string>();
                        List<string> ordinationsAvailable = new List<string>();

                        foreach (string ordin in schedule1)
                        {
                            IOrdination ordination = clinic.Ordinations.Find(o => o.Name == ordin);
                            if (ordination.DoctorAbsent) ordinationsDoctorAbsent.Add(ordin);
                            else if (ordination is Cardiology && (ordination as Cardiology).DeviceBroken) ordinationsDeviceBroken.Add(ordin);
                            else if (!ordination.OrdBusy) ordinationsAvailable.Add(ordin);
                            else ordinationsQueueExists.Add(ordination);
                        }

                        //Lambda izraz iskoristen da bi se sortirale ordinacije od najmanjeg reda cekanja do najveceg, za generisanje raspored
                        if (ordinationsQueueExists != null)
                            ordinationsQueueExists.Sort((a, b) => (a.PatientsQueue.Count.CompareTo(b.PatientsQueue.Count)));

                        foreach (string ordin in ordinationsAvailable) dynamic.Add(ordin);
                        foreach (IOrdination ordin in ordinationsQueueExists) dynamic.Add(ordin.Name);
                        foreach (string ordin in ordinationsDoctorAbsent) dynamic.Add(ordin);
                        foreach (string ordin in ordinationsDeviceBroken) dynamic.Add(ordin);

                        Patient pat = clinic.GetPatientFromID(id3);
                        clinic.Ordinations.Find(o => o.Name == pat.Schedule[0]).PatientsQueue.Remove(pat);
                        pat.Schedule = dynamic;
                        clinic.Ordinations.Find(o => o.Name == pat.Schedule[0]).NewPatient(pat);


                        Console.WriteLine("Generisani raspored je sljedeci: ");
                        for (int i = 0; i < dynamic.Count; i++)
                        {
                            if (i != dynamic.Count - 1) Console.Write("{0}, ", dynamic[i]);
                            else Console.Write("{0} ", dynamic[i]);
                        }
                        Console.WriteLine("Pacijent je izbrisan iz reda prosle ordinacije, i dodan u red ordinacije {0}", dynamic[0]);
                        Console.WriteLine();
                    }
                    Main(ref clinic);
                    break;
                case "6":
                    string ord;
                    Console.WriteLine("Odaberite ordinaciju: ");
                    Console.WriteLine("Laboratorija (L)");
                    Console.WriteLine("Kardioloska (K)");
                    Console.WriteLine("Radioloska (R)");
                    Console.WriteLine("Hirurska (H)");
                    Console.WriteLine("Dermatoloska (D)");
                    ord = Console.ReadLine();

                    if (ord == "L" || ord == "K" || ord == "R" || ord == "H" || ord == "D")
                    {
                        IOrdination ordination = clinic.Ordinations.Find(o => o.Name == ord);
                        Console.WriteLine("Dobrodosli doktore {0} {1}", ordination.Doctor.Name, ordination.Doctor.Surname);
                        if (ordination.Patient != null)
                        {
                            Console.WriteLine("Pacijent koji ce biti procesovan je: {0} {1}", ordination.Patient.Name, ordination.Patient.Surname);
                            Console.WriteLine("Upisite informacije o terapiji: ");
                            string ther;
                            ther = Console.ReadLine();
                            ordination.Patient.HealthBook.Therapies.Add(ther);
                            Console.WriteLine("Upisite rezultat pregleda: ");
                            ther = Console.ReadLine();
                            ordination.Patient.HealthBook.ExaminationResults.Add(ther);
                            Console.WriteLine("Pregled ce biti dodan na danasnji datum: {0}", DateTime.Today.ToString("dd/MM/yyyy"));
                            ordination.Patient.HealthBook.ExaminationDates.Add(DateTime.Today);
                            ordination.Patient.HealthBook.CompletedOrdinations.Add(ordination.Name);
                            Patient patient = ordination.Patient;
                            ordination.ProcessPatient();
                            Console.WriteLine("Pacijent je uspjesno procesuiran");
                            
                            if(patient.Schedule == null || patient.Schedule.Count == 0)
                            {
                                Console.WriteLine("Pacijent nema vise zakazanih pregleda na rasporedu, uputite ga ka portirnici.");
                            }
                            else
                            {
                                Console.WriteLine("Sljedeca ordinacija za pacijeta je {0}, dodan je u red cekanja za tu ordinaciju.", patient.Schedule[0]);
                                clinic.Ordinations.Find(o => o.Name == patient.Schedule[0]).NewPatient(patient);
                            }

                            if (ordination.OrdBusy == false) Console.WriteLine("Nema vise pacijenata u redu za ovu ordinaciju.");
                            else Console.WriteLine("Pacijent iz reda cekanja je prosljedjen u ovu ordinaciju.");
                        }
                        else Console.WriteLine("Nema pacijenta na redu");
                        
                    }
                    else SView.NoCommand();
                    Main(ref clinic);
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
                if (card == null)
                {
                    Console.WriteLine("Karton ne postoji ili JMBG nije ispravan.");
                    Main(ref clinic);
                }
                PrintPatientInfoFromCard(card);
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