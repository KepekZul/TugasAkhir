using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Tugas_Akhir
{
    public partial class Control_Panel : Form
    {
        string pathSource;
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
        public void sourceFolder(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.pathSource = folderDialog.SelectedPath;
                textBox1.Text = this.pathSource;
                allSourceFiles = Directory.GetFiles(this.pathSource, (comboBox1.SelectedItem as ComboboxItem).Value.ToString(), SearchOption.AllDirectories)
                    .Where(s => !s.EndsWith(".info") || !s.EndsWith(".txt")|| !s.EndsWith("*.ini")).ToArray();
                string[] keepFile = FilenameFilterBox.Text.Split(' ');
                for(int i=0; i<allSourceFiles.Length; i++)
                {
                    for (int j = 0; j < keepFile.Length; j++)
                    {
                        if (Path.GetFileName(allSourceFiles[i]).Contains(keepFile[j]))
                        {
                            break;
                        }
                        if (j == keepFile.Length-1)
                        {
                            allSourceFiles[i] = "";
                        }
                    }
                }
                //keperluan debugging daftar file yang terambil
                daftarData dd = new daftarData();
                dd.datas = allSourceFiles;
                dd.Show();
            }
        }

        public void destinationFolder(object sender, EventArgs e)
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
            System.Diagnostics.Debug.WriteLine(this.checkBox1.Checked.ToString());//debugging
            MultiCrop[] cropThread = new MultiCrop[2];
            string[] partisiAwal = new string[allSourceFiles.Length/2];
            for(int i=0; i<allSourceFiles.Length/2; i++)
            {
                partisiAwal[i] = allSourceFiles[i];
            }
            string[] partisiAkhir = new string[allSourceFiles.Length / 2 + ((allSourceFiles.Length % 2 == 1) ? 1 : 0)];
            for(int i =0; i< allSourceFiles.Length / 2 + ((allSourceFiles.Length % 2 == 1) ? 1 : 0); i++)
            {
                partisiAkhir[i] = allSourceFiles[i+ allSourceFiles.Length / 2];
            }
            cropThread[0] = new MultiCrop(partisiAwal, this.minSize, this.maxSize, this.checkBox1.Checked, this.pathTarget);
            cropThread[1] = new MultiCrop(partisiAkhir, this.minSize, this.maxSize, this.checkBox1.Checked, this.pathTarget);
            Thread[] cropingThread = new Thread[2];
            cropingThread[0] = new Thread(new ThreadStart(cropThread[0].CropStart));
            cropingThread[1] = new Thread(new ThreadStart(cropThread[1].CropStart));
            cropingThread[0].Start();
            cropingThread[1].Start();
        }
        private void resize(object sender, EventArgs e)
        {
            this.minSize = Convert.ToInt32(this.MinSizeBox.Text);
            this.maxSize = Convert.ToInt32(this.MaxSizeBox.Text);
            MultiResize[] resizeThread = new MultiResize[2];
            string[] partisiAwal = new string[allSourceFiles.Length/2];
            string[] partisiAkhir = new string[allSourceFiles.Length/2 + ((allSourceFiles.Length%2==1)?1:0)];
            for(int i=0; i<this.allSourceFiles.Length/2; i++)
            {
                partisiAwal[i] = allSourceFiles[i];
            }
            for (int i = 0; i < allSourceFiles.Length / 2 + ((allSourceFiles.Length % 2 == 1) ? 1 : 0); i++)
            {
                partisiAkhir[i] = allSourceFiles[i + allSourceFiles.Length / 2];
            }
            resizeThread[0] = new MultiResize(partisiAwal, this.minSize, this.maxSize, this.pathTarget);
            resizeThread[1] = new MultiResize(partisiAkhir, this.minSize, this.maxSize, this.pathTarget);
            Thread[] runningThread = new Thread[2];
            runningThread[0] = new Thread(new ThreadStart(resizeThread[0].Resize));
            runningThread[1] = new Thread(new ThreadStart(resizeThread[1].Resize));
            runningThread[0].Start();
            runningThread[1].Start();
        }
    }
    //selfmade class for dropddown list item
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
