using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Tugas_Akhir
{
    public partial class retrive_data : Form
    {
        public retrive_data()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.AppSettings["DataBaseConnectionString"];
            MySqlConnection MyConnect = new MySqlConnection(ConnectionString);
            using(MySqlCommand MyCommand = new MySqlCommand(this.textBox1.Text, MyConnect))
            {
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter(MyCommand);
                DataTable DTArray = new DataTable();
                MyConnect.Open();
                MyAdapter.Fill(DTArray);
                dataGridView1.DataSource = DTArray;
                MyConnect.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DRLDPDataModel SelectedData = new DRLDPDataModel { fileName = dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), dimension = Int32.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString()), data = dataGridView1.SelectedRows[0].Cells[3].Value.ToString() };
                SelectedData.parseStringToMat(true);
                Bitmap DataImage = new Bitmap(SelectedData.dimension, SelectedData.dimension);
                for (int i = 0; i < SelectedData.dimension; i++)
                {
                    for (int j = 0; j < SelectedData.dimension; j++)
                    {
                        DataImage.SetPixel(i, j, Color.FromArgb(SelectedData.matrix[i, j], SelectedData.matrix[i, j], SelectedData.matrix[i, j]));
                    }
                }
                pictureBox1.Image = DataImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine(dataGridView1.SelectedRows[0].Cells[0].Value);
            }
        }

        private void sizeForEachDatasetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "select dataset, dimension, size from data_feature group by dataset, size";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DRLDPDataModel SelectedData = new DRLDPDataModel { fileName = dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), dimension = Int32.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString()), data = dataGridView1.SelectedRows[0].Cells[3].Value.ToString() };
            SelectedData.parseStringToMat(true);
            int[] histogram = new int[256];
            for(int i=0; i< 256; i++)
            {
                histogram[i] = 0;
            }
            for(int i=0; i<SelectedData.matrix.GetLength(0); i++)
            {
                for(int j=0; j<SelectedData.matrix.GetLength(1); j++)
                {
                    histogram[SelectedData.matrix[i, j]]++;
                }
            }
            string histo = "";
            for(int i=0; i<256; i++)
            {
                histo +="color "+ i.ToString() + " " + histogram[i].ToString()+"\n";
            }
            Hasil_klasifikasi ini = new Hasil_klasifikasi(histo);
            ini.Text = "Histogram";
            ini.Show();
        }
    }
}
