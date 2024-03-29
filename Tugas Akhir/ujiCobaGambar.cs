﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Gabor;
using System.Configuration;
using MySql.Data;

namespace Tugas_Akhir
{
    public partial class ujiCobaGambar : Form
    {
        CascadeClassifier[] haar = new CascadeClassifier[1];
        Bitmap gambar;
        string imagePath;
        int min;
        int max;
        List<string> listhapus = new List<string>();
        public ujiCobaGambar()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(ConfigurationManager.AppSettings["haar_cascade_path"]);
            haar[0] = new CascadeClassifier(ConfigurationManager.AppSettings["1"]);
            //haar[1] = new CascadeClassifier("E:\\program files\\emgucv-windesktop 3.1.0.2504\\etc\\haarcascades\\haarcascade_frontalface_alt_tree.xml");
            //haar[2] = new CascadeClassifier("E:\\program files\\emgucv-windesktop 3.1.0.2504\\etc\\haarcascades\\haarcascade_frontalface_alt2.xml");
            //haar[3] = new CascadeClassifier("E:\\program files\\emgucv-windesktop 3.1.0.2504\\etc\\haarcascades\\haarcascade_frontalface_default.xml");
            //haar[4] = new CascadeClassifier("E:\\program files\\emgucv-windesktop 3.1.0.2504\\etc\\haarcascades\\haarcascade_profileface.xml");
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }
        private void deteksiWajah(object sender, EventArgs e)
        {
            this.min = Convert.ToInt32(MinBox.Text);
            this.max = Convert.ToInt32(MaxBox.Text);
            Image<Gray, byte> grayImage = new Image<Gray, byte>(gambar);
            Image<Bgr, byte> colorImgage = new Image<Bgr, byte>(gambar);
            for (int i = 0; i < haar.Length; i++)
            {
                //Rectangle[] kotaks = haar[i].DetectMultiScale(grayImage, 1.01, 4, new Size(170, 170), new Size(1200, 1200));//bebas
                //Rectangle[] kotaks = haar[i].DetectMultiScale(grayImage, 1.01, 4, new Size(170, 170), new Size(480, 480));//extended yale b
                Rectangle[] kotaks = haar[i].DetectMultiScale(grayImage, 1.01, 4, new Size(this.min,this.min), new Size(this.max, this.max));//yaleface
                //Rectangle[] kotaks = haar[i].DetectMultiScale(grayImage, 1.01, 4, new Size(60, 60), new Size(100, 100));//orlface
                int warna = 255;
                int nomor = 1;
                foreach (Rectangle kotak in kotaks)
                {
                    colorImgage.Draw(kotak, new Bgr(0, warna, warna), 3);
                    System.Diagnostics.Debug.WriteLine("Rectangle #"+nomor++.ToString()+" lebar: "+kotak.Width.ToString()+". tinggi: "+kotak.Height.ToString()+".");
                    warna -= 50;
                }
                if (kotaks.Length > 0)
                {
                    System.Diagnostics.Debug.WriteLine("cascader #"+i.ToString());
                }
            }
            pictureBox1.Image = colorImgage.ToBitmap();
            MessageBox.Show("Selesai");
            System.Diagnostics.Debug.Write("\n__________________________________________________________\n");
        }
        public void pilihGambar(object sender, EventArgs e)
        {
            OpenFileDialog pilihDialog = new OpenFileDialog();
            DialogResult hasil = pilihDialog.ShowDialog();
            if (hasil != DialogResult.OK)
                return;
            this.imagePath = pilihDialog.FileName;
            gambar = new Bitmap(pilihDialog.FileName);
            pictureBox1.Image = gambar;
            this.imagePath = pilihDialog.FileName;
        }
        public void pilihGambarPGM(object sender, EventArgs e)
        {
            OpenFileDialog pilihDialog = new OpenFileDialog();
            DialogResult hasil = pilihDialog.ShowDialog();
            if (hasil != DialogResult.OK)
                return; PortableGrayMap gambar = new PortableGrayMap(pilihDialog.FileName);
            this.imagePath = pilihDialog.FileName;
            this.gambar  = gambar.MakeBitmap(gambar,1);
            pictureBox1.Image = this.gambar;
            System.Diagnostics.Debug.WriteLine("Lebar: "+this.gambar.Width.ToString()+"Pixel\nTinggi: "+this.gambar.Height.ToString()+"Pixel");
        }
        public void cropImage(object sender, EventArgs e)
        {
            this.min = Convert.ToInt32(MinBox.Text);
            this.max = Convert.ToInt32(MaxBox.Text);
            Image<Gray, byte> grayImage = new Image<Gray, byte>(gambar);
            Image<Bgr, byte> colorImgage = new Image<Bgr, byte>(gambar);
            Image<Bgr, byte> colorImgage2 = new Image<Bgr, byte>(gambar);
            Rectangle[] kotaks = haar[0].DetectMultiScale(grayImage, 1.01, 4, new Size(this.min, this.min), new Size(this.max, this.max));
            //foreach (Rectangle kotak in kotaks)
            //{
            //    colorImgage.Draw(kotak, new Bgr(0, 255, 255), 3);
            //}
            //pictureBox1.Image = colorImgage.ToBitmap();
            MessageBox.Show("Selesai");
            //ImageCrop Croper = new ImageCrop(colorImgage2.ToBitmap(), kotaks);
            ImageCrop Croper = new ImageCrop(this.gambar , this.min, this.max, true);
            Bitmap[] hasilCrop = Croper.getImages();
            foreach(Bitmap cropImage in hasilCrop)
            {
                tampilHasilCrop form = new tampilHasilCrop(cropImage);
                form.Show();
            }
            System.Diagnostics.Debug.Write("\n__________________________________________________________\n");
        }

        private void pilihGambarPPM(object sender, EventArgs e)
        {
            OpenFileDialog pilihDialog = new OpenFileDialog();
            DialogResult hasil = pilihDialog.ShowDialog();
            if (hasil != DialogResult.OK)
                return;
            this.imagePath = pilihDialog.FileName;
            this.gambar = PPMReader.ReadBitmapFromPPM(pilihDialog.FileName);
            pictureBox1.Image = this.gambar;
            System.Diagnostics.Debug.WriteLine("Lebar: " + this.gambar.Width.ToString() + "Pixel\nTinggi: " + this.gambar.Height.ToString() + "Pixel");
        }

        private void pilihFolderDuplikat(object sender, EventArgs e)
        {
            string[] allSourceFiles;
            string selekpeth = pilihFolderHapus();
            if (selekpeth == "" || selekpeth == null)
                return;
            allSourceFiles = Directory.GetFiles(selekpeth);
            for (int i=0; i<allSourceFiles.Length-1; i++)
            {
                if (allSourceFiles[i].EndsWith("2.gif"))
                {
                    if (Path.GetFileName(allSourceFiles[i - 1]).StartsWith(Path.GetFileName(allSourceFiles[i]).Substring(0, 20)))
                    {
                        deleteGambar satu = new deleteGambar(allSourceFiles[i - 1], allSourceFiles[i]);
                        satu.ShowDialog();
                        this.listhapus.Add(satu.yangDihapus);
                    }
                }
            }
            foreach(string berkas in listhapus)
            {
                richTextBox1.AppendText(berkas+"\n");
            }
        }
        private string pilihFolderHapus()
        {
            FolderBrowserDialog folderdialog = new FolderBrowserDialog();
            folderdialog.ShowDialog();
            return folderdialog.SelectedPath;
        }

        private void hapusFile(object sender, EventArgs e)
        {
            string[] hapus = richTextBox2.Text.Split('\n');
            foreach (string berkas in hapus)
            {
                System.Diagnostics.Debug.WriteLine(berkas);
                File.Delete(berkas);
            }
        }

        private void applyHisteq(object sender, EventArgs e)
        {
            Image<Gray, byte> gambar = new Image<Gray, byte>(this.gambar);
            gambar._EqualizeHist();
            this.gambar = gambar.ToBitmap();
            this.pictureBox1.Image = gambar.ToBitmap();
        }

        private void gaborFilter(object sender, EventArgs e)
        {
            GaborKernel krnl, krnle2;
            GaborFilter filter;
            krnl = new GaborKernel(0, 4);
            krnle2 = new GaborKernel(4, 2);
            filter = new GaborFilter();
            Image<Gray, float> gaborGambar = new Image<Gray, float>(this.gambar);
            Image<Gray, float> tr = filter.Convolution(gaborGambar, krnl, GABOR_TYPE.GABOR_MAG);
            Image<Gray, float> tr2 = filter.Convolution(gaborGambar, krnle2, GABOR_TYPE.GABOR_MAG);
            tampilHasilCrop ini = new tampilHasilCrop(tr.ToBitmap());
            tampilHasilCrop itu = new tampilHasilCrop(tr2.ToBitmap());
            ini.Show();
            itu.Show();
        }

        private void saveImage(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.ShowDialog();
            string path = sd.FileName;
            pictureBox1.Image.Save(path+".gif");
        }

        private void DRLDPClick(object sender, EventArgs e)
        {
            DRLocalDirectionalPattern ldp = new DRLocalDirectionalPattern(this.gambar);
            ldp.GetDRLDPMatrix();
            int lebar = ldp.DrLdpMatrix.GetLength(0);
            int tinggi = ldp.DrLdpMatrix.GetLength(0);
            Bitmap hasil = new Bitmap(lebar, tinggi);
            for(int i=0; i<lebar; i++)
            {
                for(int j=0; j<tinggi; j++)
                {
                    hasil.SetPixel(i, j, Color.FromArgb(ldp.DrLdpMatrix[i, j], ldp.DrLdpMatrix[i, j], ldp.DrLdpMatrix[i, j]));
                }
            }
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.Image = hasil;
            System.Diagnostics.Debug.WriteLine(tinggi.ToString() + " ");
            lebar = ldp.LdpResult.GetLength(0);
            tinggi = ldp.LdpResult.GetLength(0);
            hasil = new Bitmap(lebar, tinggi);
            for (int i = 0; i < lebar; i++)
            {
                for (int j = 0; j < tinggi; j++)
                {
                    hasil.SetPixel(i, j, Color.FromArgb(ldp.LdpResult[i, j], ldp.LdpResult[i, j], ldp.LdpResult[i, j]));
                }
            }
            pictureBox1.Image = hasil;
            System.Diagnostics.Debug.Write(tinggi.ToString());
        }

        private void Rename_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.ShowDialog();
            FolderBrowserDialog df = new FolderBrowserDialog();
            df.ShowDialog();
            string[] nama = Directory.GetFiles(fd.SelectedPath);
            foreach(string namaFile in nama)
            {
                string[] namas = Path.GetFileName(namaFile).Split(' ', '.');
                string ukuran = namas[0], jeneng = namas[1], label = jeneng.Split('_')[0];
                try
                {
                    File.Copy(namaFile, Path.Combine(df.SelectedPath, ukuran + ".yale_b." + jeneng +"."+label+ ".gif"));
                }
                catch(Exception k)
                {
                    System.Diagnostics.Debug.WriteLine(k.ToString() + " "+ df.SelectedPath+"/"+ ukuran + ".orl_face." + jeneng +"."+label +".gif");
                }
            }
        }

        private void pgmToBitmap_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog bdf = new FolderBrowserDialog();
            bdf.ShowDialog();
            string[] allSourceFiles = Directory.GetFiles(bdf.SelectedPath, "*.pgm", SearchOption.AllDirectories);
            bdf.ShowDialog();
            foreach(string pathfile in allSourceFiles)
            {
                PortableGrayMap pgmImage = new PortableGrayMap(pathfile);
                Bitmap bitmapImage = pgmImage.MakeBitmap(pgmImage, 1);
                bitmapImage.Save(bdf.SelectedPath+"/"+ Path.GetFileNameWithoutExtension(pathfile)+".gif");
                System.Diagnostics.Debug.WriteLine("berhasil " + bdf.SelectedPath + Path.GetFileNameWithoutExtension(pathfile) + ".gif");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            kirschEdgeDetection kirs = new kirschEdgeDetection(this.gambar);
            kirs.getEdge(Int32.Parse( this.maskIndex.Text));
            Bitmap iniEdge = new Bitmap(kirs.finalMatrix.GetLength(0), kirs.finalMatrix.GetLength(1));
            for(int i=0; i<kirs.finalMatrix.GetLength(0); i++)
            {
                for(int j=0; j<kirs.finalMatrix.GetLength(1); j++)
                {
                    iniEdge.SetPixel(i, j, Color.FromArgb(kirs.finalMatrix[i, j], kirs.finalMatrix[i, j], kirs.finalMatrix[i, j]));
                }
            }
            this.pictureBox2.Image = iniEdge;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string ldp = "";
            Bitmap sementara = new Bitmap(this.pictureBox1.Image);
            for (int i = 0; i < this.pictureBox1.Image.Height; i++)
            {
                for (int j = 0; j < this.pictureBox1.Image.Width; j++)
                {
                    ldp += sementara.GetPixel(i, j).G+" ";
                }
                ldp += "\n";
            }
            Hasil_klasifikasi ldped = new Hasil_klasifikasi(ldp);
            ldped.Text = "ldp";

            string raw_data = "";
            Bitmap sementara2 = new Bitmap(this.pictureBox2.Image);
            for (int i = 0; i < sementara2.Height; i++)
            {
                for (int j = 0; j < sementara2.Width; j++)
                {
                    raw_data += sementara2.GetPixel(i, j).G + " ";
                }
                raw_data += "\n";
            }
            Hasil_klasifikasi raw = new Hasil_klasifikasi(raw_data);
            raw.Text = "drldp";

            raw.Show();
            ldped.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Bitmap input = new Bitmap(5, 5);
            var data = richTextBox1.Text;
            var parsed_data = data.Split('\n', ' ');
            int x = 0;
            for(int i=0; i<5; i++)
            {
                for(int j=0; j<5; j++)
                {
                    input.SetPixel(i, j, Color.FromArgb(int.Parse(parsed_data[x]), int.Parse(parsed_data[x]), int.Parse(parsed_data[x])));
                    x++;
                }
            }
            DRLocalDirectionalPattern ini = new DRLocalDirectionalPattern(input);
            ini.GetDRLDPMatrix();
            string hasil = "";
            for(int i=0; i<3; i++)
            {
                for(int j=0; j<3; j++)
                {
                    hasil += ini.LdpResult[i, j]+" ";
                }
                hasil += '\n';
            }
            richTextBox2.Text = hasil;
        }
    }
}
