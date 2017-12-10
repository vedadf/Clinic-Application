using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zadaca1RPR.Views.InitForms
{
    public partial class FormPatientInit : Form
    {
        public FormPatientInit()
        {
            InitializeComponent();
            tabControl1.TabPages.Remove(tabPage2);
        }
        
    }
}
