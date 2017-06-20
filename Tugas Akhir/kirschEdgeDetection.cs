using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Tugas_Akhir
{
    class kirschEdgeDetection
    {
        int[,] initialMatrix; 
        int[,] kirschMask;
        public int[,] finalMatrix;
        private void initMask()//genereating the kirsch mask
        {
            this.kirschMask = new int[8, 9] { {-3,-3, 5,-3, 0, 5,-3,-3, 5},//m0
                                              {-3, 5, 5,-3, 0, 5,-3,-3,-3},//m1
                                              { 5, 5, 5,-3, 0,-3,-3,-3,-3},//m2
                                              { 5, 5,-3, 5, 0,-3,-3,-3,-3},//m3
                                              { 5,-3,-3, 5, 0,-3, 5,-3,-3},//m4
                                              {-3,-3,-3, 5, 0,-3, 5, 5,-3},//m5
                                              {-3,-3,-3,-3, 0,-3, 5, 5, 5},//m6
                                              {-3,-3,-3,-3, 0, 5,-3, 5, 5},//m7
                                          };
        }
        public kirschEdgeDetection(Bitmap inputImage)
        {
            initMask();
            int width = inputImage.Width;
            int height = inputImage.Height;
            this.initialMatrix = new int[width, height];
            this.finalMatrix = new int[width, height];
            for (int i=0; i< width; i++)
            {
                for(int j=0; j<height; j++)
                {
                    this.initialMatrix[i, j] = inputImage.GetPixel(i, j).G;
                }
            }
        }
        public void getEdge(int maskIndex)
        {
            int[] subMask = new int[9];//used to fetch mask from 2 dimension array to 1 dimension array
            for (int j = 0; j < 9; j++)
            {
                subMask[j] = this.kirschMask[maskIndex, j];
            }
            for(int i=1; i<this.initialMatrix.GetLength(0)-1; i++)
            {
                for(int j=1; j<this.initialMatrix.GetLength(1)-1; j++)
                {
                    finalMatrix[i, j] = correlateMask(subMask, i, j);
                }
            }
        }
        private int correlateMask(int[] mask, int x, int y)//used to get the correlation matrix of original image and kirsch mask
        {
            int acumulation = 0;
            int[] imageMat = new int[9];
            int counter = 0;
            for(int i=-1; i<2; i++)
            {
                for(int j=-1; j<2; j++)
                {
                    imageMat[counter++] = this.initialMatrix[x+i, y+j];
                }
            }
            for (int i = 0; i < 9; i++)
            {
                //System.Diagnostics.Debug.Write(mask[i]+"*"+imageMat[i]+" ");
                acumulation += mask[i] * imageMat[i];
            }
            System.Diagnostics.Debug.WriteLine("\n"+acumulation+" "+(acumulation/9)+"\n");
            return (acumulation > 0) ? acumulation/9 : -acumulation/9; //result converted to keep the result always positive
        }
    }
}
