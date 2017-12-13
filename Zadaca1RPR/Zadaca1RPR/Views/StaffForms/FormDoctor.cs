using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zadaca1RPR.Models;
using Zadaca1RPR.Models.Employees;

namespace Zadaca1RPR.Views.StaffForms
{
    public partial class FormDoctor : Form
    {
        Clinic Clin;
        Doctor Doctor;
        public FormDoctor(ref Clinic clinic, Doctor doc)
        {
            InitializeComponent();
            Clin = clinic;
            Doctor = doc;
        }
        
    }
}
