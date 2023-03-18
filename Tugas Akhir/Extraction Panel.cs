using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Tugas_Akhir
{
    public partial class Extraction_Panel : Form
    {
        ConcurrentQueue<DRLDPDataModel> fileList;
        System.Diagnostics.Stopwatch RunTime;
        string pathSource;
        string pathTarget;
        string[] allSourceFiles;
        public Extraction_Panel()
        {
            InitializeComponent();
            ComboboxItem ppm = new ComboboxItem() , pgm = new ComboboxItem(), all = new ComboboxItem();
            ppm.Text = "ppm file";
            ppm.Value = "*ppm";
            pgm.Text = "pgm file";
            pgm.Value = "*.pgm";
            all.Text = "bitmap";
            all.Value = "*.*";
            comboBox1.Items.Add(ppm);
            comboBox1.Items.Add(pgm);
            comboBox1.Items.Add(all);
            comboBox1.SelectedIndex = 2;
        }
        public void sourceFolder(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.pathSource = folderDialog.SelectedPath;
                textBox1.Text = this.pathSource;
                allSourceFiles = Directory
                    .GetFiles(this.pathSource, (comboBox1.SelectedItem as ComboboxItem).Value.ToString(), SearchOption.AllDirectories)
                    .Where(s => !s.EndsWith(".info") || !s.EndsWith(".txt")|| !s.EndsWith("*.ini"))
                    .ToArray();
                if (FilenameFilterBox.Text != "")
                {
                    string[] keepFile = FilenameFilterBox.Text.Split(' ');
                    for (int i = 0; i < allSourceFiles.Length; i++)
                    {
                        for (int j = 0; j < keepFile.Length; j++)
                        {
                            if (Path.GetFileName(allSourceFiles[i]).Contains(keepFile[j]))
                            {
                                break;
                            }
                            if (j == keepFile.Length - 1)
                            {
                                allSourceFiles[i] = "";
                            }
                        }
                    }
                }
                FileList dd = new FileList();
                dd.datas = allSourceFiles;
                dd.Show();
            }
        }

        public void DestinationFolder(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowDialog();
            this.pathTarget = folderDialog.SelectedPath;
            textBox2.Text = this.pathTarget;
        }

        private int getThreadCountFromSettings()
        {
            return int.Parse(System.Configuration.ConfigurationManager.AppSettings["maxThread"]);
        }

        private void cropSelectedFiles(object sender, EventArgs e)
        {
            var threadCount = getThreadCountFromSettings();
            var minSize = Convert.ToInt32(this.MinSizeBox.Text);
            var maxSize = Convert.ToInt32(this.MaxSizeBox.Text);
            var processQueue = new ConcurrentQueue<string>(allSourceFiles);
            
            Parallel.For(0, threadCount, index =>
            {
                var worker = new Thread(new ThreadStart(new ImageCropper(processQueue, minSize, maxSize, this.checkBox1.Checked, this.pathTarget).Crop));
                worker.Start();
            });
        }
        private void resizeSelectedFiles(object sender, EventArgs e)
        {
            var threadCount = getThreadCountFromSettings();
            var minSize = Convert.ToInt32(this.MinSizeBox.Text);
            var maxSize = Convert.ToInt32(this.MaxSizeBox.Text);
            var processQueue = new ConcurrentQueue<string>(allSourceFiles);

            Parallel.For(0, threadCount, index =>
            {
                var worker = new Thread(new ThreadStart( new ImageResizer(processQueue, minSize, maxSize, this.pathTarget).Resize));
                worker.Start();
            });
        }

        private void ExtractImage_Click(object sender, EventArgs e)
        {
            this.reduceDimension.Enabled = false;
            int threadCount = getThreadCountFromSettings();
            ConcurrentQueue<string> filePipe = new ConcurrentQueue<string>(allSourceFiles);
            ConcurrentQueue<DRLDPDataModel> fileResult = new ConcurrentQueue<DRLDPDataModel>();
            this.fileList = fileResult;
            RunTime = new System.Diagnostics.Stopwatch();
            RunTime.Start();
            backgroundWorker1.RunWorkerAsync();
            Parallel.For(0, threadCount, index =>
            {
                var worker = new Task(new LocalDirectionalPatternExtractor(filePipe, fileResult, this.reduceDimension.Checked).Start);
                worker.Start();
            });
            this.reduceDimension.Enabled = false;
        }

        //progress bar
        private void progressBarWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                int progress = (int)(fileList.Count / (double)allSourceFiles.Length * 100);
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
            RunTime.Stop();
            this.reduceDimension.Enabled = true;
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            RunTime.Elapsed.Hours, RunTime.Elapsed.Minutes, RunTime.Elapsed.Seconds,
            RunTime.Elapsed.Milliseconds / 10);
            MessageBox.Show("Ekstraksi Selesai\n"+elapsedTime);
        }

        private void saveDatatoDatabase(object sender, EventArgs e)
        {
            try
            {
                RunTime.Reset(); RunTime.Start();
                string ConnectionString = "Server=localhost;Database=face_feature;Uid=root;Pwd=;";
                StringBuilder MyCommandString = new StringBuilder("INSERT INTO data_feature(label, dimension, fileName, data, size, dataset) VALUES ");
                using (MySqlConnection MyConnection = new MySqlConnection(ConnectionString))
                {
                    int counter = 0;
                    while (!this.fileList.IsEmpty)
                    {
                        DRLDPDataModel data; fileList.TryDequeue(out data);
                        MyCommandString.Append(string.Join(",", string.Format("('{0}','{1}','{2}','{3}','{4}','{5}')",
                                                                             MySqlHelper.EscapeString(data.Label),
                                                                             MySqlHelper.EscapeString(data.Dimension.ToString()),
                                                                             MySqlHelper.EscapeString(data.FileName),
                                                                             MySqlHelper.EscapeString(data.Size),
                                                                             MySqlHelper.EscapeString(data.Dataset))
                                                                             ));
                        counter++;
                        if (!this.fileList.IsEmpty && counter != 800)
                            MyCommandString.Append(',');
                        if (counter == 800)
                        {
                            MyCommandString.Append(';');
                            using (MySqlCommand MyCommand = new MySqlCommand(MyCommandString.ToString(), MyConnection))
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
                    using (MySqlCommand MyCommand = new MySqlCommand(MyCommandString.ToString(), MyConnection))
                    {
                        MyConnection.Open();
                        MyCommand.CommandType = CommandType.Text;
                        MyCommand.ExecuteNonQuery();
                        MyConnection.Close();
                    }
                    System.Diagnostics.Debug.WriteLine("pushed to database last");
                }
                RunTime.Stop();
                var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                RunTime.Elapsed.Hours, RunTime.Elapsed.Minutes, RunTime.Elapsed.Seconds,
                RunTime.Elapsed.Milliseconds / 10);
                MessageBox.Show(elapsedTime);
            }
            catch
            {
                MessageBox.Show("Error");
            }
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
