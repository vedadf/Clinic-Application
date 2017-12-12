using SharedView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zadaca1RPR.Abstracts;
using Zadaca1RPR.Models;
using Zadaca1RPR.Models.Employees;
using Zadaca1RPR.Views.StaffForms;

namespace Zadaca1RPR.Views.InitForms
{
    public partial class FormStaffInit : Form
    {
        Clinic Clinic;
        public FormStaffInit(ref Clinic clinic)
        {
            InitializeComponent();
            Clinic = clinic;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
                toolStripStatusLabel1.Text = "Neko od polja je prazno";
            else
            {
                MD5 pwMD5 = MD5.Create();
                if (radioButton1.Checked) ValidateDoctor(pwMD5);
                else if (radioButton2.Checked) ValidateManagement(pwMD5);
                else if (radioButton3.Checked) ValidateTech(pwMD5);
                else toolStripStatusLabel1.Text = "Molimo odaberite neku od tri date opcije";
            }
        }

        private void ValidateDoctor(MD5 md5)
        {
            bool found = false;
            foreach(Doctor doc in Clinic.Doctors)
                if(doc.UserName == textBox1.Text && doc.Password == SView.GetHash(md5, textBox2.Text))
                {
                    found = true;
                    new FormDoctor(ref Clinic, doc).ShowDialog();
                }                   
            if(!found) toolStripStatusLabel1.Text = "Doktor sa navedenim podacima ne postoji";
        }

        private void ValidateTech(MD5 md5)
        {
            bool found = false;
            foreach (Staff tech in Clinic.Employees)
                if (tech is Technician && tech.UserName == textBox1.Text && tech.Password == SView.GetHash(md5, textBox2.Text))
                {
                    found = true;
                    new FormTech(ref Clinic, tech).ShowDialog();                    
                }
            if(!found) toolStripStatusLabel1.Text = "Tehnicar sa navedenim podacima ne postoji";
        }

        private void ValidateManagement(MD5 md5)
        {
            bool found = false;
            foreach (Staff man in Clinic.Employees)
                if (man is Management && man.UserName == textBox1.Text && man.Password == SView.GetHash(md5, textBox2.Text))
                {
                    found = true;
                    new FormManagement(ref Clinic, man).ShowDialog();
                }
            if(!found) toolStripStatusLabel1.Text = "Uprava sa navedenim podacima ne postoji";
        }

    }
}
