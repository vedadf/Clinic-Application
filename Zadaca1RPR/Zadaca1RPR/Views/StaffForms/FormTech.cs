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

namespace Zadaca1RPR.Views.StaffForms
{
    public partial class FormTech : Form
    {
        Clinic Clinic;
        Staff Tech;
        public FormTech(ref Clinic clinic, Staff tech)
        {
            InitializeComponent();
            Clinic = clinic;
            Tech = tech;
        }
    }
}
