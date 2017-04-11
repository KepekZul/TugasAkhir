using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tugas_Akhir
{
    class DRLDPDataModel
    {
        public string label { get; set; }
        public int dimension { get; set; }
        public string fileName { get; set; }
        public Byte[,] matrix { get; set; }
        public string data { get; set; }
        /// <summary>
        /// Parse the current data matrix to string
        /// </summary>
        /// <param name="keep">decide whether to keep the result inside the class or not</param>
        /// <returns></returns>
        public string parseMatToString(bool keep)
        {
            string data = "";
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    data += this.matrix[i, j].ToString() + " ";
                }
            }
            if (keep == true)
            {
                this.data = data;
            }
            return data;
        }
        /// <summary>
        /// Parse the currnet data string to data matrix
        /// </summary>
        /// <param name="keep">deceide whether to keep the result inside the class or not</param>
        /// <returns></returns>
        public Byte[,] parseStringToMat(bool keep)
        {
            Byte[,] matrix = new byte[this.dimension, this.dimension];
            string[] splitedData = this.data.Split(' ');
            int x = 0;
            for(int i=0; i<dimension; i++)
            {
                for (int j=0; j < dimension; j++)
                {
                    matrix[i, j] = Convert.ToByte(splitedData[x++]);
                }
            }
            if(keep == true)
            {
                this.matrix = matrix;
            }
            return matrix;
        }
    }
}
