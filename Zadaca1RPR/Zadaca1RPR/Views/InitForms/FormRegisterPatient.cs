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
using Zadaca1RPR.Interfaces;
using Zadaca1RPR.Models;
using Zadaca1RPR.Models.Patients;

namespace Zadaca1RPR.Views.InitForms
{
    public partial class FormRegisterPatient : Form
    {
        Clinic Clin;

        bool urgentCase = false;
        string citizenID = null;
        string name = null;
        string surname = null;
        DateTime dateOfBirth = DateTime.Now;
        EnumGender gender = EnumGender.Male;
        string username = null;
        string password = null;
        string address = null;
        bool married = false;
        DateTime registerDate = DateTime.Now;
        List<string> ordinations;
        bool deceased = false;
        string firstaid = null;
        DateTime dateOfDeath = DateTime.Now;
        string timeOfDeath = null;
        string causeOfDeath = null;
        string obduction = null; 

        public FormRegisterPatient(ref Clinic clinic)
        {
            InitializeComponent();
            ordinations = new List<string>();
            groupBox1.Hide();
            groupBox4.Hide();
            textBox9.Text = DateTime.Today.ToShortDateString();
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
            name = textBox2.Text;
            errorProvider1.SetError(textBox2, "");
        }

        private void textBox4_Validated(object sender, EventArgs e)
        {
            surname = textBox4.Text;
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
            else
            {
                foreach (Patient pat in Clin.Patients)
                {
                    if (pat.CitizenID == textBox5.Text)
                    {
                        e.Cancel = true;
                        error = "JMBG vec postoji";
                        textBox5.Select(0, textBox5.Text.Length);
                        errorProvider1.SetError(textBox5, error);
                        break;
                    }
                }
            }
        }

        private void textBox5_Validated(object sender, EventArgs e)
        {
            citizenID = textBox5.Text;
            errorProvider1.SetError(textBox5, "");
            string dateFormat = "" + textBox5.Text[0] + textBox5.Text[1] + '/' + textBox5.Text[2] + textBox5.Text[3] + '/' + textBox5.Text[4] + textBox5.Text[5] + textBox5.Text[6] + textBox5.Text[7];
            textBox3.Text = dateFormat;
            citizenID = textBox5.Text;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text == "") errorProvider1.SetError(textBox5, "Prazan JMBG");       
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            username = textBox1.Text;
            errorProvider1.SetError(textBox1, "");           
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (!SView.HasOnlyLettersAndDigits(textBox1.Text, out error))
            {
                e.Cancel = true;
                textBox1.Select(0, textBox1.Text.Length);
                errorProvider1.SetError(textBox1, error);
            }
            else
            {
                foreach (Patient pat in Clin.Patients)
                {
                    if (pat.UserName == textBox1.Text)
                    {
                        e.Cancel = true;
                        error = "JMBG vec postoji";
                        textBox1.Select(0, textBox1.Text.Length);
                        errorProvider1.SetError(textBox1, error);
                        break;
                    }
                }
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
            DateTime.TryParse(textBox3.Text, out dateOfBirth);
            errorProvider1.SetError(textBox3, "");
        }

        private void textBox6_Validated(object sender, EventArgs e)
        {
            address = textBox6.Text;
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

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            if(comboBox4.SelectedIndex == 0)
            {
                checkedListBox1.SetItemChecked(0, true);
                checkedListBox1.SetItemChecked(1, true);
                checkedListBox1.SetItemChecked(4, true);
            }
            else
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1) groupBox1.Hide();
            else if(comboBox1.SelectedIndex == 0) groupBox1.Show();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked) groupBox4.Show();
            else if (radioButton3.Checked) groupBox4.Hide();
        }

        private void textBox11_Validated(object sender, EventArgs e)
        {
            DateTime.TryParse(textBox11.Text, out dateOfDeath);
            errorProvider1.SetError(textBox11, "");
        }

        private void textBox11_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (!SView.ValidDate(textBox11.Text, out error))
            {
                e.Cancel = true;
                textBox11.Select(0, textBox11.Text.Length);
                errorProvider1.SetError(textBox11, error);
            }
        }

        private void textBox12_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if(!SView.ValidTime(textBox12.Text, out error))
            {
                e.Cancel = true;
                textBox12.Select(0, textBox12.Text.Length);
                errorProvider1.SetError(textBox12, error);
            }
        }

        private void textBox12_Validated(object sender, EventArgs e)
        {
            timeOfDeath = textBox12.Text;
            errorProvider1.SetError(textBox12, "");
        }

        private void textBox10_Validated(object sender, EventArgs e)
        {
            firstaid = textBox10.Text;
            errorProvider1.SetError(textBox10, "");
        }

        private void textBox10_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if(!SView.HasOnlyLettersAndDigitsAllowSpace(textBox10.Text, out error))
            {
                e.Cancel = true;
                textBox10.Select(0, textBox10.Text.Length);
                errorProvider1.SetError(textBox10, error);
            }
        }

        private void textBox13_Validated(object sender, EventArgs e)
        {
            causeOfDeath = textBox13.Text;
            errorProvider1.SetError(textBox13, "");
        }

        private void textBox13_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (!SView.HasOnlyLettersAndDigitsAllowSpace(textBox13.Text, out error))
            {
                e.Cancel = true;
                textBox13.Select(0, textBox13.Text.Length);
                errorProvider1.SetError(textBox13, error);
            }
        }

        private void textBox14_Validated(object sender, EventArgs e)
        {
            obduction = textBox14.Text;
            errorProvider1.SetError(textBox14, "");
        }

        private void textBox14_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (!SView.HasOnlyLettersAndDigitsAllowSpace(textBox14.Text, out error))
            {
                e.Cancel = true;
                textBox14.Select(0, textBox14.Text.Length);
                errorProvider1.SetError(textBox14, error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!radioButton1.Checked && !radioButton2.Checked)
                toolStripStatusLabel1.Text = "Odaberite spol osobe";
            else if (!radioButton3.Checked && !radioButton4.Checked && groupBox1.Visible)
                toolStripStatusLabel1.Text = "Odaberite da li je pacijent prezivio";
            else if (!radioButton5.Checked && !radioButton6.Checked && groupBox4.Visible)
                toolStripStatusLabel1.Text = "Odaberite da li je odradjena obdukcija ili ne";
            else
            {
                toolStripStatusLabel1.Text = "";
                if (comboBox2.SelectedIndex == -1)
                    toolStripStatusLabel1.Text = "Odaberite bracni status";
                if (comboBox1.SelectedIndex == -1)
                    toolStripStatusLabel1.Text = "Odaberite status pacijenta";
                if (comboBox4.SelectedIndex == -1)
                    toolStripStatusLabel1.Text = "Odaberite preglede za pacijenta";

                List<string> ordinations = new List<string>();
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    if (checkedListBox1.GetItemChecked(i))
                        ordinations.Add(checkedListBox1.Items[i].ToString()[0].ToString());
                if(ordinations.Count == 0 || ordinations == null)
                {
                    toolStripStatusLabel1.Text = "Niste odabrali ordinacije";
                    return;
                }

                if (toolStripStatusLabel1.Text != "") return;

                if (comboBox1.SelectedIndex == 0) urgentCase = true;
                else if(comboBox1.SelectedIndex == 1) urgentCase = false;

                if (radioButton3.Checked) deceased = false;
                else if (radioButton4.Checked) deceased = true;

                if (urgentCase)
                {
                    if (radioButton1.Checked) gender = EnumGender.Male;
                    else if (radioButton2.Checked) gender = EnumGender.Female;
                    Patient pat = new UrgentPatient(firstaid, deceased, name, surname, dateOfBirth, citizenID, address, married, registerDate, gender, ordinations, username, password, obduction);
                    HealthCard card = new HealthCard(pat as UrgentPatient, causeOfDeath, timeOfDeath, dateOfDeath);
                }
                else
                {

                }
            }
        }

        private void textBox8_Validated(object sender, EventArgs e)
        {
            password = textBox8.Text;
            errorProvider1.SetError(textBox8, "");
        }

        private void textBox8_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (textBox8.Text.Length == 0) {
                error = "Ne smije biti prazno";
                e.Cancel = true;
                textBox8.Select(0, textBox8.Text.Length);
                errorProvider1.SetError(textBox8, error);
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton5.Checked)
            {
                label26.Hide();
                textBox14.Hide();
            }
            else if (radioButton6.Checked)
            {
                label26.Show();
                textBox14.Show();
            }
        }
        
    }
}
