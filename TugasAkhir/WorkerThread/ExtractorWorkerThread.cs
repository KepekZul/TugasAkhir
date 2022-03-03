using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;

namespace Tugas_Akhir
{
    class ExtractorWorkerThread
    {
        private ConcurrentQueue<string> _data;
        private ConcurrentQueue<DRLDPDataModel> _result;
        private bool _isReduceDimension;
        public ExtractorWorkerThread(ConcurrentQueue<string> data, ConcurrentQueue<DRLDPDataModel> result, bool reduceDimension)
        {
            _data = data;
            _result = result;
            _isReduceDimension = reduceDimension;
        }
        public void Start()
        {
            System.Diagnostics.Debug.WriteLine("this thread is start: " + ToString());
            while (!_data.IsEmpty)
            {
                _data.TryDequeue(out string fileName);

                var drldp = new DRLocalDirectionalPattern(new Bitmap(fileName));
                var dataModel = new DRLDPDataModel();

                var metaData = Path.GetFileName(fileName).Split('.');
                dataModel.size = metaData[0];
                dataModel.dataset = metaData[1];
                dataModel.fileName = metaData[2];
                dataModel.label = metaData[3];
                drldp.GetDRLDPMatrix();
                if (_isReduceDimension)
                {
                    //reduce dimension
                    dataModel.dimension = drldp.DrLdpMatrix.GetLength(0);
                    dataModel.matrix = drldp.DrLdpMatrix;
                }
                else
                {
                    //non reduced
                    dataModel.dimension = drldp.LdpResult.GetLength(0);
                    var tempMatrix = new byte[dataModel.dimension, dataModel.dimension];
                    for (var i = 0; i < dataModel.dimension; i++)
                    {
                        for (var j = 0; j < dataModel.dimension; j++)
                        {
                            tempMatrix[i, j] = Convert.ToByte(drldp.LdpResult[i, j]);
                        }
                    }
                    dataModel.matrix = tempMatrix;
                }
                dataModel.parseMatToString(true);
                _result.Enqueue(dataModel);
            }
            System.Diagnostics.Debug.WriteLine("this thread is finish: "+ToString());
        }
    }
}
