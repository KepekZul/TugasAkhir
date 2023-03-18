using System;

namespace Tugas_Akhir
{
    class DRLDPDataModel
    {
        public string Label { get; set; }
        public int Dimension { get; set; }
        public string FileName { get; set; }
        public Byte[,] Matrix { get; set; }
        public string Dataset { get; set; }
        public string Size { get; set; }
    }
}
