using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Tugas_Akhir
{
    class ResizerThreadWorker
    {
        private string[] _listFile;
        private string _destinationDirectory;
        private int _minSize;
        private int _maxSize;
        public ResizerThreadWorker(string[] fileName, int minSize, int maxSize, string destinationDirectory)
        {
            _listFile = fileName;
            _minSize = minSize;
            _maxSize = maxSize;
            _destinationDirectory = destinationDirectory;
        }
        public void Resize()
        {
            foreach(string file in _listFile)
            {
                var picture = new Image<Gray, byte>(file);
                picture.Resize(_minSize, _minSize, Emgu.CV.CvEnum.Inter.Nearest).ToBitmap().Save(_destinationDirectory + "/Mini " + Path.GetFileName(file));
                picture.Resize(_maxSize, _maxSize, Emgu.CV.CvEnum.Inter.Nearest).ToBitmap().Save(_destinationDirectory + "/Maxi " + Path.GetFileName(file));
            }
        }
    }
}
