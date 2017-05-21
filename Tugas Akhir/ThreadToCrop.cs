using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Tugas_Akhir
{
    class ThreadToCrop
    {
        string[] CropTargets;
        string Foldertarget;
        int MinSize;
        int MaxSize;
        bool UseHisTeq;
        public ThreadToCrop(String[] list, int min, int max, bool usehisteq, string targetfolder)
        {
            this.CropTargets = list;
            this.MinSize = min;
            this.MaxSize = max;
            this.UseHisTeq = usehisteq;
            this.Foldertarget = targetfolder;
        }
        public void CropStart()
        {
            foreach(string filepath in CropTargets)
            {
                if (filepath == "")
                    continue;
                ImageCrop imageCroper;
                if (Path.GetExtension(filepath) == ".ppm")
                {
                    imageCroper = new ImageCrop(PPMReader.ReadBitmapFromPPM(filepath), this.MinSize, this.MaxSize, this.UseHisTeq);
                }
                else if (Path.GetExtension(filepath) == ".pgm")
                {
                    PortableGrayMap imageConvert = new PortableGrayMap(filepath);
                    imageCroper = new ImageCrop(imageConvert.MakeBitmap(imageConvert, 1), this.MinSize, this.MaxSize, this.UseHisTeq);
                }
                else
                {
                    imageCroper = new ImageCrop(new Bitmap(filepath), this.MinSize, this.MaxSize, this.UseHisTeq);
                }
                Bitmap[] cropResult = imageCroper.getImages();
                int j = 1;
                if (cropResult.Length == 0)
                {
                    System.Diagnostics.Debug.WriteLine(filepath);
                }
                foreach(Bitmap cropedImage in cropResult)
                {
                    cropedImage.Save(Foldertarget+"/"+Path.GetFileNameWithoutExtension(filepath)+".hasil "+j.ToString()+".gif");
                    j++;
                }
            }
        }
    }
}
