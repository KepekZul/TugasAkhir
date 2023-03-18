using System;
using System.Windows.Forms;

namespace Tugas_Akhir
{
    public partial class Hasil_klasifikasi : Form
    {
        string content;
        public Hasil_klasifikasi(string result)
        {
            InitializeComponent();
            this.content = result;
        }

        private void Hasil_klasifikasi_Load(object sender, EventArgs e)
        {
            this.richTextBox1.Text = this.content;
        }
    }
}
