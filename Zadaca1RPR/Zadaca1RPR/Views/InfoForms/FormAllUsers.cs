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
using Zadaca1RPR.Models.Employees;
using Zadaca1RPR.Models.Patients;

namespace Zadaca1RPR.Views.InfoForms
{
    public partial class FormAllUsers : Form
    {
        Clinic Clin;
        public FormAllUsers(ref Clinic clinic)
        {
            InitializeComponent();
            Clin = clinic;
            FillTree();
        }

        private void FillTree()
        {
            TreeNode node;
            node = treeView1.Nodes.Add("pat", "Pacijenti");

            node.Nodes.Add("ns", "Normalni Slucajevi");
            foreach(Patient pat in Clin.Patients)
                if(pat is NormalPatient)          
                    treeView1.Nodes["pat"].Nodes["ns"].Nodes.Add("" + pat.Name + " " + pat.Surname + " " + pat.CitizenID);
            
            node.Nodes.Add("hs", "Hitni Slucajevi");
            foreach (Patient pat in Clin.Patients)
                if (pat is UrgentPatient)
                    treeView1.Nodes["pat"].Nodes["hs"].Nodes.Add("" + pat.Name + " " + pat.Surname + " " + pat.CitizenID);
                    
            node = treeView1.Nodes.Add("st", "Uposlenici");
            node.Nodes.Add("up", "Uprava");
            foreach (Staff staff in Clin.Employees)
                if (staff is Management)
                    treeView1.Nodes["st"].Nodes["up"].Nodes.Add("" + staff.Name + " " + staff.Surname);

            node.Nodes.Add("dok", "Doktori");
            foreach (Doctor doc in Clin.Doctors)
                    treeView1.Nodes["st"].Nodes["dok"].Nodes.Add("" + doc.Name + " " + doc.Surname);

            node.Nodes.Add("te", "Tehnicari");
            foreach (Staff staff in Clin.Employees)
                if (staff is Technician)
                    treeView1.Nodes["st"].Nodes["te"].Nodes.Add("" + staff.Name + " " + staff.Surname);
        }

    }
}
