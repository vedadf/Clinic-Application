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
using Zadaca1RPR.Models.Employees;
using Zadaca1RPR.Models.Patients;
using Zadaca1RPR.Views.InitForms;

namespace Zadaca1RPR.Views.StaffForms
{
    public partial class FormDoctor : Form
    {
        Clinic Clin;
        Doctor Doctor;
        IOrdination Ord;
        public FormDoctor(ref Clinic clinic, Doctor doc)
        {
            InitializeComponent();
            label10.Hide();
            label11.Hide();
            textBox1.Hide();
            textBox4.Hide();
            button5.Hide();
            TreeNode node;
            node = treeView1.Nodes.Add("pat", "Pacijenti");
            Clin = clinic;
            Doctor = doc;
            label2.Text = Doctor.Name + " " + Doctor.Surname;
            foreach(IOrdination ord in Clin.Ordinations)
            {
                if(ord.Doctor == doc)
                {
                    label4.Text = ord.FullName;
                    break;
                }
            }

            foreach (Patient pat in Clin.Patients)
                if (!pat.HasHealthCard)
                    treeView1.Nodes["pat"].Nodes.Add("" + pat.Name + " " + pat.Surname + " " + pat.CitizenID);

            foreach(IOrdination ord in Clin.Ordinations)
                if(ord.Doctor == Doctor)
                {
                    Ord = ord;
                    break;
                }

            if (Ord.Patient != null)
                label8.Text = Ord.Patient.Name + " " + Ord.Patient.Surname;
            else
                label8.Text = "Nema";
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
                    toolStripLabel1.Text = "";
                    textBox3.Text = "" + pat.Name + " " + pat.Surname;
                }
                else
                {
                    toolStripLabel1.Text = "Pacijent ne postoji";
                    textBox3.Text = "";
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "") return;
            if (Clin.GetCardFromCitizenID(textBox2.Text) == null) toolStripLabel1.Text = "Pacijent nema karton, doktor kreira kartone";
            else
            {
                if (textBox3.Text != "")
                {
                    HealthCard card = Clin.HealthCards.Find(hc => hc.Patient.CitizenID == textBox2.Text);
                    new FormPatientInit(ref Clin, card).ShowDialog();
                }
                if (textBox2.Text == "") toolStripLabel1.Text = "Prazno polje";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "") return;
            if (Clin.GetCardFromCitizenID(textBox2.Text) != null) toolStripLabel1.Text = "Pacijent vec ima karton";
            else
            {
                if (textBox3.Text != "")
                {
                    Patient pat = Clin.Patients.Find(patt => patt.CitizenID == textBox2.Text);
                                        
                    if (pat is UrgentPatient)
                    {
                        HealthCard card = new HealthCard(pat as UrgentPatient, "", "", default(DateTime));
                        Clin.HealthCards.Add(card);
                    }                        
                    else if (pat is NormalPatient)
                    {
                        HealthCard card = new HealthCard(pat as NormalPatient);
                        Clin.HealthCards.Add(card);
                    }

                    treeView1.Nodes.Clear();

                    TreeNode node;
                    node = treeView1.Nodes.Add("pat", "Pacijenti");

                    foreach (Patient patt in Clin.Patients)
                        if (!patt.HasHealthCard)
                            treeView1.Nodes["pat"].Nodes.Add("" + patt.Name + " " + patt.Surname + " " + patt.CitizenID);

                    MessageBox.Show("Karton uspjesno kreiran", "Uspjeh");
                    toolStripLabel1.Text = "";
                }
                if (textBox2.Text == "") toolStripLabel1.Text = "Prazno polje";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "") return;
            if (Clin.GetCardFromCitizenID(textBox2.Text) == null) toolStripLabel1.Text = "Pacijent nema karton, doktor kreira kartone";
            else
            {
                if (textBox3.Text != "")
                {
                    Patient pat = Clin.Patients.Find(patt => patt.CitizenID == textBox2.Text);
                    if(pat.HasHealthCard == false)
                    {
                        toolStripLabel1.Text = "Pacijent nema karton";
                        return;
                    }

                    List<string> selected = new List<string>();

                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                        if (checkedListBox1.GetItemChecked(i))
                            selected.Add(checkedListBox1.Items[i].ToString()[0].ToString());

                    if(selected.Count == 0)
                    {
                        toolStripLabel1.Text = "Niste odabrali nijednu ordinaciju";
                        return;
                    }

                    foreach (string ord in selected)
                    {
                        if (pat.Schedule.Find(ordd => ordd == ord) != null)
                        {
                            toolStripLabel1.Text = "Neka od odabranih ordinacija je vec sadrzana u rasporedu pacijenta";
                            return;
                        }
                    }

                    toolStripLabel1.Text = "";
                    foreach (string sel in selected)
                        pat.Schedule.Add(sel);

                    string txt = "Uspjesno dodane nove ordinacije u raspored pacijenta" + pat.Name + " " + pat.Surname;
                        
                    MessageBox.Show(txt, "Uspjeh");

                }
                if (textBox2.Text == "") toolStripLabel1.Text = "Prazno polje";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(Ord.Patient != null)
            {
                label10.Show();
                label11.Show();
                textBox1.Show();
                textBox4.Show();
                button5.Show();             
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string examinationRes = "";
            string therapy = "";

            if (textBox1.Text.Length == 0) toolStripLabel1.Text = "Prazno polje";
            else examinationRes = textBox1.Text;

            if (textBox4.Text.Length == 0) toolStripLabel1.Text = "Prazno polje";
            else therapy = textBox4.Text;

            if(examinationRes != "" && therapy != "")
            {
                toolStripLabel1.Text = "";
                label10.Hide();
                label11.Hide();
                textBox1.Hide();
                textBox4.Hide();
                button5.Hide();
                Ord.Patient.HealthBook.ExaminationResults.Add(examinationRes);
                Ord.Patient.HealthBook.Therapies.Add(therapy);
                Ord.Patient.HealthBook.ExaminationDates.Add(DateTime.Today);
                Ord.Patient.HealthBook.CompletedOrdinations.Add(Ord.Name);
                Patient patient = Ord.Patient;
                Ord.ProcessPatient();

                if (patient.Schedule == null || patient.Schedule.Count == 0)
                {
                    MessageBox.Show("Uputite pacijenta ka portirnici, nema vise zakazanih pregleda");
                }
                else
                {
                    string txt = "Sljedeca ordinacija za pacijenta je " + patient.Schedule[0] + ".\nUspjesno procesiranje.";
                    MessageBox.Show(txt, "Uspjeh");
                    Clin.Ordinations.Find(o => o.Name == patient.Schedule[0]).NewPatient(patient);
                }

                if (Ord.OrdBusy == false) label8.Text = "Nema";
                else label8.Text = Ord.Patient.Name + " " + Ord.Patient.Surname;

            }
            
                        
        }
    }
}
