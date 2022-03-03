using System;
using System.Drawing;
using System.IO;

namespace Tugas_Akhir
{
    class CropperThreadWorker
    {
        private string[] _cropTargets;
        private string _foldertarget;
        private int _minSize;
        private int _maxSize;
        private bool _isUseHistogramEqualization;

        public CropperThreadWorker(String[] list, int min, int max, bool usehisteq, string targetfolder)
        {
            _cropTargets = list;
            _minSize = min;
            _maxSize = max;
            _isUseHistogramEqualization = usehisteq;
            _foldertarget = targetfolder;
        }
        public void CropStart()
        {
            foreach(string filepath in _cropTargets)
            {
                if (filepath == "")
                    continue;
                ImageCrop imageCroper;
                if (Path.GetExtension(filepath) == ".ppm")
                {
                    imageCroper = new ImageCrop(PPMReader.ReadBitmapFromPPM(filepath), _minSize, _maxSize, _isUseHistogramEqualization);
                }
                else if (Path.GetExtension(filepath) == ".pgm")
                {
                    PortableGrayMap imageConvert = new PortableGrayMap(filepath);
                    imageCroper = new ImageCrop(imageConvert.MakeBitmap(imageConvert, 1), _minSize, _maxSize, _isUseHistogramEqualization);
                }
                else
                {
                    imageCroper = new ImageCrop(new Bitmap(filepath), _minSize, _maxSize, _isUseHistogramEqualization);
                }
                Bitmap[] cropResult = imageCroper.GetImages();
                int j = 1;
                if (cropResult.Length == 0)
                {
                    System.Diagnostics.Debug.WriteLine(filepath);
                }
                foreach(Bitmap cropedImage in cropResult)
                {
                    cropedImage.Save(_foldertarget+"/"+Path.GetFileNameWithoutExtension(filepath)+".hasil "+j.ToString()+".gif");
                    j++;
                }
            }
        }
    }
}
