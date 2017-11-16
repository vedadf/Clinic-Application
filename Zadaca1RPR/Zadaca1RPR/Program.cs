using System;
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

namespace Zadaca1RPR
{
    class Program
    {
        static void Main(string[] args)
        {
            

            List<Staff> employees = new List<Staff>();
            List<IOrdination> ordinations = new List<IOrdination>();
            List<Patient> patients = new List<Patient>();
            List<HealthCard> cards = new List<HealthCard>();

            Staff doct = new Doctor("Vedad", 2000);
            Staff tech = new Technician("Ekrem", 1500);
            employees.Add(tech);
            employees.Add(doct);

            IOrdination lab = new Laboratory((Doctor)doct, "EKG");
            ordinations.Add(lab);
            
            EnumGender male = EnumGender.Male;
            Patient patient = new NormalPatient("Amar", "Buric", new DateTime(1, 1, 1), "Visoko, piramida 2", false, DateTime.Today, male, new List<string> { "L", "K" });
            Patient patient2 = new UrgentPatient("Lose lose", false, "Elvir", "Crncevic", new DateTime(1, 1, 1), "Dobrinja 4568", false, DateTime.Today, male, new List<string> { "L", "R", "O" });
            patients.Add(patient);
            patients.Add(patient2);

            HealthCard hc = new HealthCard((UrgentPatient)patient2);
            cards.Add(hc);

            Clinic clinic = new Clinic(employees, ordinations, cards, patients);

            ChooseRole(ref clinic);
            
        }
        
        public static void ChooseRole(ref Clinic clinic)
        {
            while (true)
            {
                string choice;
                Console.WriteLine("Vi ste?");
                Console.WriteLine("1. Portir");
                Console.WriteLine("2. Doktor");
                Console.WriteLine("3. Uprava");
                Console.WriteLine("4. Izlaz");
                choice = Console.ReadLine();
                if (choice == "1" || choice == "portir" || choice == "Portir")
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
                else if (choice == "4" || choice == "izlaz" || choice == "Izlaz")
                {
                    Environment.Exit(0);
                }
                else { SView.NoCommand(); continue; }
            }
        }

    }
}
