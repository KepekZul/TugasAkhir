using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace Tugas_Akhir
{
    public partial class deleteGambar : Form
    {
        string pathGambar1;
        string pathGambar2;
        public string yangDihapus;
        public string answer = "";
        public deleteGambar(string path1, string path2)
        {
            InitializeComponent();
            this.pathGambar1 = path1;
            this.pathGambar2 = path2;
            this.pictureBox1.Image = new Bitmap(this.pathGambar1);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.label1.Text = Path.GetFileName(this.pathGambar1);
            this.pictureBox2.Image = new Bitmap(this.pathGambar2);
            this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            this.label2.Text = Path.GetFileName(this.pathGambar2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.yangDihapus = pathGambar1;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.yangDihapus = pathGambar2;
            this.Close();
        }
    }
}
