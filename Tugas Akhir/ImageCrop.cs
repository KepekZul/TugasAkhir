using System;
using System.IO;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Tugas_Akhir
{
    class ImageCrop
    {
        Bitmap image;
        int minimumSize;
        int maximumSize;
        bool isUsingHistogramEqualization;
        Rectangle[] cropRectangle;
        CascadeClassifier haarCascade = new CascadeClassifier(System.Configuration.ConfigurationManager.AppSettings["1"]);
        public ImageCrop(Bitmap initialImage, Rectangle[] cropArea)
        {
            image = initialImage;
            cropRectangle = cropArea;
        }
        public ImageCrop(Bitmap initialImage, int min, int max, bool useHisteq)
        {
            image = initialImage;
            minimumSize = min;
            maximumSize = max;
            isUsingHistogramEqualization = useHisteq;
            getFace();
        }
        public Bitmap[] GetImages()
        {
            Bitmap[] result = new Bitmap[this.cropRectangle.Length];
            for (int i=0; i<this.cropRectangle.Length; i++)
            {
                result[i] = new Bitmap(this.cropRectangle[i].Width, this.cropRectangle[i].Height);
                for (int x=0; x<this.cropRectangle[i].Width; x++)
                {
                    for(int y=0; y<this.cropRectangle[i].Height; y++)
                    {
                        result[i].SetPixel(x, y, this.image.GetPixel(this.cropRectangle[i].X + x, this.cropRectangle[i].Y + y));
                    }
                }
            }
            return result;
        }
        private void getFace()
        {
            Image<Gray, byte> grayImage = new Image<Gray, byte>(this.image);
            if (this.isUsingHistogramEqualization == true)
            {
                grayImage._EqualizeHist();
            }
            cropRectangle = haarCascade.DetectMultiScale(grayImage, 1.01, 4, new Size(this.minimumSize, this.minimumSize), new Size(this.maximumSize, this.maximumSize));
        }
    }
}
