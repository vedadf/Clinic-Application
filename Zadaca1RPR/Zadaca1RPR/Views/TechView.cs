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
    partial class TechView
    {

        public void Main(ref Clinic clinic)
        {
            SView.WaitInput();
            Console.Clear();
            string input;
            Console.WriteLine("PORTIR: ");
            Console.WriteLine("Dobro došli! Odaberite neku od opcija: ");
            Console.WriteLine("1. Registruj/Brisi pacijenta");
            Console.WriteLine("2. Prikazi raspored pregleda pacijenta");
            Console.WriteLine("3. Pretraga kartona pacijenta");
            Console.WriteLine("4. Naplata");
            Console.WriteLine("5. Izlaz");

            while (true)
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        string i;
                        Console.WriteLine("1. Registruj pacijenta");
                        Console.WriteLine("2. Obrisi pacijenta");
                        i = Console.ReadLine();
                        if (i == "1") ProcessPatientRegistration(ref clinic);
                        else if (i == "2") ProcessPatientDeletion(ref clinic);
                        else { SView.NoCommand(); Main(ref clinic); }
                        break;
                    case "2":
                        int id;
                        List<string> schedule = null;
                        Console.WriteLine("Upisite identifikacijski broj pacijenta: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        if (!clinic.CardExists(id)) Console.WriteLine("Pacijent nema kreiran karton ili pacijent ne postoji.");
                        else
                        {
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
                        }
                        Main(ref clinic);
                        break;
                    case "3":
                        string i4;
                        Console.WriteLine("1. Pretraga po identifikacijskom broju");
                        Console.WriteLine("2. Pretraga po prezimenu");
                        i4 = Console.ReadLine();
                        if (i4 != "1" || i4 != "2") ProcessCardSearch(ref clinic, i4);
                        else { SView.NoCommand(); Main(ref clinic); }
                        break;
                    case "4":

                        break;
                    case "5":
                        Program.ChooseRole(ref clinic);
                        break;
                    default:
                        SView.NoCommand();
                        Main(ref clinic);
                        break;
                }
            }
        }

        void ProcessPatientRegistration(ref Clinic clinic)
        {

            Patient patient = null;

            string urgent, firstAid = "", timeOfDeath = "", causeOfDeath = "";
            DateTime dateOfDeath = default(DateTime);
            bool deceased = false;

            while (true)
            {
                Console.WriteLine("Hitan slucaj? (D/N)");
                urgent = Console.ReadLine();
                if (urgent == "N") { break; }
                else if (urgent == "D")
                {

                    Console.WriteLine("Unesite podatke o prvoj pomoci.");
                    firstAid = Console.ReadLine();
                    while (true)
                    {
                        Console.WriteLine("Da li je pacijent prezivio? (D/N)");
                        string iDeceased = Console.ReadLine();
                        if (iDeceased == "D") { deceased = false; break; }
                        else if (iDeceased == "N") { deceased = true; break; }
                        else { SView.NoCommand(); continue; }
                    }
                    if (deceased)
                    {
                        int dd, mm, yy;
                        Console.WriteLine("Datum smrti: ");
                        Console.Write("-> Dan: ");
                        dd = Convert.ToInt32(Console.ReadLine());
                        Console.Write("-> Mjesec: ");
                        mm = Convert.ToInt32(Console.ReadLine());
                        Console.Write("-> Godina: ");
                        yy = Convert.ToInt32(Console.ReadLine());
                        dateOfDeath = new DateTime(yy, mm, dd);
                        Console.Write("Vrijeme smrti: (hh:mm) ");
                        timeOfDeath = Console.ReadLine();
                        Console.WriteLine("Razlog smrti: ");
                        causeOfDeath = Console.ReadLine();
                    }
                    break;
                }
                else SView.NoCommand();
            }

            string name, surname, address;
            bool married = false;
            int day, month, year;
            DateTime bDate, regDate = DateTime.Today;
            EnumGender gender = EnumGender.Female;

            Console.Write("Ime: ");
            name = Console.ReadLine();
            Console.Write("Prezime: ");
            surname = Console.ReadLine();
            Console.Write("Adresa: ");
            address = Console.ReadLine();

            Console.WriteLine("Datum rodjenja: ");
            Console.Write("-> Dan: ");
            day = Convert.ToInt32(Console.ReadLine());
            Console.Write("-> Mjesec: ");
            month = Convert.ToInt32(Console.ReadLine());
            Console.Write("-> Godina: ");
            year = Convert.ToInt32(Console.ReadLine());
            bDate = new DateTime(year, month, day);
            
            while (true)
            {
                Console.WriteLine("Spol: (Musko/Zenso) ili (M/Z)");
                string inputG = Console.ReadLine();
                if (inputG == "Musko" || inputG == "M" || inputG == "m" || inputG == "musko")
                {
                    gender = EnumGender.Male;
                    break;
                }
                else if (inputG == "Zensko" || inputG == "Z" || inputG == "z" || inputG == "zensko")
                {
                    gender = EnumGender.Female;
                    break;
                }
                else { SView.NoCommand(); continue; }
            }

            while (true)
            {
                Console.WriteLine("Bracno stanje: (Ozenjen/Neozenjen ili O/N)");
                string stanje = Console.ReadLine();
                if (stanje == "O" || stanje == "Ozenjen") { married = true; break; }
                else if (stanje == "N" || stanje == "Neozenjen") { married = false; break; }
                else { SView.NoCommand(); continue; }
            }

            List<string> schedule = new List<string>();

            if (!deceased)
            {
                string input;
                Console.WriteLine("Koje ordinacije pacijent treba posjetiti? (Odvojite u novi red, ne smiju se ordinacije ponavljati)");
                Console.WriteLine("Laboratoriju - L");
                Console.WriteLine("Kardiolosku ordinaciju - K");
                Console.WriteLine("Radiolosku ordinaciju - R");
                Console.WriteLine("Hirursku ordinaciju - H");
                Console.WriteLine("Dermatolosku ordinaciju - D");
                Console.WriteLine("--Upisite . za prekid--");
                do
                {
                    input = Console.ReadLine();
                    if (input != ".")
                    {
                        if (input == "L" || input == "K" || input == "R" || input == "H" || input == "D")
                        {
                            bool exists = schedule.Exists(i => i == input);
                            if (exists) Console.WriteLine("Vec je unesena ordinacija.");
                            else schedule.Add(input);
                        }
                        else SView.NoCommand();
                    }
                }
                while (input != ".");

                List<string> dynamic = new List<string>();

                List<string> ordinationsDoctorAbsent = new List<string>();
                List<IOrdination> ordinationsQueueExists = new List<IOrdination>();
                List<string> ordinationsDeviceBroken = new List<string>();
                List<string> ordinationsAvailable = new List<string>();

                foreach (string ord in schedule)
                {
                    IOrdination ordination = clinic.Ordinations.Find(o => o.Name == ord);
                    if (ordination.DoctorAbsent) ordinationsDoctorAbsent.Add(ord);
                    else if (ordination is Cardiology && (ordination as Cardiology).DeviceBroken) ordinationsDeviceBroken.Add(ord);
                    else if (!ordination.OrdBusy) ordinationsAvailable.Add(ord);
                    else ordinationsQueueExists.Add(ordination);
                }

                if (ordinationsQueueExists != null)
                    ordinationsQueueExists.Sort((a, b) => (a.PatientsQueue.Count.CompareTo(b.PatientsQueue.Count)));

                foreach (string ord in ordinationsAvailable) dynamic.Add(ord);
                foreach (IOrdination ord in ordinationsQueueExists) dynamic.Add(ord.Name);
                foreach (string ord in ordinationsDoctorAbsent) dynamic.Add(ord);
                foreach (string ord in ordinationsDeviceBroken) dynamic.Add(ord);
                schedule = dynamic;

                Console.WriteLine("Generisani raspored je sljedeci: ");
                for (int i = 0; i < schedule.Count; i++)
                {
                    if (i != schedule.Count - 1) Console.Write("{0}, ", schedule[i]);
                    else Console.Write("{0} ", schedule[i]);
                }
                Console.WriteLine();

            }
            else schedule = null;

            if (urgent == "N")
            {
                patient = new NormalPatient(name, surname, bDate, address, married, regDate, gender, schedule);
                DoAnamnesis(ref patient);
            }
            else if (urgent == "D")
            {
                patient = new UrgentPatient(firstAid, deceased, name, surname, bDate, address, married, regDate, gender, schedule);
                HealthCard card = new HealthCard(patient as UrgentPatient, causeOfDeath, timeOfDeath, dateOfDeath);
                if (deceased == false) DoAnamnesis(ref patient);
                clinic.HealthCards.Add(card);
            }

            clinic.Patients.Add(patient);
            Console.WriteLine();
            Console.WriteLine("Pacijent uspjesno registrovan.");
            if (urgent == "D") Console.WriteLine("Hitan slucaj: karton za pacijenta kreiran.");
            if (urgent == "N") Console.WriteLine("Potrebno je da doktor kreira karton za ovog pacijenta.");
            Console.WriteLine();
            Main(ref clinic);
        }

        void ProcessPatientDeletion(ref Clinic clinic)
        {
            int id;

            Console.WriteLine("Napisite identifikacijski broj kartona pacijenta: ");
            id = Convert.ToInt32(Console.ReadLine());

            HealthCard card = clinic.GetCardFromID(id);
            if (card == null) Console.WriteLine("Karton ne postoji.");
            else{
                Console.WriteLine("Odabrali ste da obrisete karton pacijenta: ");
                Console.Write("{0} ", card.Patient.Name);
                Console.WriteLine(card.Patient.Surname);
                if (clinic.DeletePatientCardFromCardID(id)) Console.WriteLine("Uspjesno obrisan karton pacijenta.");
                else Console.WriteLine("Identifikacijski broj je van opsega ili pacijent ne postoji.");
            }

            Main(ref clinic);

        }

        void DoAnamnesis(ref Patient patient)
        {
            int num;
            List<string> currHealthIssues = new List<string>();
            List<string> pastHealthIssues = new List<string>();
            string notes, familyHI;

            Console.WriteLine("ANAMNEZA: ");
            Console.WriteLine("Koliko zdravstvenih problema je pacijent imao u proslosti? (Mora biti broj)");
            num = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < num; i++)
            {
                Console.WriteLine("Problem {0}: ", i + 1);
                pastHealthIssues.Add(Console.ReadLine());
            }
            Console.WriteLine("Koliko zdravstvenih problema pacijent ima trenutno? (Mora biti broj)");
            num = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < num; i++)
            {
                Console.WriteLine("Problem {0}: ", i + 1);
                currHealthIssues.Add(Console.ReadLine());
            }
            Console.WriteLine("Unesite zdravstvene probleme u porodici: ");
            familyHI = Console.ReadLine();
            Console.WriteLine("Vasi dodatni komentari: ");
            notes = Console.ReadLine();

            HealthBook HB = new HealthBook(notes, currHealthIssues, pastHealthIssues, familyHI);
            patient.HealthBook = HB;

            Console.WriteLine("Podaci iz zdravstvene knjizice uspjesno spaseni.");
            
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

    partial class TechView
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
            Console.WriteLine("Datum rodjenja: {0}", card.Patient.BirthDate.ToString("dd/MM/yyyy"));
            Console.WriteLine("Datum registracije: {0}", card.Patient.RegisterDate.ToString("dd/MM/yyyy"));
            Console.WriteLine("Bracno stanje: {0}", married);

            if (card.Ordinations != null)
            {
                Console.WriteLine("Ordinacije koje mora posjetiti: ");
                for(int i = 0; i < card.Ordinations.Count; i++)
                {
                    if(i != card.Ordinations.Count - 1) Console.Write("{0}, ", card.Ordinations[i]);
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
            else if(card.Patient is NormalPatient) Console.WriteLine("Pacijent je normalan slucaj.");

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
