using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Abstracts;
using Zadaca1RPR.Interfaces;
using Zadaca1RPR.Models.Patients;
//KARTON
namespace Zadaca1RPR.Models
{
    class HealthCard
    {

        public static int ID;

        public Patient Patient { get; set; }

        public int IDnumber { get; set; }

        public string CauseOfDeath { get; set; }
        public DateTime DateOfDeath { get; set; }
        public string TimeOfDeath { get; set; }

        public bool CardActive { get; set; }

        public List<string> Ordinations { get; set; }

        public HealthCard(UrgentPatient patient, string causeOfDeath = "", string timeOfDeath = "", DateTime dateOfDeath = default(DateTime))
        {
            if (!patient.Deceased && (causeOfDeath != "" || dateOfDeath != default(DateTime)))
            {
                throw new ArgumentException("Pacijent nije preminuo, parametri nemaju smisla");
            }
            
            if(patient.Deceased && (causeOfDeath == "" || dateOfDeath == default(DateTime)))
            {
                throw new ArgumentException("Pacijent je preminuo, parametri nisu popunjeni");
            }

            Patient = patient;
            if (patient.Deceased)
            {
                CardActive = false;
                CauseOfDeath = causeOfDeath;
                DateOfDeath = dateOfDeath;
                TimeOfDeath = timeOfDeath;
            }
            else
            {
                CardActive = true;
                Ordinations = patient.Schedule;
            }
            Patient.HasHealthCard = true;
            IDnumber = ID; ID++;
            patient.numOfTimesVisited++;
        }

        public HealthCard(NormalPatient patient)
        {
            Patient = patient;
            CauseOfDeath = "";
            DateOfDeath = default(DateTime);
            CardActive = true;
            IDnumber = ID; ID++;
            Patient.HasHealthCard = true;
            Ordinations = patient.Schedule;
            patient.numOfTimesVisited++;
        }

    }
}
