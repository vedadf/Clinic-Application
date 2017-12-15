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
using Zadaca1RPR.Views.InitForms;

namespace Zadaca1RPR.Views.StaffForms
{
    public partial class FormTech : Form
    {
        Clinic Clin;
        Staff Tech;
        public FormTech(ref Clinic clinic, Staff tech)
        {
            InitializeComponent();         
            Clin = clinic;
            Tech = tech;
            label2.Text = Tech.Name + " " + Tech.Surname;
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (!SView.ValidCitizenID(textBox2.Text, out error))
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "") return; 
            if(Clin.GetCardFromCitizenID(textBox2.Text) == null) toolStripStatusLabel1.Text = "Pacijent nema karton, doktor kreira kartone";
            else
            {
                if (MessageBox.Show("Da li ste sigurni?", "Brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    DeletePatient(textBox2.Text);
            }            
        }

        private void DeletePatient(string id)
        {
            HealthCard card = Clin.GetCardFromCitizenID(id);
            
            if (Clin.DeletePatientCardFromCardID(card.IDnumber)) MessageBox.Show("Uspjesno obrisan karton pacijenta.");
            else MessageBox.Show("Doslo je do greske prilikom brisanja.");            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "") return;
            if (Clin.GetCardFromCitizenID(textBox2.Text) == null) toolStripStatusLabel1.Text = "Pacijent nema karton, doktor kreira kartone";
            else
            {
                if (textBox3.Text != "")
                {
                    HealthCard card = Clin.HealthCards.Find(hc => hc.Patient.CitizenID == textBox2.Text);
                    new FormPatientInit(ref Clin, card).ShowDialog();
                }
                if (textBox2.Text == "") toolStripStatusLabel1.Text = "Prazno polje";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            if (textBox3.Text == "") return;
            if (Clin.GetCardFromCitizenID(textBox2.Text) == null) toolStripStatusLabel1.Text = "Pacijent nema karton, doktor kreira kartone";
            else
            {
                Patient pat = Clin.GetCardFromCitizenID(textBox2.Text).Patient;
                if (pat.Schedule.Count > 0) toolStripStatusLabel1.Text = "Pacijent nije zavrsio sa svojim pregledima";
                else
                {
                    bool regular = false;
                    if (pat.numOfTimesVisited > 3) regular = true;
                    else regular = false;

                    double res = pat.Cost;
                    if (!regular) res = pat.Cost + 0.15 * pat.Cost;
                    else res = pat.Cost - 0.1 * pat.Cost;

                    string message = "Pacijent je posjetio kliniku " + pat.numOfTimesVisited + " puta.\n";
                    message += "Pacijent je odradio " + pat.HealthBook.ExaminationDates.Count + " pregleda.\n";
                    
                    message += "Cijena za placanje na 3 rate je: " + res / 3.0 + " po rati. \n";
                    message += "Cijena za placanje gotovinom je: " + res + "\n";

                    message += "Da li zelite izvrsiti naplatu?";

                    if(MessageBox.Show(message, "Naplata", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Clin.GetCardFromCitizenID(textBox2.Text).CardActive = false;
                        pat.HasHealthCard = false;
                        Clin.HealthCards.Remove(Clin.GetCardFromCitizenID(textBox2.Text));
                        MessageBox.Show("Operacija uspjesna.");
                    }
                }
            }
        }

        private void pomocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Da bi se odjavili: desni klik -> Odjava");
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FormRegisterPatient(ref Clin).ShowDialog();
        }
    }
}
