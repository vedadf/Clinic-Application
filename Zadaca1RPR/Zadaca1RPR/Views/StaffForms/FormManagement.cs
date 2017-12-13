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
using Zadaca1RPR.Models.Employees;
using Zadaca1RPR.Views.InfoForms;
using Zadaca1RPR.Views.InitForms;

namespace Zadaca1RPR.Views.StaffForms
{
    public partial class FormManagement : Form
    {
        Clinic Clin;
        Staff Man;

        public FormManagement(ref Clinic clinic, Staff man)
        {
            InitializeComponent();
            textBox1.Hide();
            Clin = clinic;
            Man = man;
            label2.Text = "" + Man.Name + " " + Man.Surname;
            label9.Text = "";
            label10.Text = "";
            label11.Hide();
        }
                
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                HealthCard card = Clin.HealthCards.Find(hc => hc.Patient.CitizenID == textBox2.Text);
                new FormPatientInit(ref Clin, card).ShowDialog();
            }
            if (textBox2.Text == "") toolStripStatusLabel1.Text = "Prazno polje";
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if(!ValidCitizenID(textBox2.Text, out error))
            {
                e.Cancel = true;
                textBox2.Select(0, textBox2.Text.Length);
                errorProvider1.SetError(textBox2, error);
            }
        }

        private bool ValidCitizenID(string input, out string error)
        {
            if(input.Length != 13)
            {
                error = "Velicina mora biti 13";
                return false;
            }
            bool parse = true;
            //da li su svi brojevi
            foreach (char c in input)
            {
                int n;
                parse = Int32.TryParse(c.ToString(), out n);
                if (!parse)
                {
                    error = "JMBG mora sadrzavati samo brojeve";
                    return false;
                }
            }

            //da li je datum ispravan
            string day = "" + input[0] + input[1];
            string month = "" + input[2] + input[3];
            string year = "" + input[4] + input[5] + input[6] + input[7];
            string Bdate = year + "-" + month + "-" + day;
            DateTime bDate = new DateTime();
            if (!DateTime.TryParse(Bdate, out bDate))
            {
                error = "Datum nije ispravan";
                return false;
            }
            error = "";
            return true;
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBox2, "");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "") errorProvider1.SetError(textBox2, "Prazan JMBG");
            else
            {
                Patient pat = Clin.Patients.Find(patt => patt.CitizenID == textBox2.Text);
                if (pat != null)
                {
                    errorProvider1.SetError(textBox2, "");
                    toolStripStatusLabel1.Text = "";
                    textBox3.Text = "" + pat.Name + " " + pat.Surname;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Pacijent ne postoji";
                    textBox3.Text = "";
                }
                    
            }
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool found = false;
            foreach (Doctor doc in Clin.Doctors)
            {
                if (doc.UserName == textBox1.Text)
                {
                    toolStripStatusLabel1.Text = "";
                    label10.Text = doc.CurrentSalary.ToString() + "KM";
                    found = true;
                    break;
                }
            }
            if(!found) toolStripStatusLabel1.Text = "Doktor ne postoji";
        }

        private void listaRegistrovanihKorisnikaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new FormAllUsers(ref Clin).ShowDialog();
        }

        private void pomocToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("JMBG mora sadrzavati 13 brojeva. Polja su osjetljiva na velika i mala slova.", "Pomoc");
        }

        private void odjavaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (comboBox1.SelectedItem == null)
            {
                toolStripStatusLabel1.Text = "Ordinacija ne postoji";
                return;
            }
            else
            {
                toolStripStatusLabel1.Text = "";
                IOrdination ord = Clin.Ordinations.Find(ordd => ordd.Name == comboBox1.SelectedItem.ToString()[0].ToString());
                if (ord.PatientsQueue == null) listBox1.Items.Add("Nema");
                else
                {
                    listBox1.Items.Add("Ime Prezime JMBG");
                    listBox1.Items.Add("");
                    if (ord.Patient != null) listBox1.Items.Add(ord.Patient.Name + " " + ord.Patient.Surname + " " + ord.Patient.CitizenID);
                    foreach (Patient pat in ord.PatientsQueue)
                        listBox1.Items.Add(pat.Name + " " + pat.Surname + " " + pat.CitizenID);
                }
            }
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null)
                toolStripStatusLabel1.Text = "Odaberite jednu od opcija";
            else
            {
                textBox1.Hide();
                label11.Hide();
                toolStripStatusLabel1.Text = "";
                if (comboBox2.SelectedItem == comboBox2.Items[0])
                {
                    label9.Text = "Najposjeceniji doktor: ";
                    label10.Text = Clin.GetDoctorMostVisited().Name + " " + Clin.GetDoctorMostVisited().Surname;
                }
                else if (comboBox2.SelectedItem == comboBox2.Items[1])
                {
                    label9.Text = "Broj hitnih slucajeva: ";
                    label10.Text = Clin.GetNumOfUrgentCases().ToString();
                }
                else if (comboBox2.SelectedItem == comboBox2.Items[2])
                {
                    label9.Text = "Pacijent: " + Clin.GetPatientMostHealthIssues().CitizenID;
                    label10.Text = Clin.GetPatientMostHealthIssues().Name + " " + Clin.GetPatientMostHealthIssues().Surname;
                }
                else if (comboBox2.SelectedItem == comboBox2.Items[3])
                {
                    label9.Text = "Plata doktora: ";
                    label10.Text = "";
                    label11.Show();
                    textBox1.Show();
                }
            }
        }
    }
}
