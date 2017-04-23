using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Tugas_Akhir
{
    public partial class Control_Panel : Form
    {
        string pathSource;
        string pathTarget;
        string[] allSourceFiles;
        int minSize;
        int maxSize;
        public Control_Panel()
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
                allSourceFiles = Directory.GetFiles(this.pathSource, (comboBox1.SelectedItem as ComboboxItem).Value.ToString(), SearchOption.AllDirectories)
                    .Where(s => !s.EndsWith(".info") || !s.EndsWith(".txt")|| !s.EndsWith("*.ini")).ToArray();
                string[] keepFile = FilenameFilterBox.Text.Split(' ');
                for(int i=0; i<allSourceFiles.Length; i++)
                {
                    for (int j = 0; j < keepFile.Length; j++)
                    {
                        if (Path.GetFileName(allSourceFiles[i]).Contains(keepFile[j]))
                        {
                            break;
                        }
                        if (j == keepFile.Length-1)
                        {
                            allSourceFiles[i] = "";
                        }
                    }
                }
                //keperluan debugging daftar file yang terambil
                daftarData dd = new daftarData();
                dd.datas = allSourceFiles;
                dd.Show();
            }
        }

        public void destinationFolder(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowDialog();
            this.pathTarget = folderDialog.SelectedPath;
            textBox2.Text = this.pathTarget;
        }

        private void cropSelectedFiles(object sender, EventArgs e)
        {
            this.minSize = Convert.ToInt32(this.MinSizeBox.Text);
            this.maxSize = Convert.ToInt32(this.MaxSizeBox.Text);
            System.Diagnostics.Debug.WriteLine(this.checkBox1.Checked.ToString());//debugging
            ThreadToCrop[] cropThread = new ThreadToCrop[2];
            string[] partisiAwal = new string[allSourceFiles.Length/2];
            for(int i=0; i<allSourceFiles.Length/2; i++)
            {
                partisiAwal[i] = allSourceFiles[i];
            }
            string[] partisiAkhir = new string[allSourceFiles.Length / 2 + ((allSourceFiles.Length % 2 == 1) ? 1 : 0)];
            for(int i =0; i< allSourceFiles.Length / 2 + ((allSourceFiles.Length % 2 == 1) ? 1 : 0); i++)
            {
                partisiAkhir[i] = allSourceFiles[i+ allSourceFiles.Length / 2];
            }
            cropThread[0] = new ThreadToCrop(partisiAwal, this.minSize, this.maxSize, this.checkBox1.Checked, this.pathTarget);
            cropThread[1] = new ThreadToCrop(partisiAkhir, this.minSize, this.maxSize, this.checkBox1.Checked, this.pathTarget);
            Thread[] cropingThread = new Thread[2];
            cropingThread[0] = new Thread(new ThreadStart(cropThread[0].CropStart));
            cropingThread[1] = new Thread(new ThreadStart(cropThread[1].CropStart));
            cropingThread[0].Start();
            cropingThread[1].Start();
        }
        private void resize(object sender, EventArgs e)
        {
            this.minSize = Convert.ToInt32(this.MinSizeBox.Text);
            this.maxSize = Convert.ToInt32(this.MaxSizeBox.Text);
            ThreadToResize[] resizeThread = new ThreadToResize[2];
            string[] partisiAwal = new string[allSourceFiles.Length/2];
            string[] partisiAkhir = new string[allSourceFiles.Length/2 + ((allSourceFiles.Length%2==1)?1:0)];
            for(int i=0; i<this.allSourceFiles.Length/2; i++)
            {
                partisiAwal[i] = allSourceFiles[i];
            }
            for (int i = 0; i < allSourceFiles.Length / 2 + ((allSourceFiles.Length % 2 == 1) ? 1 : 0); i++)
            {
                partisiAkhir[i] = allSourceFiles[i + allSourceFiles.Length / 2];
            }
            resizeThread[0] = new ThreadToResize(partisiAwal, this.minSize, this.maxSize, this.pathTarget);
            resizeThread[1] = new ThreadToResize(partisiAkhir, this.minSize, this.maxSize, this.pathTarget);
            Thread[] runningThread = new Thread[2];
            runningThread[0] = new Thread(new ThreadStart(resizeThread[0].Resize));
            runningThread[1] = new Thread(new ThreadStart(resizeThread[1].Resize));
            runningThread[0].Start();
            runningThread[1].Start();
        }

        private void ExtractImage_Click(object sender, EventArgs e)
        {
            int jumlah = 4;
            ConcurrentQueue<string> filePipe = new ConcurrentQueue<string>();
            ConcurrentQueue<DRLDPDataModel> fileResult = new ConcurrentQueue<DRLDPDataModel>();
            for(int i=0; i<allSourceFiles.GetLength(0); i++)
            {
                filePipe.Enqueue(allSourceFiles[i]);
            }
            Task[] runThread = new Task[jumlah];
            ThreadToExtract[] drldp = new ThreadToExtract[jumlah];
            var stopwatch1 = new System.Diagnostics.Stopwatch();
            stopwatch1.Start();
            for(int i=0; i<jumlah; i++)
            {
                drldp[i] = new ThreadToExtract(filePipe, fileResult);
                runThread[i] = new Task(drldp[i].startRun);
                runThread[i].Start();
            }
            Task.WaitAll(runThread);
            stopwatch1.Stop();
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            stopwatch1.Elapsed.Hours, stopwatch1.Elapsed.Minutes, stopwatch1.Elapsed.Seconds,
            stopwatch1.Elapsed.Milliseconds / 10);
            System.Diagnostics.Debug.WriteLine(elapsedTime);
            stopwatch1 = new System.Diagnostics.Stopwatch();
            stopwatch1.Start();
            string ConString = "Server=localhost;Database=face_feature;Uid=root;Pwd=;";
            string ComString = "INSERT INTO data_feature(label, dimension, fileName, data, size) VALUES(@label, @dimension, @fileName, @data, @size)";
            using (var MySqliCon = new MySqlConnection(ConString))
            {
                MySqliCon.Open();
                MySqlTransaction transaction = MySqliCon.BeginTransaction();
                var MySqliAdap = new MySqlDataAdapter("SELECT label, dimension, fileName, data, size FROM data_feature", MySqliCon);
                var dataSet = new DataSet();
                MySqliAdap.Fill(dataSet, "data_feature");
                MySqliAdap = new MySqlDataAdapter();
                MySqliAdap.InsertCommand = new MySqlCommand(ComString, MySqliCon);
                MySqliAdap.InsertCommand.Parameters.Add("@label", MySqlDbType.VarChar, 20, "label");
                MySqliAdap.InsertCommand.Parameters.Add("@dimension", MySqlDbType.Int32, 11, "dimension");
                MySqliAdap.InsertCommand.Parameters.Add("@fileName", MySqlDbType.VarChar, 20, "fileName");
                MySqliAdap.InsertCommand.Parameters.Add("@data", MySqlDbType.Text, 20000000, "data");
                MySqliAdap.InsertCommand.Parameters.Add("@size", MySqlDbType.VarChar, 10, "size");
                MySqliAdap.InsertCommand.UpdatedRowSource = UpdateRowSource.None;

                while(!fileResult.IsEmpty)
                {
                    DRLDPDataModel data = new DRLDPDataModel();
                    fileResult.TryDequeue(out data);
                    DataRow row = dataSet.Tables["data_feature"].NewRow();
                    row["label"] = data.label;
                    row["dimension"] = data.dimension;
                    row["filename"] = data.fileName;
                    row["data"] = data.data;
                    row["size"] = data.size;
                    dataSet.Tables["data_feature"].Rows.Add(row);
                }
                MySqliAdap.Update(dataSet, "data_feature");

                transaction.Commit();
                MySqliCon.Close();
            }
            stopwatch1.Stop();
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            stopwatch1.Elapsed.Hours, stopwatch1.Elapsed.Minutes, stopwatch1.Elapsed.Seconds,
            stopwatch1.Elapsed.Milliseconds / 10);
            System.Diagnostics.Debug.WriteLine(elapsedTime);
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
