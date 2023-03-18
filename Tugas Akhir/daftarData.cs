using System;
using System.Windows.Forms;

namespace Tugas_Akhir
{
    public partial class FileList : Form
    {
        public string[] datas;
        public FileList()
        {
            InitializeComponent();
        }

        private void loadList(object sender, EventArgs e)
        {
            foreach(string text in datas)
            {
                this.richTextBox1.AppendText(text+"\n");
            }
        }
    }
}
