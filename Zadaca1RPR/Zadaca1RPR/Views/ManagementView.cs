using SharedView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Models;
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
                    int id;
                    List<string> schedule = null;
                    Console.WriteLine("Upisite identifikacijski broj pacijenta: ");
                    id = Convert.ToInt32(Console.ReadLine());
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
                    i4 = Console.ReadLine();
                    if (i4 != "1" || i4 != "2") ProcessCardSearch(ref clinic, i4);
                    else SView.NoCommand();
                    Main(ref clinic);
                    break;
                case "3":
                    break;
                case "4":
                    Program.ChooseRole(ref clinic);
                    break;
                default:
                    SView.NoCommand();
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
