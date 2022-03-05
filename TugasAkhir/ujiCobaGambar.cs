using Emgu.CV;
using Emgu.CV.Structure;
using Gabor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Tugas_Akhir
{
    public partial class ujiCobaGambar : Form
    {
        private CascadeClassifier[] _haar = new CascadeClassifier[1];
        private Bitmap _image;
        private int _min;
        private int max;
        private List<string> listhapus = new List<string>();

        public ujiCobaGambar()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _haar[0] = new CascadeClassifier(ConfigurationManager.AppSettings["1"]);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }
        private void DeteksiWajah(object sender, EventArgs e)
        {
            _min = Convert.ToInt32(MinBox.Text);
            max = Convert.ToInt32(MaxBox.Text);
            var grayImage = new Image<Gray, byte>(_image);
            var colorImgage = new Image<Bgr, byte>(_image);
            for (var i = 0; i < _haar.Length; i++)
            {
                var kotaks = _haar[i].DetectMultiScale(grayImage, 1.01, 4, new Size(_min, _min), new Size(max, max));//yaleface
                int warna = 255;
                int nomor = 1;
                foreach (var kotak in kotaks)
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
        public void PilihGambar(object sender, EventArgs e)
        {
            OpenFileDialog pilihDialog = new OpenFileDialog();
            DialogResult hasil = pilihDialog.ShowDialog();
            if (hasil != DialogResult.OK)
                return;
            _image = new Bitmap(pilihDialog.FileName);
            pictureBox1.Image = _image;
        }
        public void PilihGambarPGM(object sender, EventArgs e)
        {
            OpenFileDialog pilihDialog = new OpenFileDialog();
            DialogResult hasil = pilihDialog.ShowDialog();
            if (hasil != DialogResult.OK)
                return; PortableGrayMap gambar = new PortableGrayMap(pilihDialog.FileName);
            _image  = gambar.MakeBitmap(gambar,1);
            pictureBox1.Image = _image;
            System.Diagnostics.Debug.WriteLine("Lebar: "+_image.Width.ToString()+"Pixel\nTinggi: "+_image.Height.ToString()+"Pixel");
        }
        public void CropImage(object sender, EventArgs e)
        {
            _min = Convert.ToInt32(MinBox.Text);
            max = Convert.ToInt32(MaxBox.Text);
            MessageBox.Show("Selesai");
            var Croper = new ImageCrop(_image, _min, max, true);
            var hasilCrop = Croper.GetImages();
            foreach (var cropImage in hasilCrop)
            {
                tampilHasilCrop form = new tampilHasilCrop(cropImage);
                form.Show();
            }
            System.Diagnostics.Debug.Write("\n__________________________________________________________\n");
        }

        private void PilihGambarPPM(object sender, EventArgs e)
        {
            var pilihDialog = new OpenFileDialog();
            var hasil = pilihDialog.ShowDialog();
            if (hasil != DialogResult.OK)
                return;
            _image = PPMReader.ReadBitmapFromPPM(pilihDialog.FileName);
            pictureBox1.Image = _image;
            System.Diagnostics.Debug.WriteLine("Lebar: " + _image.Width.ToString() + "Pixel\nTinggi: " + _image.Height.ToString() + "Pixel");
        }

        private void PilihFolderDuplikat(object sender, EventArgs e)
        {
            var selekpeth = PilihFolderHapus();
            if (selekpeth == "" || selekpeth == null)
                return;
            var allSourceFiles = Directory.GetFiles(selekpeth);
            for (var i=0; i<allSourceFiles.Length-1; i++)
            {
                if (allSourceFiles[i].EndsWith("2.gif"))
                {
                    if (Path.GetFileName(allSourceFiles[i - 1]).StartsWith(Path.GetFileName(allSourceFiles[i]).Substring(0, 20)))
                    {
                        var satu = new deleteGambar(allSourceFiles[i - 1], allSourceFiles[i]);
                        satu.ShowDialog();
                        listhapus.Add(satu.SelectedToDelete);
                    }
                }
            }
            foreach (var berkas in listhapus)
            {
                richTextBox1.AppendText(berkas+"\n");
            }
        }
        private string PilihFolderHapus()
        {
            var folderdialog = new FolderBrowserDialog();
            folderdialog.ShowDialog();
            return folderdialog.SelectedPath;
        }

        private void HapusFile(object sender, EventArgs e)
        {
            var hapus = richTextBox2.Text.Split('\n');
            foreach (var berkas in hapus)
            {
                System.Diagnostics.Debug.WriteLine(berkas);
                File.Delete(berkas);
            }
        }

        private void ApplyHisteq(object sender, EventArgs e)
        {
            var gambar = new Image<Gray, byte>(_image);
            gambar._EqualizeHist();
            _image = gambar.ToBitmap();
            pictureBox1.Image = gambar.ToBitmap();
        }

        private void GaborFilter(object sender, EventArgs e)
        {
            GaborKernel krnl, krnle2;
            GaborFilter filter;
            krnl = new GaborKernel(0, 4);
            krnle2 = new GaborKernel(4, 2);
            filter = new GaborFilter();
            var gaborGambar = new Image<Gray, float>(_image);
            var tr = filter.Convolution(gaborGambar, krnl, GABOR_TYPE.GABOR_MAG);
            var tr2 = filter.Convolution(gaborGambar, krnle2, GABOR_TYPE.GABOR_MAG);
            var ini = new tampilHasilCrop(tr.ToBitmap());
            var itu = new tampilHasilCrop(tr2.ToBitmap());
            ini.Show();
            itu.Show();
        }

        private void SaveImage(object sender, EventArgs e)
        {
            var sd = new SaveFileDialog();
            sd.ShowDialog();
            var path = sd.FileName;
            pictureBox1.Image.Save(path+".gif");
        }

        private void DRLDPClick(object sender, EventArgs e)
        {
            DRLocalDirectionalPattern ldp = new DRLocalDirectionalPattern(_image);
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
            for (var i = 0; i < lebar; i++)
            {
                for (var j = 0; j < tinggi; j++)
                {
                    hasil.SetPixel(i, j, Color.FromArgb(ldp.LdpResult[i, j], ldp.LdpResult[i, j], ldp.LdpResult[i, j]));
                }
            }
            pictureBox1.Image = hasil;
            System.Diagnostics.Debug.Write(tinggi.ToString());
        }

        private void Rename_Click(object sender, EventArgs e)
        {
            var fd = new FolderBrowserDialog();
            fd.ShowDialog();
            var df = new FolderBrowserDialog();
            df.ShowDialog();
            var nama = Directory.GetFiles(fd.SelectedPath);
            foreach (string namaFile in nama)
            {
                var namas = Path.GetFileName(namaFile).Split(' ', '.');
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
            var bdf = new FolderBrowserDialog();
            bdf.ShowDialog();
            bdf.ShowDialog();
            var allSourceFiles = Directory.GetFiles(bdf.SelectedPath, "*.pgm", SearchOption.AllDirectories);
            foreach (var pathfile in allSourceFiles)
            {
                var pgmImage = new PortableGrayMap(pathfile);
                var bitmapImage = pgmImage.MakeBitmap(pgmImage, 1);
                bitmapImage.Save(bdf.SelectedPath + "/" + Path.GetFileNameWithoutExtension(pathfile) + ".gif");
                System.Diagnostics.Debug.WriteLine("berhasil " + bdf.SelectedPath + Path.GetFileNameWithoutExtension(pathfile) + ".gif");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var kirs = new KirschEdgeDetection(_image);
            kirs.GetEdge(int.Parse( maskIndex.Text));
            var iniEdge = new Bitmap(kirs.FinalMatrix.GetLength(0), kirs.FinalMatrix.GetLength(1));
            for (var i = 0; i < kirs.FinalMatrix.GetLength(0); i++)
            {
                for (var j = kirs.FinalMatrix.GetLength(1) - 1; j >= 0; j--)
                {
                    iniEdge.SetPixel(i, j, Color.FromArgb(kirs.FinalMatrix[i, j], kirs.FinalMatrix[i, j], kirs.FinalMatrix[i, j]));
                }
            }
            pictureBox2.Image = iniEdge;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var ldp = "";
            var sementara = new Bitmap(pictureBox1.Image);
            for (var i = 0; i < pictureBox1.Image.Height; i++)
            {
                for (int j = pictureBox1.Image.Width - 1; j >= 0; j--)
                {
                    ldp += sementara.GetPixel(i, j).G+" ";
                }
                ldp += "\n";
            }
            var ldped = new Hasil_klasifikasi(ldp)
            {
                Text = "ldp"
            };

            var raw_data = "";
            var sementara2 = new Bitmap(pictureBox2.Image);
            for (var i = 0; i < sementara2.Height; i++)
            {
                for (var j = 0; j < sementara2.Width; j++)
                {
                    raw_data += sementara2.GetPixel(i, j).G + " ";
                }
                raw_data += "\n";
            }
            var raw = new Hasil_klasifikasi(raw_data)
            {
                Text = "drldp"
            };

            raw.Show();
            ldped.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var input = new Bitmap(5, 5);
            var data = richTextBox1.Text;
            var parsed_data = data.Split('\n', ' ');
            var x = 0;
            for(int i=0; i<5; i++)
            {
                for(int j=0; j<5; j++)
                {
                    input.SetPixel(i, j, Color.FromArgb(int.Parse(parsed_data[x]), int.Parse(parsed_data[x]), int.Parse(parsed_data[x])));
                    x++;
                }
            }
            var ini = new DRLocalDirectionalPattern(input);
            ini.GetDRLDPMatrix();
            var hasil = "";
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    hasil += ini.LdpResult[i, j]+" ";
                }
                hasil += '\n';
            }
            richTextBox2.Text = hasil;
        }
    }
}
