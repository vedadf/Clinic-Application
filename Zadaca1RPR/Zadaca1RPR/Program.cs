using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Interfaces;
using Zadaca1RPR.Models;
using Zadaca1RPR.Models.Employees;
using Zadaca1RPR.Abstracts;
using Zadaca1RPR.Models.Ordinations;
using Zadaca1RPR.Models.Patients;
using Zadaca1RPR.Views;
using SharedView;
using Zadaca1RPR.Views.InitForms;

namespace Zadaca1RPR
{
    class Program
    {       
        static void Init()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            List<Staff> employees17336_1 = new List<Staff>();
            List<IOrdination> ordinations17336_1 = new List<IOrdination>();
            List<Patient> patients17336_1 = new List<Patient>();
            List<HealthCard> cards17336_1 = new List<HealthCard>();

            Staff doctor17336_1 = new Doctor("Emir", "Amar", 2000.0, "ea", "123456");
            Staff doctor17336_2 = new Doctor("Mirza", "Mirzic", 2100.0, "mm", "123");
            Staff doctor17336_3 = new Doctor("Amar", "Emir", 1700.0, "ae", "456");
            Staff doctor17336_4 = new Doctor("Hamza", "Hamzic", 1700.0, "hh", "456fase");
            Staff doctor17336_5 = new Doctor("Tata", "Mamic", 1700.0, "tm", "fsaer1");
            Staff technician17336_1 = new Technician("Ekrem", "Ekric", 1500.0, "ee", "afad41");
            employees17336_1.Add(technician17336_1);
            employees17336_1.Add(doctor17336_1);
            IOrdination ordination17336_1 = new Laboratory((Doctor)doctor17336_1);
            IOrdination ordination17336_2 = new Dermatology((Doctor)doctor17336_2);
            IOrdination ordination17336_3 = new Cardiology((Doctor)doctor17336_3);
            IOrdination ordination17336_4 = new Surgeoncy((Doctor)doctor17336_4);
            IOrdination ordination17336_5 = new Radiology((Doctor)doctor17336_5);

            EnumGender male = EnumGender.Male;
            HealthBook healthbook17336_1 = new HealthBook("Pacijent je jos u losem stanju", new List<string> { "Teska povreda noge" }, new List<string> { "Nema" }, "Nema");
            HealthBook healthbook17336_2 = new HealthBook("Pacijent je jos u dobrom stanju", new List<string> { "Prehlada", "Glavobolja" }, new List<string> { "Problemi" }, "Nema");
            Patient patient17336_1 = new NormalPatient("Amar", "Buric", new DateTime(1996, 1, 1), "0101199612345", "Visoko, Piramida 2", false, DateTime.Today, male, new List<string> { "L", "K" }, healthbook17336_2);
            Patient patient17336_2 = new UrgentPatient("Prva pomoc je uspjesna.", false, "Elvir", "Crncevic", new DateTime(1996, 8, 17), "1708199612345", "Dobrinja 4568", false, DateTime.Today, male, new List<string> { "L", "R", "H" }, "Nema", healthbook17336_1);
            patients17336_1.Add(patient17336_1);
            patients17336_1.Add(patient17336_2);

            HealthCard healthcard17336_1 = new HealthCard((NormalPatient)patient17336_1);
            HealthCard healthcard17336_2 = new HealthCard((UrgentPatient)patient17336_2);

            cards17336_1.Add(healthcard17336_1);
            cards17336_1.Add(healthcard17336_2);

            ordination17336_1.NewPatient(patient17336_1);
            ordination17336_1.NewPatient(patient17336_2);

            ordinations17336_1.Add(ordination17336_1);
            ordinations17336_1.Add(ordination17336_2);
            ordinations17336_1.Add(ordination17336_3);
            ordinations17336_1.Add(ordination17336_4);
            ordinations17336_1.Add(ordination17336_5);

            Clinic clinic17336_1 = new Clinic(employees17336_1, ordinations17336_1, cards17336_1, patients17336_1);

        }
        [STAThread]
        static void Main(string[] args)
        {
            Init();
            //ChooseRole(ref clinic17336_1);
            Application.Run(new FormInitial());
        }
        
        public static void ChooseRole(ref Clinic clinic)
        {
            while (true)
            {
                string choice;
                Console.WriteLine("Vi ste?");
                Console.WriteLine("1. Tehnicar");
                Console.WriteLine("2. Doktor");
                Console.WriteLine("3. Uprava");
                Console.WriteLine("4. Pacijent");
                Console.WriteLine("5. Izlaz");
                choice = Console.ReadLine();
                if (choice == "1" || choice == "tehnicar" || choice == "Tehnicar")
                {
                    TechView techv = new TechView();
                    techv.Main(ref clinic);
                    break;
                }
                else if (choice == "2" || choice == "doktor" || choice == "Doktor")
                {
                    DoctorView docv = new DoctorView();
                    docv.Main(ref clinic);
                    break;
                }
                else if (choice == "3" || choice == "uprava" || choice == "Uprava")
                {
                    ManagementView mang = new ManagementView();
                    mang.Main(ref clinic);
                    break;
                }
                else if (choice == "4" || choice == "pacijent" || choice == "Pacijent")
                {
                    PatientView pat = new PatientView();
                    pat.Main(ref clinic);
                    break;
                }
                else if (choice == "5" || choice == "izlaz" || choice == "Izlaz")
                {
                    Environment.Exit(0);
                }
                else { SView.NoCommand(); continue; }
            }
        }

    }
}
