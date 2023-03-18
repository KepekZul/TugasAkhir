using System;
using System.Drawing;

namespace Tugas_Akhir
{
    class DRLocalDirectionalPattern
    {
        private int[,] _kirschMask;//kernel
        private int[,] _originalMatrix;//original green chanel image matrix
        
        public int[,] LdpResult;//ldp coded image matrix
        public byte[,] DrLdpMatrix;//reduced dimension of ldpResult

        private void InitMask()//generating the kirsch mask
        {
            _kirschMask = new int[8, 9] { {-3,-3, 5,-3, 0, 5,-3,-3, 5},//m0
                                              {-3, 5, 5,-3, 0, 5,-3,-3,-3},//m1
                                              { 5, 5, 5,-3, 0,-3,-3,-3,-3},//m2
                                              { 5, 5,-3, 5, 0,-3,-3,-3,-3},//m3
                                              { 5,-3,-3, 5, 0,-3, 5,-3,-3},//m4
                                              {-3,-3,-3, 5, 0,-3, 5, 5,-3},//m5
                                              {-3,-3,-3,-3, 0,-3, 5, 5, 5},//m6
                                              {-3,-3,-3,-3, 0, 5,-3, 5, 5},//m7
                                          };
        }
        public DRLocalDirectionalPattern(Bitmap inputImage)//basic constructor for this class
        {
            InitMask();
            GenerateInitialMatrix(inputImage);
        }
        private void GenerateInitialMatrix(Bitmap inputImage)//get the initial matrix of image from green chanel of the image
        {
            _originalMatrix = new int[inputImage.Width, inputImage.Height];
            for(int i=0; i<inputImage.Width; i++)
            {
                for(int j=0; j<inputImage.Height; j++)
                {
                    _originalMatrix[i, j] = Convert.ToInt32(inputImage.GetPixel(i, j).R.ToString());
                }
            }
        }

        private int CorrelateMask(int[] mask, int[] imageMat)//used to get the correlation matrix of original image and kirsch mask
        {
            var accumulation = 0;
            for(int i=0; i<9; i++)
            {
                accumulation += mask[i] * imageMat[i];
            }
            return (accumulation>0)?accumulation:-accumulation; //result converted to keep the result always positive
        }

        private int GetLdpCode(int[] matrixBlock)//get ldpcode for each pixel block of image
        {
            var ldpBinaryCode = "";
            var ldpMatrixSequence = new int[8];//1x9 matrix containing the result of correlation matrix
            for (int i=0; i<8; i++)
            {
                var subMask = new int[9];//used to fetch mask from 2 dimension array to 1 dimension array
                for(int j=0; j<9; j++)
                {
                    subMask[j] = this._kirschMask[i, j];
                }
                ldpMatrixSequence[i] = CorrelateMask(subMask, matrixBlock);
            }
            var temporary = new int[9];
            ldpMatrixSequence.CopyTo(temporary,0);
            Array.Sort(temporary);
            var threshold = temporary[6];
            for(int x=0; x<ldpMatrixSequence.Length; x++)
            {
                if (ldpMatrixSequence[x] < threshold)
                {
                    ldpMatrixSequence[x] = 0;
                }
                else
                {
                    ldpMatrixSequence[x] = 1;
                }
            }
            for(int i=7; i>=0; i--)
            {
                ldpBinaryCode += ldpMatrixSequence[i].ToString();//concatenating binary to string
            }
            return Convert.ToInt32(ldpBinaryCode, 2);//convert from binary string to decimal integers has been tested
        }

        private void GetLdpCodedImage()//do the ldp generating code to the entire image
        {
            LdpResult = new int[_originalMatrix.GetLength(0)-2, _originalMatrix.GetLength(1)-2];
            //int p = 0;
            for(var x =1; x<_originalMatrix.GetLength(0)-1; x++)
            {
                for(int y=1; y<_originalMatrix.GetLength(1)-1; y++)
                {
                    var matrixChunks = new int[9];//block of image needed to be correlated with the kirsch mask
                    int matIndex = 0;
                    for(var i=-1; i<2; i++)
                    {
                        for(var j=-1; j<2; j++)
                        {
                            matrixChunks[matIndex] = _originalMatrix[x + i, y + j];
                            matIndex++;
                        }
                    }
                    LdpResult[x-1,y-1]=GetLdpCode(matrixChunks);//or here
                }
            }
        }
        private void DimensionReduction()//reduce dimension by xor operation
        {
            var size = LdpResult.GetLength(0);
            DrLdpMatrix = new byte[size/3, size/3];
            for(int i=0;    i<size; i += 3)//shifting block
            {
                for(int j=0;   j<size; j += 3)//shifting block
                {
                    #region maximum this is good
                    var drldp = 0;
                    for (var x = 0; x < 3; x++)
                    {
                        for (int y = 0; y < 3; y++)
                        {
                            drldp = Math.Max(drldp, LdpResult[i + x, j + y]);
                        }
                    }
                    DrLdpMatrix[i / 3, j / 3] = Convert.ToByte(drldp);
                    #endregion
                }
            }
        }
        public byte[,] GetDRLDPMatrix()
        {
            GetLdpCodedImage();
            DimensionReduction();
            return this.DrLdpMatrix;
        }
    }
}
