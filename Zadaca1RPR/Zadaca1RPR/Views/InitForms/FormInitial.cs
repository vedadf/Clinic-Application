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
    public partial class FormInitial : Form
    {
        Clinic Clinic;
        public FormInitial(ref Clinic clinic)
        {
            Clinic = clinic;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FormPatientInit(ref Clinic).ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FormStaffInit(ref Clinic).ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
