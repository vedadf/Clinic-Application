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
            /*
            else if (Pat.Schedule.Count > 0) Console.WriteLine("Pacijent nije zavrsio sa svojim pregledima.");
            else
            {
                bool regular = false;
                Console.WriteLine("Pacijent je {0} {1}", Pat.Name, Pat.Surname);
                Console.WriteLine("Pacijent je posjetio kliniku {0} puta.", Pat.numOfTimesVisited);
                if (Pat.numOfTimesVisited > 3) regular = true;
                else regular = false;
                Console.WriteLine("Pacijent je odradio {0} pregleda.", Pat.HealthBook.ExaminationDates.Count);
                string cc;
                while (true)
                {
                    Console.WriteLine("Da li pacijent zeli platiti na rate ili gotovinom? (R/G)");
                    cc = Console.ReadLine();
                    if (cc == "R" || cc == "G") break;
                    else SView.NoCommand();
                }

                Console.WriteLine("Glavna cijena je: {0}", Pat.Cost);

                foreach (string str in Pat.HealthBook.CompletedOrdinations)
                    Console.WriteLine("Ordinacija: {0}; Cijena: {1};", str, clinic.Ordinations.Find(o => o.Name == str).Price);

                //delegat iskoristen za racunanje cijene za pacijenta
                CalculatePatientDebt price = () =>
                {
                    double res = Pat.Cost;
                    if (cc == "R")
                        if (!regular) res = Pat.Cost + 0.15 * Pat.Cost;
                        else if (cc == "G")
                            if (regular) res = Pat.Cost - 0.1 * Pat.Cost;
                    return res;
                };

                double cost = price();

                if (cc == "R")
                {
                    Console.WriteLine("Pacijent moze platiti na 3 rate podijeljene na 3 jednaka dijela.");
                    if (regular)
                    {
                        Console.WriteLine("Cijena za placanje na rate za redovnog pacijenta ostaje ista.");
                        Console.WriteLine("Prva rata koja mora biti placena odmah iznosi {0}KM", cost / 3.0);
                    }
                    else
                    {
                        Console.WriteLine("Cijena za placanje na rate za novog pacijenta: {0}KM", cost);
                        Console.WriteLine("Prva rata koja mora biti placena odmah iznosi {0}KM", cost / 3.0);
                    }
                }
                else
                {
                    if (regular)
                        Console.WriteLine("Cijena za placanje gotovinom za redovnog pacijenta: {0}KM.", cost);
                    else
                        Console.WriteLine("Cijena za placanje gotovinom za novog pacijenta ostaje ista.");
                }

                Console.WriteLine("Karton za pacijenta je zatvoren.");
                clinic.GetCardFromPatientID(idd).CardActive = false;
                Pat.HasHealthCard = false;
                clinic.HealthCards.Remove(clinic.GetCardFromPatientID(idd));
                
            }*/
        }
    }
}
