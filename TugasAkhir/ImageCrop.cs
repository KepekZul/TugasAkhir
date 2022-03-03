using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace Tugas_Akhir
{
    class ImageCrop
    {
        private Bitmap _image;
        private int _min;
        private int _max;
        private bool _isUseHistogramEqualiztion;
        private Rectangle[] _cropRectangle;
        private CascadeClassifier _haarCascade = new CascadeClassifier(System.Configuration.ConfigurationManager.AppSettings["1"]);
        public ImageCrop(Bitmap originalImage, Rectangle[] cropArea)
        {
            _image = originalImage;
            _cropRectangle = cropArea;
        }
        public ImageCrop(Bitmap originalImage, int min, int max, bool isUseHistogramEqualization)
        {
            _image = originalImage;
            _min = min;
            _max = max;
            _isUseHistogramEqualiztion = isUseHistogramEqualization;
            GetFace();
        }
        public Bitmap[] GetImages()
        {
            var hasil = new Bitmap[_cropRectangle.Length];
            for (var i=0; i<_cropRectangle.Length; i++)
            {
                hasil[i] = new Bitmap(_cropRectangle[i].Width, _cropRectangle[i].Height);
                for (var x=0; x<_cropRectangle[i].Width; x++)
                {
                    for(var y=0; y<_cropRectangle[i].Height; y++)
                    {
                        hasil[i].SetPixel(x, y, _image.GetPixel(_cropRectangle[i].X + x, _cropRectangle[i].Y + y));
                    }
                }
            }
            return hasil;
        }
        private void GetFace()
        {
            var grayImage = new Image<Gray, byte>(_image);
            if (_isUseHistogramEqualiztion == true)
            {
                grayImage._EqualizeHist();
            }
            _cropRectangle = _haarCascade.DetectMultiScale(grayImage, 1.01, 4, new Size(_min, _min), new Size(_max, _max));
        }
    }
}
