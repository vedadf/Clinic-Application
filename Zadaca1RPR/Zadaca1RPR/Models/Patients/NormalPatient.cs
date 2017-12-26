using SharedView;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Abstracts;

namespace Zadaca1RPR.Models.Patients
{
    public class NormalPatient : Patient
    {
        public override string UserName { get; set; }
        public override string Password { get; set; }
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
        public override Image img { get; set; }

        public NormalPatient(string name, string surname, DateTime birthDate, string citizenID, string address,
           bool married, DateTime registerDate, EnumGender gender, string userName, string password, Image image,
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
            UserName = userName;
            img = image;
            MD5 PasswordMD5 = MD5.Create();
            Password = SView.GetHash(PasswordMD5, password);
        }

        public override int GetID() { return IDnum; }
    }
}
