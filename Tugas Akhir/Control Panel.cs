using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tugas_Akhir
{
    public partial class Control_Panel : Form
    {
        string pathSumber;
        string pathTarget;
        string[] allSourceFiles;
        public Control_Panel()
        {
            InitializeComponent();
        }
        public void sourceFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();
            this.pathSumber = folderDialog.SelectedPath;
            textBox1.Text = this.pathSumber;
            if (result == DialogResult.OK)
            {
                if (this.pgmCheck.Checked == true)
                {
                    allSourceFiles = Directory.GetFiles(this.pathSumber, "*.pgm", SearchOption.AllDirectories);
                }
                else {
                    allSourceFiles = Directory.GetFiles(this.pathSumber, "*.*", SearchOption.AllDirectories).Where(s => !s.EndsWith(".info") || !s.EndsWith(".txt")).ToArray();
                }
                daftarData dd = new daftarData();
                dd.datas = allSourceFiles;
                dd.Show();
            }
        }

        public void destinationFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowDialog();
            this.pathTarget = folderDialog.SelectedPath;
            textBox2.Text = this.pathTarget;
        }
    }
}
