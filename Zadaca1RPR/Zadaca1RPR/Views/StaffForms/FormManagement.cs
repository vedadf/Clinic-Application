using SharedView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Zadaca1RPR.Abstracts;
using Zadaca1RPR.Interfaces;
using Zadaca1RPR.Models;
using Zadaca1RPR.Models.Employees;
using Zadaca1RPR.Models.Patients;
using Zadaca1RPR.Views.InfoForms;
using Zadaca1RPR.Views.InitForms;

namespace Zadaca1RPR.Views.StaffForms
{
    public partial class FormManagement : Form
    {
        Clinic Clin;
        Staff Man;

        string name = null;
        string surname = null;
        string userName = null;
        string password = null;
        double salary = 0;

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
            if (textBox3.Text == "") { toolStripStatusLabel1.Text = "Prazno polje"; return; }
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

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if(!SView.ValidCitizenID(textBox2.Text, out error))
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

        private void button1_Click(object sender, EventArgs e)
        {



            if(textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "")
            {
                toolStripStatusLabel1.Text = "Neka polja su prazna";
                return;
            }

            salary = (double)numericUpDown1.Value;
            userName = textBox4.Text;
            password = textBox5.Text;

            name = textBox6.Text;
            surname = textBox7.Text;

            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                toolStripStatusLabel1.Text = "Odaberite jednu od opcija";
                return;
            }

            if (radioButton1.Checked)
            {

                Staff staff = new Technician(name, surname, salary, userName, password);

                foreach(Staff s in Clin.Employees)
                    if(s.UserName == staff.UserName)
                    {
                        toolStripStatusLabel1.Text = "Korisnicko ime vec postoji";
                        return;
                    }
                    

                Clin.Employees.Add(staff);
            }
            else if (radioButton2.Checked)
            {
                Staff staff = new Doctor(name, surname, salary, userName, password);

                foreach (Staff s in Clin.Employees)
                    if (s.UserName == staff.UserName)
                    {
                        toolStripStatusLabel1.Text = "Korisnicko ime vec postoji";
                        return;
                    }

                Clin.Employees.Add(staff);
            }

            MessageBox.Show("Korisnik uspjesno registrovan");

            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";

        }

        private void textBox6_Validating(object sender, CancelEventArgs e)
        {

            string error;
            if (!SView.HasOnlyLetters(textBox6.Text, out error))
            {
                e.Cancel = true;
                textBox6.Select(0, textBox6.Text.Length);
                errorProvider1.SetError(textBox6, error);
            }

        }

        private void textBox6_Validated(object sender, EventArgs e)
        {
            name = textBox6.Text;
            errorProvider1.SetError(textBox6, "");
        }

        private void textBox4_Validated(object sender, EventArgs e)
        {
            userName = textBox4.Text;
            errorProvider1.SetError(textBox4, "");
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (!SView.HasOnlyLettersAndDigits(textBox4.Text, out error))
            {
                e.Cancel = true;
                textBox4.Select(0, textBox4.Text.Length);
                errorProvider1.SetError(textBox4, error);
            }

        }

        private void textBox5_Validated(object sender, EventArgs e)
        {
            password = textBox5.Text;
            errorProvider1.SetError(textBox5, "");
        }

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (textBox5.Text.Length == 0)
            {
                error = "Ne smije biti prazno";
                e.Cancel = true;
                textBox5.Select(0, textBox5.Text.Length);
                errorProvider1.SetError(textBox5, error);
            }
        }

        private void textBox7_Validated(object sender, EventArgs e)
        {
            surname = textBox7.Text;
            errorProvider1.SetError(textBox7, "");
        }

        private void textBox7_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (!SView.HasOnlyLetters(textBox7.Text, out error))
            {
                e.Cancel = true;
                textBox7.Select(0, textBox7.Text.Length);
                errorProvider1.SetError(textBox7, error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "") { toolStripStatusLabel1.Text = "Prazno polje"; return; }
            if (Clin.GetCardFromCitizenID(textBox2.Text) == null) toolStripStatusLabel1.Text = "Pacijent nema karton, doktor kreira kartone";
            else
            {
                if (textBox3.Text != "")
                {
                    HealthCard card = Clin.HealthCards.Find(hc => hc.Patient.CitizenID == textBox2.Text);
                                       
                    List<HealthCard> cards = new List<HealthCard> { card };
                    string id = card.Patient.CitizenID;
                    try
                    {
                        bool exists = false;
                        if (File.Exists(id + ".xml"))
                            exists = true;                        
                        
                        XmlSerializer xs = new XmlSerializer(typeof(List<HealthCard>), new Type[] { typeof(HealthBook), typeof(Patient),
                            typeof(NormalPatient), typeof(UrgentPatient) });
                        FileStream fs = new FileStream(id + ".xml", FileMode.Create);
                        xs.Serialize(fs, cards.ToList<HealthCard>());
                        fs.Close();

                        if (exists) MessageBox.Show("Datoteka je ponovno kreirana.");
                        else MessageBox.Show("Datoteka je kreirana bin -> debug.");

                    }
                    catch(Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }

                }
                if (textBox2.Text == "") toolStripStatusLabel1.Text = "Prazno polje";
            }



        }
    }
}
