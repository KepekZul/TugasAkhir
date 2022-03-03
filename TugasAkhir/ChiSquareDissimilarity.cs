using System.Collections.Generic;
using System;

namespace Tugas_Akhir
{
    class ChiSquareDissimilarity
    {
        protected double Dissimilarity;
        protected int NumberofFragment;
        protected byte[,] TestFeature;
        protected byte[,] TrainFeature;
        protected byte[,] TestFragment;
        protected byte[,] TrainFragment;
        /// <summary>
        /// TestFeature and TrainFeature only can be set from this constructor
        /// </summary>
        /// <param name="TestFeature">2 dimensional array for target data</param>
        /// <param name="TrainFeature">2 dimensional array for target data</param>
        /// <param name="NumberofFragment">Number region to be fragmented</param>
        public ChiSquareDissimilarity(byte[,] TestFeature, byte[,] TrainFeature, int NumberofFragment)
        {
            if (TestFeature.Length != TrainFeature.Length || TestFeature.Length % NumberofFragment != 0)
            {
                throw new System.ArgumentException("Input of array should have same length of element", "featureDRLDP1 + featureDRLDP2 or fragment factor not multiplication of 3");
            }
            this.TestFeature = TestFeature;
            this.TrainFeature = TrainFeature;
            this.NumberofFragment = NumberofFragment;
            FragmentingMatrix();
        }
        private void FragmentingMatrix()
        {
            int dimension = this.TestFeature.GetLength(0);
            int FragmentSize = dimension / this.NumberofFragment;
            this.TestFragment = new byte[this.NumberofFragment * this.NumberofFragment, FragmentSize * FragmentSize];
            this.TrainFragment = new byte[this.NumberofFragment * this.NumberofFragment, FragmentSize * FragmentSize];
            int i;
            for (int xCriterion = 0, FragmentIndex = 0; xCriterion < dimension; xCriterion += FragmentSize)//Shift horizontal block
            {
                for (int yCriterion = 0; yCriterion < dimension; yCriterion += FragmentSize, FragmentIndex++)//shift vertical block
                {
                    i = 0;
                    for (int xIndex = 0; xIndex < FragmentSize; xIndex++)
                    {
                        for (int yIndex = 0; yIndex < FragmentSize; yIndex++)
                        {
                            try
                            {
                                this.TestFragment[FragmentIndex, i++] = this.TestFeature[xIndex + xCriterion, yIndex + yCriterion];
                            }
                            catch
                            {
                                System.Diagnostics.Debug.WriteLine("gagal " + xIndex + " " + yIndex + " " + xCriterion + " " + yCriterion);
                            }
                        }
                    }
                }
            }

            for (int xCriterion = 0, FragmentIndex = 0; xCriterion < dimension; xCriterion += FragmentSize)//Shift horizontal block
            {
                for (int yCriterion = 0; yCriterion < dimension; yCriterion += FragmentSize, FragmentIndex++)//shift vertical block
                {
                    i = 0;
                    for (int xIndex = 0; xIndex < FragmentSize; xIndex++)
                    {
                        for (int yIndex = 0; yIndex < FragmentSize; yIndex++)
                        {
                            try
                            {
                                this.TrainFragment[FragmentIndex, i++] = this.TrainFeature[xIndex + xCriterion, yIndex + yCriterion];
                            }
                            catch
                            {
                                System.Diagnostics.Debug.WriteLine("gagal " + xIndex + " " + yIndex + " " + xCriterion + " " + yCriterion);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Get the Dissimilarity value for both matrix
        /// </summary>
        /// <returns>return double</returns>
        public double CalculateDissimilarityValue()
        {
            this.Dissimilarity = 0;
            for (int i = 0; i < this.TestFragment.GetLength(0); i++)//shifting per-block
            {
                int[] histogramTest = new int[256];
                int[] histogramTrain = new int[256];
                getHistogram(ref histogramTest, ref histogramTrain, i);
                double weight = GetWeight(getModeofRegion(this.TestFragment, this.TrainFragment, i));
                for (int j = 0; j < 256; j++)
                {
                    this.Dissimilarity += weight * (double)((double)Math.Pow(histogramTest[j] - (double)histogramTrain[j], 2) / (double)(((double)histogramTest[j] + (double)histogramTrain[j] != 0) ? (double)histogramTest[j] + (double)histogramTrain[j] : 1));
                }
            }
            return this.Dissimilarity;
        }

        private void getHistogram(ref int[] histogramTest, ref int[] histogramTrain, int fragmentIndex)
        {
            //initiate histogram
            for (int i = 0; i < 256; i++)
            {
                histogramTest[i] = 0;
                histogramTrain[i] = 0;
            }
            for (int i = 0; i < this.TestFragment.GetLength(1); i++)
            {
                histogramTest[this.TestFragment[fragmentIndex, i]]++;
                histogramTrain[this.TrainFragment[fragmentIndex, i]]++;
            }
        }

        protected double GetWeight(int modes)
        {
            if (modes >= 0 && modes < 64)
            {
                return 1;
            }
            else if (modes >= 64 && modes < 128)
            {
                return 2;
            }
            else if (modes >= 128 && modes < 192)
            {
                return 3;
            }
            else if (modes >= 192 && modes < 256)
            {
                return 4;
            }
            else
            {
                return 0;//input must be false since color coded dr-ldp should fall within 0 - 255
            }
        }
        protected int getModeofRegion(byte[,] feature1, byte[,] feature2, int index)
        {
            List<byte> feat = new List<byte>();
            for (int i = 0; i<feature1.GetLength(1); i++)
            {
                feat.Add(feature1[index, i]);
                feat.Add(feature2[index, i]);
            }
            feat.Sort((s1, s2) => s1.CompareTo(s2));
            byte modes = 0;
            int maxScore = 0;
            int counter = 0;
            for (int i=feat.Count-1; i>0; i--)
            {
                if (feat[i] == feat[i - 1])
                {
                    counter++;
                }
                else
                {
                    if (maxScore < counter)
                    {
                        maxScore = counter;
                        modes = feat[i];
                    }
                    else
                    {
                        counter = 0;
                    }
                }
            }
            return modes;
        }
        protected byte[,] SliceMatrix(byte[,,] target, int index)//mngambil potongan matrix
        {
            int length = target.GetLength(1);
            byte[,] Result = new byte[length, length];
            for (int x=0; x<length; x++)
            {
                for (int y=0; y<length; y++)
                {
                    Result[x, y] = target[index, x, y];
                }
            }
            return Result;
        }
    }
}