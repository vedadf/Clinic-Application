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

namespace Zadaca1RPR.Views
{
    public partial class DirectoryListing : Form
    {

        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

        public DirectoryListing()
        {
            InitializeComponent();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await ListItems();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            label2.Text = Path.GetFileName(folderBrowserDialog.SelectedPath);
        }

        private async Task<int> ListItems()
        {
            string foldername = folderBrowserDialog.SelectedPath;            
            string[] files = Directory.GetFiles(foldername);
            string[] folders = Directory.GetDirectories(foldername);
            string[] all = files.Concat(folders).ToArray();
            Array.Sort(all);
            foreach (string f in all)
            {
                await Task.Delay(500);
                listBox1.Items.Add(Path.GetFileName(f));
            }
            return 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            listBox1.Items.Clear();
        }
    }
}
