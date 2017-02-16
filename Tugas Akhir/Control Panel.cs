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
        int minSize;
        int maxSize;
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
                //keperluan debugging daftar file yang terambil
                //daftarData dd = new daftarData();
                //dd.datas = allSourceFiles;
                //dd.Show();
            }
        }

        public void destinationFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowDialog();
            this.pathTarget = folderDialog.SelectedPath;
            textBox2.Text = this.pathTarget;
        }

        private void cropSelectedFiles(object sender, EventArgs e)
        {
            this.minSize = Convert.ToInt32(this.MinSizeBox.Text);
            this.maxSize = Convert.ToInt32(this.MaxSizeBox.Text);
            foreach (string filePath in this.allSourceFiles)
            {
                ImageCrop cropImages = new ImageCrop(new Bitmap(filePath), this.minSize, this.maxSize);
                Bitmap[] hasil = cropImages.getImages();
                foreach(Bitmap gambar in hasil)
                {
                    string[] namaFile = filePath.Split('/');
                    gambar.Save(this.pathTarget+"/"+namaFile[namaFile.Length-1]);
                }
            }
        }
    }
}
