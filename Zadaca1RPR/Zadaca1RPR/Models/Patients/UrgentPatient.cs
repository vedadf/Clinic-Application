using SharedView;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Zadaca1RPR.Abstracts;

namespace Zadaca1RPR.Models.Patients
{
    [Serializable]
    public class UrgentPatient : Patient
    {
        
        public string FirstAid { get; set; }
        public bool Deceased { get; set; }
        public string Obduction { get; set; }

        public override string Name { get; set; }
        public override string Surname { get; set; }
        public override DateTime BirthDate { get; set; }
        public override string Address { get; set; }
        public override bool Married { get; set; }
        public override DateTime RegisterDate { get; set; }
        [XmlIgnore]
        public override EnumGender Gender { get; set; }
        public override HealthBook HealthBook { get; set; }
        public override bool HasHealthCard { get; set; }
        public override List<string> Schedule { get; set; }
        public override int IDnum { get; set; }
        public override double Cost { get; set; }
        public override int numOfTimesVisited { get; set; }
        public override string CitizenID { get; set; }
        [XmlIgnore]
        public override Image img { get; set; }

        public UrgentPatient() { }

        public UrgentPatient(string firstAid, bool deceased,
            string name, string surname, DateTime birthDate, string citizenID ,string address,
            bool married, DateTime registerDate, EnumGender gender, List<string> schedule, string userName, string password, Image image,
            string obduction = "",
            HealthBook healthBook = null)
        {
            FirstAid = firstAid;
            Deceased = deceased;
            Name = name;
            Surname = surname;
            BirthDate = birthDate;            
            Address = address;
            Married = married;
            RegisterDate = registerDate;
            Gender = gender;
            HealthBook = healthBook;
            IDnum = ID; ID++;
            Schedule = schedule;
            Cost = 0;
            CitizenID = citizenID;
            Obduction = obduction;
            UserName = userName;
            img = image;
            MD5 PasswordMD5 = MD5.Create();
            Password = SView.GetHash(PasswordMD5, password);
        }

        public override int GetID() { return IDnum; }

    }
}
