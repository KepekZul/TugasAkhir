using System.Collections.Generic;
using System;

namespace Tugas_Akhir
{
    class ChiSquareDissimilarity
    {
        protected double Dissimilarity;
        protected int FragmentFactor { get; }
        protected byte[,] TestFeature;
        protected byte[,] TrainFeature;
        protected byte[,,] TestFragment;
        protected byte[,,] TrainFragment;
        /// <summary>
        /// TestFeature and TrainFeature only can be set from this constructor
        /// </summary>
        /// <param name="TestFeature">2 dimensional array for target data</param>
        /// <param name="TrainFeature">2 dimensional array for target data</param>
        /// <param name="FragmentFactor">Number region to be fragmented</param>
        public ChiSquareDissimilarity(byte[,] TestFeature, byte[,] TrainFeature, int FragmentFactor)
        {
            if (TestFeature.Length != TrainFeature.Length || FragmentFactor%3!=0 || TestFeature.Length%FragmentFactor!=0)
            {
                throw new System.ArgumentException("Input of array should have same length of element", "featureDRLDP1 + featureDRLDP2 or fragment factor not multiplication of 3");
            }
            this.TestFeature = TestFeature;
            this.TrainFeature = TrainFeature;
            this.FragmentFactor = FragmentFactor;
            FragmentingMatrix();
        }
        private void FragmentingMatrix()
        {
            int dimension = this.TestFeature.Length;
            byte NumberOfFragment = Convert.ToByte((dimension / this.FragmentFactor) * (dimension / this.FragmentFactor));
            this.TestFragment = new byte[NumberOfFragment, this.FragmentFactor, this.FragmentFactor];
            this.TrainFragment = new byte[NumberOfFragment, this.FragmentFactor, this.FragmentFactor];
            for(int xCriterion = 0, FragmentIndex=0; xCriterion<dimension; xCriterion += this.FragmentFactor, FragmentIndex++)//Shift horizontal block
            {
                for(int yCriterion = 0; yCriterion<dimension; yCriterion+= this.FragmentFactor, FragmentIndex++)//shift vertical block
                {
                    for(int xIndex=0; xIndex<this.FragmentFactor; xIndex++)
                    {
                        for(int yIndex=0; yIndex<this.FragmentFactor; yIndex++)
                        {
                            this.TestFragment[FragmentIndex, xIndex, yIndex] = this.TestFeature[xIndex + xCriterion, yIndex + yCriterion];
                        }
                    }
                }
            }
            for (int xCriterion = 0, FragmentIndex = 0; xCriterion < dimension; xCriterion += this.FragmentFactor, FragmentIndex++)//Shift horizontal block
            {
                for (int yCriterion = 0; yCriterion < dimension; yCriterion += this.FragmentFactor, FragmentIndex++)//shift vertical block
                {
                    for (int xIndex = 0; xIndex < this.FragmentFactor; xIndex++)
                    {
                        for (int yIndex = 0; yIndex < this.FragmentFactor; yIndex++)
                        {
                            this.TrainFragment[FragmentIndex, xIndex, yIndex] = this.TrainFeature[xIndex + xCriterion, yIndex + yCriterion];
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
            for (int i=0; i<this.TestFragment.GetLength(0); i++)//shifting per-block
            {
                int Weight = GetWeight(getModeofRegion(SliceMatrix(TrainFragment, i), SliceMatrix(TestFragment,i)));
                for (int x=0; x<this.TestFragment.GetLength(1); x++)
                {
                    for(int y=0; y<this.TestFragment.GetLength(2); y++)
                    {
                        this.Dissimilarity += Weight * (Math.Pow(TestFragment[i, x, y] - TrainFragment[i, x, y], 2) / TestFragment[i, x, y] + TrainFragment[i, x, y]);
                    }
                }
            }
            return this.Dissimilarity;
        }
        protected int GetWeight(int modes)
        {
            if(modes>0 && modes < 64)
            {
                return 1;
            }else if(modes>=64 && modes < 128)
            {
                return 2;
            }else if(modes>=128 && modes < 192)
            {
                return 3;
            }else if(modes>=192 && modes < 256)
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
            byte modes=0;
            int maxScore=0;
            int counter=0;
            List<byte> SortedData = new List<byte>();
            for(int i=0; i<feature1.Length; i++)
            {
                for(int j=0; j<feature1.Length; j++)
                {
                    SortedData.Add(feature1[i, j]);
                }
            }
            for (int i = 0; i < feature2.Length; i++)
            {
                for (int j = 0; j < feature2.Length; j++)
                {
                    SortedData.Add(feature2[i, j]);
                }
            }
            SortedData.Sort((s1, s2) => s1.CompareTo(s2));
            for(int i = SortedData.Count-1; i>0; i--)
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
        protected byte[,] SliceMatrix(byte[,,] target, int index)
        {
            int lenght = target.GetLength(index);
            byte[,] Result = new byte[lenght, lenght];
            for(int x=0; x<lenght; x++)
            {
                for(int y=0; y< lenght; y++)
                {
                    Result[x, y] = target[index, x, y];
                }
            }
            return Result;
        }
    }
}
