using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Interfaces.Patient;

namespace Zadaca1RPR.Models.Patients
{
    class NormalPatient : IPatient
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int IDnumber { get; set; }
        public string Address { get; set; }
        public bool Married { get; set; }
        public DateTime RegisterDate { get; set; }
        public EnumGender Gender { get; set; }
        public HealthBook HealthBook { get; set; }

        public NormalPatient(string name, string surname, DateTime birthDate, int IDnumber, string address,
           bool married, DateTime registerDate, EnumGender gender,
           HealthBook healthBook = null)
        {
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
