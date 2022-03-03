using System.Drawing;

namespace Tugas_Akhir
{
    class KirschEdgeDetection
    {
        private int[,] _initialMatrix;
        private int[,] _kirschMask = new int[8, 9] {
            {-3,-3, 5,-3, 0, 5,-3,-3, 5},//m0
            {-3, 5, 5,-3, 0, 5,-3,-3,-3},//m1
            { 5, 5, 5,-3, 0,-3,-3,-3,-3},//m2
            { 5, 5,-3, 5, 0,-3,-3,-3,-3},//m3
            { 5,-3,-3, 5, 0,-3, 5,-3,-3},//m4
            {-3,-3,-3, 5, 0,-3, 5, 5,-3},//m5
            {-3,-3,-3,-3, 0,-3, 5, 5, 5},//m6
            {-3,-3,-3,-3, 0, 5,-3, 5, 5},//m7
        };
        public int[,] FinalMatrix;

        public KirschEdgeDetection(Bitmap inputImage)
        {
            int width = inputImage.Width;
            int height = inputImage.Height;
            _initialMatrix = new int[width, height];
            FinalMatrix = new int[width, height];
            for (int i=0; i< width; i++)
            {
                for(int j=0; j<height; j++)
                {
                    _initialMatrix[i, j] = inputImage.GetPixel(i, j).G;
                }
            }
        }
        public void GetEdge(int maskIndex)
        {
            var subMask = new int[9];//used to fetch mask from 2 dimension array to 1 dimension array
            for (var j = 0; j < 9; j++)
            {
                subMask[j] = _kirschMask[maskIndex, j];
            }
            for(var i=1; i<_initialMatrix.GetLength(0)-1; i++)
            {
                for(var j=1; j<_initialMatrix.GetLength(1)-1; j++)
                {
                    FinalMatrix[i, j] = CorrelateMask(subMask, i, j);
                }
            }
        }
        private int CorrelateMask(int[] mask, int x, int y)//used to get the correlation matrix of original image and kirsch mask
        {
            var accumulation = 0;
            var imageMat = new int[9];
            var counter = 0;
            for(var i=-1; i<2; i++)
            {
                for(var j=-1; j<2; j++)
                {
                    imageMat[counter++] = _initialMatrix[x+i, y+j];
                }
            }
            for (var i = 0; i < 9; i++)
            {
                accumulation += mask[i] * imageMat[i];
            }
            return (accumulation > 0) ? accumulation/9 : -accumulation/9; //result converted to keep the result always positive
        }
    }
}
