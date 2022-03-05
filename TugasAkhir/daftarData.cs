using System;
using System.Windows.Forms;

namespace Tugas_Akhir
{
    public partial class ListOfData : Form
    {
        public string[] datas;
        public ListOfData()
        {
            InitializeComponent();
        }

        private void daftarData_Load(object sender, EventArgs e)
        {
            foreach(string text in datas)
            {
                richTextBox1.AppendText(text+"\n");
            }
        }
    }
}
