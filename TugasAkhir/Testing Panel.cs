using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Linq;

namespace Tugas_Akhir
{
    public partial class Testing_Panel : Form
    {
        private Stopwatch _runTime;
        private int _initial;
        private int _total;
        private DataTable _DTArrayTrain;
        private DataTable _DTArrayTest;
        private List<DRLDPDataModel> _TestModel;
        private List<byte[,]> _TrainModelData;
        private List<string> _TrainModelLabel;
        private List<string> _ClassificationResult;

        public Testing_Panel()
        {
            InitializeComponent();
        }
        public void DataUjiClick(object sender, EventArgs e)
        {
            button1.Text = "Loading";
            try
            {
                var ConnectionString = ConfigurationManager.AppSettings["DataBaseConnectionString"];
                var MyConnect = new MySqlConnection(ConnectionString);
                using (var MyCommand = new MySqlCommand(textBox1.Text, MyConnect))
                {
                    var MyAdapter = new MySqlDataAdapter(MyCommand);
                    _DTArrayTest = new DataTable();
                    MyConnect.Open();
                    MyAdapter.Fill(_DTArrayTest);
                    dataGridView1.DataSource = _DTArrayTest;
                    MyConnect.Close();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            button1.Text = "Select";
        }
        public void DataTrain(object sender, EventArgs e)
        {
            button2.Text = "Loading";
            try
            {
                var ConnectionString = ConfigurationManager.AppSettings["DataBaseConnectionString"];
                var MyConnect = new MySqlConnection(ConnectionString);
                using (var MyCommand = new MySqlCommand(textBox2.Text, MyConnect))
                {
                    var MyAdapter = new MySqlDataAdapter(MyCommand);
                    _DTArrayTrain = new DataTable();
                    MyConnect.Open();
                    MyAdapter.Fill(_DTArrayTrain);
                    dataGridView2.DataSource = _DTArrayTrain;
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
            _TrainModelData = new List<byte[,]>();
            _TrainModelLabel = new List<string>();
            foreach (var dataModel in from DataRow row in _DTArrayTrain.Rows
                                      let dataModel = new DRLDPDataModel
                                      {
                                          data = row["data"].ToString(),
                                          dimension = Int32.Parse(row["dimension"].ToString()),
                                          label = row["label"].ToString()
                                      }
                                      select dataModel)
            {
                dataModel.parseStringToMat(true);
                _TrainModelData.Add(dataModel.matrix);
                _TrainModelLabel.Add(dataModel.label);
            }

            _TestModel = new List<DRLDPDataModel>();//serialize data to object
            foreach (var dataModel in from DataRow row in _DTArrayTest.Rows
                                      let dataModel = new DRLDPDataModel
                                      {
                                          data = row["data"].ToString(),
                                          dataset = row["dataset"].ToString(),
                                          dimension = Int32.Parse(row["dimension"].ToString()),
                                          size = row["size"].ToString(),
                                          fileName = row["fileName"].ToString(),
                                          label = row["label"].ToString()
                                      }
                                      select dataModel)
            {
                dataModel.parseStringToMat(true);
                _TestModel.Add(dataModel);
            }

            _ClassificationResult = new List<string>();
            _runTime = new Stopwatch();
            _runTime.Start();
            _initial = 0;
            _total = _TestModel.Count;
            backgroundWorker1.RunWorkerAsync();
            new Thread(() =>
            {
                foreach (DRLDPDataModel subject in _TestModel)
                {
                    KNearest knnObj = new KNearest(subject.matrix, this._TrainModelData, this._TrainModelLabel, Int32.Parse(this.KConstantaBox.Text), Int32.Parse(this.numberFragment.Text));
                    _ClassificationResult.Add(knnObj.GetClass());
                    _initial++;
                }
                _runTime.Stop();
                string Result = "";
                for (int i=0; i< _TestModel.Count; i++)
                {
                    Result += _TestModel[i].fileName + "\t" + _TestModel[i].label + "\t terklasifikasi sebagai:\t" + _ClassificationResult[i];
                    if (_TestModel[i].label == _ClassificationResult[i])
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
        private void ProgressBarWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                int progress =(int)((double) this._initial/ (double )this._total*100);
                backgroundWorker1.ReportProgress(progress);
                Thread.Sleep(1000);
                if (progress == 100)
                    break;
            }
        }
        private void UpdateBarWork(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        private void FinishBarWork(object sender, RunWorkerCompletedEventArgs e)
        {
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            _runTime.Elapsed.Hours, _runTime.Elapsed.Minutes, _runTime.Elapsed.Seconds,
            _runTime.Elapsed.Milliseconds / 10);
            button3.Enabled = true;
            KConstantaBox.Enabled = true;
            numberFragment.Enabled = true;
            MessageBox.Show("klasifikasi Selesai\n" + elapsedTime);
        }
    }
}
