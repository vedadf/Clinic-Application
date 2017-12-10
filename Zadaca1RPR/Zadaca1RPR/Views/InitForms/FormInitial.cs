using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zadaca1RPR.Views.InitForms
{
    public partial class FormInitial : Form
    {
        public FormInitial()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FormPatientInit().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FormStaffInit().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
