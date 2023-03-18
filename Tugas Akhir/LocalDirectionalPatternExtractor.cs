using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;

namespace Tugas_Akhir
{
    class LocalDirectionalPatternExtractor
    {
        ConcurrentQueue<string> dataQueue;
        ConcurrentQueue<DRLDPDataModel> resultQueue;
        bool isDimensionalReductionEnabled;
        public LocalDirectionalPatternExtractor(ConcurrentQueue<string> data, ConcurrentQueue<DRLDPDataModel> result, bool reduceDimension)
        {
            dataQueue = data;
            resultQueue = result;
            isDimensionalReductionEnabled = reduceDimension;
        }
        public void Start()
        {
            System.Diagnostics.Debug.WriteLine("this thread is start: " + this.ToString());
            while (!dataQueue.IsEmpty)
            {
                string fileName; dataQueue.TryDequeue(out fileName);

                DRLocalDirectionalPattern drldp = new DRLocalDirectionalPattern(new Bitmap(fileName));
                DRLDPDataModel dataModel = new DRLDPDataModel();

                string[] metaData = Path.GetFileName(fileName).Split('.');
                dataModel.Size = metaData[0];
                dataModel.Dataset = metaData[1];
                dataModel.FileName = metaData[2];
                dataModel.Label = metaData[3];
                drldp.GetDRLDPMatrix();
                if (isDimensionalReductionEnabled)
                {
                    dataModel.Dimension = drldp.DrLdpMatrix.GetLength(0);
                    dataModel.Matrix = drldp.DrLdpMatrix;
                }
                else
                {
                    dataModel.Dimension = drldp.LdpResult.GetLength(0);
                    byte[,] tempMatrix = new byte[dataModel.Dimension, dataModel.Dimension];
                    for (int i = 0; i < dataModel.Dimension; i++)
                    {
                        for (int j = 0; j < dataModel.Dimension; j++)
                        {
                            tempMatrix[i, j] = Convert.ToByte(drldp.LdpResult[i, j]);
                        }
                    }
                    dataModel.Matrix = tempMatrix;
                }
                resultQueue.Enqueue(dataModel);
            }
            System.Diagnostics.Debug.WriteLine("this thread is finish: "+this.ToString());
        }
    }
}
