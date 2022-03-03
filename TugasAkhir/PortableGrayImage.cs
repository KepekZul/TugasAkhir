using System;
using System.IO;
using System.Drawing;
namespace Tugas_Akhir
{
    class PortableGrayMap
    {
        public int Width;
        public int Heigth;
        public int MaxVal;
        public byte[][] Pixels;
        public PortableGrayMap(int width, int height, int maxVal, byte[][] pixels)
        {
            Width = width;
            Heigth = height;
            MaxVal = maxVal;
            Pixels = pixels;
        }
        public PortableGrayMap(string filename)
        {
            var x = LoadImage(filename);
            Width = x.Width;
            Heigth = x.Heigth;
            MaxVal = x.MaxVal;
            Pixels = x.Pixels;
        }
        public PortableGrayMap LoadImage(string file)
        {
            var inputFileStream = new FileStream(file, FileMode.Open);
            var binaryReader = new BinaryReader(inputFileStream);
            var fileTypeCode = NextNonCommentLine(binaryReader);
            if (fileTypeCode != "P5")
            {
                throw new Exception("Non PGM file type" + fileTypeCode);
            }
            var widthHeight = NextNonCommentLine(binaryReader);
            var tokens = widthHeight.Split(' ');
            var width = int.Parse(tokens[0]);
            var height = int.Parse(tokens[1]);
            var sMaxVal = NextNonCommentLine(binaryReader);
            var maxVal = int.Parse(sMaxVal);
            var pixels = new byte[height][];
            for(var i =0; i<height; ++i)
            {
                pixels[i] = new byte[width];
            }
            for(var i =0; i<height; ++i)
            {
                for(int j=0; j<width; ++j)
                {
                    pixels[i][j] = binaryReader.ReadByte();
                }
            }
            binaryReader.Close();
            inputFileStream.Close();
            var result = new PortableGrayMap(width, height, maxVal, pixels);
            return result;
        }
        static string NextAnyLine(BinaryReader br)
        {
            var s = "";
            var b = 0;//dummy
            while(b!=10)//enter atau baris baru
            {
                b = br.ReadByte();
                char c = (char)b;
                s += c;
            }
            return s.Trim();
        }
        static string NextNonCommentLine(BinaryReader br)
        {
            var s = NextAnyLine(br);
            while (s.StartsWith("#") || s == "")
            {
                s = NextAnyLine(br);
            }
            return s;
        }
        public Bitmap MakeBitmap(PortableGrayMap gambarPgm, int mag)
        {
            var width = gambarPgm.Width*mag;
            var height = gambarPgm.Heigth*mag;
            var result = new Bitmap(width, height);
            var gr = Graphics.FromImage(result);
            for(var i =0; i<gambarPgm.Heigth; ++i)
            {
                for (var j =0; j<gambarPgm.Width; ++j)
                {
                    var pixelColor = gambarPgm.Pixels[i][j];
                    var warna = Color.FromArgb(pixelColor, pixelColor, pixelColor);
                    var sb = new SolidBrush(warna);
                    gr.FillRectangle(sb, j * mag, i * mag, mag, mag);
                }
            }
            return result;
        }
    }
}
