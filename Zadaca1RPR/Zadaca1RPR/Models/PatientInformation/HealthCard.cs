using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Interfaces.Patient;
using Zadaca1RPR.Models.Patients;
//KARTON
namespace Zadaca1RPR.Models
{
    class HealthCard
    {

        public IPatient Patient { get; set; }

        public string CauseOfDeath { get; set; }
        public DateTime TimeOfDeath { get; set; }

        public bool CardActive { get; set; }

        public HealthCard(UrgentPatient patient, string causeOfDeath = "", DateTime timeOfDeath = default(DateTime))
        {
            if (!patient.Deceased && (causeOfDeath != "" || timeOfDeath != default(DateTime)))
            {
                //throw exception
            }
            
            if(patient.Deceased && (causeOfDeath == "" || timeOfDeath == default(DateTime)))
            {
                //throw exception
            }

            Patient = patient;
            if (patient.Deceased)
            {
                CardActive = false;
                CauseOfDeath = causeOfDeath;
                TimeOfDeath = timeOfDeath;
            }
        }

        public HealthCard(NormalPatient patient)
        {
            Patient = patient;
            CauseOfDeath = "";
            TimeOfDeath = default(DateTime);
        }
        
    }
}
