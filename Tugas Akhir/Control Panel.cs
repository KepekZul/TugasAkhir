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
            ComboboxItem ppm = new ComboboxItem() , pgm = new ComboboxItem(), all = new ComboboxItem();
            ppm.Text = "ppm file";
            ppm.Value = "*ppm";
            pgm.Text = "pgm file";
            pgm.Value = "*.pgm";
            all.Text = "bitmap";
            all.Value = "*.*";
            comboBox1.Items.Add(ppm);
            comboBox1.Items.Add(pgm);
            comboBox1.Items.Add(all);
            comboBox1.SelectedIndex = 2;
        }
        public void sourceFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();
            this.pathSumber = folderDialog.SelectedPath;
            textBox1.Text = this.pathSumber;
            if (result == DialogResult.OK)
            {
                allSourceFiles = Directory.GetFiles(this.pathSumber, (comboBox1.SelectedItem as ComboboxItem).Value.ToString(), SearchOption.AllDirectories)
                    .Where(s => !s.EndsWith(".info") || !s.EndsWith(".txt")|| !s.EndsWith("*.ini")).ToArray();
                //keperluan debugging daftar file yang terambil
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

        private void cropSelectedFiles(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(this.checkBox1.Checked.ToString());//debugging
            this.minSize = Convert.ToInt32(this.MinSizeBox.Text);
            this.maxSize = Convert.ToInt32(this.MaxSizeBox.Text);
            foreach (string filePath in this.allSourceFiles)
            {
                System.Diagnostics.Debug.WriteLine("proses file "+filePath+" min");
                ImageCrop cropImages;
                if ((comboBox1.SelectedItem as ComboboxItem).Value.ToString() == "*ppm")
                {
                    cropImages = new ImageCrop(PPMReader.ReadBitmapFromPPM(filePath), this.minSize, this.maxSize, this.checkBox1.Checked);
                } else if ((comboBox1.SelectedItem as ComboboxItem).Value.ToString() == "*.pgm")
                {
                    PortableGrayMap pgmBaru = new PortableGrayMap(filePath);
                    cropImages = new ImageCrop(pgmBaru.MakeBitmap(pgmBaru, 1), this.minSize, this.maxSize, this.checkBox1.Checked);
                    pgmBaru = null;
                }else
                {
                    cropImages = new ImageCrop(new Bitmap(filePath), this.minSize, this.maxSize, this.checkBox1.Checked);
                }
                Bitmap[] hasil = cropImages.getImages();
                int i = 1;
                if(hasil.Length==0)
                {
                    if((comboBox1.SelectedItem as ComboboxItem).Value.ToString() == "*.ppm")
                    {
                        new Bitmap(PPMReader.ReadBitmapFromPPM(filePath)).Save(Path.GetFullPath(pathTarget) + "/" + Path.GetFileName(Path.GetFileNameWithoutExtension(filePath)) + " gagal crop" + ".gif");
                    }
                    else if ((comboBox1.SelectedItem as ComboboxItem).Value.ToString() =="*.pgm")
                    {
                        PortableGrayMap pgm = new PortableGrayMap(filePath);
                        new Bitmap(pgm.MakeBitmap(pgm,1)).Save(Path.GetFullPath(pathTarget) + "/" + Path.GetFileName(Path.GetFileNameWithoutExtension(filePath)) + "gagal crop" + ".gif");
                        pgm = null;
                    }
                    else
                    {
                        new Bitmap(filePath).Save(Path.GetFullPath(pathTarget) + "/" + Path.GetFileName(filePath) + "gagal crop" + ".gif");
                    }
                }
                foreach(Bitmap gambar in hasil)
                {
                    gambar.Save(Path.GetFullPath(pathTarget)+"/"+Path.GetFileName(filePath)+i.ToString()+".gif");
                    i++;
                }
            }
        }
    }
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
