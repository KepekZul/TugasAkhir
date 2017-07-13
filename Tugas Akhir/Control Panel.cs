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
    public partial class Control_Panel : Form
    {
        public Control_Panel()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void extract_form_Click(object sender, EventArgs e)
        {
            Extraction_Panel ExP = new Extraction_Panel();
            ExP.Show();
        }

        private void testing_form_Click(object sender, EventArgs e)
        {
            ujiCobaGambar UjG = new ujiCobaGambar();
            UjG.Show();
        }

        private void get_data_Click(object sender, EventArgs e)
        {
            retrive_data Rd = new retrive_data();
            Rd.Show();
        }

        private void testing_panel_Click(object sender, EventArgs e)
        {
            Testing_Panel tF = new Testing_Panel();
            tF.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Panel_Demo dp = new Panel_Demo();
            dp.Show();
        }
    }
}
