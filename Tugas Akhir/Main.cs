using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.Util;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace Tugas_Akhir
{
    public partial class Form1 : Form
    {
        CascadeClassifier[] haar = new CascadeClassifier[1];
        Bitmap gambar;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        { 
            haar[0] = new CascadeClassifier("E:\\program files\\emgucv-windesktop 3.1.0.2504\\etc\\haarcascades\\haarcascade_frontalface_alt.xml");
            //haar[1] = new CascadeClassifier("E:\\program files\\emgucv-windesktop 3.1.0.2504\\etc\\haarcascades\\haarcascade_frontalface_alt_tree.xml");
            //haar[2] = new CascadeClassifier("E:\\program files\\emgucv-windesktop 3.1.0.2504\\etc\\haarcascades\\haarcascade_frontalface_alt2.xml");
            //haar[3] = new CascadeClassifier("E:\\program files\\emgucv-windesktop 3.1.0.2504\\etc\\haarcascades\\haarcascade_frontalface_default.xml");
            //haar[4] = new CascadeClassifier("E:\\program files\\emgucv-windesktop 3.1.0.2504\\etc\\haarcascades\\haarcascade_profileface.xml");
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }
        private void deteksiWajah(object sender, EventArgs e)
        {
            Image<Gray, byte> grayImage = new Image<Gray, byte>(gambar);
            Image<Bgr, byte> colorImgage = new Image<Bgr, byte>(gambar);
            for (int i = 0; i < haar.Length; i++)
            {
                Rectangle[] kotaks = haar[i].DetectMultiScale(grayImage, 1.01, 4, new Size(40, 40), new Size(1600, 1600));
                foreach (Rectangle kotak in kotaks)
                {
                    colorImgage.Draw(kotak, new Bgr(0, 255, 255), 3);
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
            pilihDialog.ShowDialog();
            gambar = new Bitmap(pilihDialog.FileName);
            pictureBox1.Image = gambar;
        }
        public void pilihGambarPGM(object sender, EventArgs e)
        {
            OpenFileDialog pilihDialog = new OpenFileDialog();
            pilihDialog.ShowDialog();
            PortableGrayMap gambar = new PortableGrayMap(pilihDialog.FileName);
            this.gambar  = gambar.MakeBitmap(gambar,1);
            pictureBox1.Image = this.gambar;
        }
        public void cropImage(object sender, EventArgs e)
        {
            Image<Gray, byte> grayImage = new Image<Gray, byte>(gambar);
            Image<Bgr, byte> colorImgage = new Image<Bgr, byte>(gambar);
            Image<Bgr, byte> colorImgage2 = new Image<Bgr, byte>(gambar);
            Rectangle[] kotaks = haar[0].DetectMultiScale(grayImage, 1.01, 4, new Size(40, 40), new Size(1600, 1600));
            foreach (Rectangle kotak in kotaks)
            {
                colorImgage.Draw(kotak, new Bgr(0, 255, 255), 3);
            }
            pictureBox1.Image = colorImgage.ToBitmap();
            MessageBox.Show("Selesai");
            ImageCrop Croper = new ImageCrop(colorImgage2.ToBitmap(), kotaks);
            Bitmap[] hasilCrop = Croper.getImages();
            foreach(Bitmap cropImage in hasilCrop)
            {
                tampilHasilCrop form = new tampilHasilCrop(cropImage);
                form.Show();
            }
            System.Diagnostics.Debug.Write("\n__________________________________________________________\n");
        }
    }
}
