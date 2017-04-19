using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Tugas_Akhir
{
    class ThreadToExtract
    {
        ConcurrentQueue<string> data;
        ConcurrentQueue<DRLDPDataModel> result;
        public ThreadToExtract(ConcurrentQueue<string> data, ConcurrentQueue<DRLDPDataModel> result)
        {
            this.data = data;
            this.result = result;
        }
        public void startRun()
        {
            while (!data.IsEmpty)
            {
                string fileName; data.TryDequeue(out fileName);

                DRLocalDirectionalPattern drldp = new DRLocalDirectionalPattern(new Bitmap(fileName));
                DRLDPDataModel dataModel = new DRLDPDataModel();

                string[] metaData = Path.GetFileName(fileName).Split('.');
                dataModel.size = metaData[0];
                dataModel.dataset = metaData[1];
                dataModel.fileName = metaData[2];
                dataModel.label = metaData[3];
                drldp.getDRLDPMatrix();
                dataModel.dimension = drldp.drldpMatrix.GetLength(0);
                dataModel.matrix = drldp.drldpMatrix;
                //dataModel.matrix = new byte[dataModel.dimension, dataModel.dimension];
                //for(int i=0; i< dataModel.dimension; i++)
                //{
                //    for(int j=0; j<dataModel.dimension; j++)
                //    {
                //        dataModel.matrix[i, j] = drldp.drldpMatrix[i, j];
                //        //System.Diagnostics.Debug.Write(dataModel.matrix[i,j].ToString()+" "+drldp.drldpMatrix[i,j].ToString());
                //    }
                //}
                dataModel.parseMatToString(true);
                result.Enqueue(dataModel);
            }
            System.Diagnostics.Debug.WriteLine("this thread is finish: "+this.ToString());
        }
    }
}
