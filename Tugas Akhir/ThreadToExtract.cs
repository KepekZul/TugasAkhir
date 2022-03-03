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
        bool reduceDimension;
        public ThreadToExtract(ConcurrentQueue<string> data, ConcurrentQueue<DRLDPDataModel> result, bool reduceDimension)
        {
            this.data = data;
            this.result = result;
            this.reduceDimension = reduceDimension;
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
                drldp.GetDRLDPMatrix();
                if (this.reduceDimension)
                {
                    //reduce dimension
                    dataModel.dimension = drldp.DrLdpMatrix.GetLength(0);
                    dataModel.matrix = drldp.DrLdpMatrix;
                }
                else
                {
                    //non reduced
                    dataModel.dimension = drldp.LdpResult.GetLength(0);
                    byte[,] tempMatrix = new byte[dataModel.dimension, dataModel.dimension];
                    for (int i = 0; i < dataModel.dimension; i++)
                    {
                        for (int j = 0; j < dataModel.dimension; j++)
                        {
                            tempMatrix[i, j] = Convert.ToByte(drldp.LdpResult[i, j]);
                        }
                    }
                    dataModel.matrix = tempMatrix;
                }
                dataModel.parseMatToString(true);
                result.Enqueue(dataModel);
            }
            System.Diagnostics.Debug.WriteLine("this thread is finish: "+this.ToString());
        }
    }
}
