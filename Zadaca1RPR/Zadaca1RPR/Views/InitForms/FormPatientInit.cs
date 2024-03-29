﻿using System;
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
using Zadaca1RPR.Models.Patients;

namespace Zadaca1RPR.Views.InitForms
{
    public partial class FormPatientInit : Form
    {
        Clinic Clin;
        public FormPatientInit(ref Clinic clinic, HealthCard card)
        {
            InitializeComponent();
            tabControl1.TabPages.Remove(tabPage2);
            Clin = clinic;
            FillInfo(card);
        }

        private void FillInfo(HealthCard card)
        {
            Patient pat = card.Patient;
            if (pat.HealthBook != null)
            {
                tabControl1.TabPages.Add(tabPage2);
                FillHealthBookInfo(pat);
            }
            
            textBox2.Text = pat.Name;
            textBox4.Text = pat.Surname;
            textBox5.Text = pat.CitizenID;

            textBox3.Text = pat.BirthDate.ToShortDateString();
            textBox6.Text = pat.Address;
            if (pat.Gender == EnumGender.Male) textBox7.Text = "Musko";
            else textBox7.Text = "Zensko";
            if (pat.Married) textBox8.Text = "Ozenjen";
            else textBox8.Text = "Neozenjen";
            textBox9.Text = pat.RegisterDate.ToShortDateString();
            textBox10.Text = pat.numOfTimesVisited.ToString();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = pat.img;

            if (card.CardActive) textBox11.Text = "Aktivan";
            else textBox11.Text = "Neaktivan";

            if (pat is UrgentPatient)
            {
                UrgentPatient urg = pat as UrgentPatient;
                if (urg.Deceased) textBox13.Text = "Preminuo";
                else textBox13.Text = "Nije preminuo";

                if(urg.Obduction == null || urg.Obduction == "")
                    textBox14.Text = "Nema";
                else
                    textBox14.Text = urg.Obduction;

                textBox15.Text = urg.FirstAid;

            }
            else
            {
                textBox13.Text = "Nije preminuo";
                textBox14.Text = "Normalan slucaj";
                textBox15.Text = "Normalan slucaj";
            }

            string list = "";
            for(int i = 0; i < pat.Schedule.Count; i++)
            {                
                list = list + pat.Schedule[i];
                if (i != pat.Schedule.Count - 1) list = list + ", ";
            }
            if (list != "")
                textBox12.Text = list;
            else
                textBox12.Text = "Nema zakazanih pregleda";
            
        }
        
        private void FillHealthBookInfo(Patient pat)
        {
            if (pat.HealthBook == null) return;

            if (pat.HealthBook.CurrentHealthIssues.Count != 0)
                foreach (string p in pat.HealthBook.CurrentHealthIssues)
                    listBox1.Items.Add(p);
            else listBox1.Items.Add("Nema");

            if (pat.HealthBook.PastHealthIssues.Count != 0)
                foreach (string p in pat.HealthBook.PastHealthIssues)
                    listBox2.Items.Add(p);
            else listBox2.Items.Add("Nema");

            if (pat.HealthBook.Therapies.Count != 0)
                foreach (string p in pat.HealthBook.Therapies)
                    listBox3.Items.Add(p);
            else listBox3.Items.Add("Nema");

            if (pat.HealthBook.CompletedOrdinations.Count != 0)
                foreach (string p in pat.HealthBook.CompletedOrdinations)
                    listBox4.Items.Add(p);
            else listBox4.Items.Add("Nema");

            if (pat.HealthBook.ExaminationResults.Count != 0)
                foreach (string p in pat.HealthBook.ExaminationResults)
                    listBox5.Items.Add(p);
            else listBox5.Items.Add("Nema");

            if (pat.HealthBook.DoctorNotes == null || pat.HealthBook.DoctorNotes == "")
                textBox16.Text = "Nema";
            else textBox16.Text = pat.HealthBook.DoctorNotes;
        }

        private void ResetLists()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
        }
        
    }
}
