using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Tugas_Akhir
{
    public partial class deleteGambar : Form
    {
        private string _firstImagePath;
        private string _secondImagePath;

        public string SelectedToDelete;
        public string Answer = "";
        public deleteGambar(string path1, string path2)
        {
            InitializeComponent();
            _firstImagePath = path1;
            _secondImagePath = path2;
            pictureBox1.Image = new Bitmap(_firstImagePath);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            label1.Text = Path.GetFileName(_firstImagePath);
            pictureBox2.Image = new Bitmap(_secondImagePath);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            label2.Text = Path.GetFileName(_secondImagePath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedToDelete = _firstImagePath;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectedToDelete = _secondImagePath;
            Close();
        }
    }
}
