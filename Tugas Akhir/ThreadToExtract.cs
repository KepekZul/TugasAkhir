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
            System.Diagnostics.Debug.WriteLine("this thread is start: " + this.ToString());
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
                //original
                //dataModel.dimension = drldp.drldpMatrix.GetLength(0);
                //dataModel.matrix = drldp.drldpMatrix;

                //experiment
                dataModel.dimension = drldp.ldpResult.GetLength(0);
                byte[,] tempMatrix = new byte[dataModel.dimension, dataModel.dimension];
                for (int i = 0; i < dataModel.dimension; i++)
                {
                    for (int j = 0; j < dataModel.dimension; j++)
                    {
                        tempMatrix[i, j] = Convert.ToByte(drldp.ldpResult[i, j]);
                    }
                }
                dataModel.matrix = tempMatrix;

                dataModel.parseMatToString(true);
                result.Enqueue(dataModel);
            }
            System.Diagnostics.Debug.WriteLine("this thread is finish: "+this.ToString());
        }
    }
}
