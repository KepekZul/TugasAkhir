using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tugas_Akhir
{
    public partial class Pengujian : Form
    {
        DataTable DTArrayTrain;
        DataTable DTArrayTest;
        List<DRLDPDataModel> TestModel;
        List<byte[,]> TrainModelData;
        List<string> TrainModelLabel;
        List<string> ClassificationResult;
        public Pengujian()
        {
            InitializeComponent();
        }
        public void dataUjiClick(object sender, EventArgs e)
        {
            button1.Text = "Loading";
            string ConnectionString = ConfigurationManager.AppSettings["DataBaseConnectionString"];
            MySqlConnection MyConnect = new MySqlConnection(ConnectionString);
            using(MySqlCommand MyCommand = new MySqlCommand(this.textBox1.Text, MyConnect))
            {
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter(MyCommand);
                DTArrayTest = new DataTable();
                MyConnect.Open();
                MyAdapter.Fill(DTArrayTest);
                dataGridView1.DataSource = DTArrayTest;
                MyConnect.Close();
            }
            button1.Text = "Select";
        }
        public void dataTrain(object sender, EventArgs e)
        {
            button2.Text = "Loading";
            string ConnectionString = ConfigurationManager.AppSettings["DataBaseConnectionString"];
            MySqlConnection MyConnect = new MySqlConnection(ConnectionString);
            using (MySqlCommand MyCommand = new MySqlCommand(this.textBox2.Text, MyConnect))
            {
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter(MyCommand);
                DTArrayTrain = new DataTable();
                MyConnect.Open();
                MyAdapter.Fill(DTArrayTrain);
                dataGridView2.DataSource = DTArrayTrain;
                MyConnect.Close();
            }
            button2.Text = "Select";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TrainModelData = new List<byte[,]>();
            TrainModelLabel = new List<string>();
            foreach(DataRow row in DTArrayTrain.Rows)
            {
                DRLDPDataModel dataModel = new DRLDPDataModel
                {
                    data = row["data"].ToString(),
                    dimension = Int32.Parse(row["dimension"].ToString()),
                    label = row["label"].ToString()
                };
                dataModel.parseStringToMat(true);
                TrainModelData.Add(dataModel.matrix);
                TrainModelLabel.Add(dataModel.label);
            }

            TestModel = new List<DRLDPDataModel>();//serialize data to object
            foreach (DataRow row in DTArrayTest.Rows)
            {
                DRLDPDataModel dataModel = new DRLDPDataModel
                {
                    data = row["data"].ToString(),
                    dataset = row["dataset"].ToString(),
                    dimension = Int32.Parse(row["dimension"].ToString()),
                    size = row["size"].ToString(),
                    fileName = row["fileName"].ToString(),
                    label = row["label"].ToString()
                };
                dataModel.parseStringToMat(true);
                TestModel.Add(dataModel);
            }

            ClassificationResult = new List<string>();
            foreach(DRLDPDataModel subject in TestModel)
            {
                KNearest knnObj = new KNearest(subject.matrix, this.TrainModelData, this.TrainModelLabel, Int32.Parse(this.KConstantaBox.Text));
                ClassificationResult.Add(knnObj.getClass());
            }
            string Result="";
            for(int i=0; i< TestModel.Count; i++)
            {
                Result += TestModel[i].fileName + "\t" + TestModel[i].label + "\t terklasifikasi sebagai:\t" + ClassificationResult[i];
                if (TestModel[i].label == ClassificationResult[i])
                {
                    Result += "\t 1 \n";
                }
                else
                {
                    Result += "\t 0 \n";
                }
            }
            Hasil_klasifikasi formHasil = new Hasil_klasifikasi(Result);
            formHasil.Show();
        }
    }
}
