using MySql.Data.MySqlClient;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tugas_Akhir
{
    public partial class Extraction_Panel : Form
    {
        private ConcurrentQueue<DRLDPDataModel> _fileList;
        private System.Diagnostics.Stopwatch _runTime;
        private string _sourcePath;
        private string _targetPath;
        private string[] _allSourceFiles;
        private int _minSize;
        private int _maxSize;

        public Extraction_Panel()
        {
            InitializeComponent();
            ComboboxItem ppm = new ComboboxItem
            {
                Text = "ppm file",
                Value = "*ppm"
            }, pgm = new ComboboxItem
            {
                Text = "pgm file",
                Value = "*.pgm"
            }, all = new ComboboxItem
            {
                Text = "bitmap",
                Value = "*.*"
            };
            comboBox1.Items.Add(ppm);
            comboBox1.Items.Add(pgm);
            comboBox1.Items.Add(all);
            comboBox1.SelectedIndex = 2;
        }
        public void SourceFolder(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();
            var result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _sourcePath = folderDialog.SelectedPath;
                textBox1.Text = _sourcePath;
                _allSourceFiles = Directory.
                    GetFiles(_sourcePath, (comboBox1.SelectedItem as ComboboxItem).Value.ToString(), SearchOption.AllDirectories)
                    .Where(s => !s.EndsWith(".info") || !s.EndsWith(".txt")|| !s.EndsWith("*.ini")).ToArray();
                if (FilenameFilterBox.Text != "")
                {
                    var keepFile = FilenameFilterBox.Text.Split(' ');
                    for (var i = 0; i < _allSourceFiles.Length; i++)
                    {
                        for (var j = 0; j < keepFile.Length; j++)
                        {
                            if (Path.GetFileName(_allSourceFiles[i]).Contains(keepFile[j]))
                            {
                                break;
                            }
                            if (j == keepFile.Length - 1)
                            {
                                _allSourceFiles[i] = "";
                            }
                        }
                    }
                }
                //keperluan debugging daftar file yang terambil
                var dd = new ListOfData
                {
                    datas = _allSourceFiles
                };
                dd.Show();
            }
        }

        public void DestinationFolder(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();
            folderDialog.ShowDialog();
            _targetPath = folderDialog.SelectedPath;
            textBox2.Text = _targetPath;
        }

        private void CropSelectedFiles(object sender, EventArgs e)
        {
            _minSize = Convert.ToInt32(MinSizeBox.Text);
            _maxSize = Convert.ToInt32(MaxSizeBox.Text);
            System.Diagnostics.Debug.WriteLine(checkBox1.Checked.ToString());//debugging
            var partisiAwal = new string[_allSourceFiles.Length / 2];
            for (var i=0; i<_allSourceFiles.Length/2; i++)
            {
                partisiAwal[i] = _allSourceFiles[i];
            }
            var partisiAkhir = new string[(_allSourceFiles.Length / 2) + ((_allSourceFiles.Length % 2 == 1) ? 1 : 0)];
            for(var i =0; i< _allSourceFiles.Length / 2 + ((_allSourceFiles.Length % 2 == 1) ? 1 : 0); i++)
            {
                partisiAkhir[i] = _allSourceFiles[i+ _allSourceFiles.Length / 2];
            }
            var cropThread = new CropperThreadWorker[2];
            cropThread[0] = new CropperThreadWorker(partisiAwal, _minSize, _maxSize, checkBox1.Checked, _targetPath);
            cropThread[1] = new CropperThreadWorker(partisiAkhir, _minSize, _maxSize, checkBox1.Checked, _targetPath);
            var cropingThread = new Thread[2];
            cropingThread[0] = new Thread(new ThreadStart(cropThread[0].CropStart));
            cropingThread[1] = new Thread(new ThreadStart(cropThread[1].CropStart));
            cropingThread[0].Start();
            cropingThread[1].Start();
        }
        private new void Resize(object sender, EventArgs e)
        {
            _minSize = Convert.ToInt32(MinSizeBox.Text);
            _maxSize = Convert.ToInt32(MaxSizeBox.Text);
            var partisiAwal = new string[_allSourceFiles.Length / 2];
            var partisiAkhir = new string[_allSourceFiles.Length/2 + ((_allSourceFiles.Length%2==1)?1:0)];
            for(var i=0; i<_allSourceFiles.Length/2; i++)
            {
                partisiAwal[i] = _allSourceFiles[i];
            }
            for (var i = 0; i < _allSourceFiles.Length / 2 + ((_allSourceFiles.Length % 2 == 1) ? 1 : 0); i++)
            {
                partisiAkhir[i] = _allSourceFiles[i + _allSourceFiles.Length / 2];
            }
            var resizeThread = new ResizerThreadWorker[2];
            resizeThread[0] = new ResizerThreadWorker(partisiAwal, _minSize, _maxSize, _targetPath);
            resizeThread[1] = new ResizerThreadWorker(partisiAkhir, _minSize, _maxSize, _targetPath);
            var runningThread = new Thread[2];
            runningThread[0] = new Thread(new ThreadStart(resizeThread[0].Resize));
            runningThread[1] = new Thread(new ThreadStart(resizeThread[1].Resize));
            runningThread[0].Start();
            runningThread[1].Start();
        }

        private void ExtractImage_Click(object sender, EventArgs e)
        {
            reduceDimension.Enabled = false;
            var jumlah = 24;
            var filePipe = new ConcurrentQueue<string>();
            var fileResult = new ConcurrentQueue<DRLDPDataModel>();
            for (var i = 0; i < _allSourceFiles.GetLength(0); i++)
            {
                filePipe.Enqueue(_allSourceFiles[i]);
            }
            _fileList = fileResult;
            var runThread = new Task[jumlah];
            _runTime = new System.Diagnostics.Stopwatch();
            _runTime.Start();
            backgroundWorker1.RunWorkerAsync();
            for(var i=0; i<jumlah; i++)
            {
                var drldp = new ExtractorWorkerThread[jumlah];
                drldp[i] = new ExtractorWorkerThread(filePipe, fileResult, reduceDimension.Checked);
                runThread[i] = new Task(drldp[i].Start);
            }
            for(var i=0; i<jumlah; i++)
            {
                runThread[i].Start();
            }
            reduceDimension.Enabled = false;
        }

        //progress bar
        private void ProgressBarWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                var progress = (int)(_fileList.Count / (double)_allSourceFiles.Length * 100);
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
            _runTime.Stop();
            reduceDimension.Enabled = true;
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            _runTime.Elapsed.Hours, _runTime.Elapsed.Minutes, _runTime.Elapsed.Seconds,
            _runTime.Elapsed.Milliseconds / 10);
            MessageBox.Show("Ekstraksi Selesai\n"+elapsedTime);
        }

        private void SaveDatatoDatabase(object sender, EventArgs e)
        {
            try
            {
                _runTime.Reset(); _runTime.Start();
                var ConnectionString = "Server=localhost;Database=face_feature;Uid=root;Pwd=;";
                var MyCommandString = new StringBuilder("INSERT INTO data_feature(label, dimension, fileName, data, size, dataset) VALUES ");
                using (var MyConnection = new MySqlConnection(ConnectionString))
                {
                    int counter = 0;
                    while (!_fileList.IsEmpty)
                    {
                        _fileList.TryDequeue(out DRLDPDataModel data);
                        MyCommandString.Append(string.Join(",", string.Format("('{0}','{1}','{2}','{3}','{4}','{5}')",
                                                                             MySqlHelper.EscapeString(data.label),
                                                                             MySqlHelper.EscapeString(data.dimension.ToString()),
                                                                             MySqlHelper.EscapeString(data.fileName),
                                                                             MySqlHelper.EscapeString(data.data),
                                                                             MySqlHelper.EscapeString(data.size),
                                                                             MySqlHelper.EscapeString(data.dataset))
                                                                             ));
                        counter++;
                        if (!_fileList.IsEmpty && counter != 800)
                            MyCommandString.Append(',');
                        if (counter == 800)
                        {
                            MyCommandString.Append(';');
                            using (var MyCommand = new MySqlCommand(MyCommandString.ToString(), MyConnection))
                            {
                                MyConnection.Open();
                                MyCommand.CommandType = CommandType.Text;
                                MyCommand.ExecuteNonQuery();
                                MyConnection.Close();
                            }
                            counter = 0;
                            System.Diagnostics.Debug.WriteLine("pushed to database");
                            MyCommandString = new StringBuilder("INSERT INTO data_feature(label, dimension, fileName, data, size, dataset) VALUES ");
                        }
                    }
                    MyCommandString.Append(';');
                    using (var MyCommand = new MySqlCommand(MyCommandString.ToString(), MyConnection))
                    {
                        MyConnection.Open();
                        MyCommand.CommandType = CommandType.Text;
                        MyCommand.ExecuteNonQuery();
                        MyConnection.Close();
                    }
                    System.Diagnostics.Debug.WriteLine("pushed to database last");
                }
                
                _runTime.Stop();
                var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                _runTime.Elapsed.Hours, _runTime.Elapsed.Minutes, _runTime.Elapsed.Seconds,
                _runTime.Elapsed.Milliseconds / 10);
                MessageBox.Show(elapsedTime);
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _minSize = Convert.ToInt32(MinSizeBox.Text);
            _maxSize = Convert.ToInt32(MaxSizeBox.Text);
            var partisiAwal = new string[_allSourceFiles.Length / 2];
            var partisiAkhir = new string[_allSourceFiles.Length / 2 + ((_allSourceFiles.Length % 2 == 1) ? 1 : 0)];
            for (var i = 0; i < _allSourceFiles.Length / 2; i++)
            {
                partisiAwal[i] = _allSourceFiles[i];
            }
            for (var i = 0; i < _allSourceFiles.Length / 2 + ((_allSourceFiles.Length % 2 == 1) ? 1 : 0); i++)
            {
                partisiAkhir[i] = _allSourceFiles[i + _allSourceFiles.Length / 2];
            }
            var resizeThread = new ResizerThreadWorker[2];
            resizeThread[0] = new ResizerThreadWorker(partisiAwal, _minSize, _maxSize, _targetPath);
            resizeThread[1] = new ResizerThreadWorker(partisiAkhir, _minSize, _maxSize, _targetPath);
            var runningThread = new Thread[2];
            runningThread[0] = new Thread(new ThreadStart(resizeThread[0].Resize));
            runningThread[1] = new Thread(new ThreadStart(resizeThread[1].Resize));
            runningThread[0].Start();
            runningThread[1].Start();
        }
    }
    //selfmade class for dropddown list item
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

}
