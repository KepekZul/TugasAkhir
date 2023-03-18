using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Accord.Statistics.Analysis;
using System.Configuration;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Tugas_Akhir
{
    public partial class retrive_data : Form
    {
        DRLDPDataModel[] trainData;
        DRLDPDataModel[] testData;
        PrincipalComponentAnalysis PCA;
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

        private void sizeForEachDatasetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "select dataset, dimension, size from data_feature group by dataset, size";
        }

        private void learnPca(object sender, EventArgs e)
        {
            double[][] dataLearning = new double[trainData.Length][];
            for(int i=0; i<trainData.Length; i++)
            {
                dataLearning[i] = getHistogram(trainData[i].data.Split(' '));
            }
            PCA = new PrincipalComponentAnalysis(PrincipalComponentMethod.Center);
            PCA.Learn(dataLearning);
            MessageBox.Show("learning finish");
        }
        public double[] getHistogram(string[] feature)
        {
            double[] histogram = new double[256];
            for(int i=0; i<256; i++)
            {
                histogram[i] = 0;
            }
            for(int i=0; i<feature.GetLength(0)-1; i++)
            {
                histogram[int.Parse(feature[i])]++;
            }
            return histogram;
        }

        private void learnSelectedDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trainData = new DRLDPDataModel[dataGridView1.Rows.Count-1];
            System.Diagnostics.Debug.WriteLine(dataGridView1.Rows.Count);
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                trainData[i] = new DRLDPDataModel
                {
                    data = dataGridView1["data", i].Value.ToString(),
                    FileName = dataGridView1["fileName", i].Value.ToString(),
                    Dimension = int.Parse(dataGridView1["dimension", i].Value.ToString()),
                    Label = dataGridView1["label", i].Value.ToString(),
                    Dataset = dataGridView1["dataset", i].Value.ToString(),
                    Size = dataGridView1["size", i].Value.ToString()
                };
            }
            MessageBox.Show("finish");
        }

        private void serializeToTestingObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            testData = new DRLDPDataModel[dataGridView1.RowCount];
            for (int i = 0; i < dataGridView1.RowCount-1; i++)
            {
                testData[i] = new DRLDPDataModel
                {
                    data = dataGridView1["data", i].Value.ToString(),
                    FileName = dataGridView1["fileName", i].Value.ToString(),
                    Dimension = int.Parse(dataGridView1["dimension", i].Value.ToString()),
                    Label = dataGridView1["label", i].Value.ToString(),
                    Dataset = dataGridView1["dataset", i].Value.ToString(),
                    Size = dataGridView1["size", i].Value.ToString()
                };
            }
            MessageBox.Show("finish");
        }
    }
}
