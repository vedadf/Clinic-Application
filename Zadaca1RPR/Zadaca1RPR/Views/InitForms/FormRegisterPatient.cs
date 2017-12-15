using SharedView;
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

namespace Zadaca1RPR.Views.InitForms
{
    public partial class FormRegisterPatient : Form
    {
        Clinic Clin;
        public FormRegisterPatient(ref Clinic clinic)
        {
            InitializeComponent();
            textBox9.Text = DateTime.Today.ToShortDateString();
            toolStripStatusLabel1.Text = "Popunite sva odgovarajuca polja";
            Clin = clinic;
        }

        private void pomocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Popunite sva polja.\nNova polja ce biti vidljiva u ovisnosti od unosa.");
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
             
            string error;
            if (!SView.HasOnlyLetters(textBox2.Text, out error))
            {
                e.Cancel = true;
                textBox2.Select(0, textBox2.Text.Length);
                errorProvider1.SetError(textBox2, error);
            }
            
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBox2, "");
        }

        private void textBox4_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBox4, "");
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (!SView.HasOnlyLetters(textBox4.Text, out error))
            {
                e.Cancel = true;
                textBox4.Select(0, textBox4.Text.Length);
                errorProvider1.SetError(textBox4, error);
            }
        }

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (!SView.ValidCitizenID(textBox5.Text, out error))
            {
                e.Cancel = true;
                textBox5.Select(0, textBox5.Text.Length);
                errorProvider1.SetError(textBox5, error);
            }
        }

        private void textBox5_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBox5, "");
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text == "") errorProvider1.SetError(textBox5, "Prazan JMBG");
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBox1, "");
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (!SView.HasOnlyLettersAndDigits(textBox1.Text, out error))
            {
                e.Cancel = true;
                textBox1.Select(0, textBox4.Text.Length);
                errorProvider1.SetError(textBox1, error);
            }
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (!SView.ValidDate(textBox3.Text, out error))
            {
                e.Cancel = true;
                textBox3.Select(0, textBox3.Text.Length);
                errorProvider1.SetError(textBox3, error);
            }
        }

        private void textBox3_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBox3, "");
        }

        private void textBox6_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBox6, "");
        }

        private void textBox6_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (!SView.HasOnlyLettersAndDigitsAllowSpace(textBox6.Text, out error))
            {
                e.Cancel = true;
                textBox6.Select(0, textBox6.Text.Length);
                errorProvider1.SetError(textBox6, error);
            }
        }

        private void comboBox3_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(comboBox3, "");
        }

        private void comboBox3_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (!SView.HasOnlyLetters(comboBox3.Text, out error))
            {
                e.Cancel = true;
                comboBox3.Select(0, comboBox3.Text.Length);
                errorProvider1.SetError(comboBox3, error);
            }
        }

        private void comboBox2_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(comboBox2, "");
        }

        private void comboBox2_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (!SView.HasOnlyLetters(comboBox2.Text, out error))
            {
                e.Cancel = true;
                comboBox2.Select(0, comboBox2.Text.Length);
                errorProvider1.SetError(comboBox2, error);
            }
        }
    }
}
