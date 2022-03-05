using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tugas_Akhir
{
    public partial class tampilHasilCrop : Form
    {
        private Bitmap _image;

        public tampilHasilCrop(Bitmap gambar)
        {
            _image = gambar;
            InitializeComponent();
            InitGambar();
        }
        private void InitGambar()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = _image;
            System.Diagnostics.Debug.WriteLine("masuk ini");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sd = new SaveFileDialog();
            var result = sd.ShowDialog();
            if (result == DialogResult.OK)
            {
                _image.Save(sd.FileName+".gif");
                Close();
            }
        }
    }
}
