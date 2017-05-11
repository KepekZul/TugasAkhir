using System.Collections.Generic;
using System;

namespace Tugas_Akhir
{
    class ChiSquareDissimilarity
    {
        protected double Dissimilarity;
        protected int NumberofFragment { get; }
        protected byte[,] TestFeature;
        protected byte[,] TrainFeature;
        protected byte[,,] TestFragment;
        protected byte[,,] TrainFragment;
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
            this.TestFragment = new byte[this.NumberofFragment * this.NumberofFragment, FragmentSize, FragmentSize];
            this.TrainFragment = new byte[this.NumberofFragment * this.NumberofFragment, FragmentSize, FragmentSize];
            //System.Diagnostics.Debug.WriteLine(this.NumberofFragment.ToString()+" "+FragmentSize.ToString());
            int i = 0;
            for (int xCriterion = 0, FragmentIndex = 0; xCriterion < dimension; xCriterion += FragmentSize)//Shift horizontal block
            {
                for (int yCriterion = 0; yCriterion < dimension; yCriterion += FragmentSize, FragmentIndex++)//shift vertical block
                {
                    for (int xIndex = 0; xIndex < FragmentSize; xIndex++)
                    {
                        for (int yIndex = 0; yIndex < FragmentSize; yIndex++)
                        {
                            //System.Diagnostics.Debug.WriteLine(FragmentIndex.ToString()+" "+xCriterion.ToString()+" "+yCriterion.ToString()+" "+xIndex.ToString()+" "+yIndex.ToString());
                            this.TestFragment[FragmentIndex, xIndex, yIndex] = this.TestFeature[xIndex + xCriterion, yIndex + yCriterion];
                            //System.Diagnostics.Debug.WriteLine("test "+this.TestFragment[FragmentIndex, xIndex, yIndex]);
                            //i++;
                        }
                    }
                }
            }
            //System.Diagnostics.Debug.WriteLine(i);
            i = 0;
            for (int xCriterion = 0, FragmentIndex = 0; xCriterion < dimension; xCriterion += FragmentSize)//Shift horizontal block
            {
                for (int yCriterion = 0; yCriterion < dimension; yCriterion += FragmentSize, FragmentIndex++)//shift vertical block
                {
                    for (int xIndex = 0; xIndex < FragmentSize; xIndex++)
                    {
                        for (int yIndex = 0; yIndex < FragmentSize; yIndex++)
                        {
                            //System.Diagnostics.Debug.WriteLine(FragmentIndex.ToString() + " " + xCriterion.ToString() + " " + yCriterion.ToString() + " " + xIndex.ToString() + " " + yIndex.ToString());
                            this.TrainFragment[FragmentIndex, xIndex, yIndex] = this.TrainFeature[xIndex + xCriterion, yIndex + yCriterion];
                            //System.Diagnostics.Debug.WriteLine("train "+this.TrainFragment[FragmentIndex, xIndex, yIndex]);
                            //i++;
                        }
                    }
                }
            }
            //System.Diagnostics.Debug.WriteLine(i);
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
                float Weight = GetWeight(getModeofRegion(SliceMatrix(TrainFragment, i), SliceMatrix(TestFragment, i)));
                //System.Diagnostics.Debug.WriteLine(getModeofRegion(SliceMatrix(TrainFragment, i), SliceMatrix(TestFragment, i)));
                for (int x = 0; x < this.TestFragment.GetLength(1); x++)
                {
                    for (int y = 0; y < this.TestFragment.GetLength(2); y++)
                    {
                        //System.Diagnostics.Debug.WriteLine(this.Dissimilarity+" "+Weight.ToString()+" "+ (Math.Pow((TestFragment[i, x, y] - TrainFragment[i, x, y]), 2) / (TestFragment[i, x, y] + TrainFragment[i, x, y])).ToString());
                        this.Dissimilarity += (Weight * (float)(Math.Pow((TestFragment[i, x, y] - TrainFragment[i, x, y]), 2) / (TestFragment[i, x, y] + TrainFragment[i, x, y])));
                    }
                }
            }
            return this.Dissimilarity;
        }
        protected int GetWeight(int modes)
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
        protected int getModeofRegion(byte[,] feature1, byte[,] feature2)
        {
            byte modes = 0;
            int maxScore = 0;
            int counter = 0;
            List<byte> SortedData = new List<byte>();
            for (int i = 0; i < feature1.GetLength(0); i++)
            {
                for (int j = 0; j < feature1.GetLength(1); j++)
                {
                    SortedData.Add(feature1[i, j]);
                    //System.Diagnostics.Debug.WriteLine(feature1[i, j]);
                }
            }
            for (int i = 0; i < feature2.GetLength(0); i++)
            {
                for (int j = 0; j < feature2.GetLength(1); j++)
                {
                    SortedData.Add(feature2[i, j]);
                    //System.Diagnostics.Debug.WriteLine(feature2[i, j]);
                }
            }
            //System.Diagnostics.Debug.WriteLine("unsorted");
            //foreach (byte x in SortedData)
            //{
            //    System.Diagnostics.Debug.WriteLine(x);
            //}
            //SortedData.Sort((s1, s2) => s1.CompareTo(s2));
            //System.Diagnostics.Debug.WriteLine("sorted");
            //foreach(byte x in SortedData)
            //{
            //    System.Diagnostics.Debug.WriteLine(x);
            //}
            for (int i = SortedData.Count - 1; i > 0; i--)
            {
                if (SortedData[i] == SortedData[i - 1])
                {
                    counter++;
                }
                else
                {
                    if (maxScore < counter)
                    {
                        maxScore = counter;
                        modes = SortedData[i];
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
            for (int x = 0; x < length; x++)
            {
                for (int y = 0; y < length; y++)
                {
                    //System.Diagnostics.Debug.WriteLine(index.ToString()+" "+x.ToString()+" "+y.ToString());
                    Result[x, y] = target[index, x, y];
                    //System.Diagnostics.Debug.WriteLine("result "+Result[x, y]+" harusnya "+target[index,x,y]);
                }
            }
            return Result;
        }
        private double RoundToSignificantDigits(double d, int digits)
        {
            if (d == 0)
                return 0;

            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1);
            return scale * Math.Round(d / scale, digits);
        }
    }
}
