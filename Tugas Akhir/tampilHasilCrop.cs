using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tugas_Akhir
{
    public partial class tampilHasilCrop : Form
    {
        Bitmap gambar;
        public tampilHasilCrop(Bitmap gambar)
        {
            this.gambar = gambar;
            InitializeComponent();
            initGambar();
        }
        private void initGambar()
        {
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.Image = gambar;
            System.Diagnostics.Debug.WriteLine("masuk ini");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            DialogResult result = sd.ShowDialog();
            if (result == DialogResult.OK)
            {
                gambar.Save(sd.FileName+".gif");
                this.Close();
            }
        }
    }
}
