using System.Collections.Generic;
using System.Linq;

namespace Tugas_Akhir
{
    class ChiSquareDissimilarity
    {
        protected double dissimilarity;
        protected List<int> arrayValue1;
        protected List<int> arrayValue2;
        public ChiSquareDissimilarity(int[] featureDRLDP1, int[] featureDRLDP2)
        {
            if (featureDRLDP1.Count() != featureDRLDP2.Count())
            {
                throw new System.ArgumentException("Input of array should have same length of element", "featureDRLDP1 + featureDRLDP2");
            }
            this.dissimilarity = 0;
            this.arrayValue1 = featureDRLDP1.OfType<int>().ToList();
            this.arrayValue2 = featureDRLDP2.OfType<int>().ToList();
        }
        public double CalculateDissimilarityValue()
        {
            for(int i=0; i<this.arrayValue1.Count; i++)
            {
                //pending baca paper lagi!
            }
            return dissimilarity;
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
        protected int CalculateMode(int[] data1, int[] data2)
        {
            int modes=0;
            int maxScore=0;
            int counter=0;
            List<int> SortedData = data1.OfType<int>().ToList();
            SortedData.AddRange(data2.OfType<int>().ToList());
            SortedData.Sort();
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
    }
}
