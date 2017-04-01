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
        int[,] kirschMask;//kernel
        int[,] originalMatrix;//original green chanel image matrix
        public int[,] ldpResult;//ldp coded image matrix

        public int[,] drldpMatrix;//reduced dimension of ldpResult
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
        public LocalDirectionalPattern(Bitmap inputImage)//basic constructor for this class
        {
            initMask();
            generateInitialMatrix(inputImage);
        }
        private void generateInitialMatrix(Bitmap inputImage)//get the initial matrix of image from green chanel of the image
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

        private int correlateMask(int[] mask, int[] imageMat)//used to get the correlation matrix of original image and kirsch mask
        {
            int acumulation = 0;
            for(int i=0; i<9; i++)
            {
                acumulation += mask[i] * imageMat[i];
            }
            //System.Diagnostics.Debug.WriteLine("Hasil korelasi: "+acumulation.ToString());
            return (acumulation>0)?acumulation:-acumulation; //result converted to keep the result always positive
        }
        //ultimate wrong here
        private int getLdpCode(int[] matrixBlock)//get ldpcode for each pixel block of image
        {
            string ldpBinaryCode = "";
            int[] ldpMatrixSequence = new int[9];//1x9 matrix containing the result of correlation matrix
            for(int i=0; i<8; i++)
            {
                int[] subMask = new int[9];//used to fetch mask from 2 dimension array to 1 dimension array
                for(int j=0; j<9; j++)
                {
                    subMask[j] = this.kirschMask[i, j];
                }
                ldpMatrixSequence[i] = correlateMask(subMask, matrixBlock);
            }
            int[] topThree = getMax(ldpMatrixSequence,3);//get the k (in this case is 3) most significant bit
            //for debugging
            //for (int i = 0; i < ldpMatrixSequence.GetLength(0); i++)
            //{
            //    System.Diagnostics.Debug.Write(ldpMatrixSequence[i]);
            //    System.Diagnostics.Debug.WriteLine("");
            //}
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (ldpMatrixSequence[i] == topThree[j])//set the most significant bit value to 1
                        ldpMatrixSequence[i] = 1;
                    else if (j == 2)
                        ldpMatrixSequence[i] = 0;//and other else as 0
                }
            }
            for(int i=0; i<9; i++)
            {
                ldpBinaryCode += ldpMatrixSequence[i].ToString();//concatenating binary to string
            }
            //System.Diagnostics.Debug.WriteLine(Convert.ToInt32(ldpBinaryCode, 2).ToString());
            return Convert.ToInt32(ldpBinaryCode, 2);//convert from binary string to decimal integers has been tested
        }
        private int[] getMax(int[] data, int ammount)//to get most significatn bit
        {
            Array.Sort<int>(data, new Comparison<int>((a, b) => (b.CompareTo(a))));//sort descending, lambda expression has been tested
            int[] result = new int[3];
            for(int i=0; i<ammount; i++)
            {
                result[i] = data[i];
            }
            return result;
        }

        //wrong is here
        private void getLdpCodedImage()//do the ldp generating code to the entire image
        {
            this.ldpResult = new int[this.originalMatrix.GetLength(0),this.originalMatrix.GetLength(1)];
            for(int i=1; i<this.originalMatrix.GetLength(0)-1; i++)
            {
                for(int j=1; j<this.originalMatrix.GetLength(1)-1; j++)
                {
                    int[] matrixChunks = new int[9];//block of image needed to be correlated with the kirsch mask
                    for(int x=-1; x<2; x++)
                    {
                        for(int y=-1; y<2; y++)
                        {
                            //something wrong here!!!!!!!!!!!
                            matrixChunks[y + 1 + x + 1] = this.originalMatrix[i + x, j + y];//fetching the pixel value
                            System.Diagnostics.Debug.Write(matrixChunks[y + 1 + x + 1].ToString()+" ");
                        }
                    }
                    //System.Diagnostics.Debug.Write(string.Join("", matrixChunks));
                    System.Diagnostics.Debug.Write(" to be ");
                    this.ldpResult[i,j]=getLdpCode(matrixChunks);//or here
                    System.Diagnostics.Debug.WriteLine(this.ldpResult[i, j].ToString());
                }
            }
        }
        private void dimensionReduction()//reduce dimension by xor operation
        {
            int size = this.ldpResult.GetLength(0);
            this.drldpMatrix = new int[size/3, size/3];
            for(int i=0;    i<size; i += 3)
            {
                for(int j=0;   j<size; j += 3)
                {
                    //System.Diagnostics.Debug.WriteLine(this.ldpResult[i,j].ToString());
                    int drldpcode=0;
                    for(int x=0; x<3; x++)
                    {
                        for(int y=0; y<3; y++)
                        {
                            if (x == 1 && y == 1)
                                continue;
                            drldpcode ^= this.ldpResult[i+x, j+y];
                        }
                    }
                    drldpcode = binaryChecker(drldpcode);
                    this.drldpMatrix[i / 3, j / 3] = drldpcode;
                }
            }
        }
        public int[,] getDRLDPMatrix()
        {
            getLdpCodedImage();
            dimensionReduction();
            return this.drldpMatrix;
        }
        //this part of code is finished and tested.
        private int binaryChecker(int input)//to check that maximum '1' occurs three times in a binary string
        {
            int oneCounter = 0;
            char[] result = Convert.ToString(input, 2).ToCharArray();
            //System.Diagnostics.Debug.WriteLine(new string(result) + " before. length " + result.Length.ToString());
            for (int i = 0; i < result.Length; i++)
            {
                //System.Diagnostics.Debug.WriteLine("get in loop");
                //System.Diagnostics.Debug.WriteLine(result[i]);
                if (result[i] == '1')
                {
                    oneCounter++;
                    //System.Diagnostics.Debug.WriteLine("detect 1");
                    if (oneCounter == 3)
                    {
                        //System.Diagnostics.Debug.WriteLine("set rest as zero");
                        for (int j = i + 1; j < result.Length; j++)
                        {
                            result[j] = '0';
                        }
                        break;
                    }
                }
            }
            //System.Diagnostics.Debug.WriteLine("result in char " + new string(result) + " typeof " + result.ToString());
            return Convert.ToInt32(new string(result), 2);//return decimal integer
        }
    }
}
