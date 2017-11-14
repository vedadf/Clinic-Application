using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Models.Patients;
//ZDRAVSTVENA KNJIZICA
namespace Zadaca1RPR.Models
{
    class HealthBook
    {
        public string[] DoctorNotes { get; set; }
        public string[] CurrentHealthIssues { get; set; }
        public string[] PastHealthIssues { get; set; }
        public string[] FamilyHealthIssues { get; set; }

        public HealthBook(string[] doctorNotes,
            string[] currentHealthIssues = default(string[]),
            string[] pastHealthIssues = default(string[]),
            string[] familyHealthIssues = default(string[]))
        {
            DoctorNotes = doctorNotes;
            CurrentHealthIssues = currentHealthIssues;
            PastHealthIssues = pastHealthIssues;
            FamilyHealthIssues = familyHealthIssues;
        }

    }
}
