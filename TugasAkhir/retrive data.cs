using Accord.Statistics.Analysis;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
namespace Tugas_Akhir
{
    public partial class RetrieveDataForm : Form
    {
        private DRLDPDataModel[] _trainData;
        private DRLDPDataModel[] _testData;
        private PrincipalComponentAnalysis _PCA;

        public RetrieveDataForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ConnectionString = ConfigurationManager.AppSettings["DataBaseConnectionString"];
            var MyConnect = new MySqlConnection(ConnectionString);
            using (var MyCommand = new MySqlCommand(textBox1.Text, MyConnect))
            {
                var MyAdapter = new MySqlDataAdapter(MyCommand);
                var DTArray = new DataTable();
                MyConnect.Open();
                MyAdapter.Fill(DTArray);
                dataGridView1.DataSource = DTArray;
                MyConnect.Close();
            }
        }

        private void SizeForEachDatasetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "select dataset, dimension, size from data_feature group by dataset, size";
        }

        private void LearnPca(object sender, EventArgs e)
        {
            var dataLearning = new double[_trainData.Length][];
            for(var i=0; i<_trainData.Length; i++)
            {
                dataLearning[i] = GetHistogram(_trainData[i].data.Split(' '));
            }
            _PCA = new PrincipalComponentAnalysis(PrincipalComponentMethod.Center);
            _PCA.Learn(dataLearning);
            MessageBox.Show("learning finish");
        }
        public double[] GetHistogram(string[] feature)
        {
            var histogram = new double[256];
            for(var i=0; i<256; i++)
            {
                histogram[i] = 0;
            }
            for(var i=0; i<feature.GetLength(0)-1; i++)
            {
                histogram[int.Parse(feature[i])]++;
            }
            return histogram;
        }

        private void LearnSelectedDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _trainData = new DRLDPDataModel[dataGridView1.Rows.Count-1];
            System.Diagnostics.Debug.WriteLine(dataGridView1.Rows.Count);
            for (var i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                _trainData[i] = new DRLDPDataModel
                {
                    data = dataGridView1["data", i].Value.ToString(),
                    fileName = dataGridView1["fileName", i].Value.ToString(),
                    dimension = int.Parse(dataGridView1["dimension", i].Value.ToString()),
                    label = dataGridView1["label", i].Value.ToString(),
                    dataset = dataGridView1["dataset", i].Value.ToString(),
                    size = dataGridView1["size", i].Value.ToString()
                };
            }
            MessageBox.Show("finish");
        }

        private void SerializeToTestingObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _testData = new DRLDPDataModel[dataGridView1.RowCount];
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                _testData[i] = new DRLDPDataModel
                {
                    data = dataGridView1["data", i].Value.ToString(),
                    fileName = dataGridView1["fileName", i].Value.ToString(),
                    dimension = int.Parse(dataGridView1["dimension", i].Value.ToString()),
                    label = dataGridView1["label", i].Value.ToString(),
                    dataset = dataGridView1["dataset", i].Value.ToString(),
                    size = dataGridView1["size", i].Value.ToString()
                };
            }
            MessageBox.Show("finish");
        }
    }
}
