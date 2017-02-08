using System;
using System.IO;
using System.Drawing;

namespace Tugas_Akhir
{
    class PortableGrayMap
    {
        public int width;
        public int heigth;
        public int maxVal;
        public byte[][] pixels;

        public PortableGrayMap(int width, int height, int maxVal, byte[][] pixels)
        {
            this.width = width;
            this.heigth = height;
            this.maxVal = maxVal;
            this.pixels = pixels;
        }
        public PortableGrayMap(string filename)
        {
            PortableGrayMap x = LoadImage(filename);
            this.width = x.width;
            this.heigth = x.heigth;
            this.maxVal = x.maxVal;
            this.pixels = x.pixels;
        }
        public PortableGrayMap LoadImage(string file)
        {
            FileStream inputFileStream = new FileStream(file, FileMode.Open);
            BinaryReader binaryReader = new BinaryReader(inputFileStream);

            string kodeJenisFile = NextNonCommentLine(binaryReader);
            if (kodeJenisFile != "P5")
            {
                throw new Exception("Non PGM file type" + kodeJenisFile);
            }
            string widthHeight = NextNonCommentLine(binaryReader);
            string[] tokens = widthHeight.Split(' ');
            int width = int.Parse(tokens[0]);
            int height = int.Parse(tokens[1]);

            string sMaxVal = NextNonCommentLine(binaryReader);
            int maxVal = int.Parse(sMaxVal);

            byte[][] pixels = new byte[height][];
            for(int i=0; i<height; ++i)
            {
                pixels[i] = new byte[width];
            }
            for(int i=0; i<height; ++i)
            {
                for(int j=0; j<width; ++j)
                {
                    pixels[i][j] = binaryReader.ReadByte();
                }
            }
            binaryReader.Close();
            inputFileStream.Close();

            PortableGrayMap hasil = new PortableGrayMap(width, height, maxVal, pixels);
            return hasil;
        }

        static string NextAnyLine(BinaryReader br)
        {
            string s = "";
            byte b = 0;//dummy
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
            string s = NextAnyLine(br);
            while (s.StartsWith("#") || s == "")
            {
                s = NextAnyLine(br);
            }
            return s;
        }

        public Bitmap MakeBitmap(PortableGrayMap gambarPgm, int mag)
        {
            int width = gambarPgm.width*mag;
            int height = gambarPgm.heigth*mag;
            Bitmap result = new Bitmap(width, height);
            Graphics gr = Graphics.FromImage(result);
            for(int i=0; i<gambarPgm.heigth; ++i)
            {
                for (int j=0; j<gambarPgm.width; ++j)
                {
                    int pixelColor = gambarPgm.pixels[i][j];
                    Color warna = Color.FromArgb(pixelColor, pixelColor, pixelColor);
                    SolidBrush sb = new SolidBrush(warna);
                    gr.FillRectangle(sb, j * mag, i * mag, mag, mag);
                }
            }
            return result;
        }
    }
}
