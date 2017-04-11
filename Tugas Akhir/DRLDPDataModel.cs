using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tugas_Akhir
{
    class DRLDPDataModel
    {
        public DRLDPDataModel()
        {

        }
        public string label { get; set; }
        public int dimension { get; set; }
        string fileName { get; set; }
        Byte[,] matrix { get; set; }
    }
}
