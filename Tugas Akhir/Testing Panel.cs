using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Tugas_Akhir
{
    public partial class Testing_Panel : Form
    {
        Stopwatch RunTime;
        int initial;
        int total;

        DataTable DTArrayTrain;
        DataTable DTArrayTest;
        List<DRLDPDataModel> TestModel;
        List<byte[,]> TrainModelData;
        List<string> TrainModelLabel;
        List<string> ClassificationResult;
        public Testing_Panel()
        {
            InitializeComponent();
        }
        public void dataUjiClick(object sender, EventArgs e)
        {
            button1.Text = "Loading";
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["DataBaseConnectionString"];
                MySqlConnection MyConnect = new MySqlConnection(ConnectionString);
                using (MySqlCommand MyCommand = new MySqlCommand(this.textBox1.Text, MyConnect))
                {
                    MySqlDataAdapter MyAdapter = new MySqlDataAdapter(MyCommand);
                    DTArrayTest = new DataTable();
                    MyConnect.Open();
                    MyAdapter.Fill(DTArrayTest);
                    dataGridView1.DataSource = DTArrayTest;
                    MyConnect.Close();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            button1.Text = "Select";
        }
        public void dataTrain(object sender, EventArgs e)
        {
            button2.Text = "Loading";
            try
            {
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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            button2.Text = "Select";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            KConstantaBox.Enabled = false;
            numberFragment.Enabled = false;
            TrainModelData = new List<byte[,]>();
            TrainModelLabel = new List<string>();
            foreach(DataRow row in DTArrayTrain.Rows)
            {
                DRLDPDataModel dataModel = new DRLDPDataModel
                {
                    data = row["data"].ToString(),
                    Dimension = Int32.Parse(row["dimension"].ToString()),
                    Label = row["label"].ToString()
                };
                dataModel.parseStringToMat(true);
                TrainModelData.Add(dataModel.Matrix);
                TrainModelLabel.Add(dataModel.Label);
            }

            TestModel = new List<DRLDPDataModel>();//serialize data to object
            foreach (DataRow row in DTArrayTest.Rows)
            {
                DRLDPDataModel dataModel = new DRLDPDataModel
                {
                    data = row["data"].ToString(),
                    Dataset = row["dataset"].ToString(),
                    Dimension = Int32.Parse(row["dimension"].ToString()),
                    Size = row["size"].ToString(),
                    FileName = row["fileName"].ToString(),
                    Label = row["label"].ToString()
                };
                dataModel.parseStringToMat(true);
                TestModel.Add(dataModel);
            }

            ClassificationResult = new List<string>();
            RunTime = new Stopwatch();
            RunTime.Start();
            initial = 0;
            total = TestModel.Count;
            backgroundWorker1.RunWorkerAsync();
            string Result;
            new Thread(() =>
            {
                foreach (DRLDPDataModel subject in TestModel)
                {
                    KNearest knnObj = new KNearest(subject.Matrix, this.TrainModelData, this.TrainModelLabel, Int32.Parse(this.KConstantaBox.Text), Int32.Parse(this.numberFragment.Text));
                    ClassificationResult.Add(knnObj.getClass());
                    initial++;
                }
                RunTime.Stop();
                Result="";
                for(int i=0; i< TestModel.Count; i++)
                {
                    Result += TestModel[i].FileName + "\t" + TestModel[i].Label + "\t terklasifikasi sebagai:\t" + ClassificationResult[i];
                    if (TestModel[i].Label == ClassificationResult[i])
                    {
                        Result += "\t 1 \n";
                    }
                    else
                    {
                        Result += "\t 0 \n";
                    }
                }
                Hasil_klasifikasi formHasil = new Hasil_klasifikasi(Result);
                formHasil.ShowDialog();
            }
            ).Start();
        }
        private void progressBarWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                int progress =(int)((double) this.initial/ (double )this.total*100);
                backgroundWorker1.ReportProgress(progress);
                Thread.Sleep(1000);
                if (progress == 100)
                    break;
            }
        }
        private void updateBarWork(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        private void finishBarWork(object sender, RunWorkerCompletedEventArgs e)
        {
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            RunTime.Elapsed.Hours, RunTime.Elapsed.Minutes, RunTime.Elapsed.Seconds,
            RunTime.Elapsed.Milliseconds / 10);
            button3.Enabled = true;
            KConstantaBox.Enabled = true;
            numberFragment.Enabled = true;
            MessageBox.Show("klasifikasi Selesai\n" + elapsedTime);
        }
    }
}
