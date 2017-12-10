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

namespace Zadaca1RPR.Views.InitForms
{
    public partial class FormStaffInit : Form
    {
        Clinic Clinic;
        public FormStaffInit(ref Clinic clinic)
        {
            InitializeComponent();
            Clinic = clinic;
        }
    }
}
