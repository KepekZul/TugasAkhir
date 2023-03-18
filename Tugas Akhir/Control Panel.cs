using System;
using System.Windows.Forms;

namespace Tugas_Akhir
{
    public partial class Control_Panel : Form
    {
        public Control_Panel()
        {
            InitializeComponent();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Extract_form_Click(object sender, EventArgs e)
        {
            Extraction_Panel ExP = new Extraction_Panel();
            ExP.Show();
        }

        private void Testing_form_Click(object sender, EventArgs e)
        {
            ImageTesting UjG = new ImageTesting();
            UjG.Show();
        }

        private void Get_data_Click(object sender, EventArgs e)
        {
            retrive_data Rd = new retrive_data();
            Rd.Show();
        }

        private void Testing_panel_Click(object sender, EventArgs e)
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
