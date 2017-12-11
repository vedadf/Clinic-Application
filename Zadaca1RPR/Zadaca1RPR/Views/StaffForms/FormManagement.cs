using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zadaca1RPR.Abstracts;
using Zadaca1RPR.Models;

namespace Zadaca1RPR.Views.StaffForms
{
    public partial class FormManagement : Form
    {
        Clinic Clinic;
        Staff Man;
        public FormManagement(ref Clinic clinic, Staff man)
        {
            InitializeComponent();
            Clinic = clinic;
            Man = man;
        }
    }
}
