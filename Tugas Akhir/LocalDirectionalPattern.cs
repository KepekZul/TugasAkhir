using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Program
{
    class LocalDirectionalPattern
    {
        int[,] kirschMask;
        Bitmap originalImage;
        int[,] originalMatrix;
        List<List<int>> ldpResult;
        private void initMask()
        {
            this.kirschMask = new int[9, 9] { { 5, 5,-3, 5, 0,-3,-3,-3,-3},//m3
                                              { 5, 5, 5,-3, 0,-3,-3,-3,-3},//m2
                                              {-3, 5, 5,-3, 0, 5,-3,-3,-3},//m1
                                              { 5,-3,-3, 5, 0,-3, 5,-3,-3},//m4
                                              { 0, 0, 0, 0, 0, 0, 0, 0, 0},//mpas
                                              {-3,-3, 5,-3, 0, 5,-3,-3, 5},//m0
                                              {-3,-3,-3, 5, 0,-3, 5, 5,-3},//m5
                                              {-3,-3,-3,-3, 0,-3, 5, 5, 5},//m6
                                              {-3,-3,-3,-3, 0, 5,-3, 5, 5},//m7
                                          };
            this.ldpResult = new List<List<int>>();
        }
        public LocalDirectionalPattern(Bitmap inputImage)
        {
            initMask();
            this.originalImage = new Bitmap(inputImage);
            generateInitialMatrix();
        }
        private void generateInitialMatrix()
        {
            this.originalMatrix = new int[this.originalImage.Width, this.originalImage.Height];
            for(int i=0; i<this.originalImage.Width; i++)
            {
                for(int j=0; j<this.originalImage.Height; j++)
                {
                    this.originalMatrix[i, j] = Convert.ToInt32(this.originalImage.GetPixel(i, j).G.ToString());
                }
            }
        }

        private int convoluteMask(int[] mask, int[] imageMat)
        {
            int acumulation = 0;
            for(int i=0; i<9; i++)
            {
                for(int j=0; j<9; j++)
                {
                    acumulation += mask[i] * imageMat[j];
                }
            }
            return (acumulation>0)?acumulation:-acumulation; //result converted to keep the result always positive
        }
        public int ldpCode(int[] matrixBlock)
        {
            string ldpBinaryCode = "";
            int[] ldpMatrixSequence = new int[9];
            for(int i=0; i<9; i++)
            {
                int[] subMask = new int[9];
                for(int j=0; j<9; j++)
                {
                    subMask[j] = this.kirschMask[i, j];
                }
                ldpMatrixSequence[i] = convoluteMask(subMask, matrixBlock);
            }
            int[] topThree = getMax(ldpMatrixSequence,3);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (ldpMatrixSequence[i] == topThree[j])
                        ldpMatrixSequence[i] = 1;
                    else if (j == 2)
                        ldpMatrixSequence[i] = 0;
                }
            }
            ldpBinaryCode = ldpMatrixSequence[6].ToString() + ldpBinaryCode;
            ldpBinaryCode = ldpMatrixSequence[3].ToString() + ldpBinaryCode;
            ldpBinaryCode = ldpMatrixSequence[2].ToString() + ldpBinaryCode;
            ldpBinaryCode = ldpMatrixSequence[1].ToString() + ldpBinaryCode;
            ldpBinaryCode = ldpMatrixSequence[4].ToString() + ldpBinaryCode;
            ldpBinaryCode = ldpMatrixSequence[7].ToString() + ldpBinaryCode;
            ldpBinaryCode = ldpMatrixSequence[8].ToString() + ldpBinaryCode;
            ldpBinaryCode = ldpMatrixSequence[9].ToString() + ldpBinaryCode;
            return Convert.ToInt32(ldpBinaryCode, 2);
        }
        private int[] getMax(int[] data, int ammount)//to get most significatn bit
        {
            data.OrderBy(h => h);
            int[] result = new int[3];
            for(int i=0; i<ammount; i++)
            {
                result[i] = data[i];
            }
            return result;
        }
    }
}
