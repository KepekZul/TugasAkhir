using System;
using System.Collections.Generic;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Tugas_Akhir
{
    class MultiResize
    {
        string[] listFile;
        string destinationDirectory;
        int minSize;
        int maxSize;
        public MultiResize(string[] fileName, int minSize, int maxSize, string destinationDirectory)
        {
            this.listFile = fileName;
            this.minSize = minSize;
            this.maxSize = maxSize;
            this.destinationDirectory = destinationDirectory;
        }
        public void Resize()
        {
            foreach(string file in listFile)
            {
                Image<Gray, byte> picture = new Image<Gray, byte>(file);
                picture.Resize(this.minSize, this.minSize, Emgu.CV.CvEnum.Inter.Nearest).ToBitmap().Save(this.destinationDirectory+"/Mini "+Path.GetFileName(file));
                picture.Resize(this.maxSize, this.maxSize, Emgu.CV.CvEnum.Inter.Nearest).ToBitmap().Save(this.destinationDirectory + "/Maxi " + Path.GetFileName(file));
            }
        }
    }
}
