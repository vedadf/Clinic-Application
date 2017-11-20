using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Abstracts;

namespace Zadaca1RPR.Models.Patients
{
    class NormalPatient : Patient
    {
        public override string Name { get; set; }
        public override string Surname { get; set; }
        public override DateTime BirthDate { get; set; }
        public override string Address { get; set; }
        public override bool Married { get; set; }
        public override DateTime RegisterDate { get; set; }
        public override EnumGender Gender { get; set; }
        public override HealthBook HealthBook { get; set; }
        public override bool HasHealthCard { get; set; }
        public override List<string> Schedule { get; set; }
        public override int IDnum { get; set; }
        public override double Cost { get; set; }
        public override int numOfTimesVisited { get; set; }
        public override string CitizenID { get; set; }

        public NormalPatient(string name, string surname, DateTime birthDate, string citizenID, string address,
           bool married, DateTime registerDate, EnumGender gender,
           List<string> schedule, HealthBook healthBook = null)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            Address = address;
            Married = married;
            RegisterDate = registerDate;
            Gender = gender;
            HealthBook = healthBook;
            Schedule = schedule;
            IDnum = ID; ID++;
            Cost = 0;
            CitizenID = citizenID;
        }

        public override int GetID() { return IDnum; }
    }
}
