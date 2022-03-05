using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Tugas_Akhir
{
    public partial class Panel_Demo : Form
    {
        private DataTable _dt;
        private List<byte[,]> _trainModelData;
        private List<string> _trainModelLabel;
        private DRLocalDirectionalPattern _drldp;

        public Panel_Demo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var opf = new OpenFileDialog();
            opf.ShowDialog();
            pictureBox1.Image = new Bitmap(opf.FileName);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _drldp = new DRLocalDirectionalPattern((Bitmap) pictureBox1.Image);
            byte[,] dr = _drldp.GetDRLDPMatrix();
            var drImage = new Bitmap(dr.GetLength(0), dr.GetLength(1));
            for (var i = 0; i < dr.GetLength(0); i++)
            {
                for(var j=0; j<dr.GetLength(1); j++)
                {
                    drImage.SetPixel(i, j, Color.FromArgb(dr[i, j], dr[i, j], dr[i, j]));
                }
            }
            pictureBox3.Image = drImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;

            var ldpImage = new Bitmap(_drldp.LdpResult.GetLength(0), _drldp.LdpResult.GetLength(1));
            for (var i = 0; i < _drldp.LdpResult.GetLength(0); i++)
            {
                for (var j = 0; j < _drldp.LdpResult.GetLength(1); j++)
                {
                    ldpImage.SetPixel(i, j, Color.FromArgb(_drldp.LdpResult[i, j], _drldp.LdpResult[i, j], _drldp.LdpResult[i, j]));
                }
            }
            pictureBox2.Image = ldpImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ConnectionString = ConfigurationManager.AppSettings["DataBaseConnectionString"];
            MySqlConnection MyConnect = new MySqlConnection(ConnectionString);
            MySqlCommand sqliCom = new MySqlCommand(textBox1.Text, MyConnect);
            _dt = new DataTable();
            var adap = new MySqlDataAdapter(sqliCom);
            adap.Fill(_dt);
            dataGridView1.DataSource = _dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _trainModelData = new List<byte[,]>();
            _trainModelLabel = new List<string>();
            foreach (DataRow row in _dt.Rows)
            {
                var dataModel = new DRLDPDataModel
                {
                    data = row["data"].ToString(),
                    dimension = Int32.Parse(row["dimension"].ToString()),
                    label = row["label"].ToString()
                };
                dataModel.parseStringToMat(true);
                _trainModelData.Add(dataModel.matrix);
                _trainModelLabel.Add(dataModel.label);
            }
            var ujix = new DRLDPDataModel { matrix = _drldp.DrLdpMatrix, dimension = _drldp.DrLdpMatrix.GetLength(0) };
            var knn = new KNearest(ujix.matrix, _trainModelData, _trainModelLabel, int.Parse(textBox2.Text), int.Parse(this.textBox3.Text));
            label5.Text = "Label:"+knn.GetClass();
        }
    }
}