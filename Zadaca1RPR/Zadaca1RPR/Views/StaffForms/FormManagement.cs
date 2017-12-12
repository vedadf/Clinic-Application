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

namespace Zadaca1RPR.Views.StaffForms
{
    public partial class FormManagement : Form
    {
        Clinic Clinic;
        Staff Man;

        public FormManagement(ref Clinic clinic, Staff man)
        {
            InitializeComponent();
            Clinic = clinic;
            Man = man;
            label2.Text = "" + Man.Name + " " + Man.Surname;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (!Clinic.Ordinations.Exists(ord => ord.Name == comboBox1.SelectedItem.ToString()[0].ToString()))
                toolStripStatusLabel1.Text = "Ime ordinacije nije ispravno, pogledajte 'Pomoc'";
            else
            {
                IOrdination ord = Clinic.Ordinations.Find(ordd => ordd.Name == comboBox1.SelectedItem.ToString()[0].ToString());
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

        private void button2_Click(object sender, EventArgs e)
        {
            string ordinations = "Imena dostupnih ordinacija su: ";
            for (int i = 0; i < Clinic.Ordinations.Count; i++)
            {
                ordinations += Clinic.Ordinations[i].Name;
                if (i != Clinic.Ordinations.Count - 1) ordinations += ", ";
            }
            MessageBox.Show(ordinations);
        }

        private void FillListBox(string ordName)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
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
    }
}
