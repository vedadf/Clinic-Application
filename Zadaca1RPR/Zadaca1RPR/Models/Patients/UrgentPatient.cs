using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Interfaces.Patient;

namespace Zadaca1RPR.Models.Patients
{
    class UrgentPatient : IPatient
    {

        public string FirstAid { get; set; }
        public bool Deceased { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int IDnumber { get; set; }
        public string Address { get; set; }
        public bool Married { get; set; }
        public DateTime RegisterDate { get; set; }
        public EnumGender Gender { get; set; }
        public HealthBook HealthBook { get; set; }

        public UrgentPatient(string firstAid, bool deceased,
            string name, string surname, DateTime birthDate, int IDnumber, string address,
            bool married, DateTime registerDate, EnumGender gender,
            HealthBook healthBook)
        {
            FirstAid = firstAid;
            Deceased = deceased;
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            this.IDnumber = IDnumber;
            Address = address;
            Married = married;
            RegisterDate = registerDate;
            Gender = gender;
            healthBook = HealthBook;
        }
        
    }
}
