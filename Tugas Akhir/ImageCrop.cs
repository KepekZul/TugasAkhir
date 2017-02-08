using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Tugas_Akhir
{
    class ImageCrop
    {
        Bitmap Gambar;
        Rectangle[] CropRectangle;
        public ImageCrop(Bitmap gambarAsal, Rectangle[] areaCrop)
        {
            this.Gambar = gambarAsal;
            this.CropRectangle = areaCrop;
        }
        public Bitmap[] getImages()
        {
            Bitmap[] hasil = new Bitmap[this.CropRectangle.Length];
            for (int i=0; i<this.CropRectangle.Length; i++)
            {
                hasil[i] = new Bitmap(CropRectangle[i].Width, CropRectangle[i].Height);
                for (int x=0; x<CropRectangle[i].Width; x++)
                {
                    for(int y=0; y<CropRectangle[i].Height; y++)
                    {
                        hasil[i].SetPixel(x, y, this.Gambar.GetPixel(CropRectangle[i].X + x, CropRectangle[i].Y + y));
                    }
                }
            }
            return hasil;
        }
    }
}
