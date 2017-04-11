using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Text;
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
                drldp.getDRLDPMatrix();

            }
        }
    }
}
