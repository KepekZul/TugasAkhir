using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Tugas_Akhir
{
    public partial class Panel_Demo : Form
    {
        DataTable dt;
        List<byte[,]> TrainModelData;
        List<string> TrainModelLabel;
        DRLocalDirectionalPattern drldp;
        public Panel_Demo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.ShowDialog();
            pictureBox1.Image = new Bitmap(opf.FileName);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            drldp = new DRLocalDirectionalPattern((Bitmap) pictureBox1.Image);
            byte[,] dr = drldp.GetDRLDPMatrix();
            Bitmap drImage = new Bitmap(dr.GetLength(0), dr.GetLength(1));
            for(int i=0; i<dr.GetLength(0); i++)
            {
                for(int j=0; j<dr.GetLength(1); j++)
                {
                    drImage.SetPixel(i, j, Color.FromArgb(dr[i, j], dr[i, j], dr[i, j]));
                }
            }
            pictureBox3.Image = drImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;

            Bitmap ldpImage = new Bitmap(drldp.LdpResult.GetLength(0), drldp.LdpResult.GetLength(1));
            for (int i = 0; i < drldp.LdpResult.GetLength(0); i++)
            {
                for (int j = 0; j < drldp.LdpResult.GetLength(1); j++)
                {
                    ldpImage.SetPixel(i, j, Color.FromArgb(drldp.LdpResult[i, j], drldp.LdpResult[i, j], drldp.LdpResult[i, j]));
                }
            }
            pictureBox2.Image = ldpImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.AppSettings["DataBaseConnectionString"];
            MySqlConnection MyConnect = new MySqlConnection(ConnectionString);
            MySqlCommand sqliCom = new MySqlCommand(textBox1.Text, MyConnect);
            dt = new DataTable();
            MySqlDataAdapter adap = new MySqlDataAdapter(sqliCom);
            adap.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TrainModelData = new List<byte[,]>();
            TrainModelLabel = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                DRLDPDataModel dataModel = new DRLDPDataModel
                {
                    Dimension = Int32.Parse(row["dimension"].ToString()),
                    Label = row["label"].ToString()
                };
                TrainModelData.Add(dataModel.Matrix);
                TrainModelLabel.Add(dataModel.Label);
            }
            DRLDPDataModel ujix = new DRLDPDataModel { Matrix = drldp.DrLdpMatrix, Dimension = drldp.DrLdpMatrix.GetLength(0) };
            KNearest knn = new KNearest(ujix.Matrix, TrainModelData, TrainModelLabel, int.Parse(this.textBox2.Text), int.Parse(this.textBox3.Text));
            label5.Text = "Label:"+knn.getClass();
        }
    }
}