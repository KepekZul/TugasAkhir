using System;
using System.IO;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Tugas_Akhir
{
    class ImageCrop
    {
        Bitmap Gambar;
        int Min;
        int Max;
        bool UseHistogramEqualiztion;
        Rectangle[] CropRectangle;
        CascadeClassifier haarCascade = new CascadeClassifier(System.Configuration.ConfigurationManager.AppSettings["1"]);
        public ImageCrop(Bitmap gambarAsal, Rectangle[] areaCrop)
        {
            this.Gambar = gambarAsal;
            this.CropRectangle = areaCrop;
        }
        public ImageCrop(Bitmap gambarAsal, int min, int max, bool useHisteq)
        {
            this.Gambar = gambarAsal;
            this.Min = min;
            this.Max = max;
            this.UseHistogramEqualiztion = useHisteq;
            getFace();
        }
        public Bitmap[] getImages()
        {
            Bitmap[] hasil = new Bitmap[this.CropRectangle.Length];
            for (int i=0; i<this.CropRectangle.Length; i++)
            {
                hasil[i] = new Bitmap(this.CropRectangle[i].Width, this.CropRectangle[i].Height);
                for (int x=0; x<this.CropRectangle[i].Width; x++)
                {
                    for(int y=0; y<this.CropRectangle[i].Height; y++)
                    {
                        hasil[i].SetPixel(x, y, this.Gambar.GetPixel(this.CropRectangle[i].X + x, this.CropRectangle[i].Y + y));
                    }
                }
            }
            return hasil;
        }
        private void getFace()
        {
            System.Diagnostics.Debug.WriteLine("getfaces");
            Image<Gray, byte> grayImage = new Image<Gray, byte>(this.Gambar);
            if (this.UseHistogramEqualiztion == true)
            {
                grayImage._EqualizeHist();
            }
            this.CropRectangle = haarCascade.DetectMultiScale(grayImage, 1.01, 4, new Size(this.Min, this.Min), new Size(this.Max, this.Max));
        }
    }
}
