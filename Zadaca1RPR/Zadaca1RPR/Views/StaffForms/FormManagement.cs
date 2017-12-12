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
            if (!Clinic.Ordinations.Exists(ord => ord.Name == textBox1.Text))
                toolStripStatusLabel1.Text = "Ime ordinacije nije ispravno, pogledajte 'Pomoc'";
            else
            {
                IOrdination ord = Clinic.Ordinations.Find(ordd => ordd.Name == textBox1.Text);
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
            for(int i = 0; i < Clinic.Ordinations.Count; i++)
            {
                ordinations += Clinic.Ordinations[i].Name;
                if (i != Clinic.Ordinations.Count - 1) ordinations += ", ";                
            }
            MessageBox.Show(ordinations);
        }

        private void FillListBox(string ordName)
        {

        }

       
    }
}
