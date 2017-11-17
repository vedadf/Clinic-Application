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
        public string DoctorNotes { get; set; }
        public List<string> CurrentHealthIssues { get; set; }
        public List<string> PastHealthIssues { get; set; }
        public string FamilyHealthIssue { get; set; }

        public List<string> Therapies { get; set; }
        public List<string> ExaminationResults { get; set; }
        public List<DateTime> ExaminationDates { get; set; }

        public HealthBook(string doctorNotes,
            List<string> currentHealthIssues = default(List<string>),
            List<string> pastHealthIssues = default(List<string>),
            string familyHealthIssue = default(string))
        {
            DoctorNotes = doctorNotes;
            CurrentHealthIssues = currentHealthIssues;
            PastHealthIssues = pastHealthIssues;
            FamilyHealthIssue = familyHealthIssue;
            Therapies = new List<string>();
            ExaminationResults = new List<string>();
            ExaminationDates = new List<DateTime>();
        }

    }
}
