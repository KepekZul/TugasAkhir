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
    public partial class daftarData : Form
    {
        public string[] datas;
        public daftarData()
        {
            InitializeComponent();
        }

        private void daftarData_Load(object sender, EventArgs e)
        {
            foreach(string text in datas)
            {
                this.richTextBox1.AppendText(text+"\n");
            }
        }
    }
}
