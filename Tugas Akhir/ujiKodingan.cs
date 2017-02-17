using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Configuration;

namespace Tugas_Akhir
{
    public partial class ujikoding : Form
    {
        CascadeClassifier[] haar = new CascadeClassifier[1];
        Bitmap gambar;
        string imagePath;
        public ujikoding()
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
            Image<Gray, byte> grayImage = new Image<Gray, byte>(gambar);
            Image<Bgr, byte> colorImgage = new Image<Bgr, byte>(gambar);
            for (int i = 0; i < haar.Length; i++)
            {
                //Rectangle[] kotaks = haar[i].DetectMultiScale(grayImage, 1.01, 4, new Size(170, 170), new Size(1200, 1200));//bebas
                Rectangle[] kotaks = haar[i].DetectMultiScale(grayImage, 1.01, 4, new Size(170, 170), new Size(480, 480));//extended yale b
                //Rectangle[] kotaks = haar[i].DetectMultiScale(grayImage, 1.01, 4, new Size(80, 80), new Size(300, 300));//yaleface
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
            pilihDialog.ShowDialog();
            gambar = new Bitmap(pilihDialog.FileName);
            pictureBox1.Image = gambar;
            this.imagePath = pilihDialog.FileName;
        }
        public void pilihGambarPGM(object sender, EventArgs e)
        {
            OpenFileDialog pilihDialog = new OpenFileDialog();
            pilihDialog.ShowDialog();
            PortableGrayMap gambar = new PortableGrayMap(pilihDialog.FileName);
            this.gambar  = gambar.MakeBitmap(gambar,1);
            pictureBox1.Image = this.gambar;
            System.Diagnostics.Debug.WriteLine("Lebar: "+this.gambar.Width.ToString()+"Pixel\nTinggi: "+this.gambar.Height.ToString()+"Pixel");
        }
        public void cropImage(object sender, EventArgs e)
        {
            Image<Gray, byte> grayImage = new Image<Gray, byte>(gambar);
            Image<Bgr, byte> colorImgage = new Image<Bgr, byte>(gambar);
            Image<Bgr, byte> colorImgage2 = new Image<Bgr, byte>(gambar);
            Rectangle[] kotaks = haar[0].DetectMultiScale(grayImage, 1.01, 4, new Size(40, 40), new Size(1600, 1600));
            //foreach (Rectangle kotak in kotaks)
            //{
            //    colorImgage.Draw(kotak, new Bgr(0, 255, 255), 3);
            //}
            //pictureBox1.Image = colorImgage.ToBitmap();
            MessageBox.Show("Selesai");
            //ImageCrop Croper = new ImageCrop(colorImgage2.ToBitmap(), kotaks);
            ImageCrop Croper = new ImageCrop(new Bitmap(imagePath), 170, 400);
            Bitmap[] hasilCrop = Croper.getImages();
            foreach(Bitmap cropImage in hasilCrop)
            {
                tampilHasilCrop form = new tampilHasilCrop(cropImage);
                form.Show();
            }
            System.Diagnostics.Debug.Write("\n__________________________________________________________\n");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog pilihDialog = new OpenFileDialog();
            pilihDialog.ShowDialog();
            this.gambar = PPMReader.ReadBitmapFromPPM(pilihDialog.FileName);
            pictureBox1.Image = this.gambar;
            System.Diagnostics.Debug.WriteLine("Lebar: " + this.gambar.Width.ToString() + "Pixel\nTinggi: " + this.gambar.Height.ToString() + "Pixel");
        }
    }
}
