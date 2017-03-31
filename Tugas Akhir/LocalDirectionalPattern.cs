using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tugas_Akhir
{
    class LocalDirectionalPattern
    {
        int[,] kirschMask;
        int[,] originalMatrix;
        int[,] ldpResult;

        int[,] drldpMatrix;
        private void initMask()
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
        public LocalDirectionalPattern(Bitmap inputImage)
        {
            initMask();
            generateInitialMatrix(inputImage);
        }
        private void generateInitialMatrix(Bitmap inputImage)
        {
            this.originalMatrix = new int[inputImage.Width, inputImage.Height];
            for(int i=0; i<inputImage.Width; i++)
            {
                for(int j=0; j<inputImage.Height; j++)
                {
                    this.originalMatrix[i, j] = Convert.ToInt32(inputImage.GetPixel(i, j).G.ToString());
                }
            }
        }

        private int correlateMask(int[] mask, int[] imageMat)
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
        private int ldpCode(int[] matrixBlock)
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
                ldpMatrixSequence[i] = correlateMask(subMask, matrixBlock);
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
            for(int i=0; i<9; i++)
            {
                ldpBinaryCode += ldpMatrixSequence[i].ToString();
            }
            return Convert.ToInt32(ldpBinaryCode, 2);
        }
        private int[] getMax(int[] data, int ammount)//to get most significatn bit
        {
            Array.Sort<int>(data, new Comparison<int>((a, b) => (b.CompareTo(a));
            int[] result = new int[3];
            for(int i=0; i<ammount; i++)
            {
                result[i] = data[i];
            }
            return result;
        }
        private void getLdpCodedImage()
        {
            this.ldpResult = new int[this.originalMatrix.GetLength(0),this.originalMatrix.GetLength(1)];
            for(int i=1; i<this.originalMatrix.GetLength(0)-1; i++)
            {
                for(int j=1; j<this.originalMatrix.GetLength(1)-1; j++)
                {
                    int[] matrixChunks = new int[9];
                    for(int x=-1; x<2; x++)
                    {
                        for(int y=-1; y<2; y++)
                        {
                            matrixChunks[y + 1 + x + 1] = this.originalMatrix[i + x, j + y];
                        }
                    }
                    this.ldpResult[i,j]=ldpCode(matrixChunks);
                }
            }
        }
        private void dimensionReduction()//reduce dimension by xor operation
        {
            int size = this.ldpResult.GetLength(0);
            for(int i=0;    i<size; i += 3)
            {
                for(int j=0;   j<size; j += 3)
                {
                    var drldpcode=0;
                    drldpcode = drldpcode ^ this.ldpResult[i, j];
                }
            }
        }
        private string binaryChecker(string input)//to check that maximum '1' occurs three times in a binary string
        {
            int oneCounter = 0;
            char[] result = input.ToCharArray();
            for (int i = 0; i < result.Length; i++)
            {
                //System.Diagnostics.Debug.WriteLine("get in loop");
                //System.Diagnostics.Debug.WriteLine(result[i]);
                if (result[i] == '1')
                {
                    oneCounter++;
                    //System.Diagnostics.Debug.WriteLine("detect 1");
                }
                if (oneCounter >= 3)
                {
                    //System.Diagnostics.Debug.WriteLine("set rest as zero");
                    for (int j = i + 1; j < result.Length; j++)
                    {
                        result[j] = '0';
                    }
                    break;
                }
            }
            //System.Diagnostics.Debug.WriteLine(new string(result));
            return new string(result);
        }
    }
}
